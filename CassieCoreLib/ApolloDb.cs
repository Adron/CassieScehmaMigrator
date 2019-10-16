namespace CassieCoreLib
{
    public class ApolloDb
    {
        public readonly string Keyspace;
        public readonly string Username;
        public readonly string Password;
        public readonly string SecurityBundleFilePath;

        public ApolloDb(string username, string password, string securityBundleFilePath, string keyspace)
        {
            Keyspace = keyspace;
            Username = username;
            Password = password;
            SecurityBundleFilePath = securityBundleFilePath;
        }
    }
}
