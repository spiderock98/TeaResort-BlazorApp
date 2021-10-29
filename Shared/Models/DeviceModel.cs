using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using SmartRetail.Share.Helper;

namespace SmartRetail.Share.Models
{

    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DeviceModel
    {
        // public DeviceModel SelfClone()
        // {
        //     DeviceModel md = this.GetClone();

        //     if (Status == null) Status = new Dictionary<string, string>();
        //     md.Status.Clear();
        //     foreach (var st in this.Status)
        //     {
        //         md.Status.Add(st.Key, st.Value);
        //     }

        //     if (DataSourcePara == null) DataSourcePara = new Dictionary<string, string>();
        //     md.DataSourcePara.Clear();
        //     foreach (var dt in this.DataSourcePara)
        //     {
        //         md.DataSourcePara.Add(dt.Key, dt.Value);
        //     }

        //     md.DataSaveList.Clear();
        //     foreach (var dt in this.DataSaveList)
        //     {
        //         md.DataSaveList.Add(dt);
        //     }
        //     md.MeterSaveList.Clear();
        //     foreach (var dt in this.MeterSaveList)
        //     {
        //         md.MeterSaveList.Add(dt);
        //     }
        //     md.Paras.Clear();
        //     foreach (var dt in this.Paras)
        //     {
        //         md.Paras.Add(dt.Key, dt.Value);
        //     }
        //     return md;
        // }

        public string ObjectId { get; set; }
        //[PrimaryKey]
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Capabilitie { get; set; }
        [JsonProperty]
        public string DeviceType { get; set; }
        [JsonProperty]
        public bool IsConnect { get; set; }
        [JsonProperty]
        public string DataSourceId { get; set; }
        [JsonProperty]
        public List<string> DataSaveList { get; set; } = new List<string>(); // Only Save Data In This Object
        [JsonProperty]
        public List<string> MeterSaveList { get; set; } = new List<string>(); // Only Save Data In This Object
        [JsonProperty]
        public long LastUpdate { get; set; }
        [JsonProperty]
        public int SectionId { get; set; }
        [JsonProperty]
        public Dictionary<string, string> DataSourcePara { get; set; } = new Dictionary<string, string>();
        [JsonProperty]
        public Dictionary<string, string> Paras { get; set; } = new Dictionary<string, string>();
        [JsonProperty]
        public Dictionary<string, long> LastStatusChange { get; set; } = new Dictionary<string, long>();
        [JsonProperty]
        public Dictionary<string, string> Status { get; set; } = new Dictionary<string, string>();

        public List<string> UpdateStatus(Dictionary<string, string> data)
        {
            if (Status == null) Status = new Dictionary<string, string>();
            var deviceUpdateList = new List<string>();
            foreach (var item in data)
            {
                foreach (var local in Status)
                {
                    if (local.Key == item.Key)
                    {
                        if (local.Value.Trim() != item.Value.Trim())
                        {
                            if (item.Value != null)
                            {
                                Status[local.Key] = item.Value.Trim();
                                deviceUpdateList.Add(item.Key);
                                var logTime = LastStatusChange.Where(x => x.Key == item.Key);
                                if (logTime == null)
                                {
                                    LastStatusChange.Add(item.Key, Helper.UnixTime.GetCurrentMilliSecond());
                                }
                                else LastStatusChange[item.Key] = Helper.UnixTime.GetCurrentMilliSecond();
                            }
                        }
                        break;
                    }
                }
            }
            return deviceUpdateList;
        }

        public bool UpdateStatus(string key, string value)
        {
            bool isUpdate = false;
            if (Status == null) Status = new Dictionary<string, string>();
            foreach (var local in Status)
            {
                if (local.Key == key)
                {
                    if (local.Value.Trim() != value.ToString().Trim())
                    {
                        Status[key] = value.Trim();
                        isUpdate = true;
                        var logTime = LastStatusChange.Where(x => x.Key == key);
                        if (logTime == null)
                        {
                            LastStatusChange.Add(key, Helper.UnixTime.GetCurrentMilliSecond());
                        }
                        else LastStatusChange[key] = Helper.UnixTime.GetCurrentMilliSecond();
                    }
                    break;
                }
            }
            return isUpdate;
        }

        public bool InsertValue(KeyValuePair<string, string> _pair)
        {
            if (Status == null) Status = new Dictionary<string, string>();
            foreach (var pair in Status)
            {
                if (pair.Key.Trim() == _pair.Key.Trim())
                    return false;
            }
            Status.Add(_pair.Key, _pair.Value);
            return true;
        }
        public bool RemoveValue(string key)
        {
            if (Status == null) Status = new Dictionary<string, string>();
            foreach (var pair in Status)
            {
                if (pair.Key.Trim() == key.Trim())
                {
                    Status.Remove(pair.Key);
                    try
                    {
                        LastStatusChange.Remove(pair.Key);
                    }
                    catch { }
                    return true;
                }
            }
            return true;
        }
        public bool InsertSourcePara(KeyValuePair<string, string> _pair)
        {
            if (DataSourcePara == null) DataSourcePara = new Dictionary<string, string>();
            foreach (var pair in DataSourcePara)
            {
                if (pair.Key.Trim() == _pair.Key.Trim())
                    return false;
            }
            DataSourcePara.Add(_pair.Key, _pair.Value);
            return true;
        }
        public string GetValue(string key)
        {
            if (Status == null) Status = new Dictionary<string, string>();
            foreach (var data in Status)
            {
                if (key.Trim() == data.Key.Trim())
                    return data.Value;
            }
            return null;
        }
        /// <summary>
        /// Using with Millisecond Unix TimeStamp
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetLastValueChange(string key)
        {
            foreach (var data in LastStatusChange)
            {
                if (key.Trim() == data.Key.Trim())
                {
                    return LastStatusChange[key];
                }
            }
            return -1;
        }

        /// <summary>
        /// Using with Millisecond
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetIntervalValueChange(string key)
        {
            try
            {
                var lsChangeTime = LastStatusChange[key];
                if (lsChangeTime > 0)
                {
                    return Helper.UnixTime.GetCurrentMilliSecond() - lsChangeTime;
                }
                return -1;
            }
            catch
            {
                return -1;
            }

        }

        public string GetSourcePara(string key)
        {
            try
            {
                return DataSourcePara[key];
            }
            catch { }
            return null;
        }

    }

    public class StatusModel
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Value { get; set; }
    }


}
