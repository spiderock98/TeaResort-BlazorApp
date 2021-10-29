using SmartRetail.Share.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartRetail.Services
{
    public class MasterDataService
    {
        HttpClient _client = new HttpClient();
        public List<MasterDataModel> MasterDataList { get; set; }
        public List<MasterDataModel.DeviceTypeModel> DeviceTypeList { get; set; }
        public List<MasterDataModel.AttributeModel> AttributeList { get; set; }
        public MasterDataService() { }
        public async Task<List<MasterDataModel>> GetMasterDataListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_DEVICES_MASTER_DATA(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MasterDataList = JsonConvert.DeserializeObject<List<MasterDataModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(MasterDataList);
        }

        public async Task<List<MasterDataModel.DeviceTypeModel>> GetDeviceTypeListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_DEVICES_TYPE_DATA(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    DeviceTypeList = JsonConvert.DeserializeObject<List<MasterDataModel.DeviceTypeModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(DeviceTypeList);
        }
        public async Task<List<MasterDataModel.AttributeModel>> GetAttributeListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_ATTRIBUTE_DATA(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    AttributeList = JsonConvert.DeserializeObject<List<MasterDataModel.AttributeModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(AttributeList);
        }
    }

}