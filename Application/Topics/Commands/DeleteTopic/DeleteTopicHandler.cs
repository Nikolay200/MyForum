
namespace Application.Topics.Commands.DeleteTopic
{
    public class DeleteTopicHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeleteTopicCommand, DeleteTopicResponse>
    {
        public async Task<DeleteTopicResponse> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(request.Id);

            var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

            if (topic == null || topic.IsDeleted)
            {
                throw new TopicNotFoundException(request.Id);
            }
            topic.IsDeleted = true;
            topic.DeletedAt = DateTimeOffset.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteTopicResponse(true);
        }
    }
}
