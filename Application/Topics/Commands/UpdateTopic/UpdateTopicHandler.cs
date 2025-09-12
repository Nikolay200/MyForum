
using AutoMapper;

namespace Application.Topics.Commands.UpdateTopic
{
    public class UpdateTopicHandler(IApplicationDbContext dbContext, IMapper mapper)
        : ICommandHandler<UpdateTopicCommand, UpdateTopicResponse>
    {
        public async Task<UpdateTopicResponse> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(request.Id);

            var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

            if (topic == null || topic.IsDeleted)
            {
                throw new TopicNotFoundException(request.Id);
            }
            mapper.Map(request.UpdateTopic, topic);            

            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateTopicResponse(topic.ToTopicResponseDto());
        }

        
        private Topic UpdateTopic(UpdateTopicRequestDto request, Topic topic)
        {
            topic.Update(
                request.Title ?? topic.Title, 
                request.Summary ?? topic.Summary, 
                request.TopicType ?? topic.TopicType, 
                request.EventStart, 
                Location.Of(
                    request.Location.City ?? topic.Location.City, 
                    request.Location.Street ?? topic.Location.Street
                    )
                );

            return topic;
        }
    }
}
