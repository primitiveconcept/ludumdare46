namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class ComputerComponent : IEntityComponent
    {
        private const ushort MinimumProcessId = 10000;


        #region Properties
        public List<UserAccount> Accounts { get; set; } = 
            new List<UserAccount>();


        public Cpu Cpu { get; set; }

        public ushort Ram { get; set; }


        /// <summary>
        /// Lookup table to application components running on separate entities.
        /// ushort = PID (process ID), int = Entity.Id
        /// </summary>
        public Dictionary<ushort, IApplication> RunningApplications { get; set; } =
            new Dictionary<ushort, IApplication>();
        #endregion


        public ushort GetFreeProcessId()
        {
            for (ushort pid = MinimumProcessId; pid < ushort.MaxValue; pid++)
            {
                if (!this.RunningApplications.ContainsKey(pid))
                    return pid;
            }
            
            throw new IndexOutOfRangeException("Unable to allocate new PID.");
        }
    }
}