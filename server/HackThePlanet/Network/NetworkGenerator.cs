namespace HackThePlanet
{
    using PrimitiveEngine;


    public static class NetworkGenerator
    {
        // TODO: Temporary, for testing. Remove later.
        public static void SeedInternet()
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerGenerator.GeneratePlayerEntity();
            }
        }
    }
}