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
                yield return new object[] {new BoardGame("Name", byte.MaxValue, byte.MaxValue, byte.MaxValue)};
                yield return new object[] {new BoardGame("111", byte.MinValue, byte.MinValue, byte.MinValue)};
                yield return new object[] {new BoardGame(" NameSurroundedWithSpaces ", 10, 10, 10)};
                yield return new object[] {new BoardGame("MySuper game !@#$%^&*(_+", 10, 10, 10)};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "When validated BoardGame is incorrect validation result should be invalid")]
        [ClassData(typeof(BoardGameInvalidData))]
        public void When_validated_BoardGame_is_incorrect_validation_result_should_be_invalid(BoardGame boardGame, string expectedMessageEnding)
        {
            var validationResult = _sut.Validate(boardGame);

            validationResult
                .IsValid
                .Should()
                .BeFalse($"validation result should be invalid for an invalid {nameof(BoardGame)}");
        }

        private class BoardGameInvalidData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {new BoardGame(string.Empty, byte.MaxValue, byte.MaxValue, byte.MaxValue), "be empty"};
                yield return new object[] {new BoardGame("\n\t", byte.MaxValue, byte.MaxValue, byte.MaxValue), "be empty"};
                yield return new object[] {new BoardGame(" ", byte.MaxValue, byte.MaxValue, byte.MaxValue), "be empty"};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}