using System;

namespace CommonTools.Lib11.DataStructures
{
    public interface IDocumentDTO
    {
        int       Id         { get; set; }
        string    Author     { get; set; }
        DateTime  Timestamp  { get; set; }
        string    Remarks    { get; set; }
    }
}
