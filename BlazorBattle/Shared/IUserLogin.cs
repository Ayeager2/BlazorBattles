namespace BlazorBattle.Shared
{
    public interface IUserLogin
    {
        string Password { get; set; }
        string UserName { get; set; }
    }
}