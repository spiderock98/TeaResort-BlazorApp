using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int DeviceId { get; set; }
        public string ScheduleId { get; set; }
    }
}
