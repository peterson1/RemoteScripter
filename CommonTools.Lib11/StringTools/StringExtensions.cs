using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTools.Lib11.StringTools
{
    public static class StringExtensions
    {
        public static string Before(this string text, string findThis)
        {
            var pos = text.IndexOf(findThis);
            if (pos == -1) return text;
            return text.Substring(0, pos);
        }


        public static string Between(this string fullText,
                    string firstString, string lastString,
                    bool seekLastStringFromEnd = false)
        {
            if (fullText.IsBlank()) return string.Empty;

            int pos1 = fullText.IndexOf(firstString) + firstString.Length;
            if (pos1 == -1) return fullText;

            int pos2 = seekLastStringFromEnd ?
                fullText.LastIndexOf(lastString)
                : fullText.IndexOf(lastString, pos1);
            if (pos2 == -1 || pos2 <= pos1) return fullText;

            return fullText.Substring(pos1, pos2 - pos1);
        }


        public static bool IsBlank(this string text)
        {
            if (text == null) return true;
            return string.IsNullOrWhiteSpace(text);
        }



        public static bool HasText(this string lookInHere, string findThis)
        {
            var allLength = lookInHere.Length;
            var subLength = findThis.Length;
            var difLength = lookInHere.Replace(findThis, String.Empty).Length;
            return (allLength - difLength) / subLength > 0;
        }


        /// <summary>
        /// From: HashLib 2.1 (Dec 29, 2013) Stable
        /// http://hashlib.codeplex.com/
        /// </summary>
        /// <param name="utf8Text"></param>
        /// <returns></returns>
        public static string SHA1ForUTF8(this string utf8Text)
        {
            if (utf8Text == null) utf8Text = "";
            var algo = new HashLib.Crypto.SHA1();
            var res = algo.ComputeString(utf8Text, Encoding.UTF8);
            return res.ToString().ToLower();
        }


        public static List<string> SplitTrim(this string text, string separator)
        {
            var split = text.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            return split.Select(x => x.Trim()).ToList();
        }


        public static string[] SplitByLine(this string text, 
            StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries) 
                => text.IsBlank() ? new string[] { }
                    : text.Split(new[] { "\r\n", "\r", "\n" }, splitOptions);
    }
}
