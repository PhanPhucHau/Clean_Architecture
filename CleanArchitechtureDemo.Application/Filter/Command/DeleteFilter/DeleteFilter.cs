using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.Filter.Command.DeleteFilter;

public record DeleteFilterCommand(int Id) : IRequest<BaseAPIResponse<bool>>;

public class DeleteFilterCommandHandler : IRequestHandler<DeleteFilterCommand, BaseAPIResponse<bool>>
{
    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.Filter> _filterRepository;
    private readonly ILogger<DeleteFilterCommandHandler> _logger;

    public DeleteFilterCommandHandler(
        IGenericRepository<Clean_Architecture.Domain.Entities.Filter> filterRepository,
        ILogger<DeleteFilterCommandHandler> logger)
    {
        _filterRepository = filterRepository;
        _logger = logger;
    }

    public async Task<BaseAPIResponse<bool>> Handle(DeleteFilterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing DeletefilterCommand for filter: {filterId}", request.Id);

            var filter = await _filterRepository.GetByIdAsync(request.Id);
            if (filter == null)
            {
                _logger.LogWarning("filter with Id {filterId} not found", request.Id);
                return BaseAPIResponse<bool>.FailResponse(
                    message: "filter not found",
                    errors: $"filter with Id {request.Id} does not exist"
                );
            }

            filter.Delete();
            await _filterRepository.DeleteByIdAsync(request.Id);

            return BaseAPIResponse<bool>.SuccessResponse(
                data: true,
                message: "filter deleted successfully"
            );
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Validation error in DeletefilterCommand: {Message}", ex.Message);
            return BaseAPIResponse<bool>.FailResponse(
                message: "Cannot delete filter",
                errors: ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeletefilterCommand: {Message}", ex.Message);
            return BaseAPIResponse<bool>.FailResponse(
                message: "Failed to delete filter",
                errors: ex.Message
            );
        }
    }
}
