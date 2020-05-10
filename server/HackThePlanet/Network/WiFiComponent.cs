namespace HackThePlanet
{
    using System;
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public class WiFiComponent : IEntityComponent
    {
        #region Properties
        [JsonIgnore]
        public bool CanReceive
        {
            get
            {
                return this.Mode == WirelessMode.Receiver 
                       || this.Mode == WirelessMode.Transceiver; 
            }
        }


        [JsonIgnore]
        public bool CanTransmit
        {
            get
            {
                return this.Mode == WirelessMode.Transmitter
                       || this.Mode == WirelessMode.Transceiver;
            }
        }


        public EncryptionMethod Encryption { get; set; } = EncryptionMethod.WPA2;
        public StringReference GuestLoginName { get; set; }
        public StringReference GuestLoginPassword { get; set; }
        public StringReference LoginName { get; set; }
        public StringReference LoginPassword { get; set; }
        public WirelessMode Mode { get; set; }
        public double Range { get; set; }
        #endregion


        public bool CanReceiveFrom(WiFiComponent otherWiFi)
        {
            if (!this.CanReceive
                || !otherWiFi.CanTransmit)
            {
                return false;
            }
            
            return IsWithinRangeOf(otherWiFi);
        }


        public bool CanTransmitTo(WiFiComponent otherWiFi)
        {
            if (!this.CanTransmit
                || !otherWiFi.CanReceive)
            {
                return false;
            }

            return IsWithinRangeOf(otherWiFi);
        }


        public bool IsWithinRangeOf(WiFiComponent otherWiFi)
        {
            Location thisLocation = this.GetSiblingComponent<Location>();
            Location otherLocation = otherWiFi.GetSiblingComponent<Location>();

            if (thisLocation == null
                || otherLocation == null)
            {
                return false;
            }

            double range = thisLocation.GetDistanceTo(otherLocation);
            if (range > Math.Min(this.Range, otherWiFi.Range))
                return false;

            return true;
        }
    }
}