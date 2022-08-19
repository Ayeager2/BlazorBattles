using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.CharacterService;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAllCharacters")]
        public ActionResult<List<Characater>> Get()
        {
            return Ok(_characterService.GetAllCharacters());
        }
        [HttpGet("{id}")]
        public ActionResult<Characater> GetSingle(int id)
        {
            return Ok(_characterService.GetCharacterById(id));
        }
        [HttpGet("GetCharacterByName/{name}")]
        public ActionResult<List<Characater>> GetCharacterByName(string name)
        {
            return Ok(_characterService.GetCharacterByName(name));
        }
        [HttpPost]
        public ActionResult<List<Characater>> AddCharacter(Characater newCharacter)
        {           
            return Ok(_characterService.AddCharacter(newCharacter));
        }
    }
}