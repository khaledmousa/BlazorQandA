using QA.Domain.Dto;
using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Services
{
    public interface IPostQueryService
    {
        Question GetQuestion(Guid questionId);
        QuestionListDto GetQuestions(string searchTerm);
        QuestionListDto GetQuestions(string searchTerm, int itemsPerPage, int page);        
        IEnumerable<Tag> GetTags();

    }
}
