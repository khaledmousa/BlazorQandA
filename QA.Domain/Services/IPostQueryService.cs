using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Services
{
    public interface IPostQueryService
    {
        Question GetQuestion(Guid questionId);
        IEnumerable<Question> GetQuestions(Tag tag);
        IEnumerable<Question> GetQuestions(string searchTerm);
        IEnumerable<Tag> GetTags(string searchTerm);

    }
}
