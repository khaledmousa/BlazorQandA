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
        private readonly IAuthenticationManager _authenticationManager;
        public string Email { get; set; }
        public string Password { get; set; }

        public bool? IsAuthenticated { get; set; }

        public LoginViewModel(HttpClient httpClient, NavigationManager navigationManager, IAuthenticationManager authenticationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _authenticationManager = authenticationManager;
            IsAuthenticated = null;
        }

        public async Task Login()
        {
            var result = await _httpClient.PostJsonAsync<LoginToken>("api/login", new { Email, Password });
            
            if (!result.Success)
            {
                IsAuthenticated = false;                
            }
            else
            {
                await _authenticationManager.LoginAsync(result);

                IsAuthenticated = true;
                _navigationManager.NavigateTo("/");
            }
        }
    }   
}
