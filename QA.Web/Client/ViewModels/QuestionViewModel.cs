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
        public Question Question { get; set; }
        public IEnumerable<AnswerViewModel> Answers { get; set; }
        public string NewCommentText { get; set; }

        public QuestionViewModel(HttpClient httpClient, NavigationManager navigationManager, Question question)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            Question = question;
            Answers = Question.Answers.Select(a => new AnswerViewModel(httpClient, navigationManager, this, a))
                .OrderByDescending(a => a.IsAccepted)
                .ThenByDescending(a => a.Votes.Sum(v => v.Direction == Direction.Up ? 1 : -1)).ToList();
        }

        public Guid Id => Question.Id;
        public string Title => Question.Title;
        public string Text => Question.Text;
        public IEnumerable<Tag> Tags => Question.Tags;
        public IEnumerable<Comment> Comments => Question.Comments;
        public IEnumerable<Vote> Votes => Question.Votes;

        public async Task AddComment()
        {
            var comment = await _httpClient.PostJsonAsync<Comment>($"api/Post/{Id}/comment", NewCommentText);
            Question.Comments.Add(comment);
            NewCommentText = string.Empty;
        }

        public Guid? AcceptedAnswer
        {
            get => Question.AcceptedAnswerId;
            set => Question.AcceptedAnswerId = value;
        }
    }
}
