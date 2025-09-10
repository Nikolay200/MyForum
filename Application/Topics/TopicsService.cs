
using Application.Data.DataBaseContext;
using Application.DTO;
using Application.Exceptions;
using Application.Extensions;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Topics
{
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

        public async Task DeleteTopicAsync(Guid topicId, CancellationToken token)
        {
            //try
            //{
            //    for (int i = 1; i <= 10; i++)
            //    {
            //        token.ThrowIfCancellationRequested();
            //        await Task.Delay(200, token);
            //        logger.LogInformation($"{i} сек.");
            //    }
            //    var topics = await GetAllTopicsAsync(token);
            //    var currentTopic = topics.FirstOrDefault();
            //    return currentTopic;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            throw new NotImplementedException();
        }

        public async Task<Topic> GetTopicAsync(Guid topicId, CancellationToken token)
        {
            await StartTiming(10, 200, logger, token);

            TopicId id = TopicId.Of(topicId);
            var topic = await dbContext.Topics.FindAsync([id]);

            if (topic is null)
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

                var topics = await dbContext.Topics.AsNoTracking().ToListAsync(token);
                return topics.ToTopicResponseDtoList();
            }
            catch (Exception)
            {
                return new List<TopicResponseDto>();
            }
        }

        public Task<Topic> UpdateTopicAsync(Guid topicId, UpdateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
