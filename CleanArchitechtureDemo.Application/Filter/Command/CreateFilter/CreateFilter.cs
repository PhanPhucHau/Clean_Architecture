using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Filter.Model;
using Clean_Architecture.Share.Filter.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Filter.Command.CreateFilter;

public record CreateFilterCommand(FilterRequest Request) : IRequest<BaseAPIResponse<FilterDto>>;

public class CreateFilterCommandHandler : IRequestHandler<CreateFilterCommand, BaseAPIResponse<FilterDto>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Filter> _filterRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateFilterCommandHandler> _logger;

    public CreateFilterCommandHandler(
        IGenericRepository<Device> deviceRepository,
        IGenericRepository<Clean_Architecture.Domain.Entities.Filter> filterRepository,
        IMapper mapper,
        ILogger<CreateFilterCommandHandler> logger)
    {
        _deviceRepository = deviceRepository;
        _filterRepository = filterRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<FilterDto>> Handle(CreateFilterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing CreateDeviceCommand for Device: {DeviceName}", request.Request.Name);

            var filter = Clean_Architecture.Domain.Entities.Filter.Create(
                name: request.Request.Name,
                description: request.Request.Description,
                filterType: request.Request.FilterType,
                installDate: request.Request.InstallDate,
                lifeDate: request.Request.LifeDate,
                lateDate: request.Request.LateDate,
                deviceId: request.Request.DeviceId
            );

            await _filterRepository.AddAsync(filter);

            var filterDto = _mapper.Map<FilterDto>(filter);
            return BaseAPIResponse<FilterDto>.SuccessResponse(
                data: filterDto,
                message: "Filter created successfully"
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error in CreateFilterCommand: {Message}", ex.Message);
            return BaseAPIResponse<FilterDto>.FailResponse(
                message: "Invalid filter data",
                errors: ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateFilterCommand: {Message}", ex.Message);
            return BaseAPIResponse<FilterDto>.FailResponse(
                message: "Failed to create filter",
                errors: ex.Message
            );
        }
    }
}
