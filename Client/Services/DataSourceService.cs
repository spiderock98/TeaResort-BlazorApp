using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmartRetail.Share.Models;
using Newtonsoft.Json;
using System.Linq;

namespace SmartRetail.Client.Services
{
    public class DataSourceService
    {
        HttpClient _client = new HttpClient();
        List<DataSourceModel> DataSourceList = new List<DataSourceModel>();

        public DataSourceService() { }
        public async Task<List<DataSourceModel>> GetDataSourceListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_ALL_SOURCE(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    DataSourceList = JsonConvert.DeserializeObject<List<DataSourceModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(DataSourceList);
        }

        public async Task<bool> UpdateDataSourceItem(DataSourceModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_UPDATE_SOURCE(token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return await Task.FromResult(true);
                }
                return await Task.FromResult(false);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
    }

}