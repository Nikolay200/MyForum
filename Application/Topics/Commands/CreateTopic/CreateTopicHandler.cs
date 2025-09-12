using AutoMapper;

namespace Application.Topics.Commands.CreateTopic
{
    public class CreateTopicHandler(IApplicationDbContext dbContext, IMapper mapper)
        : ICommandHandler<CreateTopicCommand, CreateTopicResponse>
    {
        public async Task<CreateTopicResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var newTopic = mapper.Map<Topic>(request.TopicDto);
            dbContext.Topics.Add(newTopic);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateTopicResponse(newTopic.ToTopicResponseDto());
        }
    }
}
