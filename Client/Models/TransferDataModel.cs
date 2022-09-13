using Newtonsoft.Json;
using System.Collections.Generic;


namespace SmartRetail.Client.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserInforTransfer
    {
        [JsonProperty]
        public string UserName
        {
            get; set;
        }
        [JsonProperty]
        public string Password
        {
            get; set;
        }
        [JsonProperty]
        public string Email
        {
            get; set;
        }
        [JsonProperty]
        public string Location
        {
            get; set;
        }
        [JsonProperty]
        public string Company
        {
            get; set;
        }
        [JsonProperty]
        public string PhoneNumber
        {
            get; set;
        }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class UserDataTransfer
    {
        [JsonProperty]
        public string UserName
        {
            get; set;
        }
        [JsonProperty]
        public string Password
        {
            get; set;
        }
        [JsonProperty]
        public string Email
        {
            get; set;
        }
        [JsonProperty]
        public bool IsActive
        {
            get; set;
        }
        [JsonProperty]
        public string Location
        {
            get; set;
        }
        [JsonProperty]
        public string Company
        {
            get; set;
        }
        [JsonProperty]
        public string PhoneNumber
        {
            get; set;
        }
    }

}
