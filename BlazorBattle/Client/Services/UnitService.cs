using BlazorBattle.Shared;
using Blazored.Toast.Services;
using System.Net.Http.Json;

namespace BlazorBattle.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _tostService;
        private readonly HttpClient _http;
        private readonly IBananaService _bananaService;

        public UnitService(IToastService tostService, HttpClient http, IBananaService bananaService)
        {
            _tostService = tostService;
            _http = http;
            _bananaService = bananaService;
        }

        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();


        public async Task AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.UnitId == unitId);

            var result = await _http.PostAsJsonAsync<int>("api/userunit", unitId);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _tostService.ShowWarning(await result.Content.ReadAsStringAsync());
            }
            else
            {
                await _bananaService.GetBananas();
                _tostService.ShowSuccess($"Your {unit.Title} has been built!", $"Unit {unit.Title} Built! ");
            }


        }

        public async Task LoadUnitsAsync()
        {
            if (Units.Count == 0)
            {
                Units = await _http.GetFromJsonAsync<IList<Unit>>("API/Unit");
            }
        }

        public async Task LoadUserUnitsAsync()
        {
            MyUnits = await _http.GetFromJsonAsync<IList<UserUnit>>("api/userunit");
        }

        public async Task ReviveArmy()
        {
            var result = await _http.PostAsJsonAsync<string>("api/user/revive", null);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                _tostService.ShowSuccess(await result.Content.ReadAsStringAsync());
            else
                _tostService.ShowError(await result.Content.ReadAsStringAsync());

            await LoadUserUnitsAsync();
            await _bananaService.GetBananas();

        }
    }
}
