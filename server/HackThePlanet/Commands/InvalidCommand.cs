namespace HackThePlanet
{
    public class InvalidCommand : Command
    {
        public override string Execute(string playerId)
        {
            return $"{this.Name}: command not found";
        }
    }
}