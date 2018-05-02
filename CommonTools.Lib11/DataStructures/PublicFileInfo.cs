using System;

namespace CommonTools.Lib11.DataStructures
{
    public class PublicFileInfo
    {
        public string     DownloadURL  { get; set; }
        public string     Version      { get; set; }
        public string     SHA1         { get; set; }
        public DateTime   LastUpdate   { get; set; }
    }
}
