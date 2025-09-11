
namespace Application.Topics.Queries.GetTopic
{
    public record GetTopicQuery(Guid id, CancellationToken Token):IQuery<GetTopicResponse>;

    public record GetTopicResponse(TopicResponseDto Topic);
}
