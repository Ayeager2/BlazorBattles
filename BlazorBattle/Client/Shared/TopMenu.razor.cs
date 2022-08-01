
namespace BlazorBattle.Client.Shared
{
     public partial class TopMenu
    {

        protected override void OnInitialized()
        {
            BananaService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            BananaService.OnChange += StateHasChanged;
        }
    }
}
