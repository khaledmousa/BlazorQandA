using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Commands
{
    public class CreateCommentCommand : CommandBase
    {
        public string Text { get; private set; }
        public Guid QuestionOrAnswerId { get; private set; }
        public CreateCommentCommand(User issuedBy, string text, Guid questionOrAnswerId) : base(issuedBy)
        {
            Text = text;
            QuestionOrAnswerId = questionOrAnswerId;
        }
    }

    public class EditCommentCommand : CommandBase
    {
        public Guid CommentId { get; private set; }
        public string Text { get; private set; }
        public EditCommentCommand(User issuedBy, Guid commentId, string text) : base(issuedBy)
        {
            CommentId = commentId;
            Text = text;
        }
    }

    public class DeleteCommentCommand : CommandBase
    {
        public Guid CommentId { get; private set; }

        public DeleteCommentCommand(User issuedBy, Guid commentId) : base(issuedBy) => CommentId = commentId;
    }
}
