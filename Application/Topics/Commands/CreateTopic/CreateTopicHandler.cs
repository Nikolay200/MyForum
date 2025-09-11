namespace Application.Topics.Commands.CreateTopic
{
    public class CreateTopicHandler(IApplicationDbContext dbContext)
        : ICommandHandler<CreateTopicCommand, CreateTopicResponse>
    {
        public async Task<CreateTopicResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var newTopic = CreateTopic(request.TopicDto);
            dbContext.Topics.Add(newTopic);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateTopicResponse(newTopic.ToTopicResponseDto());
        }

        private Topic CreateTopic(CreateTopicRequestDto request)
        {
            return Topic.Create(
                       TopicId.Of(Guid.NewGuid()),
                       request.Title,
                       request.EventStart,
                       request.TopicType,
                       request.Summary,
                       Location.Of(request.Location.City, request.Location.Street)
                       );
        }
    }
}
