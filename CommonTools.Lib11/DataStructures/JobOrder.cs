using System;

namespace CommonTools.Lib11.DataStructures
{
    public class JobOrder
    {
        public string      Command    { get; set; }
        public string      Argument   { get; set; }
        public DateTime    Requested  { get; set; }
        public DateTime?   Started    { get; set; }
        public JobResult   Result     { get; set; }
    }
}
