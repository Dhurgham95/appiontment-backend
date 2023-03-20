namespace AppiontmentBackEnd.ViewModels.Login
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string? Token { get; set; }
        public bool IsLoginSucceed { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
