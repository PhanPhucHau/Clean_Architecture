using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.User.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Application.User.Queries.GetAllUser
{
    public record GetAllUsersQuery : IRequest<BaseAPIResponse<List<UserDto>>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, BaseAPIResponse<List<UserDto>>>
    {
        private readonly IGenericRepository<Clean_Architecture.Domain.Entities.User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IGenericRepository<Clean_Architecture.Domain.Entities.User> userRepository, IMapper mapper, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseAPIResponse<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting GetAllUsersQuery");
                var users = await _userRepository.GetQuery().ToListAsync(cancellationToken);
                var userDtos = _mapper.Map<List<UserDto>>(users);
                if (userDtos == null)
                {
                    _logger.LogError("AutoMapper failed to map users to UserDto");
                    throw new Exception("AutoMapper failed to map users to UserDto.");
                }

                return BaseAPIResponse<List<UserDto>>.SuccessResponse(
                    data: userDtos,
                    message: "All users retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllUsersQuery: {Message}", ex.Message);
                return BaseAPIResponse<List<UserDto>>.FailResponse(
                    message: "Failed to retrieve users",
                    errors: ex.Message
                );
            }
        }
    }
}
