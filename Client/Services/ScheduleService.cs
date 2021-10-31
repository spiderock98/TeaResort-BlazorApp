using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmartRetail.Share.Models;
using Newtonsoft.Json;

namespace SmartRetail.Services
{
    public class ScheduleService
    {
        HttpClient _client = new HttpClient();
        public List<GlobalScheduleModel> scheduleList = new List<GlobalScheduleModel>();
        public ScheduleService()
        { }

        public async Task<bool> UpdateItemAsync(GlobalScheduleModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_UPDATE_SCHEDULE(token));

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

        public async Task<string> GetDebugAsync(int scheduleId, string token)
        {
            var uri = new Uri(Resources.GetLink.GET_SCHEDULE_DEBUG(scheduleId, token));

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return await Task.FromResult(content);
                }
            }
            catch { }
            return await Task.FromResult("");

        }

        public async Task<int> InsertItemAsync(GlobalScheduleModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_INSERT_SCHEDULE(token));

            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    int newDeviceId = int.Parse(content);
                    return await Task<int>.FromResult(newDeviceId);
                }
            }
            catch { }
            return await Task.FromResult(-1);
        }

        public async Task<bool> DeleteScheduleAsync(int scheduleId, string token)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_SCHEDULE(scheduleId, token);
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

        public List<GlobalScheduleModel> GetLocalSchedule()
        {
            return scheduleList;
        }

        public async Task<GlobalScheduleModel> GetScheduleAsync(int scheduleId, string token)
        {
            var uri = new Uri(Resources.GetLink.GET_SCHEDULE(scheduleId, token));
            GlobalScheduleModel _schedule = new GlobalScheduleModel();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _schedule = JsonConvert.DeserializeObject<GlobalScheduleModel>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(_schedule);
        }

        public async Task<List<GlobalScheduleModel>> GetItemsAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_ALL_SCHEDULE(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    scheduleList = JsonConvert.DeserializeObject<List<GlobalScheduleModel>>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(scheduleList);
        }

        public async Task<string> GetRunScheduleAsync(string token, int scheduleId)
        {
            string returnValue = "";
            var uri = new Uri(Resources.GetLink.GET_RUN_SCHEDULE(scheduleId, token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = content.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(returnValue);
        }
    }
}
