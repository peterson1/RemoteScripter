using CommonTools.Lib11.DataStructures;
using CommonTools.Lib11.ExceptionTools;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommonTools.Lib45.LiteDbTools
{
    public abstract partial class SharedCollectionBase<T>
        where T : IDocumentDTO
    {
        public T Find(int recordId, bool errorIfMissing = true)
        {
            using (var db = _db.OpenRead())
            {
                var rec = GetCollection(db).FindById(recordId);

                if (rec == null && errorIfMissing)
                    throw RecordNotFoundException.For<T>("Id", recordId);

                return rec;
            }
        }


        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            using (var db = _db.OpenRead())
                return GetCollection(db).Find(predicate).ToList();
        }


        private List<T> Find(Query query)
        {
            using (var db = _db.OpenRead())
                return GetCollection(db).Find(query).ToList();
        }


        public virtual List<T> GetAll()
        {
            using (var db = _db.OpenRead())
                return GetCollection(db).FindAll().ToList();
        }


        public Dictionary<int, T> ToDictionary()
            => GetAll().ToDictionary(_ => _.Id);



        public bool Any() => CountAll() != 0;


        public T Earliest()
        {
            using (var db = _db.OpenRead())
            {
                var colxn = GetCollection(db);
                var minId = colxn.Min();
                return colxn.FindById(minId);
            }
        }


        public T Latest()
        {
            using (var db = _db.OpenRead())
            {
                var colxn = GetCollection(db);
                var maxId = colxn.Max();
                return colxn.FindById(maxId);
            }
        }


        public int Count(Expression<Func<T, bool>> predicate)
        {
            using (var db = _db.OpenRead())
                return GetCollection(db).Count(predicate);
        }


        public int CountAll()
        {
            using (var db = _db.OpenRead())
                return GetCollection(db).Count();
        }


        private T Single(string field, BsonValue value, bool errorIfMissing)
        {
            using (var db = _db.OpenRead())
            {
                var coll = GetCollection(db);

                if (coll.Count() == 0)
                    return DefaultOrThrow(field, value, errorIfMissing);

                var matches = coll.Find(Query.EQ(field, value));

                if (!matches.Any())
                    return DefaultOrThrow(field, value, errorIfMissing);

                if (matches.Count() > 1)
                    throw DuplicateRecordsException.For(matches, field, value);

                return matches.Single();
            }
        }


        private static T DefaultOrThrow(string field, BsonValue value, bool errorIfMissing)
        {
            if (errorIfMissing)
                throw RecordNotFoundException.For<T>(field, value);
            else
                return default(T);
        }


        public T ByName(string recordName, bool errorIfMissing = true, string field = "Name")
            => Single(field, recordName, errorIfMissing);


        public bool HasName(string recordName, string field = "Name") 
            => ByName(recordName, false, field) != null;


        public bool TryGetName(string recordName, out T record, string field = "Name")
        {
            record = Single(field, recordName, false);
            return record != null;
        }


        public bool AnotherHasName(int recId, string recordName, string field = "Name")
        {
            var matches = Find(Query.EQ(field, recordName));
            if (!matches.Any()) return false;
            if (matches.Count() == 1 && matches.Single().Id == recId) return false;
            return true;
        }
    }
}
