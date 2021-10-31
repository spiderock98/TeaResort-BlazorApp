using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartRetail.Share.Models.MasterDataModel;

namespace SmartRetail.Share.Models
{
    public enum LogicExpression { Or, And, Not, OpenpArenthesis, CloseParenthesis };
    public class ScenesExpressionModel
    {
        public bool IsLogicExpression { get; set; }
        public LogicExpression Logic { get; set; }
        public int DeviceId { get; set; }
        public string Attribute { get; set; }
        public CompareOperators Comparison { get; set; }
        public string CompareValue { get; set; }
        /// <summary>
        /// Milisecond.
        /// </summary>
        public int Interval { get; set; }
    }
}
