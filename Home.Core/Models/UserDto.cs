namespace Home.Core.Models
{
    using Home.Core.Interfaces;

    public class UserDto : IUserDto
    {
        public IUser ToUser()
        {
            return new User();
        }
    }
}
