using FluentValidation;

namespace Models.Validation
{
    public class BoardGameValidator : AbstractValidator<BoardGame>
    {
        public BoardGameValidator()
        {
            RuleFor(boardGame => boardGame.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
    }
}