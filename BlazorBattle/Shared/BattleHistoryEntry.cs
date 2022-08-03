using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattle.Shared
{
    public class BattleHistoryEntry
    {
        public int BattleId { get; set; }
        public DateTime BattleDate { get; set; }
        public int AttackerId { get; set; }
        public int OpponenetId { get; set; }
        public bool YouWon { get; set; }
        public string AttackerName { get; set; }
        public string OpponentName { get; set; }
        public int RoundsFought { get; set; }
        public int WinnderDamageDealt { get; set; }

    }
}
