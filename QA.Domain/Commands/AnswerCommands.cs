using QA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Domain.Commands
{
    public class CreateAnswerCommand : CommandBase
    {
        public string Text { get; private set; }
        public Guid QuestionId { get; private set; }
        public CreateAnswerCommand(User issuedBy, string text, Guid questionId) : base(issuedBy)
        {            
            Text = text;
            QuestionId = questionId;
        }
    }

    public class EditAnswerCommand : CommandBase
    {
        public Guid AnswerId { get; private set; }
        public string Text { get; private set; }
        public EditAnswerCommand(User issuedBy, Guid answerId, string text) : base(issuedBy)
        {
            AnswerId = answerId;
            Text = text;
        }
    }

    public class DeleteAnswerCommand : CommandBase
    {
        public Guid AnswerId { get; private set; }

        public DeleteAnswerCommand(User issuedBy, Guid answerId) : base(issuedBy) => AnswerId = answerId;
    }

    public class VoteAnswerCommand : CommandBase
    {
        public Guid AnswerId { get; private set; }
        public Direction VoteDirection { get; private set; }
        public VoteAnswerCommand(User issuedBy, Guid answerId, Direction voteDirection) : base(issuedBy)
        {
            AnswerId = answerId;
            VoteDirection = voteDirection;
        }
    }

    public class AcceptAnswerCommand : CommandBase
    {
        public Guid AnswerId { get; private set; }
        public bool Accept { get; protected set; }

        public AcceptAnswerCommand(User issuedBy, Guid answerId, bool isAccepted = true) : base(issuedBy)
        {
            AnswerId = answerId;
        }
    }
}
