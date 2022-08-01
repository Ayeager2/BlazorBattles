namespace BlazorBattle.Shared
{
    public interface IUnit
    {
        int Attack { get; set; }
        int BananaCost { get; set; }
        int Defense { get; set; }
        int HitPoints { get; set; }
        string Title { get; set; }
        int UnitId { get; set; }
    }
}