using System;

namespace CommonTools.Lib45.LiteDbTools
{
    public class MetadataCollection : SharedCollectionBase<DbMetadata>
    {
        public MetadataCollection(SharedLiteDB sharedLiteDB) : base(sharedLiteDB)
        {
        }


        public string this[string key]
        {
            get => ByName(key, false)?.Value;
            set
            {
                if (TryGetName(key, out DbMetadata rec))
                {
                    rec.Value = value;
                    Update(rec);
                }
                else
                    AddPair(key, value);
            }
        }


        private void AddPair(string key, string value) 
                => Insert(new DbMetadata(key, value));


        internal void CreateInitialRecord() 
            => Insert(new DbMetadata("version", "0.01"));


        public void AddIfNone(string key, string value)
        {
            if (!HasName(key))
                AddPair(key, value);
        }
    }
}
