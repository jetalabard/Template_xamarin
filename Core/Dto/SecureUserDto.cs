namespace Core.Dto
{
    public class SecureUserDto : UserDto
    {
        public SecureUserDto()
        {
        }

        public string Password { get; set; }

        public SecureUserDto(UserDto user, string password)
        {
            if (user != null)
            {
                FirstName = user.FirstName;
                Id = user.Id;
                LastName = user.LastName;
                Role = user.Role;
                PictureByte = user.PictureByte;
                Token = user.Token;
            }

            Password = password;
        }
    }
}
