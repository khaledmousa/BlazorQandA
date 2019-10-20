using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Dto
{
    public class QuestionListDto
    {
        public IEnumerable<Question> Questions { get; set; }
        public int? Page { get; set; }
        public int FullCount { get; set; }
    }
}
