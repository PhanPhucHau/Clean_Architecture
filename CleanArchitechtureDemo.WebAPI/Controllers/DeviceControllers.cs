using Clean_Architecture.Application.Deivce.Command.CreateDevice;
using Clean_Architecture.Application.Deivce.Command.DeleteDevice;
using Clean_Architecture.Application.Deivce.Command.UpdateDevice;
using Clean_Architecture.Application.Deivce.Queries.GetAllDevice;
using Clean_Architecture.Application.Deivce.Queries.GetDeviceById;
using Clean_Architecture.Application.Deivce.Queries.GetDeviceWithPagination;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using Clean_Architecture.Share.Device.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DeviceControllers : ControllerBase
{
    private readonly IMediator _mediator;
    public DeviceControllers(IMediator mediator) => _mediator = mediator;

    // create deivce
    [HttpPost("create")]
    public async Task<IActionResult> CreateDeviceAsync([FromBody] DeviceRequest request)
    {
        var result = await _mediator.Send(new CreateDeviceCommand(request));
        return Ok(result);
    }

    // get device with paging
    [HttpGet("paged")]
    public async Task<ActionResult<BaseAPIResponse<List<DeviceDto>>>> GetDevicesWithPaging([FromQuery] PaginationRequest request)
    {
        var query = new GetDeviceWithPagingQuery(request);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // get all devices
    [HttpGet]
    public async Task<ActionResult<BaseAPIResponse<List<DeviceDto>>>> GetDeviceUsers()
    {
        var query = new GetAllDeviceQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // update device by id
    [HttpPut("{id}")]
    public async Task<ActionResult<BaseAPIResponse<DeviceDto>>> UpdateDevice(int id, [FromBody] DeviceRequest request)
    {
        var command = new UpdateDeviceCommand(id, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseAPIResponse<bool>>> DeleteDevice(int id)
    {
        var command = new DeleteDeviceCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseAPIResponse<DeviceDto>>> GetDeviceById(int id)
    {
        var query = new GetDeviceByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
