using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
           private static List<Characater> characters = new List<Characater>{
            new Characater(),
            new Characater{Id=1,Name="Minnie",HitPoint=100,Strength=10,Defense=10,Intelligence=10,Class=RPGClass.Mage},
            new Characater{Id=2,Name="Cindy",HitPoint=100,Strength=10,Defense=10,Intelligence=10,Class=RPGClass.Cleric}
        };
        public List<Characater> AddCharacter(Characater newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public List<Characater> GetAllCharacters()
        {
            return characters;
        }

        public Characater GetCharacterByName(string name)
        {
                 return characters.Where(c => c.Name.Contains(name)).FirstOrDefault();
        }

        public Characater GetCharacterById(int id)
        {
            return characters.FirstOrDefault(c => c.Id == id);
        }
    }
}