using AutoMapper;
using ClientAPI.Models;
using ClientAPI.Shared.DTOs;
using ClientAPI.Shared.ViewModels;

namespace ClientAPI.Core.Shared
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegistrationVM,Person>();

            CreateMap<Person,UsersDTO>();
            
        }
    }
}