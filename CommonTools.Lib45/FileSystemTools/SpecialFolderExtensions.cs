using System;
using System.Linq;

namespace CommonTools.Lib45.FileSystemTools
{
    public static class SpecialFolderExtensions
    {
        public static string Path(this Environment.SpecialFolder foldr)
            => Environment.GetFolderPath(foldr);


        public static string Path(this Environment.SpecialFolder foldr, params string[] paths)
        {
            var list = paths.ToList();
            list.Insert(0, foldr.Path());
            return System.IO.Path.Combine(paths.ToArray());
        }
    }
}
