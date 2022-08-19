using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.DTO.Character;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Characater, GetCharacterDTO>();
            CreateMap<GetCharacterDTO, Characater>();
            CreateMap<AddCharacterDTO, Characater>();
            CreateMap<Characater, AddCharacterDTO>();
            CreateMap<Characater, UpdateCharacterDTO>();
            CreateMap<UpdateCharacterDTO, Characater>();
        }
    }
}