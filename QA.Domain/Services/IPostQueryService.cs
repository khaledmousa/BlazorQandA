using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Services
{
    public interface IPostQueryService
    {
        Question GetQuestion(Guid questionId);
        IEnumerable<Question> GetQuestions(string searchTerm);
        IEnumerable<Question> GetQuestions(string searchTerm, int itemsPerPage, int page);
        int GetQuestionCount(string searchTerm);        
        IEnumerable<Tag> GetTags();

    }
}
