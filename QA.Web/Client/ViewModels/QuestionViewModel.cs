using Microsoft.AspNetCore.Components;
using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Web.Client.ViewModels
{
    public class QuestionViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public Question Question;

        public QuestionViewModel(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            Question = null;
        }

        public async Task LoadQuestionAsync(string questionId)
        {
            Question = await _httpClient.GetJsonAsync<Question>($"/api/Post/{questionId}");            
        }

    }
}
