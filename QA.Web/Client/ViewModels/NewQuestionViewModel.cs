using Microsoft.AspNetCore.Components;
using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Web.Client.ViewModels
{
    public class NewQuestionViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        
        public string Title { get; set; }

        public string Text { get; set; }

        public List<Tag> Tags { get; set; }

        public NewQuestionViewModel(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;            
        }

        public void SetQuestionText(string questionText)
        {
            Text = questionText;
        }

        public async Task PostQuestion()
        {
            var question = await _httpClient.PostJsonAsync<Question>($"/api/Post/", new Question
            {
                Title = Title,
                Text = Text,
                Tags = Tags
            });

            _navigationManager.NavigateTo($"/Question/{question.Id}");
        }
    }
}
