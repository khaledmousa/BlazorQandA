using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Web.Client.ViewModels
{
    public class LoginViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsAuthenticated { get; set; }

        public LoginViewModel(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async void Login()
        {
            var result = await _httpClient.PostJsonAsync<bool>("api/login", new { Username, Password });
            if (!result)
            {
                IsAuthenticated = false;
            }
            else
            {
                IsAuthenticated = true;
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
