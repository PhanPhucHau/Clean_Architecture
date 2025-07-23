using Clean_Architecture.Application.Notify.Command.CreateNotify;
using Clean_Architecture.Application.Notify.Queries.GetAllNotify;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Notify.Model;
using Clean_Architecture.Share.Notify.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotifyController : ControllerBase
{
    private readonly IMediator _mediator;
    public NotifyController(IMediator mediator) => _mediator = mediator;

    // create deivce
    [HttpPost("create")]
    public async Task<IActionResult> CreateNotifyAsync([FromBody] NotifyRequest request)
    {
        var result = await _mediator.Send(new CreateNotifyCommand(request));
        return Ok(result);
    }

    // get all notify
    [HttpGet]
    public async Task<ActionResult<BaseAPIResponse<List<NotifyDto>>>> GetAllNotify()
    {
        var query = new GetAllNotifyQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
