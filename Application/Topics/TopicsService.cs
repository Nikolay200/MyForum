
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

        public Task<Topic> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            throw new NotImplementedException();
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

                for (int i = 1; i <= 10; i++)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(200, token);
                    logger.LogInformation($"{i} сек.");
                }     
                TopicId id = TopicId.Of(topicId); 
                var topic = await dbContext.Topics.FindAsync([id]);

                if(topic is null)
                {
                    throw new TopicNotFoundException(topicId);
                }

                return topic;

        }

        public async Task<List<TopicResponseDto>> GetAllTopicsAsync(CancellationToken token)
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(200, token);
                    logger.LogInformation($"{i} сек.");
                }
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
