
namespace Application.DTO
{
    public record CreateTopicRequestDto(
            string Title,
            string Summary,
            string TopicType,
            LocationDto Location,
            DateTime EventStart
        );
}
