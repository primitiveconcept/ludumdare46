namespace HackThePlanet
{
    using System;


    public class CommandAttribute : Attribute
    {
        public readonly string Name;

        public CommandAttribute(string name)
        {
            this.Name = name;
        }
    }
}