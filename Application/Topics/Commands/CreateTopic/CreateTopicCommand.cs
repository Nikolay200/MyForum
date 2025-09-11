namespace Application.Topics.Commands.CreateTopic
{
    public record CreateTopicCommand(CreateTopicRequestDto TopicDto) : ICommand<CreateTopicResponse>;

    public record CreateTopicResponse(TopicResponseDto Response);

}
