
namespace Application.Topics.Queries.GetTopic
{
    public class GetTopicHandler(IApplicationDbContext dbContext) : IQueryHandler<GetTopicQuery, GetTopicResponse>
    {
        public async Task<GetTopicResponse> Handle(GetTopicQuery request, CancellationToken cancellationToken)
        {
            TopicId id = TopicId.Of(request.id);
            var topic = await dbContext.Topics.FindAsync([id], cancellationToken);

            if (topic is null || topic.IsDeleted)
            {
                throw new TopicNotFoundException(request.id);
            }

            return new GetTopicResponse(topic.ToTopicResponseDto());
        }
    }
}
