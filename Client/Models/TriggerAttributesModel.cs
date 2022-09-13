using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartRetail.Client.Models
{
    public class TriggerAttributesModel
    {
        [JsonProperty]
        public int DeviceId;
        [JsonProperty]
        public string Attribute;
    }
}
