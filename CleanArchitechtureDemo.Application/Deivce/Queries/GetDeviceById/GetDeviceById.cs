using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Deivce.Queries.GetDeviceById;

public record GetDeviceByIdQuery(int Id) : IRequest<BaseAPIResponse<DeviceDto>>;

public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, BaseAPIResponse<DeviceDto>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetDeviceByIdQueryHandler> _logger;

    public GetDeviceByIdQueryHandler(
        IGenericRepository<Device> deviceRepository,
        IMapper mapper,
        ILogger<GetDeviceByIdQueryHandler> logger)
    {
        _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BaseAPIResponse<DeviceDto>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing GetDeviceByIdQuery for DeviceId: {DeviceId}", request.Id);

            var device = await _deviceRepository.GetByIdAsync(request.Id);
            if (device == null)
            {
                _logger.LogWarning("Device with Id {DeviceId} not found", request.Id);
                return BaseAPIResponse<DeviceDto>.FailResponse(
                    message: "Device not found",
                    errors: $"Device with Id {request.Id} does not exist"
                );
            }

            var deviceDto = _mapper.Map<DeviceDto>(device);
            return BaseAPIResponse<DeviceDto>.SuccessResponse(
                data: deviceDto,
                message: "Device retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetDeviceByIdQuery: {Message}", ex.Message);
            return BaseAPIResponse<DeviceDto>.FailResponse(
                message: "Failed to retrieve device",
                errors: ex.Message
            );
        }
    }
}
