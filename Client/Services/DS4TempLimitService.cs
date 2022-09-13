using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmartRetail.Share.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace SmartRetail.Client.Services
{
    public class DS4TempLimitService
    {

        HttpClient _client = new HttpClient();
        
        // NovaHills

        public DS4TempLimitService()
        {
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        // public async Task<List<GlobalSceneModel>> GetScenesListAsync(string token)
        // {
        //     List<GlobalSceneModel> scenesItems = new List<GlobalSceneModel>();
        //     var uri = new Uri(Resources.GetLink.GET_USER_SCENES_2(token));
        //     try
        //     {
        //         HttpResponseMessage response = await _client.GetAsync(uri);
        //         if (response.IsSuccessStatusCode)
        //         {
        //             var content = await response.Content.ReadAsStringAsync();
        //             scenesItems = JsonConvert.DeserializeObject<List<GlobalSceneModel>>(content);
        //         }
        //     }
        //     catch (Exception ex)
        //     {

        //     }
        //     return await Task.FromResult(scenesItems);
        // }

        public async Task<bool> UpdateItemAsyncBySrc(TemperatureDS4Model item, string token,string srcId)
        {
            var svData = new DataService();
            var ip = await svData.GetDataValue(token, "DS4Gateway", srcId);

            return await UpdateItemAsync(item, ip);
        }
        public async Task<bool> UpdateItemAsync(TemperatureDS4Model item, string ip)
        {
            var uri = new Uri(Resources.GetLink.GET_DS4_TEMP_LIMIT(ip));
            item.cmd = "setTempLimit";
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return await Task.FromResult(true);
                }
            }
            catch { }
            return await Task.FromResult(false);
        }

        public async Task<TemperatureDS4Model> GetTmpLimitBySrc(string token, string srcId)
        {
            var svData = new DataService();
            var ip = await svData.GetDataValue(token, "DS4Gateway", srcId);

            return await GetTmpLimit(ip);
        }
        public async Task<TemperatureDS4Model> GetTmpLimit(string ip)
        {
            // var objPayload = new { cmd = cmd, data = new { id = id } };
            var objPayload = new TemperatureDS4Model() { cmd = "getTempLimit" };
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(objPayload), Encoding.UTF8, "application/json");
            var resultItem = new TemperatureDS4Model();
            var uri = new Uri(Resources.GetLink.GET_DS4_TEMP_LIMIT(ip));
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resultItem = JsonConvert.DeserializeObject<TemperatureDS4Model>(content);
                }
            }
            catch (Exception ex)
            { }
            return await Task.FromResult(resultItem);
        }
    }
}