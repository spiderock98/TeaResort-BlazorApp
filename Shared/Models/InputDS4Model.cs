using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Share.Models
{
    public class InputData
    {
        public int id { get; set; }
        public int status { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<InterlockDS4>(myJsonResponse); 
    public class InputDS4Model
    {
        public string cmd { get; set; }
        public InputData data { get; set; }

        // ! misc function and attr
        public InputDS4Model DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<InputDS4Model>(_tmpSerializeString);
        }
        public InputDS4Model ShallowCopy()
        {
            return (InputDS4Model)this.MemberwiseClone();
        }
    }
}
