using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.Fight
{
    public class WeaponAttackDto
    {
        public int AttackerId { get; set; }
        public int DefenderId { get; set; }
    }
}