using AutoMapper;
using BoardGamesServices.Mapping;
using Xunit;

namespace BoardGamesServicesTest.Mapping
{
    public class BoardGamesMappingProfileTest
    {
        private readonly IConfigurationProvider _configuration;

        public BoardGamesMappingProfileTest()
        {
            _configuration = new MapperConfiguration(config =>
                                                         config.AddProfile<BoardGamesMappingProfile>());
        }

        [Fact(DisplayName = "When mapping BoardGameDto from/to BoardGame entity all properties should be mapped correctly")]
        public void When_mapping_BoardGameDto_from_to_BoardGame_entity_all_properties_should_be_mapped_correctly()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}