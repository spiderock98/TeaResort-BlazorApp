using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Share.Models
{

    public class DataTemperature
    {
        public int high { get; set; }
        public int low { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<InterlockDS4>(myJsonResponse); 
    public class TemperatureDS4Model
    {
        public string cmd { get; set; }
        public DataTemperature data { get; set; } = new DataTemperature();

        // ! misc function and attr
        public GlobalSceneModel DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<GlobalSceneModel>(_tmpSerializeString);
        }
        public GlobalSceneModel ShallowCopy()
        {
            return (GlobalSceneModel)this.MemberwiseClone();
        }
    }
}
