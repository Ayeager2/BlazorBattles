using System.Net.Http.Json;

namespace BlazorBattle.Client.Services
{
    public class BananaService : IBananaService
    {
        private readonly HttpClient _http;

        public BananaService(HttpClient http)
        {
            _http = http;
        }
        public int Bananas { get; set; } = 0;

        public event Action OnChange;

        public void AddBananas(int amout)
        {
            Bananas += amout;
            BananasChanged();
        }

        public void EatBananas(int amout)
        {
            Bananas -= amout;
            BananasChanged();
        }

        public async Task GetBananas()
        {
            Bananas = await _http.GetFromJsonAsync<int>("api/user/getbananas");
            BananasChanged();
        }

        void BananasChanged() => OnChange?.Invoke();
    }
}
