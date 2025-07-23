using Clean_Architecture.Application.User.Command.CreateUser;
using Clean_Architecture.Application.User.Command.DeleteUser;
using Clean_Architecture.Application.User.Command.UpdateUser;
using Clean_Architecture.Application.User.Queries.GetAllUser;
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


    // create user
    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request)
    {
        var result = await _mediator.Send(new CreateUserCommand(request));
        return Ok(result);
    }

    // get user with paging
    [HttpGet("paged")]
    public async Task<ActionResult<BaseAPIResponse<List<UserDto>>>> GetUsersWithPaging([FromQuery] PaginationRequest request)
    {
        var query = new GetUserWithPagingQuery(request);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // get all users
    [HttpGet]
    public async Task<ActionResult<BaseAPIResponse<List<UserDto>>>> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // update user by id
    [HttpPut("{id}")]
    public async Task<ActionResult<BaseAPIResponse<UserDto>>> UpdateUser(int id, [FromBody] UserRequest request)
    {
        var command = new UpdateUserCommand(id, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    // delete user by id
    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseAPIResponse<bool>>> DeleteUser(int id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
