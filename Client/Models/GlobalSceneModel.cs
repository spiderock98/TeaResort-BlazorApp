using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{
    public class GlobalSceneModel
    {
        public GlobalSceneModel()
        { }

        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        // Description For Scenes
        public string Name { get; set; }
        [JsonProperty]
        public bool Enable { get; set; }
        [JsonProperty]
        public bool IsJavaScriptType { get; set; }
        [JsonProperty]
        // Description For Scenes
        public string JavaScriptCode { get; set; }
        [JsonProperty]
        //  Các Biến Trigger
        public List<TriggerAttributesModel> TriggerAttributes { get; set; } = new List<TriggerAttributesModel>(); // Format: [Id]
        [JsonProperty]
        // Biểu Thức Thực Thi Logics
        public List<ScenesExpressionModel> Expressions { get; set; } = new List<ScenesExpressionModel>();
        // With Id Parameter It Should Follow Struture: [@Id:Attribute:TimeInterval:CompareOperators:Value] With TimeInterval In Milliseconds.
        [JsonProperty]
        // Do Action
        public List<RunningActionModel> Actions { get; set; } = new List<RunningActionModel>();

        [JsonProperty]
        public long LastRunTime { get; set; }
        [JsonProperty]
        // Save Log For Scenes Run, It's should optimaze for saving store area.
        public string Debug { get; set; }
        [JsonProperty]
        // Description For Scenes
        public string Description { get; set; }

        // ! misc function and attr

        // [JsonIgnore]
        // public List<RunningActionModel> BufferActions { get; set; } = new List<RunningActionModel>();

        public GlobalSceneModel DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<GlobalSceneModel>(_tmpSerializeString);
        }
        public GlobalSceneModel ShallowCopy()
        {
            return (GlobalSceneModel)this.MemberwiseClone();
        }

        public void createActionsByGrSetVal()
        {
            var grActionBySetVal = this.Actions.GroupBy(r => JsonConvert.SerializeObject(r.SetValues));
            var finalLstAction = new List<RunningActionModel>();
            foreach (var item in grActionBySetVal)
            {
                var objAction = item.FirstOrDefault();
                objAction.LstDeviceId = item.Select(r => r.DeviceId).ToList();
                finalLstAction.Add(objAction);
            }
            this.Actions = finalLstAction;
        }
        public void ReGenerateActionsFromGroupDvId()
        {
            var _finalLstActions = new List<RunningActionModel>();
            foreach (var _action in this.Actions)
            {
                _finalLstActions.AddRange(_action.GenActionByLstDeviceId());
            }
            this.Actions = _finalLstActions;
        }

        // ! misc function and attr
        // [JsonIgnore]
        // public List<RunningActionModel> BufferActions { get; set; } = new List<RunningActionModel>();

        public GlobalSceneModel DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<GlobalSceneModel>(_tmpSerializeString);
        }
        public GlobalSceneModel ShallowCopy()
        {
            return (GlobalSceneModel)this.MemberwiseClone();
        }

        public void createActionsByGrSetVal()
        {
            var grActionBySetVal = this.Actions.GroupBy(r => JsonConvert.SerializeObject(r.SetValues));
            var finalLstAction = new List<RunningActionModel>();
            foreach (var item in grActionBySetVal)
            {
                var objAction = item.FirstOrDefault();
                objAction.LstDeviceId = item.Select(r => r.DeviceId).ToList();
                finalLstAction.Add(objAction);
            }
            this.Actions = finalLstAction;
        }
        public void ReGenerateActionsFromGroupDvId()
        {
            var _finalLstActions = new List<RunningActionModel>();
            foreach (var _action in this.Actions)
            {
                _finalLstActions.AddRange(_action.GenActionByLstDeviceId());
            }
            this.Actions = _finalLstActions;
        }

        public async void GenerateTriggerAttributes()
        {
            this.TriggerAttributes.Clear();
            foreach (var _express in Expressions)
            {
                if (_express.IsLogicExpression) continue;

                this.TriggerAttributes.Add(new TriggerAttributesModel()
                {
                    DeviceId = _express.DeviceId,
                    Attribute = _express.Attribute
                });
            }
        }
    }
}
