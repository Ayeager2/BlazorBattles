using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services.CharacterService
{
    public interface ICharacterService
    {
        List<Characater> GetAllCharacters();
        Characater GetCharacterById(int id);
        Characater GetCharacterByName(string name);
        List<Characater> AddCharacter(Characater newCharacter);
    }
}