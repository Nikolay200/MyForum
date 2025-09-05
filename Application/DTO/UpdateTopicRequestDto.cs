
namespace Application.DTO
{
    public record UpdateTopicRequestDto(
            string Title,
            string Summary,
            string TopicType,
            LocationDto Location,
            DateTime EventStart
        );
}
