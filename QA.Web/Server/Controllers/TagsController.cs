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
    public class TagsController : ControllerBase
    {
        private readonly IPostQueryService _postQueryService;

        public TagsController(IPostQueryService postQueryService)
        {
            _postQueryService = postQueryService;
        }

        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            return _postQueryService.GetTags();
        }
    }
}