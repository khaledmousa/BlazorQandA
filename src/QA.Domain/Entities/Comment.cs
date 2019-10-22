using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Entities
{
    public class Comment : AuthoredEntity
    {
        public string Text { get; set; }
    }
}
