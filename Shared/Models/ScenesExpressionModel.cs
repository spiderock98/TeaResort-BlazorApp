using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartRetail.Share.Models.MasterDataModel;
using Newtonsoft.Json;

namespace SmartRetail.Share.Models
{
    public enum LogicExpression { Or, And, Not, OpenpArenthesis, CloseParenthesis, None };
    public class ScenesExpressionModel
    {
        [JsonProperty]
        public bool IsLogicExpression { get; set; }
        [JsonProperty]
        public LogicExpression Logic { get; set; }
        [JsonProperty]
        public int DeviceId { get; set; }
        [JsonProperty]
        public string Attribute { get; set; }
        [JsonProperty]
        public CompareOperators Comparison { get; set; }
        [JsonProperty]
        public string CompareValue { get; set; }
        /// <summary>
        /// Milisecond.
        /// </summary>
        [JsonProperty]
        public int Interval { get; set; }

        [JsonIgnore]
        public string RandExpId { get; set; } = Guid.NewGuid().ToString("N");

    }
}
