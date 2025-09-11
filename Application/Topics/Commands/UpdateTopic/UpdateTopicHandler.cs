

using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Topics.Commands.UpdateTopic
{
    public class UpdateTopicHandler(IApplicationDbContext dbContext)
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

            var updatedTopic = UpdateTopic(request.UpdateTopic, topic);

            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateTopicResponse(topic.ToTopicResponseDto());
        }

        private Topic UpdateTopic(UpdateTopicRequestDto request, Topic topic)
        {            
            topic.Title = request.Title ?? topic.Title;
            topic.Summary = request.Summary ?? topic.Summary;
            topic.TopicType = request.TopicType ?? topic.TopicType;
            topic.EventStart = request.EventStart;
            topic.Location = Location.Of(
                request.Location.City,
                request.Location.City
                );
            return topic;
        }
    }
}
