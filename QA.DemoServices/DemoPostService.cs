using Bogus;
using QA.Domain.Commands;
using QA.Domain.Entities;
using QA.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaffleGenerator;

namespace QA.DemoServices
{
    public class DemoPostService : IPostCommandService, IPostQueryService
    {
        #region ctor and setup
        private readonly IUserService _userService;

        private List<User> Users { get; set; }
        private List<Question> Questions{ get; set; }
        private List<Tag> Tags { get; set; }
        private Random Random { get; }
        private Faker<Vote> VoteGenerator { get; }
        private Faker<Comment> CommentGenerator { get; }
        private Faker<Answer> AnswerGenerator { get; }

        public DemoPostService(IUserService userService)
        {
            _userService = userService;
            Users = _userService.GetUsers().ToList();
            SetupTags();
            Questions = new List<Question>();
            Random = new Random(DateTime.Now.Millisecond);
            VoteGenerator = new Faker<Vote>()
                .RuleFor(v => v.Id, f => f.Random.Guid())
                .RuleFor(v => v.Author, f => Users[f.Random.Int(0, Users.Count - 1)])
                .RuleFor(v => v.Direction, f => f.Random.Bool(0.3f) ? Direction.Down : Direction.Up)
                .RuleFor(v => v.Timestamp, f => DateTime.Now);

            CommentGenerator = new Faker<Comment>()
                .RuleFor(c => c.Author, f => Users[f.Random.Int(0, Users.Count - 1)])
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.Text, f => f.Waffle().Text(1, false));

            AnswerGenerator = new Faker<Answer>()
                .RuleFor(a => a.Id, f => f.Random.Guid())
                .RuleFor(a => a.Text, f => f.WaffleText(Random.Next(1, 3), false))
                .RuleFor(a => a.Timestamp, f => DateTime.Now)
                .RuleFor(a => a.Votes, f => VoteGenerator.GenerateForever().Take(Random.Next(0, 10)).ToList())
                .RuleFor(a => a.Comments, f => CommentGenerator.GenerateForever().Take(Random.Next(0, 3)).ToList());

            foreach (var i in Enumerable.Range(0, 50)) Questions.Add(GenerateQuestion());
        }

        private Question GenerateQuestion(Guid? userId = null, string title = null, string question = null, IEnumerable<Tag> tags = null)
        {
            return new Question
            {
                Id = Guid.NewGuid(),
                Author = userId.HasValue ? Users.Single(u => u.Id == userId.Value) : Users[Random.Next(0, Users.Count - 1)],
                Tags = tags == null ? Tags.OrderBy(t => Random.Next(0, 100)).Take(Random.Next(1, 4)).OrderBy(t => t.Name).ToList() : tags.ToList(),
                Text = string.IsNullOrEmpty(question) ? WaffleEngine.Text(Random.Next(1, 3), false) : question,
                Title = string.IsNullOrEmpty(title) ? WaffleEngine.Title() : title,
                Votes = VoteGenerator.GenerateForever().Take(Random.Next(0, 50)).ToList(),
                Timestamp = DateTime.Now,
                Comments = CommentGenerator.GenerateForever().Take(Random.Next(0, 10)).ToList(),
                Answers = AnswerGenerator.GenerateForever().Take(Random.Next(0, 5)).ToList()
            };                            
        }

        private void SetupTags()
        {
            Tags = new List<Tag>
            {
                new Tag { Id = Guid.NewGuid(), Name = "ASP.NET" },
                new Tag { Id = Guid.NewGuid(), Name = "Ajax" },
                new Tag { Id = Guid.NewGuid(), Name = "Blazor" },
                new Tag { Id = Guid.NewGuid(), Name = "Busines Intelligence" },
                new Tag { Id = Guid.NewGuid(), Name = "C#" },
                new Tag { Id = Guid.NewGuid(), Name = "COM" },
                new Tag { Id = Guid.NewGuid(), Name = "Continuous Integration" },
                new Tag { Id = Guid.NewGuid(), Name = "Cross Platform" },
                new Tag { Id = Guid.NewGuid(), Name = "Django" },
                new Tag { Id = Guid.NewGuid(), Name = "Entity Framework" },
                new Tag { Id = Guid.NewGuid(), Name = "F#" },
                new Tag { Id = Guid.NewGuid(), Name = "FORTRAN" },
                new Tag { Id = Guid.NewGuid(), Name = "Golang" },
                new Tag { Id = Guid.NewGuid(), Name = "Hadoop" },                
                new Tag { Id = Guid.NewGuid(), Name = "Java" },
                new Tag { Id = Guid.NewGuid(), Name = "jQuery" },
                new Tag { Id = Guid.NewGuid(), Name = "Javascript" },
                new Tag { Id = Guid.NewGuid(), Name = "Kotlin" },
                new Tag { Id = Guid.NewGuid(), Name = ".Net" },
                new Tag { Id = Guid.NewGuid(), Name = "OOP" },
                new Tag { Id = Guid.NewGuid(), Name = "Pascal" },
                new Tag { Id = Guid.NewGuid(), Name = "Php" },
                new Tag { Id = Guid.NewGuid(), Name = "Python" },
                new Tag { Id = Guid.NewGuid(), Name = "SQL" },                                
                new Tag { Id = Guid.NewGuid(), Name = "WPF" },
            };
        }

        #endregion

        #region IPostCommandService
        public CommandResponse Execute(CommandBase command)
        {
            switch (command)
            {
                case EditQuestionCommand editQuestion:
                    return ExecuteEditQuestion(editQuestion);
                case CreateQuestionCommand createQuestion:
                    return ExecuteCreateQuestion(createQuestion);               
                case DeleteQuestionCommand deleteQuestion:
                    return ExecuteDeleteQuestion(deleteQuestion);
                case VoteQuestionCommand voteQuestion:
                    return ExecuteVoteQuestionCommand(voteQuestion);
                case CreateAnswerCommand createAnswer:
                    return ExecuteCreateAnswer(createAnswer);
                case EditAnswerCommand editAnswer:
                    return ExecuteEditAnswer(editAnswer);
                case DeleteAnswerCommand deleteAnswer:
                    return ExecuteDeleteAnswer(deleteAnswer);
                case VoteAnswerCommand voteAnswer:
                    return ExecuteVoteAnswer(voteAnswer);
                case AcceptAnswerCommand acceptAnswer:
                    return ExecuteAcceptAnswerCommand(acceptAnswer);
                case CreateCommentCommand createComment:
                    return ExecuteCreateComment(createComment);
                case EditCommentCommand editComment:
                    return ExecuteEditComment(editComment);
                case DeleteCommentCommand deleteComment:
                    return ExecuteDeleteComment(deleteComment);
            }
            return CommandResponse.Failure($"Command {command.GetType().Name} has no handlers");
        }

        private CommandResponse ExecuteCreateQuestion(CreateQuestionCommand command)
        {
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Author = command.IssuedBy,
                Tags = command.Tags?.ToList() ?? new List<Tag>(),
                Text = command.Text,
                Title = command.Title,
                Votes = new List<Vote>(),
                Timestamp = DateTime.Now,
                Comments = new List<Comment>(),
                Answers = new List<Answer>()
            };

            Questions.Add(question);
            return CommandResponse.Success(question);
        }

        private CommandResponse ExecuteEditQuestion(EditQuestionCommand command)
        {
            var existingQuestion = Questions.FirstOrDefault(q => q.Id == command.QuestionId);
            if (existingQuestion == null) return CommandResponse.Failure($"Question {command.QuestionId} doesn't exist.");

            existingQuestion.History.Add(new PostHistoryItem
            {
                Author = existingQuestion.Author,
                Text = existingQuestion.Text
            });

            existingQuestion.Title = command.Title;
            existingQuestion.Text = command.Text;
            return CommandResponse.Success(existingQuestion);
        }

        private CommandResponse ExecuteDeleteQuestion(DeleteQuestionCommand command)
        {
            var existingQuestion = Questions.FirstOrDefault(q => q.Id == command.QuestionId);
            if (existingQuestion == null) return CommandResponse.Failure($"Question {command.QuestionId} doesn't exist.");
            Questions.Remove(existingQuestion);
            return CommandResponse.Success();
        }

        private CommandResponse ExecuteVoteQuestionCommand(VoteQuestionCommand command)
        {
            var existingQuestion = Questions.FirstOrDefault(q => q.Id == command.QuestionId);
            if (existingQuestion == null) return CommandResponse.Failure($"Question {command.QuestionId} doesn't exist.");

            existingQuestion.Votes.Add(new Vote
            {
                Author = command.IssuedBy,
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Direction = command.VoteDirection
            });
            return CommandResponse.Success(existingQuestion);
        }

        private CommandResponse ExecuteCreateAnswer(CreateAnswerCommand command)
        {
            var existingQuestion = Questions.FirstOrDefault(q => q.Id == command.QuestionId);
            if (existingQuestion == null) return CommandResponse.Failure($"Question {command.QuestionId} doesn't exist.");

            var answer = new Answer
            {
                Author = command.IssuedBy,
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Text = command.Text
            };
            existingQuestion.Answers.Add(answer);

            return CommandResponse.Success(answer);
        }

        private CommandResponse ExecuteEditAnswer(EditAnswerCommand command)
        {
            var existingAnswer = Questions.SelectMany(q => q.Answers).SingleOrDefault(a => a.Id == command.AnswerId);
            if (existingAnswer == null) return CommandResponse.Failure($"Answer {command.AnswerId} doesn't exist.");

            existingAnswer.History.Add(new PostHistoryItem
            {
                Author = existingAnswer.Author,
                Text = existingAnswer.Text
            });

            existingAnswer.Text = command.Text;
            return CommandResponse.Success(existingAnswer);
        }

        private CommandResponse ExecuteDeleteAnswer(DeleteAnswerCommand command)
        {
            foreach(var q in Questions)
            {
                var answer = q.Answers.SingleOrDefault(a => a.Id == command.AnswerId);
                if(answer != null)
                {
                    q.Answers.Remove(answer);
                    return CommandResponse.Success();
                }
            }
            return CommandResponse.Failure($"Answer {command.AnswerId} doesn't exist.");

        }

        private CommandResponse ExecuteVoteAnswer(VoteAnswerCommand command)
        {
            var existingAnswer = Questions.SelectMany(q => q.Answers).SingleOrDefault(a => a.Id == command.AnswerId);
            if (existingAnswer == null) return CommandResponse.Failure($"Answer {command.AnswerId} doesn't exist.");

            existingAnswer.Votes.Add(new Vote
            {
                Author = command.IssuedBy,
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Direction = command.VoteDirection
            });
            
            return CommandResponse.Success(existingAnswer);
        }

        private CommandResponse ExecuteCreateComment(CreateCommentCommand command)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Author = command.IssuedBy,
                Text = command.Text
            };

            foreach (var q in Questions)
            {
                if(q.Id == command.QuestionOrAnswerId)
                {
                    q.Comments.Add(comment);
                    return CommandResponse.Success(comment);
                }

                foreach(var a in q.Answers)
                {
                    if(a.Id == command.QuestionOrAnswerId)
                    {
                        a.Comments.Add(comment);
                        return CommandResponse.Success(comment);
                    }
                }
            }
            return CommandResponse.Failure($"Question or answer with id {command.QuestionOrAnswerId} does not exist.");
        }

        private CommandResponse ExecuteEditComment(EditCommentCommand command)
        {
            var existingComment = Questions.SelectMany(q => q.Comments).Concat(Questions.SelectMany(q => q.Answers.SelectMany(a => a.Comments)))
                 .SingleOrDefault(c => c.Id == command.CommentId);
            if(existingComment == null) return CommandResponse.Failure($"Comment with id {command.CommentId} does not exist.");

            existingComment.Text = command.Text;
            return CommandResponse.Success(existingComment);
        }

        private CommandResponse ExecuteDeleteComment(DeleteCommentCommand command)
        {
            foreach (var q in Questions)
            {
                var existingComment = q.Comments.SingleOrDefault(c => c.Id == command.CommentId);
                if(existingComment != null)
                {
                    q.Comments.Remove(existingComment);
                    return CommandResponse.Success();
                }

                foreach (var a in q.Answers)
                {
                    existingComment = a.Comments.SingleOrDefault(c => c.Id == command.CommentId);
                    if (existingComment != null)
                    {
                        a.Comments.Remove(existingComment);
                        return CommandResponse.Success();
                    }
                }
            }
            return CommandResponse.Failure($"Comment with id {command.CommentId} does not exist.");
        }

        private CommandResponse ExecuteAcceptAnswerCommand(AcceptAnswerCommand command)
        {
            foreach (var q in Questions)
            {
                var answer = q.Answers.SingleOrDefault(a => a.Id == command.AnswerId);
                if (answer != null)
                {
                    q.AcceptedAnswerId = answer.Id;
                    return CommandResponse.Success(answer);
                }
            }
            return CommandResponse.Failure($"Answer {command.AnswerId} doesn't exist.");
        }
        #endregion

        #region IPostQueryService
        public Question GetQuestion(Guid questionId)
        {
            return Questions.SingleOrDefault(q => q.Id == questionId);
        }

        public IEnumerable<Question> GetQuestions(string searchTerm)
        {
            return GetQuestionsInternal(searchTerm, null, null);
        }

        public IEnumerable<Question> GetQuestions(string searchTerm, int itemsPerPage, int page)
        {
            return GetQuestionsInternal(searchTerm, itemsPerPage, page);
        }
        
        public int GetQuestionCount(string searchTerm)
        {
            return GetQuestionsInternal(searchTerm, null, null).Count();
        }

        public IEnumerable<Tag> GetTags()
        {
            return Tags;
        }
        #endregion

        private IEnumerable<Question> GetQuestionsInternal(string searchTerm, int? itemsPerPage, int? page)
        {
            var search = string.Empty;
            var tag = string.Empty;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (searchTerm.StartsWith("[") && searchTerm.EndsWith("]")) tag = searchTerm.Trim('[', ']');
                else search = searchTerm;
            }

            var questions = string.IsNullOrEmpty(search) ? 
                Questions : 
                Questions.Where(q => q.Text.Contains(search));
            
            questions = string.IsNullOrEmpty(tag) ? 
                questions : 
                questions.Where(q => q.Tags.Any(t => t.Name.Equals(tag, StringComparison.InvariantCultureIgnoreCase)));

            if (page.HasValue && itemsPerPage.HasValue)
                questions = questions.Skip(page.Value * itemsPerPage.Value).Take(itemsPerPage.Value);

            return questions.OrderByDescending(q => q.Timestamp).ToList();
        }
    }
}
