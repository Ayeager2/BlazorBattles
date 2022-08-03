using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattle.Shared
{
    public class BattleResult
    {
        public IList<string> BattleLog { get; set; } = new List<string>();
        public int  AttackDamageSum { get; set; }
        public int OpponentDamageSum { get; set; }
        public bool IsVictory { get; set; }
        public int RoundsFought { get; set; }
    }
}
