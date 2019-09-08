using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Entities
{
    public class Vote : AuthoredEntity
    {
        public Direction Direction { get; set; }
    }

    public enum Direction
    {
        Up,
        Down
    }
}
