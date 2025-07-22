using Clean_Architecture.Application.User.Command.CreateUser;
using Clean_Architecture.Application.User.Queries.GetUserWithPagination;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.User.Model;
using Clean_Architecture.Share.User.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserControllers : ControllerBase
{
    private readonly IMediator _mediator;
    public UserControllers(IMediator mediator) => _mediator = mediator;


    // create project
    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request)
    {
        var result = await _mediator.Send(new CreateUserCommand(request));
        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<ActionResult<BaseAPIResponse<List<UserDto>>>> GetUsersWithPaging([FromQuery] PaginationRequest request)
    {
        var query = new GetUserWithPagingQuery(request);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
