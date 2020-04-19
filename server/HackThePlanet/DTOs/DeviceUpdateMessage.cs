namespace HackThePlanet
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public class DeviceUpdateMessage
    {
        public readonly string update = "Devices";
        public object payload;


        public static DeviceUpdateMessage Create(NetworkAccessComponent networkAccessComponent)
        {
            List<Device> devices = new List<Device>();
            foreach (KeyValuePair<int, AccessLevel> entry in networkAccessComponent.KnownEntities)
            {
                Entity deviceEntity = GameService.GetEntity(entry.Key);
                ComputerComponent deviceComputer = deviceEntity.GetComponent<ComputerComponent>();
                string ip = deviceComputer.IpAddress.ToIPString();
                
                Device device = new Device();
                device.status = "idle"; // TODO
                device.ip = ip;
                // TODO
                device.commands = new[]
                                      {
                                          $"[Port Scan](portscan|{ip})"
                                      };
                devices.Add(device);
            }
            
            DeviceUpdateMessage updateMessage = new DeviceUpdateMessage();
            updateMessage.payload = 
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
            public string ip;
            public string status;
            public string[] commands;
        }
    }
}