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
            List<DeviceState> devices = new List<DeviceState>();
            foreach (KeyValuePair<int, AccessOptions> entry in networkAccessComponent.AccessOptions)
            {
                GetDeviceState(entry: entry, devices: devices);
            }
            
            DeviceUpdateMessage updateMessage = new DeviceUpdateMessage();
            updateMessage.payload = 
                new 
                    { 
                        devices = devices
                    };
            
            return updateMessage;
        }


        private static void GetDeviceState(KeyValuePair<int, AccessOptions> entry, List<DeviceState> devices)
        {
            Entity deviceEntity = Game.GetEntity(entry.Key);
            ComputerComponent deviceComputer = deviceEntity.GetComponent<ComputerComponent>();
            string ip = deviceComputer.IpAddress.ToIPString();

            DeviceState device = new DeviceState();
            device.status = "idle"; // TODO
            device.ip = ip;
            // TODO
            device.commands = entry.Value.GetAccessOptions(ip);
            devices.Add(device);
        }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}