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
    public class DS4InputService
    {

        HttpClient _client = new HttpClient();

        // NovaHills

        public DS4InputService()
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

        // public async Task<bool> UpdateItemAsync(InputDS4Model item, string ip)
        // {
        //     var uri = new Uri(Resources.GetLink.GET_DS4_TEMP_LIMIT(ip));
        //     item.cmd = "setTempLimit";
        //     var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        //     try
        //     {
        //         HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

        //         if (response.IsSuccessStatusCode)
        //         {
        //             var content = await response.Content.ReadAsStringAsync();
        //             return await Task.FromResult(true);
        //         }
        //     }
        //     catch { }
        //     return await Task.FromResult(false);
        // }

        public async Task<InputDS4Model> GetInputBySrc(string token, string srcId, int id)
        {
            var svData = new DataService();
            var ip = await svData.GetDataValue(token, "DS4Gateway", srcId);

            var objPayload = new InputDS4Model() { cmd = "getInput", data = new InputData() { id = id } };
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(objPayload), Encoding.UTF8, "application/json");
            var resultItem = new InputDS4Model();
            var uri = new Uri(Resources.GetLink.GET_DS4_INPUT(ip));
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resultItem = JsonConvert.DeserializeObject<InputDS4Model>(content);
                }
            }
            catch (Exception ex)
            { }
            return await Task.FromResult(resultItem);
        }
        public async Task<InputDS4Model> GetInput(string ip, int id)
        {
            // var objPayload = new { cmd = cmd, data = new { id = id } };
            var objPayload = new InputDS4Model() { cmd = "getInput", data = new InputData() { id = id } };
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(objPayload), Encoding.UTF8, "application/json");
            var resultItem = new InputDS4Model();
            var uri = new Uri(Resources.GetLink.GET_DS4_INPUT(ip));
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resultItem = JsonConvert.DeserializeObject<InputDS4Model>(content);
                }
            }
            catch (Exception ex)
            { }
            return await Task.FromResult(resultItem);
        }
    }
}