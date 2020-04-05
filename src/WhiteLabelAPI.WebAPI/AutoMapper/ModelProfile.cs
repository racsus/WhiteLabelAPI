using AutoMapper;
using Core.DTO.Security;

using Core.Entities.Security;


namespace WhiteLabelAPI.WebAPI.AutoMapper
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            //Security
            CreateMap<UserDTO, User>();
        }
    }
}
