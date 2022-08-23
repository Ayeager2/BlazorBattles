

namespace WebAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RPGClass
    {
        Adventurer = 0,
        Knight = 1,
        Mage = 2,
        Cleric = 3
    }
}