using AutoMapper;
using BoardGamesServices.Mapping;
using Xunit;

namespace BoardGamesServicesTest.Mapping
{
    public class BoardGamesMappingProfileTest
    {
        [Fact(DisplayName = "When mapping BoardGameDto from/to BoardGame entity all properties should be mapped correctly")]
        public void When_mapping_BoardGameDto_from_to_BoardGame_entity_all_properties_should_be_mapped_correctly()
        {
            var configuration = new MapperConfiguration(config =>
                                                            config.AddProfile<BoardGamesMappingProfile>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact(DisplayName = "When mapping BoardGameLastDisplaysDto from/to BoardGamesDisplayLog entity all properties should be mapped correctly")]
        public void When_mapping_BoardGameLastDisplaysDto_from_to_ApplicationLog_entity_all_properties_should_be_mapped_correctly()
        {
            var configuration = new MapperConfiguration(config =>
                                                            config.AddProfile<ApplicationLogMappingProfile>());

            configuration.AssertConfigurationIsValid();
        }
    }
}