using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Filter.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Filter.Queries.GetAllFilter;


public record GetAllFilterQuery : IRequest<BaseAPIResponse<List<FilterDto>>>;

public class GetAllFilterQueryHandler : IRequestHandler<GetAllFilterQuery, BaseAPIResponse<List<FilterDto>>>
{
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Filter> _filterRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllFilterQueryHandler> _logger;

    public GetAllFilterQueryHandler(IGenericRepository<Clean_Architecture.Domain.Entities.Filter> filterRepository, IMapper mapper, ILogger<GetAllFilterQueryHandler> logger)
    {
        _filterRepository = filterRepository ?? throw new ArgumentNullException(nameof(filterRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BaseAPIResponse<List<FilterDto>>> Handle(GetAllFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting GetAllDeviceQuery");
            var filters = await _filterRepository.GetQuery().ToListAsync(cancellationToken);
            var filterDtos = _mapper.Map<List<FilterDto>>(filters);
            if (filterDtos == null)
            {
                _logger.LogError("AutoMapper failed to map devices to filterDtos");
                throw new Exception("AutoMapper failed to map devices to filterDtos.");
            }

            return BaseAPIResponse<List<FilterDto>>.SuccessResponse(
                data: filterDtos,
                message: "All filterDtos retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllFilterQuery: {Message}", ex.Message);
            return BaseAPIResponse<List<FilterDto>>.FailResponse(
                message: "Failed to retrieve filterDtos",
                errors: ex.Message
            );
        }
    }
}
