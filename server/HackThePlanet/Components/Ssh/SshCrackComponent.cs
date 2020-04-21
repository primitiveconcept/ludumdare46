namespace HackThePlanet
{
    using PrimitiveEngine;


    public class SshCrackComponent : IEntityComponent
    {
        public int InitiatingEntity;
        public int TargetEntity;
        public long elapsedTime = 0;
        
        public float Progress;
    }
}