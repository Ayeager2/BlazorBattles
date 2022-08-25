using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO.Character;
using WebAPI.DTO.Skill;

namespace WebAPI.Services.SkillService
{
    public class SkillService : ISkillService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public SkillService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills()
        {
            ServiceResponse<List<GetSkillDto>> response = new ServiceResponse<List<GetSkillDto>>();
            response.Data = await _context.Skills.Select(c => _mapper.Map<GetSkillDto>(c)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill)
        {
            ServiceResponse<List<GetSkillDto>> response = new ServiceResponse<List<GetSkillDto>>();
            try
            {
                Skill skill = _mapper.Map<Skill>(newSkill);

                _context.Skills.Add(skill);

                await _context.SaveChangesAsync();

                response.Data = await _context.Skills
                .Select(s => _mapper.Map<GetSkillDto>(s))
                .ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}