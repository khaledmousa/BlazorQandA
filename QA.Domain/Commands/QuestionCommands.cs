using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Commands
{
    public class CreateQuestionCommand : CommandBase
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public IEnumerable<Tag> Tags { get; private set; }

        public CreateQuestionCommand(User issuedBy, string title, string text, IEnumerable<Tag> tags) : base(issuedBy)
        {
            Title = title;
            Text = text;
            Tags = tags;
        }
    }

    public class EditQuestionCommand : CreateQuestionCommand
    {
        public Guid QuestionId { get; private set; }
        public EditQuestionCommand(User issuedBy, Guid questionId, string title, string text, IEnumerable<Tag> tags) : 
            base(issuedBy, title, text, tags)
        {
            QuestionId = questionId;
        }
    }

    public class DeleteQuestionCommand : CommandBase
    {
        public Guid QuestionId { get; private set; }

        public DeleteQuestionCommand(User issuedBy, Guid questionId) : base(issuedBy) => QuestionId = questionId;
    }

    public class VoteQuestionCommand : CommandBase
    {
        public Guid QuestionId { get; private set; }
        public Direction VoteDirection { get; private set; }
        public VoteQuestionCommand(User issuedBy, Guid questionId, Direction voteDirection) : base(issuedBy)
        {
            QuestionId = questionId;
            VoteDirection = voteDirection;
        }
    }
}
