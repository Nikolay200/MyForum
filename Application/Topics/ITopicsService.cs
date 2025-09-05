
using Application.DTO;

namespace Application.Topics
{
    public interface ITopicsService
    {
        Task<List<TopicResponseDto>> GetAllTopicsAsync(CancellationToken token);
        Task<Topic> GetTopicAsync(Guid topicId, CancellationToken token);
        Task<Topic> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken token);
        Task<Topic> UpdateTopicAsync(Guid topicId, UpdateTopicRequestDto topicRequestDto, CancellationToken token);
        Task DeleteTopicAsync(Guid topicId, CancellationToken token);
    }
}
