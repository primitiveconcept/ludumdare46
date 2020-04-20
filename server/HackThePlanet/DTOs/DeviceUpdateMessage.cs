namespace HackThePlanet
{
    using System.Collections;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public partial class DeviceUpdateMessage
    {
        public readonly string update = "Devices";
        public object payload;


        public static DeviceUpdateMessage Create(NetworkAccessComponent networkAccessComponent)
        {
            List<Device> devices = new List<Device>();
            foreach (KeyValuePair<int, AccessOptions> entry in networkAccessComponent.AccessOptions)
            {
                Entity deviceEntity = Game.GetEntity(entry.Key);
                ComputerComponent deviceComputer = deviceEntity.GetComponent<ComputerComponent>();
                string ip = deviceComputer.IpAddress.ToIPString();
                
                Device device = new Device();
                device.status = "idle"; // TODO
                device.ip = ip;
                // TODO
                device.commands = entry.Value.GetAccessOptions(ip);
                devices.Add(device); 
            }
            
            DeviceUpdateMessage updateMessage = new DeviceUpdateMessage();
            updateMessage.payload = 
                new 
                    { 
                        devices = devices
                    };
            
            return updateMessage;
        }

        
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}