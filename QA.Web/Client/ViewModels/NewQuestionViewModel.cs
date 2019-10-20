using Blazored.LocalStorage;
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
        private readonly ILocalStorageService _localStorage;

        public string Title { get; set; }

        public string Text { get; set; }

        public IEnumerable<string> TagsText { get; set; }        

        public IEnumerable<Tag> AllTags { get; set; }

        public NewQuestionViewModel(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
        }

        public async Task InitializeAsync()
        {
            var tags = await _localStorage.GetItemAsync<IEnumerable<Tag>>("all_tags");
            if (tags == null)
            {
                AllTags = await _httpClient.GetJsonAsync<IEnumerable<Tag>>("/api/Tags/");
                await _localStorage.SetItemAsync("all_tags", AllTags);
            }
            else AllTags = tags;
            
        }

        public void SetQuestionText(string questionText)
        {
            Text = questionText;
        }

        public async Task PostQuestion()
        {
            var selectedTags = TagsText ?? new string[] { };
            var question = await _httpClient.PostJsonAsync<Question>($"/api/Post/", new Question
            {
                Title = Title,
                Text = Text,
                Tags = AllTags.Where(t => selectedTags.Contains(t.Name))?.ToList()
            });

            _navigationManager.NavigateTo($"/Question/{question.Id}");
        }
    }
}
