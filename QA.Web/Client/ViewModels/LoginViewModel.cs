using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QA.Web.Client.ViewModels
{
    public class LoginViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public string Email { get; set; }
        public string Password { get; set; }

        public bool? IsAuthenticated { get; set; }

        public LoginViewModel(HttpClient httpClient, NavigationManager navigationManager,
            ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            IsAuthenticated = null;
        }

        public async Task Login()
        {
            var result = await _httpClient.PostJsonAsync<LoginResult>("api/login", new { Email, Password });
            
            if (!result.Success)
            {
                IsAuthenticated = false;                
            }
            else
            {
                await _localStorage.SetItemAsync("authToken", result);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                IsAuthenticated = true;
                _navigationManager.NavigateTo("/");
            }
        }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
