using System.Collections.Generic;

namespace CommonTools.Lib11.GoogleTools
{
    public class GmailSenderSettings
    {
        public List<Credentials>  Accounts  { get; set; }


        public class Credentials
        {
            public string   SenderName  { get; set; }
            public string   Email       { get; set; }
            public string   Password    { get; set; }
        }
    }
}
