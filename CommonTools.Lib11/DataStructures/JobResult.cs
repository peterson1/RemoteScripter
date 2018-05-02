using System;

namespace CommonTools.Lib11.DataStructures
{
    public class JobResult
    {
        public string           Message    { get; set; }
        public ExceptionReport  Error      { get; set; }
        public DateTime         TimeEnded  { get; set; }

        public bool IsSuccessful => Error == null;


        public static JobResult Success(string message = "Job completed successfully.")
            => new JobResult
            {
                Message   = message,
                TimeEnded = DateTime.Now,
            };

        public static JobResult Fail(string context, Exception ex)
            => new JobResult
            {
                Message   = ex.Message,
                Error     = new ExceptionReport(context, ex),
                TimeEnded = DateTime.Now,
            };
    }
}
