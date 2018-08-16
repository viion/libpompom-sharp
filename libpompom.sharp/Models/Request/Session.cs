using Newtonsoft.Json;

namespace Pompom.Models.Request
{
    public class DeviceInfo
    {
        /// <summary>
        /// Device id.
        /// </summary>
        /// <remarks>
        /// You need to sign a uuid you choose with a public key which was shipped with the companion app.
        /// </remarks>
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("platform")]
        public PlatformType Platform { get; set; }
    }

    public class FcmToken
    {
        [JsonProperty("fcmToken")]
        public string Value { get; set; }
    }

    public class CharacterInfo
    {
        [JsonProperty("appLocaleType")]
        public string LocalType { get; set; }
    }

    public class TelemetryInfo
    {
        [JsonProperty("advertisingId")]
        public string Id { get; set; }

        [JsonProperty("isTrackingEnabled")]
        public int Enabled { get; set; }
    }
}
