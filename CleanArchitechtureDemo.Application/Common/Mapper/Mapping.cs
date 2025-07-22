using AutoMapper;
using Clean_Architecture.Share.User.Model;

namespace Clean_Architecture.Application.Common.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            CreateMap< Clean_Architecture.Domain.Entities.User, UserDto >();
        }
    }
}
