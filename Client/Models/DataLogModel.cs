using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRetail.Client.Models
{
    public class DataLogModel
    {
        public DataLogModel()
        {

        }

        public string DeviceName { get; set; }
        public string SectionName { get; set; }
        public string ZoneName { get; set; }
        //-------------------------------------


        [JsonProperty]
        public Int64 Time { get; set; }
        [JsonProperty]
        public int DeviceId { get; set; }
        [JsonProperty]
        public string Attribute { get; set; }
        [JsonProperty]
        public string OldValue { get; set; }
        [JsonProperty]
        public string Value { get; set; }
    }

}
