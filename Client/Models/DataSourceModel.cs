using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SmartRetail.Client.Models
{
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DataSourceModel
    {
        public DataSourceModel()
        { }

        //public DataSourceModel SelfClone()
        //{
        //    var sour = this.GetClone();
        //    sour.Para.Clear();
        //    foreach (var it in this.Para)
        //    {
        //        sour.Para.Add(it.Key, it.Value);
        //    }
        //    return sour;
        //}

        public string ObjectId { get; set; }
        //[PrimaryKey]
        [JsonProperty]
        public string ID { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public Dictionary<string, string> Para { get; set; } = new Dictionary<string, string>();

        [JsonProperty]
        public string SourceType { get; set; }
        [JsonProperty]
        public bool IsRequestServer { get; set; } = true; // True Mean Background Service Will Call at interval Time.
        [JsonProperty]
        public bool IsUpdateOutsideDevice { get; set; } = true; // True Mean It Need Connect to Update Out Side Device.
        /// <summary>
        /// Milisecond Unix Timestamp
        /// </summary>
        [JsonProperty]
        public int Interval { get; set; }
        /// <summary>
        /// Milisecond Unix Timestamp
        /// </summary>
        [JsonProperty]
        public long LastUpdateDeviceTime { get; set; }


        public bool Update(DataSourceModel dataSource)
        {
            Name = dataSource.Name;
            Para.Clear();
            foreach (var dt in dataSource.Para) Para.Add(dt.Key, dt.Value);
            SourceType = dataSource.SourceType;
            IsRequestServer = dataSource.IsRequestServer;
            Interval = dataSource.Interval;
            return true;
        }
        public dynamic GetPara(string key)
        {
            foreach (var data in Para)
            {
                if (key.Trim() == data.Key.Trim())
                    return data.Value;
            }
            return null;
        }

        public bool UpdatePara(Dictionary<string, dynamic> data)
        {
            bool isUpdate = false;
            foreach (var item in data)
            {
                foreach (var local in Para)
                {
                    if (local.Key == item.Key)
                    {
                        var type = item.Value.GetType();
                        if (local.Value != item.Value.ToString())
                        {
                            Para[local.Key] = item.Value;
                            isUpdate = true;
                        }
                        break;
                    }
                }
            }
            return isUpdate;
        }
    }
}
