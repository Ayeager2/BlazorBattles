using BlazorBattle.Shared;
using System.Net.Http.Json;

namespace BlazorBattle.Client.Services
{
    public class BattleService : IBattleService
    {
        private readonly HttpClient _httpClient;

        public BattleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public BattleResult LastBattle { get; set; } = new BattleResult();

        public IList<BattleHistoryEntry> History { get ; set ; } = new List<BattleHistoryEntry>();  


        public async Task<BattleResult> StartBattle(int oponenetId)
        {
            var result = await _httpClient.PostAsJsonAsync("api/battle", oponenetId);

            LastBattle = await result.Content.ReadFromJsonAsync<BattleResult>();
            return LastBattle;
        }
        public async Task GetHistory()
        {
            History = await _httpClient.GetFromJsonAsync<BattleHistoryEntry[]>("api/user/history");
        }
    }
}
