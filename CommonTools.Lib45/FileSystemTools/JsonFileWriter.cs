using CommonTools.Lib11.JsonTools;
using System.IO;
using System.Text;

namespace CommonTools.Lib45.FileSystemTools
{
    class JsonFileWriter
    {
    }


    public static partial class JsonFile
    {
        public static void Write<T> (T @object, string filepathOrName, bool indented = true)
        {
            //var frmt = indented ? Formatting.Indented : Formatting.None;
            //var json = JsonConvert.SerializeObject(@object, frmt);
            var json = @object.ToJson(indented);
            var path = filepathOrName.MakeAbsolute();
            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}
