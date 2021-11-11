using SmartRetail.Share.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartRetail.Client.Services
{
    public class DataLogService
    {
        HttpClient _client = new HttpClient();
        public DataLogService()
        {

        }

        public async Task<List<DataLogModel>> GetAllDataAsync(string token, int fromTime, int toTime)
        {
            List<DataLogModel> dataList = new List<DataLogModel>();
            var uri = new Uri(Resources.GetLink.GET_ALL_DATA_LOG(token, fromTime, toTime));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<DataLogModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(dataList);
        }

        public async Task<List<DataLogModel>> GetDataAsync(string token, int deviceId, int fromTime, int toTime)
        {
            List<DataLogModel> dataList = new List<DataLogModel>();
            var uri = new Uri(Resources.GetLink.GET_DATA_LOG(token, deviceId, fromTime, toTime));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<DataLogModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(dataList);
        }


    }
}
