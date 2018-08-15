using System;
using RestSharp.Deserializers;

namespace Pompom.Models.Request
{
    public class DeviceInfo
    {
        /// <summary>
        /// Random uuid string.
        /// </summary>
        [DeserializeAs(Name = "uid")]
        public string Uuid { get; set; }

        [DeserializeAs(Name = "platform")]
        public PlatformType Platform { get; set; }
    }

    public class FcmToken
    {
        [DeserializeAs(Name = "fcmToken")]
        public string Value { get; set; }
    }

    public class CharacterInfo
    {
        [DeserializeAs(Name = "appLocaleType")]
        public string LocalType { get; set; }
    }

    public class TelemetryInfo
    {
        [DeserializeAs(Name = "advertisingId")]
        public string Id { get; set; }

        [DeserializeAs(Name = "isTrackingEnabled")]
        public int Enabled { get; set; }
    }
}
