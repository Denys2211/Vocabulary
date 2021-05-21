using System;
using System.Collections.Generic;
using SQLite;
using Wocabulary.AppData;

namespace Vocabulary.Interface
{
    public interface IDataRepository
    {

        void AddInDataBase(string name, SQLiteConnection connection, double result, params string[] inpu);

        IEnumerable<Words> ReadDataBase(string name, SQLiteConnection connection);

        void DeleteDataTable(string name, SQLiteConnection connection);
    }
}
