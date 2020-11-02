using AutoMapper;
using Common.Entities;
using ServicesLayer.Dto;

namespace WebApi.ViewModels
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<AccountDto, AppUser>();
        }
    }
}
