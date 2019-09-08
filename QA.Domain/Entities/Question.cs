using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace QA.Domain.Entities
{
    public class Question : PostEntity
    {        
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Answer> Answers { get; set; }
        public Guid? AcceptedAnswerId { get; set; }

        public Question() : base()
        {
            Tags = new List<Tag>();
            Answers = new List<Answer>();
        }
    }
}
