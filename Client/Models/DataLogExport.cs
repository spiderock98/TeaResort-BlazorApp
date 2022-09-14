using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DataLogExport : DataLogModel
    {
        [JsonProperty]
        public DateTime TimeUTC { get; set; }
        [JsonProperty]
        public string DeviceName { get; set; }

        public DataLogExport(DataLogModel item)
        {
            Attribute = item.Attribute;
            OldValue = item.OldValue;
            Value = item.Value;
        }

        public DataLogExport ShallowCopy()
        {
            return (DataLogExport)this.MemberwiseClone();
        }
        public DataLogExport DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<DataLogExport>(_tmpSerializeString);
        }

    }
}
