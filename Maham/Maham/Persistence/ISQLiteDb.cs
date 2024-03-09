using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Maham.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnectionAsync();
    }
}
