using AutoMapper;
using RealesApi.DTO.Property;
using RealesApi.DTO.WhatsSpecialDTO;
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
                    .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.Users.PhoneNumber))

                    .ForMember(dest => dest.ConditionName, opt => opt.MapFrom(src => src.Condition.Name))
                    .ForMember(dest => dest.PropertyTypeName, opt => opt.MapFrom(src => src.PropertyType.Name))
                    .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.Purpose.Name))
                    .ForMember(dest => dest.WhatsSpecialNames, opt => opt.MapFrom(src => src.PropertyWhatsSpecialLinks));

            CreateMap<WhatsSpecialLinkDTO, PropertyWhatsSpecialLink>().ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SpecialName, opt => opt.MapFrom(src => src.WhatsSpecial.Name))
                    .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.Property.Id))
                    .ForMember(dest => dest.WhatsSpecialId, opt => opt.MapFrom(src => src.WhatsSpecial.Id));
        }
    }
}