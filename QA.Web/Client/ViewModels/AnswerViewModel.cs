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

        public AnswerViewModel(HttpClient httpClient, NavigationManager navigationManager, Answer answer)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            Answer = answer;
        }

        public string Text => Answer.Text;
        public IEnumerable<Comment> Comments => Answer.Comments;
    }
}
