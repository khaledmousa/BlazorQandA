using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace QA.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }

    public abstract class AuthoredEntity : EntityBase
    {        
        public User Author { get; set; }
        public DateTime Timestamp { get; set; }        
    }    

    public abstract class PostEntity : AuthoredEntity
    {
        public List<Comment> Comments { get; set; }
        public List<Vote> Votes { get; set; }
        public List<PostHistoryItem> History { get; set; }

        public PostEntity()
        {
            Comments = new List<Comment>();
            Votes = new List<Vote>();
            History = new List<PostHistoryItem>();
        }
    }

    public class PostHistoryItem
    {
        public User Author { get; set; }
        public string Text { get; set; }
    }    
}
