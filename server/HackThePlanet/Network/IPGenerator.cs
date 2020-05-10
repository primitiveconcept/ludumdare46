namespace HackThePlanet
{
    using System;
    using System.Net;


    public static class IPGenerator
    {
        public static IP GenerateRandomPublic()
        {
            byte[] randomBytes = new byte[4];
            new Random().NextBytes(randomBytes);
            IP ip = new IP(randomBytes);

            return ip.IsValidPublicAddress() 
                       ? ip 
                       : GenerateRandomPublic();
        }


        public static IP GenerateGatewayAddressFor(IP ip)
        {
            ip[3] = 1;

            return ip;
        }
        

        public static IP GenerateRandomGatewayAddress()
        {
            IP randomAddress = GenerateRandomPublic();
            randomAddress[3] = 1;

            // TODO: Redo if IP already exists in the world.
            
            return randomAddress;
        }
    }
}