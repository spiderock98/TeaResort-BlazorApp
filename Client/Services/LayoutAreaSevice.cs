using SmartRetail.Share.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartRetail.Services
{
    public class LayoutAreaSevice
    {
        HttpClient _client = new HttpClient();
        public List<SectionModel> SectionList { get; set; }
        public List<ZoneModel> ZoneList { get; set; }
        public List<AreaModel> AreaList { get; set; }
        public LayoutAreaSevice()
        {

        }

        public async Task<List<SectionModel>> GetSectionListAsync(string token)
        {

            var uri = new Uri(Resources.GetLink.GET_SECTION_LIST(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    SectionList = JsonConvert.DeserializeObject<List<SectionModel>>(content);
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult(SectionList);
        }

        public async Task<bool> UpdateSectionAsync(SectionModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.PUT_UPDATE_SECTION(token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PutAsync(uri, _JsonValue);

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

        public async Task<bool> InsertSectionAsync(SectionModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_INSERT_SECTION(token));
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

        public async Task<bool> DeleteSectionAsync(int id, string token)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_SECTION(id, token);
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


        public async Task<List<ZoneModel>> GetZoneListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_ZONE_LIST(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ZoneList = JsonConvert.DeserializeObject<List<ZoneModel>>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(ZoneList);
        }

        public async Task<bool> UpdataZoneAsync(ZoneModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.PUT_UPDATE_ZONE(token));
            var _JsonValue = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PutAsync(uri, _JsonValue);

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

        public async Task<bool> InsertZoneAsync(ZoneModel item, string token)
        {
            var uri = new Uri(Resources.GetLink.POST_INSERT_ZONE(token));
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

        public async Task<bool> DeleteZoneAsync(int id, string token)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_ZONE(id, token);
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

        public async Task<List<AreaModel>> GetAreaListAsync(string token)
        {
            var uri = new Uri(Resources.GetLink.GET_AREA_LIST(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    AreaList = JsonConvert.DeserializeObject<List<AreaModel>>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(AreaList);
        }
    }
}
