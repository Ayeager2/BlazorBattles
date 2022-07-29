using BlazorBattle.Shared;
using Blazored.Toast.Services;

namespace BlazorBattle.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _tostService;

        public UnitService(IToastService tostService)
        {
            _tostService = tostService;        
        }

        public IList<Unit> Units => new List<Unit>
        {
            new Unit {UnitId = 1, Title="Knight", Attack=10,Defense =10, BananaCost = 100 },
            new Unit {UnitId = 2, Title="Archer", Attack=15,Defense =5, BananaCost = 150 },
            new Unit {UnitId = 3, Title="Mage", Attack=20,Defense =1, BananaCost = 200 },
        };

        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();        

        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.UnitId == unitId);
            MyUnits.Add(new UserUnit { UnitId = unit.UnitId, HitPoints = unit.HitPoints });
            _tostService.ShowSuccess($"Your {unit.Title} has been built!", $"Unit {unit.Title} Built! ");
        }
    }
}
