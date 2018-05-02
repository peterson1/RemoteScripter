using CommonTools.Lib11.ExceptionTools;
using CommonTools.Lib11.StringTools;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CommonTools.Lib11.LoggingTools
{
    public static class Loggly
    {
        private static readonly HttpClient _client = new HttpClient();
        private static string _userAgent;
        private static string _token;

        public static void Initialize(string userAgent, string customerToken)
        {
            _userAgent = userAgent;
            _token     = customerToken;
        }


        public static Task<HttpResponseMessage> Post(
            string message, 
            string senderClass = "LogglySender",
            [CallerMemberName] string callingMethod = null)
                => PostLog(message, senderClass, callingMethod);


        public static Task<HttpResponseMessage> Post(
            Exception exception,
            string senderClass = "LogglySender",
            [CallerMemberName] string callingMethod = null)
                => PostLog(exception.Info(true, true), 
                    senderClass, callingMethod);


        private static async Task<HttpResponseMessage> PostLog(
            string message, string senderClass, string callingMethod)
        {
            if (_token.IsBlank())
                //throw Fault.CallFirst(nameof(Initialize));
                return null;

            var tag = $"{senderClass},{callingMethod},{_userAgent}";
            var url = $"https://logs-01.loggly.com/inputs/{_token}/tag/{tag}";
            var txt = new StringContent(message);

            return await _client.PostAsync(url, txt);
        }
    }
}
