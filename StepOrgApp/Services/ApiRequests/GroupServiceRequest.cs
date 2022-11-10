using Android.Views;
using Newtonsoft.Json;
using StepOrgApp.DTOs;
using StepOrgApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StepOrgApp.Services.ApiRequests
{
    public  class GroupServiceRequest
    {
        private HttpClient _httpClient { get; set; }
        public GroupServiceRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<GroupDto>> GetAllMyGroups()
        {
            string token = await SecureStorage.GetAsync(SD.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetAsync(SD.BaseAPIUrl + "api/group");
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GroupDto>>(contentTemp);
                return result;
            }
            return null;
        }

        public async Task<GroupDto> GetGroupById(string Id)
        {
            string token = await SecureStorage.GetAsync(SD.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetAsync(SD.BaseAPIUrl + $"api/group/getbyid?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GroupDto>(contentTemp);
                return result;
            }
            return null;
        }
        public async Task<bool> UploadProductImage(MultipartFormDataContent content, string groupId)
        {
            string token = await SecureStorage.GetAsync(SD.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var postResult = await _httpClient.PostAsync(SD.BaseAPIUrl + $"api/group/Avatar?groupId={groupId}", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            return true;
        }
        public async Task<bool> ChangeGroupName(CreateGroup editGroup, string groupId)
        {
            string token = await SecureStorage.GetAsync(SD.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetAsync(SD.BaseAPIUrl + $"api/group/changeName?ShortName={editGroup.ShortName}&Name={editGroup.Name}&groupId={groupId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<GroupDto> CreateGroup(CreateGroup newGroup)
        {
            string token = await SecureStorage.GetAsync(SD.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetAsync(SD.BaseAPIUrl + $"api/group/create?ShortName={newGroup.ShortName}&Name={newGroup.Name}");
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GroupDto>(contentTemp);
                return result;
            }
            return null;
        }
    }
}
