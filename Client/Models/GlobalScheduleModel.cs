using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{
    public class GlobalScheduleModel
    {
        public enum ScheduleTypeEnum { OneTime, Yearly, Monthly, Daily, IntervalTime }

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

        public GlobalScheduleModel DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<GlobalScheduleModel>(_tmpSerializeString);
        }

        public GlobalScheduleModel ShallowCopy()
        {
            return (GlobalScheduleModel)this.MemberwiseClone();
        }

        public string FormatScheduleTypeStringCulture()
        {
            switch (ScheduleType)
            {
                case ScheduleTypeEnum.OneTime:
                    return Helper.UnixTime.UnixSecondToLocalTime(Time).ToString("f");

                case ScheduleTypeEnum.IntervalTime:
                    return string.Format("Every {0} ms", Time);

                case ScheduleTypeEnum.Daily:
                    var dataDaily = DateTimeOffset.Now;
                    dataDaily = new DateTime(dataDaily.Year, dataDaily.Month, dataDaily.Day, 0, 0, 0);
                    dataDaily = dataDaily.AddSeconds(Time);

                    var lstStrRepeatDate = new List<string>();
                    int idxDate = 0;
                    foreach (var it in RepeatDayOfWeek)
                    {
                        if (it == 1)
                        {
                            lstStrRepeatDate.Add(((DayOfWeek)idxDate).ToString());
                        }
                        ++idxDate;
                    }
                    return string.Format("At {0} on {1}", dataDaily.ToString("HH:mm"), string.Join(',', lstStrRepeatDate));

                case ScheduleTypeEnum.Monthly:
                    var dataMonthly = DateTimeOffset.Now;
                    dataMonthly = new DateTime(dataMonthly.Year, dataMonthly.Month, 1, 0, 0, 0);
                    dataMonthly = dataMonthly.AddSeconds(Time);

                    return string.Format("On {0} every month at {1}", dataMonthly.ToString("dd"), dataMonthly.ToString("HH:mm"));

                case ScheduleTypeEnum.Yearly:
                    var dataYearly = DateTimeOffset.Now;
                    dataYearly = new DateTime(dataYearly.Year, 1, 1, 0, 0, 0);
                    dataYearly = dataYearly.AddSeconds(Time);
                    return string.Format("");
            }
            return string.Empty;

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
