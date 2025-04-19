using AutoMapper;
using RealesApi.DTO.ConditionsDTO;
using RealesApi.DTO.Property;
using RealesApi.DTO.PropertyTypeDTO;
using RealesApi.DTO.PurposeDTO;
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
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WhatsSpecial.Name));

            CreateMap<PropertyTypeDTO, PropertyType>().ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<PurposeDTO, Purpose>().ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            
            CreateMap<ConditionsDTO, Condition>().ReverseMap()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<WhatsSpecialDTO, WhatsSpecial>().ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        }
    }
}