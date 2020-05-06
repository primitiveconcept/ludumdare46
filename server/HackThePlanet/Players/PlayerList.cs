namespace HackThePlanet
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;


    public class PlayerList
    {
        private ConcurrentDictionary<string, PlayerComponent> players = 
            new ConcurrentDictionary<string, PlayerComponent>();


        public PlayerComponent CreateNewPlayer()
        {
            PlayerComponent player = PlayerGenerator.Generate();
            Add(player);

            return player;
        }

        
        public void Add(IEnumerable<PlayerComponent> players)
        {
            foreach (PlayerComponent player in players)
            {
                Add(player);
            }
        }


        public void Add(PlayerComponent player)
        {
            this.players.TryAdd(player.Id, player);
        }


        public string GetPlayerId(PlayerComponent player)
        {
            foreach (KeyValuePair<string, PlayerComponent> entry in this.players)
            {
                if (entry.Value == player)
                    return entry.Key;
            }

            return null;
        }


        public PlayerComponent GetReadonlyPlayer(string playerId)
        {
            return this.players.TryGetValue(playerId, out PlayerComponent player) 
                       ? player.Clone() 
                       : null;
        }


        public async void Remove(PlayerComponent player)
        {
            Remove(GetPlayerId(player));
        }


        public void Remove(string playerId)
        {
            this.players.TryRemove(playerId, out PlayerComponent socket);
        }


        internal PlayerComponent GetPlayer(string playerId)
        {
            return this.players.TryGetValue(playerId, out PlayerComponent player) 
                       ? player 
                       : null;
        }
    }
}