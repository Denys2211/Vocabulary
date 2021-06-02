using System;
using SQLite;
using System.Collections.Generic;
using Vocabulary.Interface;
using System.IO;

namespace Vocabulary.Model
{
    public class WordsRepository : IDataRepository
    {

        private readonly SQLiteConnection connection;

        public WordsRepository()
        {
            connection = new SQLiteConnection(Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Wordsdata.db"));

            connection.CreateTable<Words>();
        }

        static readonly object locker = new object();

        public void AddInDataBase(string name, params string[] input)
        {
            try
            {

                if (name == "Words")

                    connection.Execute($"INSERT INTO {name}(EnglishWords, UkrainianWords, DateTime) VALUES ('{input[0]}','{input[1]}','{DateTime.Now}')");
                else

                    throw new Exception("Database Add error");

            }
            catch
            {
                throw new Exception("Database Add error");
            }

        }

        public IEnumerable<Words> ReadDataBase()

        {
            try
            {
                 List<Words> resourcesTable = connection.Table<Words>().ToList();

                if(resourcesTable.Count != 0)
                {
                    lock (locker)
                    {
                        return resourcesTable;
                    }
                }
                else
                {
                    lock (locker)
                    {
                        return new List<Words> {
                            new Words {
                                UkrainianWords = "No Data",
                                EnglishWords ="No Data",
                                DateTime =DateTime.Now.ToString()
                            }
                        };
                    }

                }
            }
            catch
            {
                throw new Exception("Database reader error");
            }


        }
        public void DeleteDataTable(string name, params string[] input )
        {
            try
            {
                connection.Execute($"DELETE FROM {name} WHERE _Id = {input[0]}");
                connection.Execute($"UPDATE SQLITE_SEQUENCE SET SEQ = 0 WHERE NAME = '{name}'");

            }
            catch
            {
                throw new Exception("Database clean error");
            }
        }

        
    }
}
