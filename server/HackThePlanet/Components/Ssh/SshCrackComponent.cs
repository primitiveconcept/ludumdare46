namespace HackThePlanet
{
    using PrimitiveEngine;


    public class SshCrackComponent : IEntityComponent, IProcess
    {
        public int InitiatingEntity;
        public int TargetEntity;
        public long elapsedTime = 0;

        public float Progress;


        #region Properties
        public string Command
        {
            get { return "sshcrack"; }
        }


        public ushort RamUse
        {
            get { return 1; }
        }


        public string Status { get; set; }
        #endregion
    }
}