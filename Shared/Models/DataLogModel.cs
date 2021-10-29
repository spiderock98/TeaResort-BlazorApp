using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRetail.Share.Models
{
    public class DataLogModel : BaseNotifyModel
    {
        public DataLogModel()
        {

        }

        //-- For Internal Using ---------------
        string _DeviceName;
        string _SectionName;
        string _ZoneName;

        public string DeviceName
        {
            get { return _DeviceName; }
            set { SetProperty(ref _DeviceName, value); }
        }

        public string SectionName
        {
            get { return _SectionName; }
            set { SetProperty(ref _SectionName, value); }
        }

        public string ZoneName
        {
            get { return _ZoneName; }
            set { SetProperty(ref _ZoneName, value); }
        }
        //-------------------------------------

        Int64 _Time;
        int _DeviceId;
        string _Attribute;
        string _OldValue;
        string _Value;

        [JsonProperty]
        public Int64 Time
        {
            get { return _Time; }
            set { SetProperty(ref _Time, value); }
        }
        [JsonProperty]
        public int DeviceId
        {
            get { return _DeviceId; }
            set { SetProperty(ref _DeviceId, value); }
        }
        [JsonProperty]
        public string Attribute
        {
            get { return _Attribute; }
            set { SetProperty(ref _Attribute, value); }
        }
        [JsonProperty]
        public string OldValue
        {
            get { return _OldValue; }
            set { SetProperty(ref _OldValue, value); }
        }
        [JsonProperty]
        public string Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
    }

}
