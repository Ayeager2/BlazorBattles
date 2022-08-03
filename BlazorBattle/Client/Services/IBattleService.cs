using BlazorBattle.Shared;

namespace BlazorBattle.Client.Services
{
    public interface IBattleService
    {
        Task<BattleResult> StartBattle(int oponenetId);
        BattleResult LastBattle { get; set; }
        IList<BattleHistoryEntry> History { get; set; }
        Task GetHistory();
        
    }
}
