

namespace WebAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Adventurer = 0,
        Knight = 1,
        Mage = 2,
        Cleric = 3,
        Rogue = 4,
        Druid = 5,
        Bard = 6,
        Paladin = 7,
        Ranger = 8,
        Sorcerer = 9,
        Warlock = 10,
        Monk = 11,
        Barbarian = 13,
        Warrior = 14,
        Pleb = 15,
    }
}
