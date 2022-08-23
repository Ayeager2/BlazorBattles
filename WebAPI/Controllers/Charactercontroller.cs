using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Character;
using WebAPI.Services.CharacterService;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        // [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> Get(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Add(AddCharacterDTO newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut()]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> Update(UpdateCharacterDTO updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response.Success)
            {
                return Ok(await _characterService.UpdateCharacter(updatedCharacter));

            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Delete(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            
            if (response.Success)
            {
                return Ok(await _characterService.DeleteCharacter(id));

            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}