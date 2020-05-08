namespace HackThePlanet
{
    public static class ComputerGenerator
    {
        public static ComputerComponent Generate()
        {
            ComputerComponent computer =
                new ComputerComponent()
                    {
                        Ram = 2048,
                        Cpu = new Cpu()
                                  {
                                      Cores = 1,
                                      ClockSpeed = 1024
                                  }
                    };
            
            return computer;
        }
    }
}