﻿
using Application.DTO;

namespace Application.Topics
{
    public interface ITopicsService
    {
        Task<List<TopicResponseDto>> GetAllTopicsAsync(CancellationToken token);
        Task<Topic> GetTopicAsync(Guid topicId, CancellationToken token);
        Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken token);
        Task<TopicResponseDto> UpdateTopicAsync(Guid topicId, UpdateTopicRequestDto topicRequestDto, CancellationToken token);
        Task DeleteTopicAsync(Guid topicId, CancellationToken token);
    }
}
