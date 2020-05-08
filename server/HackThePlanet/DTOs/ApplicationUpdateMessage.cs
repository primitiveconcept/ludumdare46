namespace HackThePlanet
{
    public class ApplicationUpdateMessage : UpdateMessage<IApplication>
    {
        private const string UpdateType = "application"; 
        
        public override IApplication Payload { get; set; }


        public override string Type
        {
            get { return UpdateType; }
        }
    }
}