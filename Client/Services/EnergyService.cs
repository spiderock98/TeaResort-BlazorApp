using SmartRetail.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartRetail.Client.Services
{
    public class EnergyService
    {
        HttpClient _client = new HttpClient();
        public EnergyService()
        { }
        //public async Task<PowerTagMeterModel> GetDataAsync(string user, string password)
        //{
        //    PowerTagMeterModel result = new PowerTagMeterModel();
        //    var uri = new Uri(Resources.GetLink.GET_POWERTAG_METERINSTANCE());
        //    try
        //    {
        //        var byteArray = Encoding.ASCII.GetBytes(user + ":" + password);
        //        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        //        HttpResponseMessage response = await _client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            result = JsonConvert.DeserializeObject<PowerTagMeterModel>(content);
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //    return await Task.FromResult(result);
        //}
    }
}
