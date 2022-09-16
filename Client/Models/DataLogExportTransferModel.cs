using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DataLogExportTransferModel : DataLogModel
    {
        [JsonProperty]
        public string TimeUTC { get; set; }
        [JsonProperty]
        public string DeviceName { get; set; }

        public DataLogExportTransferModel(DataLogModel item)
        {
            Attribute = item.Attribute;
            OldValue = item.OldValue;
            Value = item.Value;
        }

        public DataLogExportTransferModel ShallowCopy()
        {
            return (DataLogExportTransferModel)this.MemberwiseClone();
        }
        public DataLogExportTransferModel DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<DataLogExportTransferModel>(_tmpSerializeString);
        }

    }
}
