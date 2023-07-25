using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Helpers
{
    public class IPInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
