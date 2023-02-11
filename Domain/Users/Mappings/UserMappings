
using System.Net;
using Abyster_Test_Project.Domain.Users.Dtos;
using AutoMapper;

namespace Abyster_Test_Project.Domain.Users.Mappings;

public class UserMapping : Profile {


    public UserMapping(){
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<RegistrationRequest, User>();
        CreateMap<User, AuthenticationResponse>();
    }

}
