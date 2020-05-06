namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class ComputerComponent : IEntityComponent
    {
        private const ushort MinimumProcessId = 
        
        public ushort Ram;
        public Cpu Cpu;
        
        public Dictionary<ushort, int> RunningApplications = 
            new Dictionary<ushort,int>();


        public ushort GetFreeProcessId()
        {
            
        }
    }
}