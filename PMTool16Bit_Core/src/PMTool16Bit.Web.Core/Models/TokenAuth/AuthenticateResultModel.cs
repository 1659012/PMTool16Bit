using PMTool16Bit.Users;

namespace PMTool16Bit.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        //public string EncryptedAccessToken { get; set; }

        //public int ExpireInSeconds { get; set; }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public UserDto Profile { get; set; }

    }
}
