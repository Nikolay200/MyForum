using Application.DTO;
using Application.Topics;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(ITopicsService topicsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Topic>>> GetAllTopics(CancellationToken token)
        {
            return Ok(await topicsService.GetAllTopicsAsync(token));
        }

        [HttpPost]
        public async Task<ActionResult> CreateTopic(CreateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            return Ok(await topicsService.CreateTopicAsync(topicRequestDto, token));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteTopic(Guid topicId, CancellationToken token)
        {
            await topicsService.DeleteTopicAsync(topicId, token);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<TopicResponseDto>> UpdateTopic(Guid topicId, UpdateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            return Ok(await topicsService.UpdateTopicAsync(topicId, topicRequestDto, token));
        }

        [HttpGet("{topicId}")]
        public async Task<ActionResult<Topic>> GetTopic(Guid topicId, CancellationToken token)
        {
            return Ok(await topicsService.GetTopicAsync(topicId, token));
        }
    }
}
