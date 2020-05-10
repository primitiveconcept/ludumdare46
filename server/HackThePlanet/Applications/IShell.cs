namespace HackThePlanet
{
    public interface IShell : IApplication
    {
        UserAccount ActiveUser { get; set; }
    }
}