using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using Clean_Architecture.Share.Device.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Deivce.Command.CreateDevice;
public record CreateDeviceCommand(DeviceRequest Request) : IRequest<BaseAPIResponse<DeviceDto>>;

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, BaseAPIResponse<DeviceDto>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateDeviceCommandHandler> _logger;

    public CreateDeviceCommandHandler(
        IGenericRepository<Device> deviceRepository,
        IMapper mapper,
        ILogger<CreateDeviceCommandHandler> logger)
    {
        _deviceRepository = deviceRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<DeviceDto>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing CreateDeviceCommand for Device: {DeviceName}", request.Request.Name);

            var device = Device.Create(
                name: request.Request.Name,
                userId: request.Request.UserId,
                deviceType: request.Request.DeviceType,
                deviceStatus: request.Request.DeviceStatus,
                brand: request.Request.Brand,
                model: request.Request.Model,
                installDate: request.Request.InstallDate
            );

            await _deviceRepository.AddAsync(device);

            var deviceDto = _mapper.Map<DeviceDto>(device);
            return BaseAPIResponse<DeviceDto>.SuccessResponse(
                data: deviceDto,
                message: "Device created successfully"
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error in CreateDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<DeviceDto>.FailResponse(
                message: "Invalid device data",
                errors: ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<DeviceDto>.FailResponse(
                message: "Failed to create device",
                errors: ex.Message
            );
        }
    }
}