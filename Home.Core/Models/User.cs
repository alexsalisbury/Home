namespace Home.Core.Models
{
    using Home.Core.Interfaces;

    public class User : IUser
    {
        public IUserDto ToDto()
        {
            return new UserDto();
        }
    }
}
