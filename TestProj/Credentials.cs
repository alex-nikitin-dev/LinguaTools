namespace TestProj
{
    public class Credentials
    {
        public string UserName;
        public string Password;

        public Credentials(string userName, string password)
        {
            Password = password;
            UserName = userName;
        }

        public Credentials()
        {

        }
    }
}
