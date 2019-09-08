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
        public Answer Answer { get; set; }
        public Guid QuestionId { get; private set; }

        public AnswerViewModel(HttpClient httpClient, NavigationManager navigationManager, Guid questionId, Answer answer)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            Answer = answer;
            QuestionId = questionId;
        }

        public string Text => Answer.Text;
        public IEnumerable<Comment> Comments => Answer.Comments;
        public IEnumerable<Vote> Votes => Answer.Votes;

        public async Task VoteUp()
        {
            var result = await _httpClient.PostJsonAsync<Answer>($"/api/Post/{QuestionId}/{Answer.Id}/vote", true);
            if (result != null) Answer = result as Answer;
        }

        public async Task VoteDown()
        {
            var result = await _httpClient.PostJsonAsync<Answer>($"/api/Post/{QuestionId}/{Answer.Id}/vote", false);
            if (result != null) Answer = result;
        }
    }
}
