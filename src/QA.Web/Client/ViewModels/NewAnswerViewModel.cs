using Markdig;
using Microsoft.AspNetCore.Components;
using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Web.Client.ViewModels
{
    public class NewAnswerViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public string QuestionId;
        public Question Question;

        private Action _refresh;
        
        public string AnswerText { get; set; }

        public NewAnswerViewModel(HttpClient httpClient, NavigationManager navigationManager, string questionId, Action refresh)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            QuestionId = questionId;
            _refresh = refresh;
        }

        public async Task InitializeAsync()
        {
            Question = await _httpClient.GetJsonAsync<Question>($"/api/Post/{QuestionId}");
        }

        public void SetAnswer(string answerText)
        {
            AnswerText = answerText;
        }

        public async Task AddAnswer()
        {
            await _httpClient.PostJsonAsync<Answer>($"/api/Post/{QuestionId}/answer", AnswerText);
            _navigationManager.NavigateTo($"/Question/{QuestionId}");
        }
    }
}
