using FluentValidation;

namespace Models.Validation
{
    public class BoardGameValidator : AbstractValidator<BoardGame>
    {
        public BoardGameValidator()
        {
            RuleFor(boardGame => boardGame.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(boardGame => boardGame.MinPlayers)
                .GreaterThanOrEqualTo((byte) 1);

                //TODO I forgot to ensure that maxPlayers number is not lesser than minPlayers number, to be fixed

            RuleFor(boardGame => boardGame.MaxPlayers)
                .GreaterThanOrEqualTo((byte) 1);

            RuleFor(boardGame => boardGame.MinRecommendedAge)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .LessThanOrEqualTo((byte) 18)
                .GreaterThanOrEqualTo((byte) 3);
        }
    }
}