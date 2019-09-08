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
    public class AnswerViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public string QuestionId;
        public Question Question;

        private Action _refresh;
        
        public string AnswerText { get; set; }

        public MarkupString Markdown { get; set; }

        public AnswerViewModel(HttpClient httpClient, NavigationManager navigationManager, string questionId, Action refresh)
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

        public void OnInputChanged(ChangeEventArgs e)
        {
            AnswerText = e.Value.ToString();
            Markdown = (MarkupString)BuildHtmlFromMarkdown(AnswerText);
            _refresh?.Invoke();
        }

        private string BuildHtmlFromMarkdown(string value) => Markdig.Markdown.ToHtml(
            markdown: value,
            pipeline: new MarkdownPipelineBuilder().UseAdvancedExtensions().Build()
        );
    }
}
