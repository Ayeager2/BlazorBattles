using BlazorBattle.Shared;
using System.Net.Http.Json;

namespace BlazorBattle.Client.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly HttpClient _http;

        public LeaderboardService(HttpClient http)
        {
           _http = http;
        }
        public IList<UserStatistics> Leaderboard { get ; set ; }

        public async Task GetLeaderboard()
        {
            Leaderboard = await _http.GetFromJsonAsync<IList<UserStatistics>>("api/user/leaderboard");
        }
    }
}
