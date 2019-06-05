using AutoMapper;
using BusinessLogic.Models.ClassesDto;
using DataAccessLayer.Models.Classes;
using PWApp.ViewModels.Classes;

namespace PWApp.AutoMapper.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember("FullName", opt => opt.MapFrom(c => string.Format("{0} {1} {2}", c.FirstName, c.MiddleName, c.LastName)));
            CreateMap<UserViewModel, UserDto>();
            CreateMap<UserDto, UserViewModel>()
                .ForMember("Login", opt => opt.MapFrom(c => c.UserName))
                //.ForMember("PasswordConfirm", opt => opt.Ignore())
                ;

            CreateMap<RegistrationViewModel, UserDto>()
                .ForMember("Id", opt => opt.Ignore())
                .ForMember("UserName", opt => opt.MapFrom(c => c.Login))
                .ForMember("FullName", opt => opt.Ignore());

            CreateMap<SignInViewModel, UserDto>()
                .ForMember("Id", opt => opt.Ignore())
                .ForMember("UserName", opt => opt.Ignore())
                .ForMember("FullName", opt => opt.Ignore());

            CreateMap<UserViewModel, UserDto>()
                .ForMember("UserName", opt => opt.MapFrom(c => c.Login))
                .ForMember("FullName", opt => opt.Ignore());            
        }
    }
}
