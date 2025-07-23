using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using Clean_Architecture.Share.Device.Request;
using Clean_Architecture.Share.Filter.Model;
using Clean_Architecture.Share.Filter.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Filter.Command.UpdateFilter;

public record UpdateFilterCommand(int Id, FilterRequest Request) : IRequest<BaseAPIResponse<FilterDto>>;

public class UpdateFilterCommandHandler : IRequestHandler<UpdateFilterCommand, BaseAPIResponse<FilterDto>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Filter> _filterRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateFilterCommandHandler> _logger;

    public UpdateFilterCommandHandler(
        IGenericRepository<Device> deviceRepository,
        IGenericRepository<Clean_Architecture.Domain.Entities.Filter> filterRepository,
        IMapper mapper,
        ILogger<UpdateFilterCommandHandler> logger)
    {
        _deviceRepository = deviceRepository;
        _filterRepository = filterRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<FilterDto>> Handle(UpdateFilterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing UpdateDeviceCommand for DeviceId: {DeviceId}", request.Id);

            var filter = await _filterRepository.GetByIdAsync(request.Id);
            if (filter == null)
            {
                _logger.LogWarning("filter with Id {filterId} not found", request.Id);
                return BaseAPIResponse<FilterDto>.FailResponse(
                    message: "Filter not found",
                    errors: $"Filter with Id {request.Id} does not exist"
                );
            }

            var deviceExists = await _deviceRepository.ExistsAsync(request.Request.DeviceId);
            if (!deviceExists)
            {
                _logger.LogWarning("User with Id {UserId} not found", request.Request.DeviceId);
                return BaseAPIResponse<FilterDto>.FailResponse(
                    message: "DeviceId not found",
                    errors: $"DeviceId with Id {request.Request.DeviceId} does not exist"
                );
            }

            filter.Update(
                name: request.Request.Name,
                description: request.Request.Description,
                filterType: request.Request.FilterType,
                installDate: request.Request.InstallDate,
                lifeDate: request.Request.LifeDate,
                lateDate: request.Request.LateDate,
                deviceId: request.Request.DeviceId
            );

            await _filterRepository.UpdateByIdAsync(request.Id, filter);

            var filterDto = _mapper.Map<FilterDto>(filter);
            return BaseAPIResponse<FilterDto>.SuccessResponse(
                data: filterDto,
                message: "Filter updated successfully"
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error in UpdateFilterCommand: {Message}", ex.Message);
            return BaseAPIResponse<FilterDto>.FailResponse(
                message: "Invalid device data",
                errors: ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateFilterCommand: {Message}", ex.Message);
            return BaseAPIResponse<FilterDto>.FailResponse(
                message: "Failed to update device",
                errors: ex.Message
            );
        }
    }
}