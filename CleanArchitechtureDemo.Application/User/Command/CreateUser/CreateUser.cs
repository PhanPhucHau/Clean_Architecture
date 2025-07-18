using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Enums;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.User.Request;
using MediatR;

namespace Clean_Architecture.Application.User.Command.CreateUser;

public record CreateUserCommand(UserRequest UserRequest) : IRequest<BaseAPIResponse<object>>;
public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, BaseAPIResponse<object>>
{
    private readonly IApplicationDbContext _context;

    private readonly IGenericRepository<Clean_Architecture.Domain.Entities.User> _userRepository;

    public CreateUserCommandHandle(IApplicationDbContext context ,IGenericRepository<Clean_Architecture.Domain.Entities.User> userRepository)
    {

        _userRepository = userRepository;
        _context = context;
    }

    public async Task<BaseAPIResponse<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.AnyAsync(x => x.PhoneNumber.ToLower() == request.UserRequest.PhoneNumber.ToLower()))
            return  BaseAPIResponse<object>.FailResponse("Trùng số điện thoại, vui lòng nhập số khác");
        var user = Clean_Architecture.Domain.Entities.User.Create(
            request.UserRequest.Name,
            request.UserRequest.Email,
            request.UserRequest.PhoneNumber,
            request.UserRequest.Address,
            request.UserRequest.DateOfBirth,
            request.UserRequest.Gender,
            request.UserRequest.NotificationUser);

        await _userRepository.AddAsync(user);

        await _context.SaveChangesAsync(cancellationToken);
        return BaseAPIResponse<object>.SuccessResponse(
              data: new { 
                  UserId = user.Id,
                  Name = user.Name,
                  Email = user.Email,
                  PhoneNumber = user.PhoneNumber,
                  Address = user.Address,
                  Date = user.DateOfBirth,
                  Gender = user.Gender},
              message: "User created successfully"
          );
    }
}


