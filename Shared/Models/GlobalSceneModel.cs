using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Share.Models
{
    [JsonObject(MemberSerialization.OptIn)]

    public class GlobalSceneModel
    {

        public GlobalSceneModel()
        {

        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }

        //[PrimaryKey]
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        // Description For Scenes
        public string Name { get; set; }
        [JsonProperty]
        public bool Enable { get; set; } = true;
        [JsonProperty]
        public bool IsJavaScriptType { get; set; } = false;
        [JsonProperty]
        // Description For Scenes
        public string JavaScriptCode { get; set; } = "";
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

        public void UpdateValue(GlobalSceneModel _scenes)
        {
            this.Name = _scenes.Name;
            this.Description = _scenes.Description;
            this.Enable = _scenes.Enable;
            this.Debug = _scenes.Debug;
            this.IsJavaScriptType = _scenes.IsJavaScriptType;
            this.JavaScriptCode = _scenes.JavaScriptCode;
            this.LastRunTime = _scenes.LastRunTime;

            //-- List Update ------------------
            this.TriggerAttributes.Clear();
            this.Expressions.Clear();
            this.Actions.Clear();

            foreach (var item in _scenes.TriggerAttributes) this.TriggerAttributes.Add(item);
            for (int i = 0; i < _scenes.Expressions.Count; i++) this.Expressions.Add(_scenes.Expressions[i]);
            foreach (var item in _scenes.Actions) this.Actions.Add(item);

        }
    }

}
