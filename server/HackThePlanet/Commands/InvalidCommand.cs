namespace HackThePlanet
{
    public class InvalidCommand : Command
    {
        public override string Execute(int playerId)
        {
            return $"{this.Name}: command not found";
        }
    }
}