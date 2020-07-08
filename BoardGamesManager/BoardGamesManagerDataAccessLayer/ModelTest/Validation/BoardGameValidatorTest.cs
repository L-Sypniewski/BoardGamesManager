using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Models;
using Models.Validation;
using Xunit;

namespace ModelTest.Validation
{
    public class BoardGameValidatorTest
    {
        private readonly BoardGameValidator _sut;

        public BoardGameValidatorTest()
        {
            _sut = new BoardGameValidator();
        }

        [Theory(DisplayName = "When validated BoardGame is correct validation result should be valid")]
        [ClassData(typeof(BoardGameValidData))]
        public void When_validated_BoardGame_is_correct_validation_result_should_be_valid(BoardGame boardGame)
        {
            var validationResult = _sut.Validate(boardGame).IsValid;


            validationResult
                .Should()
                .BeTrue($"validation result should be valid for a valid {nameof(BoardGame)}");
        }

        private class BoardGameValidData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {new BoardGame("Name", 1, 1, 5)};
                yield return new object[] {new BoardGame("Other name", 13, 26, 16)};
                yield return new object[] {new BoardGame("111", byte.MaxValue, byte.MaxValue, 5)};
                yield return new object[] {new BoardGame(" NameSurroundedWithSpaces ", 10, 10, 3)};
                yield return new object[] {new BoardGame("MySuper game !@#$%^&*(_+", 10, 10, 18)};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "When validated BoardGame has incorrect name validation result should be invalid")]
        [ClassData(typeof(BoardGameInvalidData))]
        public void When_validated_BoardGame_has_incorrect_name_validation_result_should_be_invalid(BoardGame boardGame)
        {
            var validationResult = _sut.Validate(boardGame);


            validationResult
                .IsValid
                .Should()
                .BeFalse($"validation result should be invalid for an invalid name of {nameof(BoardGame)}");
        }

        private class BoardGameInvalidData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {new BoardGameBuilder().WithName(string.Empty).Build()};
                yield return new object[] {new BoardGameBuilder().WithName("\n\t").Build()};
                yield return new object[] {new BoardGameBuilder().WithName(" ").Build()};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "When validated BoardGame has minimal recommended age greater than 18 years or lesser than 3 result should be invalid")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(19)]
        [InlineData(99)]
        public void When_validated_BoardGame_has_minimal_recommended_age_greater_than_18_years_or_lesser_than_3_result_should_be_invalid(byte minRecommendedAge)
        {
            var boardGame = new BoardGameBuilder()
                            .WithMinRecommendedAge(minRecommendedAge)
                            .Build();


            var validationResult = _sut.Validate(boardGame);


            validationResult
                .IsValid
                .Should()
                .BeFalse($"validation result should be invalid if the minimal recommended age of {nameof(BoardGame)} is not between 3 and 18");
        }

        [Fact(DisplayName = "When validated BoardGame has minimal amount of players lesser than one result should be invalid")]
        public void When_validated_BoardGame_has_minimal_amount_of_players_lesser_than_one_result_should_be_invalid()
        {
            var boardGame = new BoardGameBuilder()
                            .WithMinPlayers(0)
                            .Build();
            ;

            var validationResult = _sut.Validate(boardGame);


            validationResult
                .IsValid
                .Should()
                .BeFalse($"{nameof(BoardGame)} must have at least one player");
        }

        // Copy-pasted the previous test here, but in my opinion trying to DRY test code at all cost may lead to less readable tests
        [Fact(DisplayName = "When validated BoardGame has maximum amount of players lesser than one result should be invalid")]
        public void When_validated_BoardGame_has_maximum_amount_of_players_lesser_than_one_result_should_be_invalid()
        {
            var boardGame = new BoardGameBuilder()
                            .WithMaxPlayers(0)
                            .Build();
            ;

            var validationResult = _sut.Validate(boardGame);


            validationResult
                .IsValid
                .Should()
                .BeFalse($"{nameof(BoardGame)} must have at least one player");
        }

        [Theory(DisplayName = "When validated BoardGame has too long name result should be invalid")]
        [InlineData(61, true)]
        [InlineData(60, false)]
        [InlineData(100, true)]
        [InlineData(59, false)]
        [InlineData(1, false)]
        public void When_validated_BoardGame_has_too_long_name_result_should_be_invalid(int stringLength, bool isNameTooLong)
        {
            var nameOfGivenLength = new string('x', stringLength);
            var boardGame = new BoardGameBuilder()
                            .WithName(nameOfGivenLength)
                            .Build();


            var validationResult = _sut.Validate(boardGame);


            var nameHasCorrectLength = !isNameTooLong;
            validationResult
                .IsValid
                .Should()
                .Be(nameHasCorrectLength, $"for {nameof(BoardGame)}s name of length equal to {stringLength} validation should equal {nameHasCorrectLength}");
        }
    }
}