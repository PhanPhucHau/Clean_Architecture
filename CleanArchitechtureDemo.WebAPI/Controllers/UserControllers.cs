using Clean_Architecture.Application.User.Command.CreateUser;
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
}
