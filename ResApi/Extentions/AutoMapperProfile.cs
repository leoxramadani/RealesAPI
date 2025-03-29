using System.Linq;
using AutoMapper;
using RealesApi.DTO.Property;
using RealesApi.Models;

namespace RealesApi.Extentions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PropertyDTO, Property>().ReverseMap()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.SellerId))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Users.Name))
                    .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.Users.LastName))
                    .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Users.Email))
                    .ForMember(dest => dest.ConditionName, opt => opt.MapFrom(src => src.Condition.Name))
                    .ForMember(dest => dest.PropertyTypeName, opt => opt.MapFrom(src => src.PropertyType.Name))
                    .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.Purpose.Name))
                    .ForMember(dest => dest.WhatsSpecialName, opt => opt.MapFrom(src => src.WhatsSpecial.Name));
        }

    }
}