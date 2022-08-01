using BlazorBattle.Shared;
using Blazored.Toast.Services;
using System.Net.Http.Json;

namespace BlazorBattle.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _tostService;
        private readonly HttpClient _http;

        public UnitService(IToastService tostService, HttpClient http)
        {
            _tostService = tostService;
            _http = http;
        }

        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();
        //public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>
        //{
        //    new UserUnit
        //    {
        //        UnitId = 1,
        //        HitPoints = 100,
        //    },
        //};

        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.UnitId == unitId);
            MyUnits.Add(new UserUnit { UnitId = unit.UnitId, HitPoints = unit.HitPoints });
            _tostService.ShowSuccess($"Your {unit.Title} has been built!", $"Unit {unit.Title} Built! ");
        }

        public async Task LoadUnitsAsync()
        {
            if(Units.Count == 0)
            {
                Units = await _http.GetFromJsonAsync<IList<Unit>>("API/Unit");
            }
        }
    }
}
