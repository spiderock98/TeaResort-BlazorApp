using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmartRetail.Client.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace SmartRetail.Client.Services
{
    public class ScenesService
    {
        HttpClient _client = new HttpClient();
        public ScenesService()
        { }

        public async Task<List<GlobalSceneModel>> GetScenesListAsync(string token)
        {

            List<GlobalSceneModel> scenesItems = new List<GlobalSceneModel>();
            var uri = new Uri(Resources.GetLink.GET_USER_SCENES_2(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    scenesItems = JsonConvert.DeserializeObject<List<GlobalSceneModel>>(content);
                }
                //Console.WriteLine("debug 1");
                //Console.WriteLine(JsonConvert.SerializeObject(scenesItems));
                //Console.WriteLine("==================");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetScenesListAsync ==================");
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(scenesItems);
        }

        public async Task<string> RunScenesAsync(int scenesId, string token)
        {
            var uri = new Uri(Resources.GetLink.GET_RUN_SCENES(scenesId, token));
            string content = "";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(content);
        }

        // public async void UpdateScenesAsync(GlobalSceneModel item, string token)
        // {
        //     var uri = new Uri(Resources.GetLink.POST_UPDATE_SCENES(token));

        //     var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        //     try
        //     {
        //         HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

        //         if (response.IsSuccessStatusCode)
        //         {
        //             var content = await response.Content.ReadAsStringAsync();
        //         }
        //     }
        //     catch { }
        // }

        public async Task<bool> UpdateItemAsync(GlobalSceneModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_UPDATE_SCENES(token));

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

        public async Task<bool> InsertItemAsync(GlobalSceneModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_INSERT_SCENES(token));

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

        public async Task<bool> DeleteItemAsync(int id, string token)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_SCENES(id, token);
                var uri = new Uri(RequestUrl);
                HttpResponseMessage response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<InterlockDS4Model> GetDS4InterlockAsyncBySrc(string srcId, string token, string cmd, int id)
        {
            var svData = new DataService();
            var ip = await svData.GetDataValue(token, "DS4Gateway", srcId);

            return await GetDS4InterlockAsync(cmd, id, ip);
        }
        public async Task<InterlockDS4Model> GetDS4InterlockAsync(string cmd, int id, string ds4_ip)
        {
            var objPayload = new { cmd = cmd, data = new { id = id } };
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(objPayload), Encoding.UTF8, "application/json");
            var resultItem = new InterlockDS4Model();
            var uri = new Uri(Resources.GetLink.GET_DS4_INTERLOCK(ds4_ip));
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resultItem = JsonConvert.DeserializeObject<InterlockDS4Model>(content);
                }
            }
            catch (Exception ex)
            { }
            return await Task.FromResult(resultItem);
        }

        public async Task<bool> UpdateDS4InterlockAsyncBySrc(string token, string srcId, InterlockDS4Model item)
        {
            var svData = new DataService();
            var ip = await svData.GetDataValue(token, "DS4Gateway", srcId);
            return await UpdateDS4InterlockAsync(item, ip);
        }
        public async Task<bool> UpdateDS4InterlockAsync(InterlockDS4Model item, string ds4_ip)
        {
            var uri = new Uri(Resources.GetLink.POST_DS4_INTERLOCK(ds4_ip));
            item.cmd = "setInterlock";
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
    }
}