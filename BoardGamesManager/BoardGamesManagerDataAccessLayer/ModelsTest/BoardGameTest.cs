using FluentAssertions;
using FluentAssertions.Execution;
using Models;
using Xunit;

namespace ModelsTest
{
    public class BoardGameTest
    {
        [Theory(DisplayName = "When initialized with constructor BoardGame should have correct properties' values")]
        [InlineData("name", 12, 24, 48, "name")]
        [InlineData("  nameWithLeadingSpace", byte.MinValue, byte.MinValue, byte.MinValue, "nameWithLeadingSpace")]
        [InlineData("nameWithTrailingSpaces     ", byte.MaxValue, byte.MaxValue, byte.MaxValue, "nameWithTrailingSpaces")]
        [InlineData("      !#@#11234NameSurroundedWithSpacesAndTabs         ", byte.MaxValue, byte.MaxValue, byte.MaxValue, "!#@#11234NameSurroundedWithSpacesAndTabs")]
        public void When_initialized_with_constructor_BoardGame_should_be_correctly_initialized(string name,
                                                                                                byte minPlayers,
                                                                                                byte maxPlayers,
                                                                                                byte minRecommendedAge,
                                                                                                string expectedName)
        {
            var actualBoardGame = new BoardGame(name, minPlayers, maxPlayers, minRecommendedAge);

            using (new AssertionScope())
            {
                actualBoardGame.Name
                               .Should()
                               .Be(expectedName, $"{nameof(BoardGame.Name)} should be equal to trimmed string passed to a constructor");

                actualBoardGame.MinPlayers.Should()
                               .Be(minPlayers, $"{nameof(BoardGame.MinPlayers)} should be equal to the value passed to a constructor");

                actualBoardGame.MaxPlayers.Should()
                               .Be(maxPlayers, $"{nameof(BoardGame.MaxPlayers)} should be equal to the value passed to a constructor");

                actualBoardGame.MinRecommendedAge.Should()
                               .Be(minRecommendedAge, $"{nameof(BoardGame.MinRecommendedAge)} should be equal to the value passed to a constructor");
            }
        }
    }
}