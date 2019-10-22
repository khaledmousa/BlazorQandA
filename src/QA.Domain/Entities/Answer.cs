using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace QA.Domain.Entities
{
    public class Answer : PostEntity
    {
        public string Text { get; set; }

        public Answer() : base() { }
    }
}
