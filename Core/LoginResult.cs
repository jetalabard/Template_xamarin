using Core.Dto;

namespace Core
{
    public class LoginResult
    {
        public LoginResult(LoginStatus status, UserDto profil)
        {
            Status = status;
            Profil = profil;
        }

        public LoginStatus Status { get; set; }

        public UserDto Profil { get; set; }
    }
}
