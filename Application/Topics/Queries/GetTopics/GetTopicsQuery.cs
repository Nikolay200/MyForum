
namespace Application.Topics.Queries.GetTopics
{
    public record GetTopicsQuery(CancellationToken Token) : IQuery<GetTopicsResponse>
    {
    }
    public record GetTopicsResponse(List<TopicResponseDto> Topics) 
    { }
}
