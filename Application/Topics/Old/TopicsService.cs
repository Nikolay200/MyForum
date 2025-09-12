using Application.Data.DataBaseContext;

namespace Application.Topics.Old
{
    [Obsolete("Устарело", true)]
    public class TopicsService(IApplicationDbContext dbContext, ILogger<TopicsService> logger) : ITopicsService
    {

        public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            Topic newTopic = Topic.Create(
                TopicId.Of(Guid.NewGuid()),
                    topicRequestDto.Title,
                    topicRequestDto.EventStart,
                    topicRequestDto.TopicType,
                    topicRequestDto.Summary,
                    Location.Of(topicRequestDto.Location.City, topicRequestDto.Location.Street)
                );
            dbContext.Topics.Add(newTopic);
            await dbContext.SaveChangesAsync(token);
            return newTopic.ToTopicResponseDto();
        }

        public async Task DeleteTopicAsync(Guid currentTopicId, CancellationToken token)
        {
            await StartTiming(10, 200, logger, token);
            TopicId topicId = TopicId.Of(currentTopicId);

            var topic = await dbContext.Topics.FindAsync([topicId]);

            if (topic == null || topic.IsDeleted)
            {
                throw new TopicNotFoundException(currentTopicId);
            }
            topic.IsDeleted = true;
            topic.DeletedAt = DateTimeOffset.UtcNow;
            //dbContext.Topics.Remove(topic);
            await dbContext.SaveChangesAsync(token);
        }

        public async Task<Topic> GetTopicAsync(Guid topicId, CancellationToken token)
        {
            await StartTiming(10, 200, logger, token);

            TopicId id = TopicId.Of(topicId);
            var topic = await dbContext.Topics.FindAsync([id]);

            if (topic is null || topic.IsDeleted)
            {
                throw new TopicNotFoundException(topicId);
            }

            return topic;
        }

        private static async Task StartTiming(int counter, int delay, ILogger<TopicsService> logger, CancellationToken token)
        {
            for (int i = 1; i <= counter; i++)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(delay, token);
                logger.LogInformation($"{i} сек.");
            }
        }

        public async Task<List<TopicResponseDto>> GetAllTopicsAsync(CancellationToken token)
        {
            try
            {
                await StartTiming(10, 200, logger, token);

                var topics = await dbContext.Topics
                    .AsNoTracking()
                    .Where(x => x.IsDeleted == false)
                    .ToListAsync(token);
                return topics.ToTopicResponseDtoList();
            }
            catch (Exception)
            {
                return new List<TopicResponseDto>();
            }
        }

        public async Task<TopicResponseDto> UpdateTopicAsync(Guid currentTopicId, UpdateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            await StartTiming(10, 200, logger, token);
            TopicId topicId = TopicId.Of(currentTopicId);

            var topic = await dbContext.Topics.FindAsync([topicId]);

            if (topic == null || topic.IsDeleted)
            {
                throw new TopicNotFoundException(currentTopicId);
            }

            topic.Title = topicRequestDto.Title ?? topic.Title;
            topic.Summary = topicRequestDto.Summary ?? topic.Summary;
            topic.TopicType = topicRequestDto.TopicType ?? topic.TopicType;
            topic.EventStart = topicRequestDto.EventStart;
            topic.Location = Location.Of(
                topicRequestDto.Location.City,
                topicRequestDto.Location.Street
                );

            await dbContext.SaveChangesAsync(token);
            return topic.ToTopicResponseDto();
        }
    }
}
