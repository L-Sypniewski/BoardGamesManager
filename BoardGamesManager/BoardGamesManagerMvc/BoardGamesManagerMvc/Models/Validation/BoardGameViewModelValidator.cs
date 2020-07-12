using FluentValidation;
using Models;

namespace BoardGamesManagerMvc.Models.Validation
{
    // Logic is duplicated with BoardGameValidator, the code should be made DRY
    public class BoardGameViewModelValidator : AbstractValidator<BoardGameViewModel>
    {
        public BoardGameViewModelValidator()
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

        private static bool MinPlayersIsLessThanOrEqualToMaxPlayers(BoardGameViewModel boardGame) => boardGame.MinPlayers <= boardGame.MaxPlayers;
    }
}