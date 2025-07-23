using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Notify.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Notify.Queries.GetAllNotify;


public record GetAllNotifyQuery : IRequest<BaseAPIResponse<List<NotifyDto>>>;

public class GetAllNotifyQueryHandler : IRequestHandler<GetAllNotifyQuery, BaseAPIResponse<List<NotifyDto>>>
{
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Notify> _notifyRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllNotifyQueryHandler> _logger;

    public GetAllNotifyQueryHandler(IGenericRepository<Clean_Architecture.Domain.Entities.Notify> notifyRepository, IMapper mapper, ILogger<GetAllNotifyQueryHandler> logger)
    {
        _notifyRepository = notifyRepository ?? throw new ArgumentNullException(nameof(notifyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BaseAPIResponse<List<NotifyDto>>> Handle(GetAllNotifyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting NotifyDto");
            var notifies = await _notifyRepository.GetQuery()
                .Include(n => n.User)
                .Include(n => n.Device)
                .Include(n => n.Filter)
                .ToListAsync(cancellationToken);
            var notifiesDtos = _mapper.Map<List<NotifyDto>>(notifies);
            if (notifiesDtos == null)
            {
                _logger.LogError("AutoMapper failed to map devices to UserDto");
                throw new Exception("AutoMapper failed to map devices to UserDto.");
            }

            return BaseAPIResponse<List<NotifyDto>>.SuccessResponse(
                data: notifiesDtos,
                message: "All devices retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllDeviceQuery: {Message}", ex.Message);
            return BaseAPIResponse<List<NotifyDto>>.FailResponse(
                message: "Failed to retrieve devices",
                errors: ex.Message
            );
        }
    }
}