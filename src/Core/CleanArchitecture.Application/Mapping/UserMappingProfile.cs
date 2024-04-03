using AutoMapper;
using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mapping;
internal sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterCommand, AppUser>().ReverseMap();
        CreateMap<LoginCommand, AppUser>().ReverseMap();
    }
}
