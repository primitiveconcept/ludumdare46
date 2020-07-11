namespace HackThePlanet
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using PrimitiveEngine;


    public class Game
    {
        private static Game _instance;

        private EntityWorld entityWorld = new EntityWorld();
        private GameTime gameTime = new GameTime();
        private PlayerList players = new PlayerList();
        private Network internet = new Network();
        private readonly ConcurrentDictionary<int, List<string>> incomingPlayerCommands = 
            new ConcurrentDictionary<int, List<string>>();


        #region Constructors
        private Game()
        {
            this.entityWorld.InitializeAll();
            JsonConvert.DefaultSettings = () =>
                new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        Converters = new List<JsonConverter>()
                                         {
                                             new StringEnumConverter(),
                                             new StringReferenceConverter()
                                         }
                    };
        }
        #endregion


        private event Action<int, string> messageForClient;


        #region Properties
        public static Network Internet
        {
            get { return Instance.internet; }
        }


        public static PlayerList Players
        {
            get { return Instance.players; }
        }


        public static GameTime Time
        {
            get { return Instance.gameTime; }
        }


        public static EntityWorld World
        {
            get { return Instance.entityWorld; }
        }


        private static Game Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Game();
                return _instance;
            }
        }
        #endregion


        public static void QueueCommand(int playerId, string command)
        {
            if (!Instance.incomingPlayerCommands.ContainsKey(playerId))
                Instance.incomingPlayerCommands.TryAdd(playerId, new List<string>());

            Instance.incomingPlayerCommands[playerId].Add(command);
        }


        public static void RemovePlayerCommandQueue(int playerId)
        {
            Instance.incomingPlayerCommands.TryRemove(playerId, out List<string> unusedValue);
        }


        public static void SubscribeToClientMessages(Action<int, string> eventHandler)
        {
            Instance.messageForClient += eventHandler;
        }


        public static void Update()
        {
            Instance.ExecuteIncomingCommands();
            Instance.entityWorld.FixedUpdate(Instance.gameTime.ElapsedTicks);
            Thread.Sleep(30); // Fuck it, let's not get fancy here.
        }


        private void ExecuteIncomingCommands()
        {
            foreach (KeyValuePair<int, List<string>> playerEntry in this.incomingPlayerCommands)
            {
                int playerId = playerEntry.Key;
                List<string> playerCommands = playerEntry.Value;
                
                foreach (string commandString in playerCommands)
                {
                    Command command = Command.ParseCommand(commandString);
                    string response = command.Execute(playerEntry.Key);

                    if (!string.IsNullOrEmpty(response))
                    {
                        SendMessageToClient(
                            playerId, 
                            new TerminalUpdateMessage(response).ToJson());
                    }
                }
                
                playerCommands.Clear();
            }
        }


        internal static void SendMessageToClient(int playerId, string playerStateJson)
        {
            Instance.messageForClient.Raise(playerId, playerStateJson);
        }
    }
}