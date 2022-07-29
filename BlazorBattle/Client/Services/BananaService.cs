namespace BlazorBattle.Client.Services
{
    public class BananaService : IBananaService
    {
        public int Bananas { get; set; } = 1000;

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

        void BananasChanged() => OnChange?.Invoke();
    }
}
