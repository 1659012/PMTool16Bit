using PMTool16Bit.Users;

namespace PMTool16Bit.Models.TokenAuth
{
    public class AuthenticateUserModel
    {
        public string AccessToken { get; set; }
        public UserDto Profile { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
    }
}
