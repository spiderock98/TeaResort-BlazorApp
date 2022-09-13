using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{

    [JsonObject(MemberSerialization.OptIn)]
    public class SectionModel
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int Zone { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public string Icon { get; set; }
        [JsonProperty]
        public int LastUpdate { get; set; }
        [JsonProperty]
        public Dictionary<string, string> Infos { get; set; }

        public SectionModel()
        { }

        public void Update(SectionModel _room)
        {
            Id = _room.Id;
            Name = _room.Name;
            Zone = _room.Zone;
            Description = _room.Description;
            Icon = _room.Icon;
            Infos = _room.Infos;
        }

        public SectionModel ShallowCopy()
        {
            return (SectionModel)this.MemberwiseClone();
        }
        public SectionModel DeepCopy()
        {
            var _tmpSerializeString = JsonConvert.SerializeObject(ShallowCopy());
            return JsonConvert.DeserializeObject<SectionModel>(_tmpSerializeString);
        }
    }

}
