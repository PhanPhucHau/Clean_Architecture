using AutoMapper;
using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Device.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Clean_Architecture.Application.Deivce.Queries.GetDeviceWithPagination;

public record GetDeviceWithPagingQuery(PaginationRequest PaginationRequest) : IRequest<ResultPaginationDataResponse<List<DeviceDto>>>;

public class GetDeviceWithPagingQueryHandler : IRequestHandler<GetDeviceWithPagingQuery, ResultPaginationDataResponse<List<DeviceDto>>>
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IMapper _mapper;

    public GetDeviceWithPagingQueryHandler(IGenericRepository<Device> deviceRepository, IMapper mapper)
    {
        _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ResultPaginationDataResponse<List<DeviceDto>>> Handle(GetDeviceWithPagingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Lấy query cơ bản từ repository
            var query = _deviceRepository.GetQuery();

            // Tìm kiếm (nếu có)
            if (!string.IsNullOrEmpty(request.PaginationRequest.SearchString))
            {
                var search = request.PaginationRequest.SearchString.ToLower();
                query = query.Where(u => u.Name.ToLower().Contains(search));
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
            var devices = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // Ánh xạ sang UserDto
            var deviceDtos = _mapper.Map<List<DeviceDto>>(devices);

            return ResultPaginationDataResponse<List<DeviceDto>>.SuccessResponse(
                data: deviceDtos,
                totalRecords: totalCount,
                pageNumber: pageNumber,
                pageSize: pageSize,
                message: "Users retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            return ResultPaginationDataResponse<List<DeviceDto>>.FailResponse(
                message: "Failed to retrieve users",
                errors: ex.Message
            );
        }
    }
}
