using BlazorBattle.Shared;

namespace BlazorBattle.Client.Services
{
    public interface ILeaderboardService
    {
        IList<UserStatistics> Leaderboard { get; set; }
        Task GetLeaderboard();
    }
}
