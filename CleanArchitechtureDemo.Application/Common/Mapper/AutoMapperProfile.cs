using AutoMapper;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Share.Device.Model;
using Clean_Architecture.Share.Device.Request;
using Clean_Architecture.Share.Filter.Model;
using Clean_Architecture.Share.Filter.Request;
using Clean_Architecture.Share.Notify.Model;
using Clean_Architecture.Share.Notify.Request;
using Clean_Architecture.Share.User.Model;
using Clean_Architecture.Share.User.Request;

namespace Clean_Architecture.Application.Common.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User Mapping
            CreateMap< Clean_Architecture.Domain.Entities.User, UserDto>();
            CreateMap< UserRequest, Clean_Architecture.Domain.Entities.User>();

            // Device Mapping
            CreateMap<Device, DeviceDto>();
            CreateMap<DeviceRequest, Device>();
            // Filter Mapping
            CreateMap<Clean_Architecture.Domain.Entities.Filter, FilterDto>();
            CreateMap<FilterRequest, Clean_Architecture.Domain.Entities.Filter>();
            // Notify Mapping
            CreateMap<Clean_Architecture.Domain.Entities.Notify, NotifyDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Device.Name))
                .ForMember(dest => dest.FilterName, opt => opt.MapFrom(src => src.Filter.Name));

            CreateMap<NotifyRequest, Clean_Architecture.Domain.Entities.Notify>();
        }
    }
}
