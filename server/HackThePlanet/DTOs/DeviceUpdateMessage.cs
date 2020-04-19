namespace HackThePlanet
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public class DeviceUpdateMessage
    {
        public readonly string Update = "devices";
        public object Payload;


        public static DeviceUpdateMessage Create(NetworkAccessComponent networkAccessComponent)
        {
            List<Device> devices = new List<Device>();
            foreach (KeyValuePair<int, AccessLevel> entry in networkAccessComponent.KnownEntities)
            {
                Entity deviceEntity = GameService.GetEntity(entry.Key);
                ComputerComponent deviceComputer = deviceEntity.GetComponent<ComputerComponent>();
                string ip = deviceComputer.IpAddress.ToIPString();
                
                Device device = new Device();
                device.Status = "idle"; // TODO
                device.IP = ip;
                // TODO
                device.Commands = new[]
                                      {
                                          $"[Port Scan](portscan|{ip})"
                                      };
                devices.Add(device);
            }
            
            DeviceUpdateMessage updateMessage = new DeviceUpdateMessage();
            updateMessage.Payload = 
                new 
                    { 
                        devices = devices, 
                    };
            
            return updateMessage;
        }
        
        public static DeviceUpdateMessage Create(ComputerComponent computerComponent)
        {
            return null;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


        private class Device
        {
            public string IP;
            public string Status;
            public string[] Commands;
        }
    }
}