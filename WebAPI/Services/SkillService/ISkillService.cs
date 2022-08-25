using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.Character;
using WebAPI.DTO.Skill;

namespace WebAPI.Services.SkillService
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills();
        Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill);      
    }
}