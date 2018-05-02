using System;
using System.Collections.Generic;

namespace CommonTools.Lib11.DatabaseTools
{
    public interface ISimpleRepo<T>
    {
        event EventHandler<T> ContentChanged;

        List<T> GetAll  ();
        int     Insert  (T newRecord);
        bool    Update  (T changedRecord);
        bool    Delete  (T record);
    }
}
