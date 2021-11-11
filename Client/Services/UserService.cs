using SmartRetail.Share.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace SmartRetail.Client.Services
{
    public class UserService
    {
        HttpClient _client = new HttpClient();
        public CloudUser CurrentUser { get; set; }
        public UserService()
        { }
        public async Task<string> Login(string user, string password)
        {
            string token = "";

            var uri = new Uri(Resources.GetLink.POST_LOGIN(user, password));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    token = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(token);
        }

        public async Task<string> SetRoles(string user, List<Role> roleList, string token)
        {

            var uri = new Uri(Resources.GetLink.POST_UPDATE_ROLE(user, token));
            try
            {
                var _JsonValue = new StringContent(JsonConvert.SerializeObject(roleList), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    token = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(token);
        }

        // public async Task<List<CurrentRole>> GetRoles(string token)
        // {
        //     var roleItem = new List<CurrentRole>();
        //     var uri = new Uri(Resources.GetLink.GET_ROLE(token));

        //     try
        //     {
        //         HttpResponseMessage response = await _client.GetAsync(uri);
        //         if (response.IsSuccessStatusCode)
        //         {
        //             var content = await response.Content.ReadAsStringAsync();
        //             roleItem = JsonConvert.DeserializeObject<List<CurrentRole>>(content);
        //         }
        //     }
        //     catch (Exception ex)
        //     {

        //     }
        //     return await Task.FromResult(roleItem);
        // }

        public async Task<UserInfor> GetUserInforAsync(string token)
        {
            UserInfor userItem = new UserInfor();

            var uri = new Uri(Resources.GetLink.GET_USER_INFO(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    userItem = JsonConvert.DeserializeObject<UserInfor>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(userItem);
        }

        public async Task<CloudUser> GetUserInfoByToken(string token)
        {
            var userItem = new CloudUser();

            var uri = new Uri(Resources.GetLink.GET_USER_ALL_INFO(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    userItem = JsonConvert.DeserializeObject<CloudUser>(content);
                }
            }
            catch (Exception ex)
            { }
            return await Task.FromResult(userItem);
        }

        public async Task<List<CloudUser>> GetUserListAsync(string token)
        {
            var userItem = new List<CloudUser>();

            var uri = new Uri(Resources.GetLink.GET_USER_LIST(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    userItem = JsonConvert.DeserializeObject<List<CloudUser>>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(userItem);
        }

        public async Task<CloudUser> GetUserAllInforAsync(string token)
        {
            CloudUser userItem = new CloudUser();

            var uri = new Uri(Resources.GetLink.GET_USER_ALL_INFO(token));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    userItem = JsonConvert.DeserializeObject<CloudUser>(content);
                    if (userItem != null)
                        CurrentUser = userItem;
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(userItem);
        }
        public async Task<string> CreateNewUser(UserInforTransfer infor)
        {
            string rawResult = "";

            var uri = new Uri(Resources.GetLink.POST_NEW_USER());
            try
            {
                var _JsonValue = new StringContent(JsonConvert.SerializeObject(infor), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    rawResult = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(rawResult);
        }
        public async Task<string> UpdateUser(UserDataTransfer infor, string Token)
        {
            string rawResult = "";

            var uri = new Uri(Resources.GetLink.POST_USER_UPDATE(Token));
            try
            {
                var _JsonValue = new StringContent(JsonConvert.SerializeObject(infor), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    rawResult = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(rawResult);
        }

        public async Task<string> UpdateUserInfor(UserInforTransfer infor, string Token)
        {
            string rawResult = "";

            var uri = new Uri(Resources.GetLink.POST_USER_INFO_UPDATE(Token));
            try
            {
                var _JsonValue = new StringContent(JsonConvert.SerializeObject(infor), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, _JsonValue);
                if (response.IsSuccessStatusCode)
                {
                    rawResult = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(rawResult);
        }
        public async Task<bool> UpdateUserDeviceName(string token, string _GateWay, string _ChildId, string _Name)
        {

            try
            {
                string RequestUrl = Resources.GetLink.POST_USER_DEVICE_NAME(token, _GateWay, _ChildId, _Name);
                var uri = new Uri(RequestUrl);
                HttpResponseMessage response = await _client.PostAsync(uri, null);
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
        public async Task<bool> DeleteUserDeviceName(string token, string _GateWay, string _ChildId)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_USER_DEVICE_NAME(token, _GateWay, _ChildId);
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

        public async Task<string> AddNewGateway(string gw, string activeCode, string token)
        {
            string rawResult = "";

            var uri = new Uri(Resources.GetLink.POST_ADD_USER_GATEWAY(gw, activeCode, token));
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, null);
                if (response.IsSuccessStatusCode)
                {
                    rawResult = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(rawResult);
        }
        public async Task<bool> DeleteUserGateway(string _GateWay, string token)
        {
            try
            {
                string RequestUrl = Resources.GetLink.DELETE_USER_GATEWAY(_GateWay, token);
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
        public async Task<string> GetEmailConfirm(string user, string email)
        {
            string content = "";
            var uri = new Uri(Resources.GetLink.GET_CHECK_CONFIRM_EMAIL(user, email));
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

        public async Task<string> GetRequestResetUser(string User)
        {
            string content = "";
            var uri = new Uri(Resources.GetLink.GET_RESET_USER(User));
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

        public async Task<string> GetRequestAdminChangePassword(string User, string password, string token)
        {
            string content = "";
            var uri = new Uri(Resources.GetLink.GET_ADMIN_RESET_PASSWORD(User, password, token));
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

        public async Task<string> GetRequestChangePassword(string User, string oldPassword, string newPassword)
        {
            string content = "";
            var uri = new Uri(Resources.GetLink.GET_CHANGE_PASSWORD(User, oldPassword, newPassword));
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
    }
}
