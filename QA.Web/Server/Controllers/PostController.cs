using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QA.Domain.Entities;
using QA.Domain.Services;

namespace QA.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostQueryService _postQueryService;

        public PostController(IPostQueryService postQueryService)
        {
            _postQueryService = postQueryService;
        }

        // GET: api/Post
        [HttpGet]
        public IEnumerable<Question> Get(string searchTerm, int page, int count)
        {
            var questions = _postQueryService.GetQuestions(searchTerm);
            return questions.Skip(count * page).Take(count).ToArray();
        }        

        //GET: api/Post/id
        [HttpGet]
        [Route("{questionId}")]
        public Question Get(string questionId)
        {
            return _postQueryService.GetQuestion(Guid.Parse(questionId));
        }
    }
}
