using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartRetail.Client.Models
{
    public class MasterDataModel
    {
        public List<CapabilitieModel> Capabilities = new List<CapabilitieModel>();
        public List<AttributeModel> Attributes = new List<AttributeModel>();
        public List<SourceTypeModel> SourceTypes = new List<SourceTypeModel>();
        public List<DeviceTypeModel> DeviceTypes = new List<DeviceTypeModel>();
        public enum CompareOperators { Equal, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual }

        [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
        public class SourceTypeModel
        {
            public SourceTypeModel()
            { }


            //[PrimaryKey]
            [JsonProperty]
            public string Name { get; set; }
            [JsonProperty]
            public List<string> SourcePara { get; set; } = new List<string>();
            [JsonProperty]
            public List<string> DevicePara { get; set; } = new List<string>();
            /// <summary>
            /// In case true: Server will send request data every interval time.
            /// </summary>
            [JsonProperty]
            public bool IsRequestServer { get; set; }
            /// <summary>
            /// In case true: When Device is changed by user or program, it will automatically update data at the out side.
            /// </summary>
            [JsonProperty]
            public bool IsUpdateOutsideDevice { get; set; }
            [JsonProperty]
            public int LastUpdate { get; set; }

        }

        [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
        public class AttributeModel
        {
            //[PrimaryKey]
            [JsonProperty]
            public string Name { get; set; }
            [JsonProperty]
            public List<string> ValueRange { get; set; } = new List<string>();
            [JsonProperty]
            public string Unit { get; set; }
            [JsonProperty]
            public SERVER_TYPES ValueType { get; set; }
            [JsonProperty]
            public BacnetObjectTypes BacnetType { get; set; }
            [JsonProperty]
            public bool IsReadOnly { get; set; } = false;
            [JsonProperty]
            public int LastUpdate { get; set; }
            public AttributeModel()
            {
            }
        }
        [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
        public class CapabilitieModel
        {
            //[PrimaryKey]
            [JsonProperty]
            public string Name { get; set; }
            [JsonProperty]
            public List<string> Attributes { get; set; } = new List<string>();
            [JsonProperty]
            public string DefaultDeviceType { get; set; }
            /// <summary>
            /// Mapping "AttributeName:StandardName"
            /// </summary>
            [JsonProperty]
            public List<string> DeviceTypeMapping { get; set; } = new List<string>(); // if the attribute name is different with standard, it should to mapping
            [JsonProperty]
            public List<string> DataSaveList { get; set; } = new List<string>();
            [JsonProperty]
            public List<string> MeterSaveList { get; set; } = new List<string>();
            [JsonProperty]
            public List<string> Paras { get; set; } = new List<string>();
            [JsonProperty]
            public Dictionary<string, string> Descriptions { get; set; } = new Dictionary<string, string>();
            [JsonProperty]
            public int LastUpdate { get; set; }
        }

        /// <summary>
        /// This is standard properties of Device Type
        /// </summary>
        [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
        public class DeviceTypeModel
        {
            //[PrimaryKey]
            [JsonProperty]
            public string Name { get; set; }
            [JsonProperty]
            public List<string> Attributes { get; set; } = new List<string>();
            [JsonProperty]
            public int LastUpdate { get; set; }
        }
    }
}
