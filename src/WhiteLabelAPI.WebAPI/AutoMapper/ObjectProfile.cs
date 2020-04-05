using System.Linq;
using AutoMapper;
using Core.DTO.Security;
using Core.Entities.Security;

namespace WhiteLabelAPI.WebAPI.AutoMapper
{
    public class ObjectProfile : Profile
    {
        public ObjectProfile()
        {
            //Security
            CreateMap<User, UserDTO>();
        }
    }
}
