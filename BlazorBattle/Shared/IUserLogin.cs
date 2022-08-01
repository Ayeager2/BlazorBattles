namespace BlazorBattle.Shared
{
    public interface IUserLogin
    {
        string Password { get; set; }
        string Email { get; set; }
    }
}