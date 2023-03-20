namespace AppiontmentBackEnd.ViewModels.Register
{
    public class RegisterResponse : RegisterRequest
    {

        public bool IsRegisterSucceed { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
