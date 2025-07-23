using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using Clean_Architecture.Share.Device.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Deivce.Command.UpdateDevice;
public record UpdateDeviceCommand(int Id, DeviceRequest Request) : IRequest<BaseAPIResponse<DeviceDto>>;

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, BaseAPIResponse<DeviceDto>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.User> _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateDeviceCommandHandler> _logger;

    public UpdateDeviceCommandHandler(
        IGenericRepository<Device> deviceRepository,
        IGenericRepository<Clean_Architecture.Domain.Entities.User> userRepository,
        IMapper mapper,
        ILogger<UpdateDeviceCommandHandler> logger)
    {
        _deviceRepository = deviceRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<DeviceDto>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing UpdateDeviceCommand for DeviceId: {DeviceId}", request.Id);

            var device = await _deviceRepository.GetByIdAsync(request.Id);
            if (device == null)
            {
                _logger.LogWarning("Device with Id {DeviceId} not found", request.Id);
                return BaseAPIResponse<DeviceDto>.FailResponse(
                    message: "Device not found",
                    errors: $"Device with Id {request.Id} does not exist"
                );
            }

            var userExists = await _userRepository.ExistsAsync(request.Request.UserId);
            if (!userExists)
            {
                _logger.LogWarning("User with Id {UserId} not found", request.Request.UserId);
                return BaseAPIResponse<DeviceDto>.FailResponse(
                    message: "User not found",
                    errors: $"User with Id {request.Request.UserId} does not exist"
                );
            }

            device.Update(
                name: request.Request.Name,
                userId: request.Request.UserId,
                deviceType: request.Request.DeviceType,
                deviceStatus: request.Request.DeviceStatus,
                brand: request.Request.Brand,
                model: request.Request.Model,
                installDate: request.Request.InstallDate
            );

            await _deviceRepository.UpdateByIdAsync(request.Id, device);

            var deviceDto = _mapper.Map<DeviceDto>(device);
            return BaseAPIResponse<DeviceDto>.SuccessResponse(
                data: deviceDto,
                message: "Device updated successfully"
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error in UpdateDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<DeviceDto>.FailResponse(
                message: "Invalid device data",
                errors: ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<DeviceDto>.FailResponse(
                message: "Failed to update device",
                errors: ex.Message
            );
        }
    }
}