using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using StepOrgApp.DTOs;
using StepOrgApp.Models;
using System.Net.Http.Headers;
using System.Text;

namespace StepOrgApp.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _client;
        //private readonly AuthStateProvider _authStateProvider;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _authStateProvider = authStateProvider; 
        }

        public async Task<LoginModelResponse> Login(LoginModel signInRequest)
        {
            var content = JsonConvert.SerializeObject(signInRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(SD.BaseAPIUrl + "api/account/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginModelResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await SecureStorage.SetAsync(SD.AccessToken, result.Token);
                string serializedUserDetails = JsonConvert.SerializeObject(result);
                await SecureStorage.SetAsync(SD.UserDetails, serializedUserDetails);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new LoginModelResponse();
            }
            else
            {
                return null;
            }
        }

        public async Task Logout()
        {
            SecureStorage.Remove(SD.AccessToken);
            SecureStorage.Remove(SD.UserDetails);
            SecureStorage.Remove(SD.CurrentGroup);
            SecureStorage.Remove(SD.ProfilePicture);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> RegisterUser(RegisterModel signUpRequest)
        {
            RegisterModelRequest model = new();
            model.displayName = signUpRequest.DisplayName;
            model.email = signUpRequest.Email;
            model.password = signUpRequest.Password;
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(SD.BaseAPIUrl + "api/Account/register", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
    }
}
