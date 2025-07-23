using Clean_Architecture.Application.Filter.Command.CreateFilter;
using Clean_Architecture.Application.Filter.Command.DeleteFilter;
using Clean_Architecture.Application.Filter.Command.UpdateFilter;
using Clean_Architecture.Application.Filter.Queries.GetAllFilter;
using Clean_Architecture.Application.Filter.Queries.GetFilterById;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Filter.Model;
using Clean_Architecture.Share.Filter.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FilterControllers : ControllerBase
{
    private readonly IMediator _mediator;
    public FilterControllers(IMediator mediator) => _mediator = mediator;

    // create filter
    [HttpPost("create")]
    public async Task<IActionResult> CreateFilterAsync([FromBody] FilterRequest request)
    {
        var result = await _mediator.Send(new CreateFilterCommand(request));
        return Ok(result);
    }

    // get filter with paging
    // [HttpGet("paged")]
    //public async Task<ActionResult<BaseAPIResponse<List<FilterDto>>>> GetFilterWithPaging([FromQuery] PaginationRequest request)
    //{
    //    //var query = new GetFilterWithPagingQuery(request);
    //    //var result = await _mediator.Send(query);
    //    //return Ok(result);
    //}

    // get all filter
    [HttpGet]
    public async Task<ActionResult<BaseAPIResponse<List<FilterDto>>>> GetFilterUsers()
    {
        var query = new GetAllFilterQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // update filter by id
    [HttpPut("{id}")]
    public async Task<ActionResult<BaseAPIResponse<FilterDto>>> UpdateFilter(int id, [FromBody] FilterRequest request)
    {
        var command = new UpdateFilterCommand(id, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseAPIResponse<bool>>> DeleteFilter(int id)
    {
        var command = new DeleteFilterCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseAPIResponse<FilterDto>>> GetFilterById(int id)
    {
        var query = new GetFilterByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
