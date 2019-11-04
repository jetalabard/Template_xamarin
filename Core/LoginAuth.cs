using Core.Dto;

namespace Core
{
    public class LoginAuth
    {
        public LoginAuth(string userName, string password)
        {
            User = userName;
            Password = password;
        }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
