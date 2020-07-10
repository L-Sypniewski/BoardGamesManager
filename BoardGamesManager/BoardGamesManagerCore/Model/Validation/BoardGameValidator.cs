using FluentValidation;
using Models;

namespace BoardGamesManagerCore.Model.Validation
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

            RuleFor(boardGame => boardGame)
                .Must(MinPlayersIsLessThanOrEqualToMaxPlayers)
                .WithMessage($"{nameof(BoardGame)} cannot have maximum amount of players lesser than minimum amount of players");

            RuleFor(boardGame => boardGame.MaxPlayers)
                .GreaterThanOrEqualTo((byte) 1);

            RuleFor(boardGame => boardGame.MinRecommendedAge)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .LessThanOrEqualTo((byte) 18)
                .GreaterThanOrEqualTo((byte) 3);
        }

        private static bool MinPlayersIsLessThanOrEqualToMaxPlayers(BoardGame boardGame) => boardGame.MinPlayers <= boardGame.MaxPlayers;
    }
}