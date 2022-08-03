namespace BlazorBattle.Client.Services
{
    public interface IBananaService
    {
        event Action OnChange;
        int Bananas { get; set; }
        void EatBananas(int amout);
        Task AddBananas(int amout);
        Task GetBananas();

    }
}
