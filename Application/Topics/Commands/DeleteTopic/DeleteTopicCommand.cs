
namespace Application.Topics.Commands.DeleteTopic
{
    public record DeleteTopicCommand(Guid Id, CancellationToken Token)
        : ICommand<DeleteTopicResponse>;


    public record DeleteTopicResponse(bool IsSuccess);

}
