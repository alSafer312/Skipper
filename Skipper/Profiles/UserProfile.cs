using AutoMapper;
using Skipper.Enums;
using Skipper.Extensions;
using Skipper.Helpers;
using Skipper.Models.DTOs.Incomig;
using Skipper.Models.DTOs.Outgoing;

namespace Skipper.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {

            byte[] PasswordSalt = System.Text.Encoding.UTF8.GetBytes("Salt_from_St'Petersburg");

            CreateMap<AuthenticateRequest, User>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(
                    dest => dest.UserSettings,
                    opt => opt.MapFrom(src => new UserSettings()))
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(
                    dest => dest.PasswordHash,
                    opt => opt.MapFrom(
                        src => src.Password.ConvertToHash(PasswordSalt)))
                .ForMember(
                    dest => dest.PasswordSalt,
                    opt => opt.MapFrom(src => PasswordSalt));
                /*.ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => UserRoleEnum.Unverified));*/


            CreateMap<User, AuthenticateResponse>()
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email));
            /*
                .ForMember(
                    dest => dest.AccesToken,
                    opt => opt.MapFrom(GenerateJwt((User)opt))); //needTest
                    //opt => opt.MapFrom(_configuration.GenerateJwt(_user))); //?
            */
        }
    }

}
