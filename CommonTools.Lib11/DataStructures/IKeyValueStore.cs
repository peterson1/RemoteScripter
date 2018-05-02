using System;

namespace CommonTools.Lib11.DataStructures
{
    public interface IKeyValueStore
    {
        //T Get<T>(string key);

        string     GetText  (string key);
        DateTime?  GetDate  (string key);

        object this[string key] { get; set; }
    }
}
