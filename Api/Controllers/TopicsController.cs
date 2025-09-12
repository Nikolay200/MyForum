using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
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
        public async Task<IResult> CreateTopic(CreateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            var response = await mediator.Send(new CreateTopicCommand(topicRequestDto));
            return Results.Created($"/topics/{response.Response.Id}", response.Response);
        }

        [HttpDelete("{topicId}")]
        public async Task<IResult> DeleteTopic(Guid topicId, CancellationToken token)
        {
            return Results.Ok(await mediator.Send(new DeleteTopicCommand(topicId, token)));
        }

        [HttpPut("{topicId}")]
        public async Task<IResult> UpdateTopic(Guid topicId, [FromBody] UpdateTopicRequestDto topicRequestDto, CancellationToken token)
        {
            return Results.Ok(await mediator.Send(new UpdateTopicCommand(topicId, topicRequestDto, token)));
        }
    }
}
