using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRetail.Client.Models
{
    public class DataMode
    {
        string id;
        int time;
        string key;
        string classify;
        string _value;
        string description;

        [JsonProperty]
        public string Id { get; set; }
        [JsonProperty]
        public string Key { get; set; }
        [JsonProperty]
        public string Classify { get; set; }
        [JsonProperty]
        public string Value { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public int Time { get; set; }

    }
}
