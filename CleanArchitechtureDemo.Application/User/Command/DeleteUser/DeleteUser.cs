using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.User.Command.DeleteUser;
public record DeleteUserCommand(int Id) : IRequest<BaseAPIResponse<bool>>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseAPIResponse<bool>>
{
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.User> _userRepository;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(
        IGenericRepository<Clean_Architecture.Domain.Entities.User> userRepository,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing DeleteDeviceCommand for DeviceId: {DeviceId}", request.Id);

            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                _logger.LogWarning("Device with Id {DeviceId} not found", request.Id);
                return BaseAPIResponse<bool>.FailResponse(
                    message: "Device not found",
                    errors: $"Device with Id {request.Id} does not exist"
                );
            }

            user.Delete();
            await _userRepository.DeleteByIdAsync(request.Id);

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