namespace CommonTools.Lib11.GoogleTools
{
    public class FirebaseCredentials
    {
        public string   BaseURL    { get; set; }
        public string   ApiKey     { get; set; }
        public string   Email      { get; set; }
        public string   Password   { get; set; }

        public string   HumanName  { get; set; }
        public string   Roles      { get; set; }

        public string NameAndRole => $"“{HumanName}”  ({Roles})";
    }
}
