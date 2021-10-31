using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmartRetail.Share.Models;
using Newtonsoft.Json;
using System.Linq;

namespace SmartRetail.Services
{
    public class DeviceDataStore
    {
        List<DeviceModel> deviceItems;
        public MasterDataModel MasterData { get; set; }
        HttpClient _client = new HttpClient();

        public DeviceDataStore()
        {
            _client.Timeout = TimeSpan.FromSeconds(5);
        }
        public List<DeviceModel> GetLocalDeviceList()
        {
            return deviceItems;
        }
        public async void GetMasterDataAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_DEVICES_MASTER_DATA(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MasterData = JsonConvert.DeserializeObject<MasterDataModel>(content);
                }
            }
            catch (Exception ex)
            { }
        }

        // Lấy toàn bộ danh sách thiết bị đang sử dụng của User. 
        public async Task<List<DeviceModel>> GetDevicesListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_DEVICES_LIST_USER(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    deviceItems = JsonConvert.DeserializeObject<List<DeviceModel>>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(deviceItems);
        }

        // Lấy toàn bộ danh sách thiết bị thuộc quản lý của User.
        public async Task<List<DeviceModel>> GetOwnerDevicesListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_OWNER_DEVICES_LIST_USER(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    deviceItems = JsonConvert.DeserializeObject<List<DeviceModel>>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(deviceItems);
        }

        public async Task<DeviceModel> GetDeviceAsync(int deviceId, string token)
        {
            var uri = new Uri(Resources.GetLink.GET_DEVICE(deviceId, token));
            DeviceModel _device = new DeviceModel();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _device = JsonConvert.DeserializeObject<DeviceModel>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(_device);
        }

        public async Task<bool> AddItemAsync(string dataSource, string deviceName, string capabilitie, Dictionary<string, string> datas, int? roomId, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_CREATE_NEW_DEVICE(dataSource, deviceName, capabilitie, roomId, token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(datas), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
            }
            catch { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(int deviceId, string deviceType, string dataSource, string deviceName, int roomId, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_DEVICE_UPDATE(deviceId, deviceType, dataSource, deviceName, roomId, token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
            }
            catch { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateDeviceStatusAsync(int dvId, Dictionary<string, string> data, string token)
        {
            var uri = new Uri(Resources.GetLink.PUT_DEVICE_SETTING(dvId, token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PutAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
            }
            catch { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateGroupDeviceStatusBySectionAsync(int sectionId, Dictionary<string, string> data, string token)
        {
            var LstDevice = await GetDevicesListAsync(token);

            var lstDvIdInSection = LstDevice.Where(r => r.SectionId == sectionId).Select(r => r.Id).ToList();
            foreach (var dvId in lstDvIdInSection)
            {
                var isSuccess = await UpdateDeviceStatusAsync(dvId, data, token);
                if (!isSuccess) return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateGroupDeviceStatusByZoneAsync(int zoneId, Dictionary<string, string> data, string token)
        {
            var svLayout = new LayoutAreaSevice();
            var LstSection = await svLayout.GetSectionListAsync(token);

            var lstSectionIdOfZone = LstSection.Where(r => r.Zone == zoneId).Select(r => r.Id).ToList();
            foreach (var secId in lstSectionIdOfZone)
            {
                var isSuccess = await UpdateGroupDeviceStatusBySectionAsync(secId, data, token);
                if (!isSuccess) return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateGroupDeviceStatusByAreaAsync(int areaId, Dictionary<string, string> data, string token)
        {
            var svLayout = new LayoutAreaSevice();
            var LstZone = await svLayout.GetZoneListAsync(token);

            var lstZoneIdOfZone = LstZone.Where(r => r.Area == areaId).Select(r => r.Id).ToList();
            foreach (var zoneId in lstZoneIdOfZone)
            {
                var isSuccess = await UpdateGroupDeviceStatusByZoneAsync(zoneId, data, token);
                if (!isSuccess) return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        //public async Task<bool> UpdateStatusAsync(List<DeviceParaListTransfer> datas, string token)
        //{
        //    var uri = new Uri(Resources.GetLink.PUT_MUL_DEVICE_SETTING(token));
        //    var _JsonValue = new StringContent(JsonConvert.SerializeObject(datas), Encoding.UTF8, "application/json");
        //    try
        //    {
        //        HttpResponseMessage response = await _client.PutAsync(uri, _JsonValue);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    catch { return await Task.FromResult(false); }
        //    return await Task.FromResult(true);
        //}
    }
}