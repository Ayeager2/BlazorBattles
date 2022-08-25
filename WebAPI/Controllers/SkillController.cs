
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Skill;
using WebAPI.Services.SkillService;

namespace WebAPI.Controllers 
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> AddCharacter(AddSkillDto newSkill)
        {
            return Ok(await _skillService.AddSkill(newSkill));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> Get()
        {
            return Ok(await _skillService.GetAllSkills());
        }
    }
 
}