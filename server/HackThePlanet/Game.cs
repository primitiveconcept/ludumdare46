namespace HackThePlanet
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public class Game
    {
        private static Game _instance;

        private EntityWorld entityWorld = new EntityWorld();
        private GameTime gameTime = new GameTime();
        private PlayerList players = new PlayerList();
        private GlobalNetwork globalNetwork = new GlobalNetwork();
        private readonly ConcurrentDictionary<string, List<string>> incomingPlayerCommands = 
            new ConcurrentDictionary<string, List<string>>();


        #region Constructors
        private Game()
        {
            this.entityWorld.InitializeAll();
        }
        #endregion


        private event Action<string, string> messageForClient;


        #region Properties
        public static GlobalNetwork GlobalNetwork
        {
            get { return Instance.globalNetwork; }
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


        public static void QueueCommand(string playerId, string command)
        {
            if (!Instance.incomingPlayerCommands.ContainsKey(playerId))
                Instance.incomingPlayerCommands.TryAdd(playerId, new List<string>());

            Instance.incomingPlayerCommands[playerId].Add(command);
        }


        public static void RemovePlayerCommandQueue(string playerId)
        {
            Instance.incomingPlayerCommands.TryRemove(playerId, out List<string> unusedValue);
        }


        public static void SubscribeToClientMessages(Action<string, string> eventHandler)
        {
            Instance.messageForClient += eventHandler;
        }


        public static void Update()
        {
            Instance.ExecuteIncomingCommands();
            Instance.entityWorld.FixedUpdate(Instance.gameTime.ElapsedTime);
            Thread.Sleep(30); // Fuck it, let's not get fancy here.
        }


        private void ExecuteIncomingCommands()
        {
            foreach (KeyValuePair<string, List<string>> playerEntry in this.incomingPlayerCommands)
            {
                string playerId = playerEntry.Key;
                List<string> playerCommands = playerEntry.Value;
                
                foreach (string commandString in playerCommands)
                {
                    Command command = Command.ParseCommand(commandString);
                    string response = command.Execute(playerEntry.Key);

                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = new
                                         {
                                             Update = "Terminal",
                                             Payload = new {
                                                                   Message = response,
                                                               }
                                         };
                        SendMessageToClient(playerId, JsonConvert.SerializeObject(result));
                    }
                }
                
                playerCommands.Clear();
            }
        }


        internal static void SendMessageToClient(string playerId, string playerStateJson)
        {
            Instance.messageForClient.Raise(playerId, playerStateJson);
        }
    }
}