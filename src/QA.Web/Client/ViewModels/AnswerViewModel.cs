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
        public QuestionViewModel Parent { get; private set; }

        public bool IsAccepted => Parent.AcceptedAnswer.HasValue && Parent.AcceptedAnswer.Value == Answer.Id;

        public AnswerViewModel(HttpClient httpClient, NavigationManager navigationManager, QuestionViewModel parent, Answer answer)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            Answer = answer;
            Parent = parent;
        }

        public string Text => Answer.Text;
        public IEnumerable<Comment> Comments => Answer.Comments;
        public IEnumerable<Vote> Votes => Answer.Votes;

        public async Task VoteUp()
        {
            var result = await _httpClient.PostJsonAsync<Answer>($"/api/Post/{Parent.Id}/{Answer.Id}/vote", true);
            if (result != null) Answer = result as Answer;
        }

        public async Task VoteDown()
        {
            var result = await _httpClient.PostJsonAsync<Answer>($"/api/Post/{Parent.Id}/{Answer.Id}/vote", false);
            if (result != null) Answer = result;
        }

        public async Task AcceptAnswer()
        {
            await _httpClient.PostJsonAsync($"/api/Post/{Parent.Id}/{Answer.Id}/accept", null);
            Parent.AcceptedAnswer = Answer.Id;
        }
    }
}
