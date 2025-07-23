using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Deivce.Queries.GetAllDevice;

public record GetAllDeviceQuery : IRequest<BaseAPIResponse<List<DeviceDto>>>;

public class GetAllDeviceQueryHandler : IRequestHandler<GetAllDeviceQuery, BaseAPIResponse<List<DeviceDto>>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllDeviceQueryHandler> _logger;

    public GetAllDeviceQueryHandler(IGenericRepository<Device> deviceRepository, IMapper mapper, ILogger<GetAllDeviceQueryHandler> logger)
    {
        _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BaseAPIResponse<List<DeviceDto>>> Handle(GetAllDeviceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting GetAllDeviceQuery");
            var devices = await _deviceRepository.GetQuery().ToListAsync(cancellationToken);
            var deviceDtos = _mapper.Map<List<DeviceDto>>(devices);
            if (deviceDtos == null)
            {
                _logger.LogError("AutoMapper failed to map devices to UserDto");
                throw new Exception("AutoMapper failed to map devices to UserDto.");
            }

            return BaseAPIResponse<List<DeviceDto>>.SuccessResponse(
                data: deviceDtos,
                message: "All devices retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllDeviceQuery: {Message}", ex.Message);
            return BaseAPIResponse<List<DeviceDto>>.FailResponse(
                message: "Failed to retrieve devices",
                errors: ex.Message
            );
        }
    }
}
