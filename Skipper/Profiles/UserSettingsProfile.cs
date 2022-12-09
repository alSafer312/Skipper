using AutoMapper;
using Skipper.Models.DTOs.Incomig;
using Skipper.Models.DTOs.Outgoing;

namespace Skipper.Profiles
{
    public class UserSettingsProfile : Profile
    {

        public UserSettingsProfile()
        {
            CreateMap<UserSettingsRequest, UserSettings>()
                /*.ForMember(
                    dest => dest.AvatarURL,
                    opt => opt.MapFrom(src => src.ProfileImage)) //to do: IFormFile Extension*/
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(
                    dest => dest.Patronymic,
                    opt => opt.MapFrom(src => src.Patronymic))
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(
                    dest => dest.TimeZoneId,
                    opt => opt.MapFrom(src => src.TimeZoneId))
                .ForMember(
                    dest => dest.AboutMe,
                    opt => opt.MapFrom(src => src.AboutMe))
                .ForMember(
                    dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(
                    dest => dest.BrowserNotifications,
                    opt => opt.MapFrom(src => src.BrowserNotifications))
                .ForMember(
                    dest => dest.NotificationSettings,
                    opt => opt.MapFrom(src => src.NotificationSettings))
                .ForMember(
                    dest => dest.CommunicationWays,
                    opt => opt.MapFrom(src => src.Links));

            CreateMap<UserSettings, UserSettingsResponse>()
                .ForMember(
                    dest => dest.AvatarURL,
                    opt => opt.MapFrom(src => src.AvatarURL))
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(
                    dest => dest.Patronymic,
                    opt => opt.MapFrom(src => src.Patronymic))
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(
                    dest => dest.TimeZoneId,
                    opt => opt.MapFrom(src => src.TimeZoneId))
                .ForMember(
                    dest => dest.AboutMe,
                    opt => opt.MapFrom(src => src.AboutMe))
                .ForMember(
                    dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(
                    dest => dest.BrowserNotifications,
                    opt => opt.MapFrom(src => src.BrowserNotifications))
                .ForMember(
                    dest => dest.NotificationSettings,
                    opt => opt.MapFrom(src => src.NotificationSettings))
                .ForMember(
                    dest => dest.Links,
                    opt => opt.MapFrom(src => src.CommunicationWays));
        }
    }
}
