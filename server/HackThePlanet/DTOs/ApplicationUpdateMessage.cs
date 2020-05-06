namespace HackThePlanet
{
    public class ApplicationUpdateMessage : UpdateMessage<IApplicationComponent>
    {
        private const string UpdateType = "application"; 
        
        public override IApplicationComponent Payload { get; set; }


        public override string Type
        {
            get { return UpdateType; }
        }
    }
}