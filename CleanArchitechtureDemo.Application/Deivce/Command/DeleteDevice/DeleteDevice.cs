using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Deivce.Command.DeleteDevice;
public record DeleteDeviceCommand(int Id) : IRequest<BaseAPIResponse<bool>>;

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, BaseAPIResponse<bool>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly ILogger<DeleteDeviceCommandHandler> _logger;

    public DeleteDeviceCommandHandler(
        IGenericRepository<Device> deviceRepository,
        ILogger<DeleteDeviceCommandHandler> logger)
    {
        _deviceRepository = deviceRepository;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<bool>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing DeleteDeviceCommand for DeviceId: {DeviceId}", request.Id);

            var device = await _deviceRepository.GetByIdAsync(request.Id);
            if (device == null)
            {
                _logger.LogWarning("Device with Id {DeviceId} not found", request.Id);
                return BaseAPIResponse<bool>.FailResponse(
                    message: "Device not found",
                    errors: $"Device with Id {request.Id} does not exist"
                );
            }

            device.Delete();
            await _deviceRepository.DeleteByIdAsync(request.Id);

            return BaseAPIResponse<bool>.SuccessResponse(
                data: true,
                message: "Device deleted successfully"
            );
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Validation error in DeleteDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<bool>.FailResponse(
                message: "Cannot delete device",
                errors: ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<bool>.FailResponse(
                message: "Failed to delete device",
                errors: ex.Message
            );
        }
    }
}
