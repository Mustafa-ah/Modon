using System;
using System.IO;
using SQLite;
using Maham.iOS.Persistence;
using Maham.Persistence;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]
namespace Maham.iOS.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnectionAsync()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}