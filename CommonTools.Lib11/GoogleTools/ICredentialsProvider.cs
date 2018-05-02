namespace CommonTools.Lib11.GoogleTools
{
    public interface ICredentialsProvider
    {
        bool                 IsValidUser  { get; }
        FirebaseCredentials  Credentials  { get; }
    }
}
