namespace IcloudApi
{
    public class Credentials
    {
        public string AppleId { get; }
        public string Password { get; }
        public bool ExtendedLogin { get; }

        public Credentials(string _appleId, string _password, bool _extendedLogin = false)
        {
            AppleId = _appleId;
            Password = _password;
            ExtendedLogin = _extendedLogin;
        }
    }
}