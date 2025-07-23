using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Notify.Model;
using Clean_Architecture.Share.Notify.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Notify.Command.CreateNotify;

public record CreateNotifyCommand(NotifyRequest Request) : IRequest<BaseAPIResponse<NotifyDto>>;

public class CreateNotifyCommandHandler : IRequestHandler<CreateNotifyCommand, BaseAPIResponse<NotifyDto>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.User> _userRepository;
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Filter> _filterRepository;
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Notify> _notifyRepository;

    private readonly IMapper _mapper;
    private readonly ILogger<CreateNotifyCommandHandler> _logger;

    public CreateNotifyCommandHandler(
        IGenericRepository<Device> deviceRepository,
        IGenericRepository<Clean_Architecture.Domain.Entities.User> userRepository,
        IGenericRepository<Clean_Architecture.Domain.Entities.Filter> filterRepository,
        IGenericRepository<Clean_Architecture.Domain.Entities.Notify> notifyRepository,
        IMapper mapper,
        ILogger<CreateNotifyCommandHandler> logger)
    {
        _deviceRepository = deviceRepository;
        _userRepository = userRepository;
        _filterRepository = filterRepository;
        _notifyRepository = notifyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<NotifyDto>> Handle(CreateNotifyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing CreateNotifyCommand for Notify: {NotifyTitile}", request.Request.Title);

            var userExists = await _userRepository.ExistsAsync(request.Request.UserId);
            if (!userExists)
            {
                _logger.LogWarning("User with Id {UserNotifyId} not found", request.Request.UserId);
                return BaseAPIResponse<NotifyDto>.FailResponse(
                    message: "UserNotifyId not found",
                    errors: $"UserNotifyId with Id {request.Request.UserId} does not exist"
                );
            }

            var deviceExists = await _deviceRepository.ExistsAsync(request.Request.DeviceId);
            if (!deviceExists)
            {
                _logger.LogWarning("User with Id {DeviceNotifyId} not found", request.Request.DeviceId);
                return BaseAPIResponse<NotifyDto>.FailResponse(
                    message: "DeviceNotifyId not found",
                    errors: $"DeviceNotifyId with Id {request.Request.DeviceId} does not exist"
                );
            }

            var filterExists = await _filterRepository.ExistsAsync(request.Request.FilterId);
            if (!filterExists)
            {
                _logger.LogWarning("User with Id {FilterNotifyId} not found", request.Request.FilterId);
                return BaseAPIResponse<NotifyDto>.FailResponse(
                    message: "FilterNotifyId not found",
                    errors: $"FilterNotifyId with Id {request.Request.FilterId} does not exist"
                );
            }

            var notify = Clean_Architecture.Domain.Entities.Notify.Create(
                    title: request.Request.Title,
                    message: request.Request.Message,
                    dateTime: request.Request.NotifyDate,
                    isRead: request.Request.IsRead,
                    userNotifyId: request.Request.UserId,
                    deviceNotifyId: request.Request.DeviceId,
                    filterNotifyId: request.Request.FilterId
            );

            await _notifyRepository.AddAsync(notify);

            var notifyDto = _mapper.Map<NotifyDto>(notify);
            return BaseAPIResponse<NotifyDto>.SuccessResponse(
                data: notifyDto,
                message: "notifyDto created successfully"
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error in CreateDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<NotifyDto>.FailResponse(
                message: "Invalid device data",
                errors: ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateDeviceCommand: {Message}", ex.Message);
            return BaseAPIResponse<NotifyDto>.FailResponse(
                message: "Failed to create device",
                errors: ex.Message
            );
        }
    }
}