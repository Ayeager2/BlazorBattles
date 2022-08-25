using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.DTO.Character;
using WebAPI.DTO.Skill;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<GetCharacterDto, Character>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Character, AddCharacterDto>();
            CreateMap<Character, UpdateCharacterDto>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<GetWeaponDto, Weapon>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<GetSkillDto, Skill>();
            CreateMap<AddSkillDto, Skill>();
            CreateMap<Skill, AddSkillDto>();
            CreateMap<GetSkillDto, Skill>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}