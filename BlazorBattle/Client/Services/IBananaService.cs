namespace BlazorBattle.Client.Services
{
    public interface IBananaService
    {
        event Action OnChange;
        int Bananas { get; set; }
        void EatBananas(int amout);
        void AddBananas(int amout);

    }
}
