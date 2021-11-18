using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SmartRetail.Share.Models
{
    public enum MemberType { Device, Scenes, Schedule };
    public class RunningActionModel
    {
        public MemberType Type { get; set; }
        /// <summary>
        /// For Fix Value
        /// Key: Attribute; Value: Value
        /// </summary>
        public Dictionary<string, string> SetValues { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// For Refer to Attribute of other Device
        /// Key: Attribute; Value : Tupble<ref Device Id, ref Attribut> 
        /// </summary>
        public Dictionary<string, Tuple<int, string>> SetReferValue { get; set; } = new Dictionary<string, Tuple<int, string>>();

        public List<int> DeviceList { get; set; } = new List<int>();
        public string ScheduleScenesId { get; set; }

        // ! misc function and attr
        [JsonIgnore]
        public int DeviceId { get; set; }
        [JsonIgnore]
        public string ScheduleId { get; set; }

        [JsonIgnore]
        public string ActionId { get; set; } = Guid.NewGuid().ToString("N");
        [JsonIgnore]
        public List<int> LstDeviceId { get; set; } = new List<int>();

        public List<RunningActionModel> GenActionByLstDeviceId()
        {
            List<RunningActionModel> result = new List<RunningActionModel>();
            foreach (var item in LstDeviceId)
            {
                result.Add(new RunningActionModel()
                {
                    Type = this.Type,
                    SetValues = this.SetValues,
                    SetReferValue = this.SetReferValue,
                    ScheduleId = this.ScheduleId,
                    DeviceId = item
                });
            }
            return result;
        }
    }
}
