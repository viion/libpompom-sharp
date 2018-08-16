using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pompom.Models.Response
{
    public class RetainerListResponse
    {
        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }

        [JsonProperty("retainer")]
        public List<RetainerInfo> Retainers { get; set; }

        public class RetainerInfo
        {
            [JsonProperty("createUnixTime")]
            public long CreatedUnixTime { get; set; }

            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("retainerId")]
            public string RetainerId { get; set; }

            [JsonProperty("retainerName")]
            public string RetainerName { get; set; }

            [JsonProperty("isRenamed")]
            public bool IsRenamed { get; set; }
        }
    }
}
