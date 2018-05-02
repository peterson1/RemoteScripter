using CommonTools.Lib11.DataStructures;
using LiteDB;

namespace CommonTools.Lib45.LiteDbTools
{
    public abstract partial class SharedCollectionBase<T>
        where T : IDocumentDTO
    {
        protected SharedLiteDB _db;

        public SharedCollectionBase(SharedLiteDB sharedLiteDB)
        {
            _db = sharedLiteDB;
        }


        protected LiteDatabase ReadableDB() => _db.OpenRead();
        protected LiteDatabase WritableDB() => _db.OpenWrite();


        public virtual LiteCollection<T> GetCollection(LiteDatabase db)
            => db.GetCollection<T>();
    }
}
