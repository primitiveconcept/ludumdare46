namespace HackThePlanet
{
    using PrimitiveEngine;


    [Command("wifiscan")]
    public class WiFiScanCommand : Command
    {
        public override string Execute(int playerId)
        {
            Entity playerEntity = Game.World.GetEntityById(playerId);
            
            WiFiComponent playerWifi = playerEntity.GetComponent<WiFiComponent>();
            if (playerWifi == null)
                return "WiFi network device not found.";

            




            return null;
        }
    }
}