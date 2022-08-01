namespace BlazorBattle.Shared
{
    public interface IUserRegister
    {
        int Bananas { get; set; }
        string Bio { get; set; }
        string ConfirmPassword { get; set; }
        DateTime DateOfBirth { get; set; }
        string Email { get; set; }
        bool IsConfirmed { get; set; }
        string Password { get; set; }
        int StartUnitId { get; set; }
        string UserName { get; set; }
    }
}