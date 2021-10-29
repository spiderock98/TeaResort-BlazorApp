using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Share.Models
{
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class GlobalScheduleModel
    {
        public enum ScheduleTypeEnum { OneTime, Yearly, Monthly, Daily, IntervalTime }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        //[PrimaryKey]
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        // Description For Scenes
        public string Name { get; set; }
        /// <summary>
        /// Unix Second Time
        /// </summary>
        [JsonProperty]
        public long Time { get; set; }
        [JsonProperty]
        public ScheduleTypeEnum ScheduleType { get; set; }
        [JsonProperty]
        public List<int> RepeatDayOfWeek { get; set; } = new List<int>();
        [JsonProperty]
        public bool Enable { get; set; } = true;
        [JsonProperty]
        public bool IsJavaScriptAction { get; set; } = false;
        [JsonProperty]
        // Description For Scenes
        public string JavaScriptAction { get; set; } = "";
        [JsonProperty]
        // Do Action
        public List<RunningActionModel> Actions { get; set; } = new List<RunningActionModel>();

        /// <summary>
        /// Unix Milliseconds Time
        /// </summary>
        [JsonProperty]
        public long LastRunTime { get; set; }
        [JsonProperty]
        // Save Log For Scenes Run, It's should optimaze for saving store area.
        public string Debug { get; set; } = "";
        [JsonProperty]
        // Description For Scenes
        public string Description { get; set; }
        public bool IsRunning { get; set; } = false;

        public GlobalScheduleModel ShallowCopy()
        {
            return (GlobalScheduleModel)this.MemberwiseClone();
        }
        public void UpdateValue(GlobalScheduleModel _schedule)
        {
            this.Name = _schedule.Name;
            this.Time = _schedule.Time;
            this.ScheduleType = _schedule.ScheduleType;
            this.Description = _schedule.Description;
            this.Enable = _schedule.Enable;
            this.Debug = _schedule.Debug;
            this.IsJavaScriptAction = _schedule.IsJavaScriptAction;
            this.JavaScriptAction = _schedule.JavaScriptAction;
            this.LastRunTime = _schedule.LastRunTime;


            this.Actions.Clear();
            foreach (var item in _schedule.Actions) this.Actions.Add(item);
            this.RepeatDayOfWeek.Clear();
            foreach (var item in _schedule.RepeatDayOfWeek) this.RepeatDayOfWeek.Add(item);
        }
    }
}
