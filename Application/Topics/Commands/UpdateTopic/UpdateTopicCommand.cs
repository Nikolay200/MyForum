
namespace Application.Topics.Commands.UpdateTopic
{
    public record UpdateTopicCommand(Guid Id, UpdateTopicRequestDto UpdateTopic, CancellationToken Token)
        :ICommand<UpdateTopicResponse>
    {
    }

    public record UpdateTopicResponse(TopicResponseDto UpdatedTopic)
    {
    }
}
