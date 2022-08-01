namespace BlazorBattle.Shared
{
    public interface IUserUnit
    {
        System.Int32 HitPoints { get; set; }
        System.Int32 UnitId { get; set; }
        System.Int32 UserId { get; set; }
    }
}