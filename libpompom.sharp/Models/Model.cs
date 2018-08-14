using System;
using RestSharp.Deserializers;

namespace Pompom.Models
{
    public class DeviceInfo
    {
        /// <summary>
        /// Random uuid string.
        /// </summary>
        public string Uuid { get; set; }
    }

    public class CharacterInfo
    {
        [DeserializeAs(Name = "appLocaleType")]
        public string LocalType { get; set; }
    }
}
