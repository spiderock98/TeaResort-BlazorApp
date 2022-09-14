using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{
    public class Interlock
    {
        public int id { get; set; }
        public int stt { get; set; }
        public List<int> start { get; set; } = new List<int>();
        public List<int> stop { get; set; } = new List<int>();
    }

    public class Input
    {
        public List<int> id { get; set; } = new List<int>();
        public List<int> opt { get; set; } = new List<int>();
        public List<int> delay { get; set; } = new List<int>();
    }

    public class Control
    {
        public int status { get; set; }
        public int mode { get; set; }
        public int fan { get; set; }
        public int setpoint { get; set; }
        public int delay { get; set; }
        public int @lock { get; set; }
    }

    public class Data
    {
        public Interlock interlock { get; set; }
        public Input input { get; set; }
        public Control control { get; set; }
        public List<int> unit { get; set; } = new List<int>();
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<InterlockDS4>(myJsonResponse); 
    public class InterlockDS4Model
    {
        public string cmd { get; set; }
        public Data data { get; set; }

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
