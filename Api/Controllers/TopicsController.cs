using Application.Topics.Queries.GetTopic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(IMediator mediator) 
        : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetAllTopics(CancellationToken token)
        {
            return Results.Ok(await mediator.Send(new GetTopicsQuery(token)));
        }

        [HttpGet("{topicId}")]
        public async Task<IResult> GetTopic(Guid topicId, CancellationToken token)
        {
            return Results.Ok(await mediator.Send(new GetTopicQuery(topicId, token)));
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
    }
}
