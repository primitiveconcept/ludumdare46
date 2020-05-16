namespace HackThePlanet
{
    using Newtonsoft.Json;


    public abstract class UpdateMessage<T>
    {
        #region Properties
        public string Error { get; set; }
        public string Message { get; set; }
        public abstract T Payload { get; set; }
        public abstract string UpdateType { get; }
        public IP? IP { get; set; }


        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}