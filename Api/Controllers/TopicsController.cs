using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(IMediator mediator) 
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Topic>>> GetAllTopics(CancellationToken token)
        {
            return Ok(await mediator.Send(new GetTopicsQuery(token)));
        }

        [HttpPost]
        public async Task<ActionResult> CreateTopic(CreateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            return Ok(null);
        }

        [HttpDelete("{topicId}")]
        public async Task<ActionResult<bool>> DeleteTopic(Guid topicId, CancellationToken token)
        {            
            return Ok("True");
        }

        [HttpPut("{topicId}")]
        public async Task<ActionResult<TopicResponseDto>> UpdateTopic(Guid topicId, [FromBody] UpdateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            return Ok(null);
        }

        [HttpGet("{topicId}")]
        public async Task<ActionResult<Topic>> GetTopic(Guid topicId, CancellationToken token)
        {
            return Ok(null);
        }
    }
}
