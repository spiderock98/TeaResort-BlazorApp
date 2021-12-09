using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmartRetail.Client.Models;
using Newtonsoft.Json;

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
            }
            catch (Exception ex)
            {

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

    }
}