using System;
using SQLite;
using System.Collections.Generic;
using Vocabulary.Interface;

namespace Wocabulary.AppData
{
    public class HistoryRepository : IDataRepository
    {
        static readonly object locker = new object();
        public void AddInDataBase(string name, SQLiteConnection connection, double result, params string[] input)
        {
            try
            {
                connection.CreateTable<Words>();

                if (name == "Words")

                    connection.Execute($"INSERT INTO History(Expression, Result, DateTime) VALUES ('{input[0]}','{result}','{DateTime.Now}')");
                else

                    throw new Exception("Database Add error");

            }
            catch
            {
                throw new Exception("Database Add error");
            }

        }

        public IEnumerable<Words> ReadDataBase(string name, SQLiteConnection connection)

        {
            try
            {
                lock (locker)
                {
                    return connection.Table<Words>().ToList();
                }
            }
            catch
            {
                throw new Exception("Database reader error");
            }


        }
        public void DeleteDataTable(string name, SQLiteConnection connection)
        {
            try
            {
                connection.Execute($"DELETE FROM {name}");
                connection.Execute($"UPDATE SQLITE_SEQUENCE SET SEQ = 0 WHERE NAME = '{name}'");

            }
            catch
            {
                throw new Exception("Database clean error");
            }
        }

        IEnumerable<Words> IDataRepository.ReadDataBase(string name, SQLiteConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
