using CommonTools.Lib11.DataStructures;
using System;

namespace CommonTools.Lib45.LiteDbTools
{
    public class DbMetadata : IDocumentDTO
    {
        public DbMetadata()
        {
        }

        public DbMetadata(string key, string value)
        {
            Name  = key;
            Value = value;
        }


        public string    Name       { get; set; }
        public string    Value      { get; set; }
        public int       Id         { get; set; }
        public string    Author     { get; set; }
        public DateTime  Timestamp  { get; set; }
        public string    Remarks    { get; set; }
    }
}
