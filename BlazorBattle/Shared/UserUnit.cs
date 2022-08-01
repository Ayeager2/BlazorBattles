using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattle.Shared
{
    public class UserUnit : IUserUnit
    {
        public int UserId { get; set; }
        public int UnitId { get; set; }
        public int HitPoints { get; set; }

    }
}
