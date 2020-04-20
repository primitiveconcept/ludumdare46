namespace HackThePlanet
{
    using System.Collections.Generic;


    public static class Computer
    {
        public static ComputerComponent Find(string ipAddress)
        {
            return Find(ipAddress.ToIPLong());
        }


        public static ComputerComponent Find(long ipAddress)
        {
            IEnumerable<ComputerComponent> computers = Game.GetComponents<ComputerComponent>();
            foreach (var computer in computers)
            {
                if (computer.IpAddress == ipAddress)
                    return computer;
            }

            return null;
        }
    }
}