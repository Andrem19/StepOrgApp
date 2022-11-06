using Newtonsoft.Json;
using StepOrgApp.DTOs;
using StepOrgApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StepOrgApp.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthStateProvider _authStateProvider;
        //private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, AuthStateProvider authStateProvider)
        {
            _client = client;
            _authStateProvider = authStateProvider; 
        }

        public async Task<LoginModelResponse> Login(LoginModel signInRequest)
        {
            var content = JsonConvert.SerializeObject(signInRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginModelResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                Preferences.Set(SD.AccessToken, result.Token);
                string serializedUserDetails = JsonConvert.SerializeObject(result.UserDTO);
                Preferences.Set(SD.UserDetails, serializedUserDetails);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new LoginModelResponse() { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            Preferences.Remove(SD.AccessToken);
            Preferences.Remove(SD.UserDetails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegisterModelResponse> RegisterUser(RegisterModel signUpRequest)
        {
            var content = JsonConvert.SerializeObject(signUpRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterModelResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegisterModelResponse { IsRegisterationSuccessful = true };
            }
            else
            {
                return new RegisterModelResponse { IsRegisterationSuccessful = false, Errors = result.Errors };
            }
        }
    }
}
