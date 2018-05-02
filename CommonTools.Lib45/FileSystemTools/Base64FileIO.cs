using System;
using System.IO;

namespace CommonTools.Lib45.FileSystemTools
{
    public static class Base64FileIO
    {
        public static void WriteBase64ToFile (this string b64String, string targetPath)
        {
            var byts = Convert.FromBase64String(b64String);
            File.WriteAllBytes(targetPath, byts);
        }


        public static string ReadFileAsBase64 (this string filePath)
        {
            var byts = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(byts);
        }
    }
}
