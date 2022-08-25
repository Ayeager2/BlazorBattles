using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.Fight
{
    public class HighScoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Battles { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }

    }
}