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
                Entity playerEntity = PlayerGenerator.GeneratePlayerEntity();
                ComputerComponent playerComputer = playerEntity.GetComponent<ComputerComponent>();
                
                // TODO: Temporary, for testing
                SshServerApplication sshServer =
                    ProcessPool<SshServerApplication>.RunApplication(playerComputer);
                sshServer.Accounts.Add(new UserAccount()
                                           {
                                               UserName = "root",
                                               Password = "god",
                                               AccessLevel = AccessLevel.Root
                                           });
            }
        }
    }
}