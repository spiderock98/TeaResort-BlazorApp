using SmartRetail.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartRetail.Client.Services
{
    public class DataService
    {
        HttpClient _client = new HttpClient();
        public DataService()
        { }
        public async Task<List<DataLogModel>> GetDataLogAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_LOG(token));
            List<DataLogModel> dataList = new List<DataLogModel>();
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
        public async Task<List<DataLogModel>> GetDataLogAsync(string token, int fromUnixTS, int toUnixTS)
        {
            var uri = new Uri(Resources.GetLink.GET_LOG_BY_TIME(token, fromUnixTS, toUnixTS));
            List<DataLogModel> dataList = new List<DataLogModel>();
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

        public async Task<List<DataModel>> GetDataByClassifyAsync(string token, string classify)
        {
            List<DataModel> dataList = new List<DataModel>();
            var uri = new Uri(Resources.GetLink.GET_DATA_BY_CLASSIFY(token, classify));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<DataModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(dataList);
        }

        public async Task<List<DataModel>> GetDataByClassifyAsync(string token, string classify, List<string> values)
        {
            List<DataModel> dataList = new List<DataModel>();
            var filter = string.Empty;
            foreach (var item in values)
            {
                filter += item + ";";
            }
            // remove last ; sign
            if (filter.EndsWith(';'))
            {
                filter = filter.Remove(filter.Length - 1);
            }
            var uri = new Uri(Resources.GetLink.GET_DATA_BY_KEY_FILTER(token, classify, filter));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<DataModel>>(content);
                }
            }
            catch (Exception ex) { }
            return await Task.FromResult(dataList);
        }
        public async Task<List<DataModel>> GetDataByClassifyAsync(string token, string classify, int fromUnix, int toUnix)
        {
            List<DataModel> dataList = new List<DataModel>();
            var uri = new Uri(Resources.GetLink.GET_DATA_BY_CLASSIFY_TIME(token, classify, fromUnix, toUnix));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<DataModel>>(content);
                }
            }
            catch (Exception ex) { }
            return await Task.FromResult(dataList);
        }

        public async Task<string> GetDataValue(string token, string classify, string key)
        {
            var result = string.Empty;
            var uri = new Uri(Resources.GetLink.GET_DATA_BY_KEY_VALUE(token, classify, key));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(result);
        }

        public async Task<DataModel> GetDataByIdAsync(string token, string Id)
        {
            DataModel dataList = new DataModel();
            var uri = new Uri(Resources.GetLink.GET_DATA_BY_ID(token, Id));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<DataModel>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(dataList);
        }

        public async Task<DataModel> GetDataByKeyAsync(string token, string classify, string key)
        {
            DataModel dataList = new DataModel();
            var uri = new Uri(Resources.GetLink.GET_DATA_BY_KEY(token, classify, key));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<DataModel>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(dataList);
        }

        public async Task<bool> UpdateDataByIdAsync(string token, string id, string value, string description)
        {
            var uri = new Uri(Resources.GetLink.PUT_DATA_BY_ID(token, id));
            var item = new DataModel();
            item.Value = value;
            item.Description = description;
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PutAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Trim() == "{\"Result\":\"OK\"}") return await Task.FromResult(true);
                    else return await Task.FromResult(false);
                }
                return await Task.FromResult(false);
            }
            catch { return await Task.FromResult(false); }
        }

        public async Task<bool> ReplaceDataByKeyAsync(string token, string classify, string key, string value, string description)
        {
            var uri = new Uri(Resources.GetLink.POST_REPLACE_DATA_BY_KEY(token, classify, key));
            var item = new DataModel();
            item.Value = value;
            item.Description = description;
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Trim() == "{\"Result\":\"OK\"}") return await Task.FromResult(true);
                    else return await Task.FromResult(false);
                }
                return await Task.FromResult(false);
            }
            catch { return await Task.FromResult(false); }
        }

        public async Task<bool> ReplaceDataByKeyAsync(string token, List<DataModel> datas)
        {
            var uri = new Uri(Resources.GetLink.POST_REPLACES_DATA_BY_KEY(token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(datas), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Trim() == "{\"Result\":\"OK\"}") return await Task.FromResult(true);
                    else return await Task.FromResult(false);
                }
                return await Task.FromResult(false);
            }
            catch { return await Task.FromResult(false); }
        }

        public async Task<bool> UpdateDataByKeyAsync(string token, string classify, string key, string value, string description)
        {
            var uri = new Uri(Resources.GetLink.PUT_DATA_BY_KEY(token, classify, key));
            var item = new DataModel();
            item.Value = value;
            item.Description = description;
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PutAsync(uri, _JsonValue);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Trim() == "{\"Result\":\"OK\"}") return await Task.FromResult(true);
                    else return await Task.FromResult(false);
                }
                return await Task.FromResult(false);
            }
            catch { return await Task.FromResult(false); }
        }

        public async Task<bool> InsertDataAsync(DataModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_ADD_DATA(token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Trim() == "{\"Result\":\"OK\"}") return await Task.FromResult(true);
                    else return await Task.FromResult(false);
                }
                return await Task.FromResult(false);
            }
            catch { return await Task.FromResult(false); }
        }

        public async Task<bool> InsertDataAsync(string token, string deviceId, string attribute, string oldValue, string newValue)
        {
            var uri = new Uri(Resources.GetLink.POST_ADD_LOG(token, deviceId, attribute, oldValue, newValue));
            try
            {
                // null ở request body
                HttpResponseMessage response = await _client.PostAsync(uri, null);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Trim() == "{\"Result\":\"OK\"}") return await Task.FromResult(true);
                    else return await Task.FromResult(false);
                }
                return await Task.FromResult(false);
            }
            catch { return await Task.FromResult(false); }
        }

        public async Task<bool> DeleteDataByIdAsync(string id, string token)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_DATA_BY_ID(token, id);
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
        public async Task<bool> DeleteDataByKeyAsync(string classify, string key, string token)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_DATA_BY_KEY(token, classify, key);
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
