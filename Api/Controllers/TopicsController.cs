using Microsoft.AspNetCore.Mvc;

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

        [HttpDelete("{topicId}")]
        public async Task<ActionResult<bool>> DeleteTopic(Guid topicId, CancellationToken token)
        {
            await topicsService.DeleteTopicAsync(topicId, token);
            return Ok("True");
        }

        [HttpPut("{topicId}")]
        public async Task<ActionResult<TopicResponseDto>> UpdateTopic(Guid topicId, [FromBody] UpdateTopicRequestDto topicRequestDto, CancellationToken token)
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
