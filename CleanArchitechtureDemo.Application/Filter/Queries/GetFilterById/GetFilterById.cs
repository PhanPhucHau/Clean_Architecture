using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Filter.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Filter.Queries.GetFilterById;

public record GetFilterByIdQuery(int Id) : IRequest<BaseAPIResponse<FilterDto>>;

public class GetFilterByIdQueryHandler : IRequestHandler<GetFilterByIdQuery, BaseAPIResponse<FilterDto>>
{
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Filter> _filterRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetFilterByIdQueryHandler> _logger;

    public GetFilterByIdQueryHandler(
        IGenericRepository<Clean_Architecture.Domain.Entities.Filter> filterRepository,
        IMapper mapper,
        ILogger<GetFilterByIdQueryHandler> logger)
    {
        _filterRepository = filterRepository ?? throw new ArgumentNullException(nameof(filterRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BaseAPIResponse<FilterDto>> Handle(GetFilterByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing GetDeviceByIdQuery for FilterDto: {FilterDto}", request.Id);

            var filter = await _filterRepository.GetByIdAsync(request.Id);
            if (filter == null)
            {
                _logger.LogWarning("Device with Id {FilterDto} not found", request.Id);
                return BaseAPIResponse<FilterDto>.FailResponse(
                    message: "Device not found",
                    errors: $"Device with Id {request.Id} does not exist"
                );
            }

            var filterDto = _mapper.Map<FilterDto>(filter);
            return BaseAPIResponse<FilterDto>.SuccessResponse(
                data: filterDto,
                message: "Device retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetDeviceByIdQuery: {Message}", ex.Message);
            return BaseAPIResponse<FilterDto>.FailResponse(
                message: "Failed to retrieve device",
                errors: ex.Message
            );
        }
    }
}
