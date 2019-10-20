using Microsoft.AspNetCore.Components;
using QA.Domain.Entities;
using QA.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Web.Client.ViewModels
{
    public class ListQuestionsViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;        

        public QuestionBrief[] Questions { get; private set; }

        public ListQuestionsViewModel(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            Questions = null;
        }

        public async Task LoadQuestionsAsync(string searchTerm, int page)
        {            
            var questions = await _httpClient.GetJsonAsync<Question[]>($"api/Post?searchTerm={searchTerm}&page={page}&count=10");            
            Questions = questions.Select(q => new QuestionBrief(q)).ToArray();            
        }
    }

    public class QuestionBrief
    {
        private readonly Question _question;

        public QuestionBrief(Question question)
        {
            _question = question;
        }

        public Guid Id => _question.Id;
        public string AuthorName => _question.Author?.Username;        
        public string Title => _question.Title;
        public string Text => _question.Text.Length < 200 ? _question.Text : _question.Text.Substring(0, 200) + "...";
        public IEnumerable<Tag> Tags => _question.Tags;

        public string Timestamp => _question.Timestamp.ToString("dd.MM.yyyy HH:mm");

    }
}
