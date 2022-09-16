
using System.Collections.Generic;

namespace SmartRetail.Resources
{
    public static class GetLink
    {
        //public static string  SERVER_IP = "http://103.1.239.33:5000";
        //public static string SERVER_IP = "http://10.0.2.2:5000";
        //public static string SERVER_IP = "localhost:5000";
        //public static string SERVER_IP = "http://viot.ddns.net:5000";
        //public static string SERVER_IP = "kst-fibaro.ddns.net:5000";
        //public static string SERVER_IP = "45.251.112.69:1001";
        //public static string SERVER_IP = "192.168.99.198:5000";
        //public static string SERVER_POWERTAG = "http://45.251.112.69:5004/192.168.99.116";

        //public static string SERVER_IP = "45.251.112.69:9003";
        public static string SERVER_IP = "192.168.1.209:5000";
        //public static string SERVER_IP = "112.213.87.154:1001";
        public static string CLIENT_SERVER = "";

        // todo: hardcode here
        //public static Dictionary<string, string> DictDS4IP = new Dictionary<string, string>()
        //{
        //    {"DS4-54CBE7AB6224", "45.251.112.69:5002/10.97.17.247"},
        //    {"DS4-EC448312CFA4", "45.251.112.69:5002/10.97.17.246"},
        //    {"DS4-38468312CFA4", "45.251.112.69:5002/10.97.17.248"},
        //    {"DS4-0C7EE9AB6224_office", "45.251.112.69:5004/192.168.99.210"},
        //    {"DS4-0C7EE9AB6224", "45.251.112.69:5004/192.168.99.210"},
        //    {"DS4-B4CCE7AB6224", "45.251.112.69:5003/10.10.8.3"},
        //    {"DS4-38D420C4F5FC", "45.251.112.69:5003/10.10.8.10"},
        //    {"DS4-507EE9AB6224", "45.251.112.69:5012/192.168.10.200"},
        //    {"DS4-C8089EC40A24", "172.27.18.234:8080/172.27.18.248"},
        //    {"DS4-F0448312CFA4", "172.27.18.234:8080/172.27.18.249"},
        //    {"DS4-38458312CFA4", "172.27.18.234:8080/172.27.18.250"},
        //    {"DS4-F0458312CFA4", "172.27.18.234:8080/172.27.18.252"},
        //    {"DS4-D4448312CFA4", "172.27.18.234:8080/172.27.18.253"},
        //    {"DS4-34458312CFA4", "172.27.18.234:8080/172.27.18.222"},
        //    {"DS4-300A9EC40A24", "172.27.18.234:8080/172.27.18.223"},
        //    {"DS4-240A9EC40A24", "172.27.18.234:8080/172.27.18.235"},
        //    {"DS4-6C458312CFA4", "172.27.18.234:8080/172.27.18.238"},
        //    {"DS4-98458312CFA4", "172.27.18.234:8080/172.27.18.247"},
        //    {"DS4-B4CE12A4AE30", "172.27.18.234:8080/172.27.18.236"},
        //    {"DS4-A80A9EC40A24", "172.27.18.234:8080/172.27.18.242"},
        //    {"DS4-C8448312CFA4", "172.27.18.234:8080/172.27.18.245"},
        //};

        public static string GET_LOG(string token)
        {
            var url = "http://" + SERVER_IP + "/api/datalog?token=" + token + "&fromTime=1&toTime=15976097926&deviceId=1";
            return url;
        }
        public static string GET_LOG_BY_TIME(string token, int fromUnix, int toUnix)
        {
            var url = "http://" + SERVER_IP + "/api/datalog?token=" + token + "&fromTime=" + fromUnix.ToString() + "&toTime=" + toUnix.ToString() + "&deviceId=1";
            return url;
        }
        public static string POST_ADD_LOG(string token, string deviceId, string attribute, string oldValue, string newValue)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datalog/add?token=" + token + "&deviceId=" + deviceId + "&attribute=" + attribute + "&oldValue=" + oldValue + "&newValue=" + newValue;
            return RequestUrl;
        }

        public static string GET_DATA_BY_KEY_FILTER(string token, string classify, string filter)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/filter?token=" + token + "&Classify=" + classify + "&values=" + filter;
            return RequestUrl;
        }
        public static string GET_DATA_BY_CLASSIFY_TIME(string token, string classify, int fromUnix, int toUnix)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data?token=" + token + "&Classify=" + classify +
                                "&fromUnix=" + fromUnix.ToString() + "&toUnix=" + toUnix.ToString();
            return RequestUrl;
        }
        public static string GET_DEVICES_MASTER_DATA(string token)
        {
            var url = "http://" + SERVER_IP + "/api/Masterdata/Masterdata?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string GET_DEVICES_TYPE_DATA(string token)
        {
            var url = "http://" + SERVER_IP + "/api/Masterdata/AllDeviceType?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string GET_ATTRIBUTE_DATA(string token)
        {
            var url = "http://" + SERVER_IP + "/api/Masterdata/AllAttribute?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        /// <summary>
        /// Link: Get Devices Belon To User.
        /// </summary>
        /// <param name="token">Token Security of User</param>
        /// <returns></returns>
        public static string GET_DEVICES_LIST_USER(string token)
        {
            var url = "http://" + SERVER_IP + "/api/user/Devices?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_OWNER_DEVICES_LIST_USER(string token)
        {
            var url = "http://" + SERVER_IP + "/api/user/OwnerDevices?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string GET_USER_INFO(string token)
        {
            var url = "http://" + SERVER_IP + "/api/user/infor/" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_USER_ALL_INFO(string token)
        {
            var url = "http://" + SERVER_IP + "/api/user/all/" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "?ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_CHECK_CONFIRM_EMAIL(string user, string email)
        {
            var url = "http://" + SERVER_IP + "/api/user/CheckEmailConfirm?User=" + user + "&to=" + email;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_RESET_USER(string User)
        {
            var url = "http://" + SERVER_IP + "/api/user/ResetPassword?User=" + User;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_CHANGE_PASSWORD(string User, string oldPassword, string newPassword)
        {
            var url = "http://" + SERVER_IP + "/api/user/ChangePassword?User=" + User + "&Password=" + oldPassword + "&newPassword=" + newPassword;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_USER_INFO(string token, string key)
        {
            var url = "http://" + SERVER_IP + "/api/user/all/" + token + "?key=" + key;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_USER_LIST(string token)
        {
            var url = "http://" + SERVER_IP + "/api/user?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string GET_ADMIN_RESET_PASSWORD(string user, string newPassword, string token)
        {
            var url = "http://" + SERVER_IP + "/api/user/AdminResetPassword?user=" + user + "&newPassword=" + newPassword + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_CREATE_NEW_ACCOUNT()
        {
            var url = "http://" + SERVER_IP + "/api/user/NewAccount";

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_ROLE(string token)
        {
            var url = "http://" + SERVER_IP + "/api/user/role?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string POST_UPDATE_ROLE(string user, string token)
        {
            var url = "http://" + SERVER_IP + "/api/user/UpdateRole?token=" + token + "&user=" + user;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_LOGIN(string user, string password)
        {
            var url = "http://" + SERVER_IP + "/api/user/login?user=" + user + "&Password=" + password;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_NEW_USER()
        {
            var url = "http://" + SERVER_IP + "/api/user/NewAccount";

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_DEVICE(int dẹviceId, string token)
        {
            var url = "http://" + SERVER_IP + "/api/User/IotDevice/" + dẹviceId.ToString() + "?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string PUT_DEVICE_SETTING(int deviceId, string token)
        {
            var url = "http://" + SERVER_IP + "/api/IotDevice/" + deviceId.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string PUT_MUL_DEVICE_SETTING(string token)
        {
            var url = "http://" + SERVER_IP + "/api/IotDevice?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_DEVICE_UPDATE(int deviceId, string deviceType, string dataSource, string deviceName, int roomId, string token)
        {
            var url = "http://" + SERVER_IP + "/api/IotDevice/Update?deviceId=" + deviceId.ToString() + "&deviceType=" + deviceType + "&dataSource="
                    + dataSource + "&deviceName=" + deviceName + "&roomId=" + roomId.ToString() + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string DELETE_DEVICE(int deviceId, string token)
        {
            var url = "http://" + SERVER_IP + "/api/IotDevice/" + deviceId.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_CREATE_NEW_DEVICE(string dataSource, string deviceName, string capabilitie, int? roomId, string token)
        {
            var url = "http://" + SERVER_IP + "/api/iotdevice/add?token=" + token + "&source=" + dataSource + "&capabilitie=" + capabilitie + "&deviceName=" + deviceName;
            if (roomId != null)
                url += "&roomId=" + roomId.Value.ToString();

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_UPDATE_DEVICE(string token)
        {
            var url = "http://" + SERVER_IP + "/api/IotDevice/UpdateAll?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_DS4_INTERLOCK(string DS4_IP)
        {
            return "http://" + DS4_IP + "/interlock";
        }
        public static string POST_DS4_INTERLOCK(string DS4_IP)
        {
            return "http://" + DS4_IP + "/interlock";
        }

        public static string POST_DS4_TEMP_LIMIT(string DS4_IP)
        {
            return "http://" + DS4_IP + "/templimit";
        }
        public static string GET_DS4_TEMP_LIMIT(string DS4_IP)
        {
            return "http://" + DS4_IP + "/templimit";
        }
        public static string GET_DS4_INPUT(string DS4_IP)
        {
            return "http://" + DS4_IP + "/input";
        }

        //public static string GET_POWERTAG_METERINSTANCE()
        //{
        //    return SERVER_POWERTAG + "/rsa1/MeterInstantData";
        //}

        public static string GET_USER_SCENES(string token)
        {
            var url = "http://" + SERVER_IP + "/api/User/Scenes?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_USER_SCENES_2(string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalScenes?Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_RUN_SCENES(int scenesId, string token)
        {
            var url = "http://" + SERVER_IP + "/api/User/RunScene?Id=" + scenesId.ToString() + "&Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_RUN_SCHEDULE(int scenesId, string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalSchedule/Run?Id=" + scenesId.ToString() + "&Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_ALL_SCHEDULE(string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalSchedule?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_SCHEDULE(int Id, string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalSchedule/" + Id.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string GET_SCHEDULE_DEBUG(int Id, string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalSchedule/debug?id=" + Id.ToString() + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }


        public static string POST_UPDATE_SCHEDULE(string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalSchedule/Update?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_INSERT_SCENES(string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalScenes/Insert?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }
        public static string POST_UPDATE_SCENES(string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalScenes/Update?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_INSERT_SCHEDULE(string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalSchedule/Insert?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string DELETE_SCHEDULE(int id, string token)
        {
            var url = "http://" + SERVER_IP + "/api/GlobalSchedule/" + id.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                url += "&ClientServer=" + CLIENT_SERVER;
            return url;
        }

        public static string POST_USER_DEVICE_NAME(string token, string GateWay, string ChildId, string Name)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/user/AddUserChild?Id=" + GateWay + "&ChildId=" + ChildId + "&token=" + token + "&DeviceName=" + Name;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string POST_ADD_USER_GATEWAY(string _GatewayId, string _ActiveCode, string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/User/AddGateway?Id=" + _GatewayId + "&ActiveCode=" + _ActiveCode + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string DELETE_USER_GATEWAY(string _GatewayId, string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/User/UserGateway?Id=" + _GatewayId + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string POST_USER_UPDATE(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/user/UpdateAccount?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string POST_USER_INFO_UPDATE(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/user/UpdateAccountInfo?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string DELETE_SCENES(int id, string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/GlobalScenes/" + id.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string DELETE_USER_DEVICE_NAME(string token, string GateWay, string ChildId)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/user/UserDevice?Id=" + GateWay + "&ChildId=" + ChildId + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string GET_SUMMARY_ENERGY_DATA(string token, int deviceId, int fromDate, int lastDate)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/EnergyData/summary?deviceId=" + deviceId.ToString() + "&token=" + token + "&fromDate=" + fromDate + "&LastDate=" + lastDate;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_ENERGY_DATA(string token, int deviceId, int fromDate, int lastDate)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/EnergyData/data?deviceId=" + deviceId.ToString() + "&token=" + token + "&fromDate=" + fromDate + "&LastDate=" + lastDate;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string GET_ALL_DATA_LOG(string token, int fromTime, int toTime)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datalog/all?fromTime=" + fromTime.ToString() + "&toTime=" + toTime.ToString() + "&Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_DATA_LOG(string token, int fromTime, int deviceId, int toTime)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datalog?fromTime=" + fromTime.ToString() + "&deviceId=" + deviceId.ToString() + "&toTime=" + toTime.ToString() + "&Token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_DATA_DEVICES_LOG(string token, string deviceIds, int fromTime, int toTime)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datalog?fromTime=" + fromTime.ToString() + "&deviceIds=" + deviceIds + "&toTime=" + toTime.ToString() + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_SECTION_LIST(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/section/All?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string PUT_UPDATE_SECTION(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/section/Update?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string POST_INSERT_SECTION(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/section/Add?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string DELETE_SECTION(int Id, string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/section/" + Id.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string GET_ZONE_LIST(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/zone/All?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string PUT_UPDATE_ZONE(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/zone/Update?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string POST_INSERT_ZONE(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/zone/Add?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string DELETE_ZONE(int Id, string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/zone/" + Id.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_AREA_LIST(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/area/All?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string DELETE_AREA(int Id, string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/area/" + Id.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string POST_INSERT_AREA(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/area/Add?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string PUT_UPDATE_AREA(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/area/Update?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_ALL_SOURCE(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datasource?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_SOURCE(string token, string sourceId)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datasource/" + sourceId + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string POST_UPDATE_SOURCE(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datasource/Update?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string POST_INSERT_SOURCE(string token, string sourceType, string sourceName)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datasource/Build?sourceType=" + sourceType + "&sourceName=" + sourceName + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string DELETE_SOURCE(string Id, string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/datasource/" + Id.ToString() + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_ALL_DATA(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/all?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_DATA_BY_CLASSIFY(string token, string classify)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data?token=" + token + "&Classify=" + classify;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string GET_DATA_BY_KEY(string token, string classify, string key)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/key?token=" + token + "&Classify=" + classify + "&key=" + key;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string GET_DATA_BY_KEY_VALUE(string token, string classify, string key)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/value?token=" + token + "&Classify=" + classify + "&Key=" + key;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string GET_DATA_BY_ID(string token, string id)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/" + id + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string POST_ADD_DATA(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string POST_REPLACE_DATA_BY_KEY(string token, string classify, string key)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/replace?token=" + token + "&classify=" + classify + "&key=" + key;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string POST_REPLACES_DATA_BY_KEY(string token)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/replaces?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }


        public static string PUT_DATA_BY_ID(string token, string id)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/" + id + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string PUT_DATA_BY_KEY(string token, string classify, string key)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/key?token=" + token + "&classify=" + classify + "&key=" + key;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string DELETE_DATA_BY_ID(string token, string id)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/" + id + "?token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
        public static string DELETE_DATA_BY_KEY(string token, string classify, string key)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/data/key?token=" + token + "&classify=" + classify + "&key=" + key;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }

        public static string POST_IMAGE_DATA(string token, string fileName)
        {
            string RequestUrl = "http://" + SERVER_IP + "/api/Images?FileName=" + fileName + "&token=" + token;

            if (CLIENT_SERVER.Trim() != "")
                RequestUrl += "&ClientServer=" + CLIENT_SERVER;
            return RequestUrl;
        }
    }
}
