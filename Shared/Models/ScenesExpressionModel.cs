using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartRetail.Share.Models.MasterDataModel;

namespace SmartRetail.Share.Models
{
    public class ScenesExpressionModel
    {
        public int DeviceId;
        public string Attribute;
        public CompareOperators Copareation;
        public string CompareValue;
        /// <summary>
        /// Milisecond.
        /// </summary>
        public int Interval;
    }
}
