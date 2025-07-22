using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.User.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Clean_Architecture.Application.User.Queries.GetUserWithPagination
{
    public record GetUserWithPagingQuery(PaginationRequest PaginationRequest) : IRequest<ResultPaginationDataResponse<List<UserDto>>>;

    public class GetUserWithPagingQueryHandler : IRequestHandler<GetUserWithPagingQuery, ResultPaginationDataResponse<List<UserDto>>>
    {
        private readonly IGenericRepository<Clean_Architecture.Domain.Entities.User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserWithPagingQueryHandler(IGenericRepository<Clean_Architecture.Domain.Entities.User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResultPaginationDataResponse<List<UserDto>>> Handle(GetUserWithPagingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy query cơ bản từ repository
                var query = _userRepository.GetQuery();

                // Tìm kiếm (nếu có)
                if (!string.IsNullOrEmpty(request.PaginationRequest.SearchString))
                {
                    var search = request.PaginationRequest.SearchString.ToLower();
                    query = query.Where(u => u.Name.ToLower().Contains(search) ||
                                             u.Email.ToLower().Contains(search) ||
                                             u.PhoneNumber.ToLower().Contains(search));
                }

                // Sắp xếp (nếu có)
                if (!string.IsNullOrEmpty(request.PaginationRequest.OrderBy))
                {
                    var order = request.PaginationRequest.Order?.ToLower() == "desc" ? "descending" : "ascending";
                    query = query.OrderBy($"{request.PaginationRequest.OrderBy} {order}");
                }

                // Phân trang
                var pageSize = request.PaginationRequest.PageSize;
                var pageNumber = request.PaginationRequest.PageNumber;
                var totalCount = await query.CountAsync(cancellationToken);
                var users = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                // Ánh xạ sang UserDto
                var userDtos = _mapper.Map<List<UserDto>>(users);

                return ResultPaginationDataResponse<List<UserDto>>.SuccessResponse(
                    data: userDtos,
                    totalRecords: totalCount,
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    message: "Users retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                return ResultPaginationDataResponse<List<UserDto>>.FailResponse(
                    message: "Failed to retrieve users",
                    errors: ex.Message
                );
            }
        }
    }

}
