using AutoMapper;
using YourSport.Api.ViewModels.User;
using YourSport.Application.Responses.User;
using YourSport.Application.ServiceModels.User;
using YourSport.Business.Models;

namespace YourSport.Api.Configuration.AutoMapper;

public class YourSportMapperConfig : Profile
{
    public YourSportMapperConfig()
    {
        #region User
        CreateMap<CreateUserViewModel, CreateUserModel>().ReverseMap();
        CreateMap<CreateUserModel, UserResponse>().ReverseMap();
        CreateMap<CreateUserViewModel, User>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
        #endregion
    }
}