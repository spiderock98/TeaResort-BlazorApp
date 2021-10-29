using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRetail.Share.Models
{
    public class DataModel : BaseNotifyModel
    {
        string id;
        int time;
        string key;
        string classify;
        string _value;
        string description;

        [JsonProperty]
        public string Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        [JsonProperty]
        public string Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }
        [JsonProperty]
        public string Classify
        {
            get { return classify; }
            set { SetProperty(ref classify, value); }
        }
        [JsonProperty]
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
        [JsonProperty]
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
        [JsonProperty]
        public int Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }

    }
}
