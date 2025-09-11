

namespace Application.Topics.Queries.GetTopics
{
    public class GetTopicsHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetTopicsQuery, GetTopicsResponse>
    {
        public async Task<GetTopicsResponse> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
        {
            var topics = await dbContext.Topics
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync(cancellationToken);
            return new GetTopicsResponse(topics.ToTopicResponseDtoList());
        }
    }
}
