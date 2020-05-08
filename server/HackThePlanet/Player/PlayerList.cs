namespace HackThePlanet
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class PlayerList
    {
        private ConcurrentDictionary<int, PlayerComponent> players = 
            new ConcurrentDictionary<int, PlayerComponent>();


        public Entity CreateNewPlayer()
        {
            Entity playerEntity = PlayerGenerator.GeneratePlayerEntity();
            PlayerComponent playerComponent = playerEntity.GetComponent<PlayerComponent>();
            Add(playerComponent);

            return playerEntity;
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


        public int GetPlayerId(PlayerComponent player)
        {
            foreach (KeyValuePair<int, PlayerComponent> entry in this.players)
            {
                if (entry.Value == player)
                    return entry.Key;
            }

            return 0;
        }


        public PlayerComponent GetReadonlyPlayer(int playerId)
        {
            return this.players.TryGetValue(playerId, out PlayerComponent player) 
                       ? player.Clone() 
                       : null;
        }


        public async void Remove(PlayerComponent player)
        {
            Remove(GetPlayerId(player));
        }


        public void Remove(int playerId)
        {
            this.players.TryRemove(playerId, out PlayerComponent socket);
        }


        internal PlayerComponent GetPlayer(int playerId)
        {
            return this.players.TryGetValue(playerId, out PlayerComponent player) 
                       ? player 
                       : null;
        }
    }
}