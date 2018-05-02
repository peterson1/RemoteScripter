using CommonTools.Lib11.ExceptionTools;
using System;

namespace CommonTools.Lib11.DataStructures
{
    public class ExceptionReport
    {
        public ExceptionReport() { }

        public ExceptionReport(string context, Exception ex)
        {
            Context   = context;
            Message   = ex.Info(true, true);
            Timestamp = DateTime.Now;
        }


        public string    Context    { get; set; }
        public string    Message    { get; set; }
        public DateTime  Timestamp  { get; set; }
    }
}
