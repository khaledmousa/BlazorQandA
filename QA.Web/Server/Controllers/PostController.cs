using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QA.Domain.Entities;
using QA.Domain.Services;
using QA.Domain.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace QA.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostQueryService _postQueryService;
        private readonly IPostCommandService _postCommandService;
        private readonly IUserService _userService;

        public PostController(IPostQueryService postQueryService, IPostCommandService postCommandService, IUserService userService)
        {
            _postQueryService = postQueryService;
            _postCommandService = postCommandService;
            _userService = userService;
        }

        // GET: api/Post
        [HttpGet]        
        public IEnumerable<Question> Get(string searchTerm, int page, int count)
        {
            return _postQueryService.GetQuestions(searchTerm, count, page);
        }
        
        [HttpGet]
        [Route("count")]
        public int GetCount(string searchTerm)
        {
            return _postQueryService.GetQuestionCount(searchTerm);
        }

        //GET: api/Post/id
        [HttpGet]
        [Route("{questionId}")]
        public Question Get(string questionId)
        {
            return _postQueryService.GetQuestion(Guid.Parse(questionId));
        }

        [HttpPost]        
        public Question AddQuestion([FromBody]Question question)
        {
            var user = GetCurrentUser();

            var result = _postCommandService.Execute(new CreateQuestionCommand(user, question.Title, question.Text, question.Tags));
            if (result.IsSuccessful) return result.Entity as Question;
            else return null;
        }

        [HttpPost]
        [Route("{questionId}/vote")]
        public Question VoteQuestion(string questionId, [FromBody] bool up)
        {
            var user = GetCurrentUser();

            var result = _postCommandService.Execute(new VoteQuestionCommand(user, Guid.Parse(questionId), up ? Direction.Up : Direction.Down));
            if (result.IsSuccessful) return result.Entity as Question;
            else return null;
        }

        [HttpPost]
        [Route("{questionId}/comment")]
        public Comment AddQuestionComment(string questionId, [FromBody] string text)
        {
            var user = GetCurrentUser();

            var result = _postCommandService.Execute(new CreateCommentCommand(user, text, Guid.Parse(questionId)));
            if (result.IsSuccessful) return result.Entity as Comment;
            else return null;
        }

        [HttpPost]
        [Route("{questionId}/{answerId}/accept")]
        public void AcceptAnswer(string questionId, string answerId)
        {
            var user = GetCurrentUser();

            var result = _postCommandService.Execute(new AcceptAnswerCommand(user, Guid.Parse(answerId), true));
        }

        [HttpPost]
        [Route("{questionId}/{answerId}/vote")]
        public Answer VoteAnswer(string questionId, string answerId, [FromBody] bool up)
        {
            var user = GetCurrentUser();

            var result = _postCommandService.Execute(new VoteAnswerCommand(user, Guid.Parse(answerId), up ? Direction.Up : Direction.Down));
            if (result.IsSuccessful) return result.Entity as Answer;
            else return null;
        }

        [HttpPost]
        [Route("{questionId}/answer")]
        public Answer AddAnswer(string questionId, [FromBody]string answer)
        {
            var user = GetCurrentUser();

            var result = _postCommandService.Execute(new CreateAnswerCommand(user, answer, Guid.Parse(questionId)));
            if (result.IsSuccessful) return result.Entity as Answer;
            else return null;
        }

        private User GetCurrentUser()
        {
            var userId = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = _userService.GetById(Guid.Parse(userId));
                return user;
            }
            return null;
        }
    }
}
