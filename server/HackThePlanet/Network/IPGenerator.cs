namespace HackThePlanet
{
    using System;
    using System.Net;


    public static class IPGenerator
    {
        public static IP GenerateRandom()
        {
            byte[] randomBytes = new byte[4];
            new Random().NextBytes(randomBytes);
            IP ip = new IP(randomBytes);
            
            // TODO: Redo if IP already exists in the world.

            return ip;
        }


        public static IP GenerateGatewayAddressFor(IP ip)
        {
            ip[3] = 1;

            return ip;
        }
        

        public static IP GenerateRandomGatewayAddress()
        {
            IP randomAddress = GenerateRandom();
            randomAddress[3] = 1;

            // TODO: Redo if IP already exists in the world.
            
            return randomAddress;
        }
    }
}