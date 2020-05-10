namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class PlayerComponent : IEntityComponent
    {
        #region Properties
        public int Id { get; set; }


        public Dictionary<string, List<AvailableIPActions>> KnownIPs { get; set; } = 
            new Dictionary<string, List<AvailableIPActions>>();


        public string Name { get; set; }
        #endregion


        public void AddKnownIP(string ip)
        {
            if (!this.KnownIPs.ContainsKey(ip))
                this.KnownIPs.Add(ip, new List<AvailableIPActions>());
        }


        public PlayerComponent Clone()
        {
            return new PlayerComponent()
                       {
                           Id = this.Id,
                           Name = this.Name,
                           KnownIPs = new Dictionary<string, List<AvailableIPActions>>(this.KnownIPs)
                       };
        }


        public void RemoveKnownIP(string ip)
        {
            if (this.KnownIPs.ContainsKey(ip))
                this.KnownIPs.Remove(ip);
        }


        public void SetKnownIPFlag(string ip, AvailableIPActions actions)
        {
            AddKnownIP(ip);
            if (!this.KnownIPs[ip].Contains(actions))
                this.KnownIPs[ip].Add(actions);
        }


        public void UnsetKnownIPFlag(string ip, AvailableIPActions actions)
        {
            if (this.KnownIPs.ContainsKey(ip)
                && this.KnownIPs[ip].Contains(actions))
            {
                this.KnownIPs[ip].Remove(actions);
            }
        }
    }
}