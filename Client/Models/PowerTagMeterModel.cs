using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{
    public class MeterInstantData
    {
        public int currentA { get; set; }
        public double currentB { get; set; }
        public double currentC { get; set; }
        public double voltageAB { get; set; }
        public double voltageBC { get; set; }
        public double voltageCA { get; set; }
        public double voltageAN { get; set; }
        public double voltageBN { get; set; }
        public double voltageCN { get; set; }
        public int powerA { get; set; }
        public double powerB { get; set; }
        public double powerC { get; set; }
        public int powerTActive { get; set; }
        public double powerTReactive { get; set; }
        public int powerTApparent { get; set; }
        public double powerFactorT { get; set; }
        public double frequency { get; set; }
        public int slaveId { get; set; }
        public int channel { get; set; }
        public double neutralCurrent { get; set; }
        public double powerPhAReactive { get; set; }
        public double powerPhBReactive { get; set; }
        public double powerPhCReactive { get; set; }
        public double powerPhAApparent { get; set; }
        public double powerPhBApparent { get; set; }
        public double powerPhCApparent { get; set; }
        public double powerFactorA { get; set; }
        public double powerFactorB { get; set; }
        public double powerFactorC { get; set; }
    }
    public class PowerTagMeterModel
    {
        public List<MeterInstantData> MeterInstantData { get; set; }

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
