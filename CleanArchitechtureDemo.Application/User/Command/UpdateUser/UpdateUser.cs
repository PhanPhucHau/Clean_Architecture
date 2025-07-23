using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using Clean_Architecture.Share.Device.Request;
using Clean_Architecture.Share.User.Model;
using Clean_Architecture.Share.User.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.User.Command.UpdateUser
{
    public record UpdateUserCommand(int Id, UserRequest Request) : IRequest<BaseAPIResponse<UserDto>>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseAPIResponse<UserDto>>
    {
        private readonly IGenericRepository<Clean_Architecture.Domain.Entities.User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(
            IGenericRepository<Clean_Architecture.Domain.Entities.User> userRepository,
            IMapper mapper,
            ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseAPIResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Processing UpdateUserCommand for DeviceId: {DeviceId}", request.Id);

                var user = await _userRepository.GetByIdAsync(request.Id);
                if (user == null)
                {
                    _logger.LogWarning("Device with Id {DeviceId} not found", request.Id);
                    return BaseAPIResponse<UserDto>.FailResponse(
                        message: "User not found",
                        errors: $"User with Id {request.Id} does not exist"
                    );
                }

                user.Update(
                    name: request.Request.Name,
                    email: request.Request.Email,
                    phoneNumber: request.Request.PhoneNumber,
                    address: request.Request.Address,
                    date: request.Request.DateOfBirth,
                    gender: request.Request.Gender,
                    notificationUser: request.Request.NotificationUser
                );

                await _userRepository.UpdateByIdAsync(request.Id, user);

                var userDto = _mapper.Map<UserDto>(user);
                return BaseAPIResponse<UserDto>.SuccessResponse(
                    data: userDto,
                    message: "Device updated successfully"
                );
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Validation error in UpdateUserCommand: {Message}", ex.Message);
                return BaseAPIResponse<UserDto>.FailResponse(
                    message: "Invalid device data",
                    errors: ex.Message
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateUserCommand: {Message}", ex.Message);
                return BaseAPIResponse<UserDto>.FailResponse(
                    message: "Failed to update user",
                    errors: ex.Message
                );
            }
        }
    }
}
