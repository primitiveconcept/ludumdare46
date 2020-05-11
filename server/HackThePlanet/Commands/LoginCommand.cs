namespace HackThePlanet
{
    using PrimitiveEngine;


    [Command("internal_login")]
    public class LoginCommand : Command
    {
        public override string Execute(int playerId)
        {
            Entity playerEntity = Game.World.GetEntityById(playerId);

            SshServerApplication sshServer = 
                playerEntity.GetComponent<ProcessPool<SshServerApplication>>()?[0];

            if (sshServer != null)
            {
                // TODO: Add account on first login.
                // TODO: Set account active

                return PlayerStateMessage.Create(playerId);
            }
            
            return null;
        }
    }
}