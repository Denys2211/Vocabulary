using System;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Vocabulary.Model;

namespace Vocabulary.Services
{
    public class WordsRepository : IDataRepository<Words>
    {

        private readonly SQLiteConnection connection;

        public WordsRepository()
        {
            connection = new SQLiteConnection(Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Wordsdata.db"));

            connection.CreateTable<Words>();
        }

        public async Task<bool> AddInDataBase(string name, params string[] input)
        {
            try
            {

                if (name == "Words")
                {
                    connection.Execute($"INSERT INTO {name}(EnglishWords, UkrainianWords, DateTime) VALUES ('{input[0]}','{input[1]}','{DateTime.Now}')");
                    return await Task.FromResult(true);
                }                   
                else

                    throw new Exception("Database Add error");

            }
            catch
            {
                throw new Exception("Database Add error");
            }

        }

        public async Task<IEnumerable<Words>> ReadDataBase(bool forceRefresh = false)

        {
            try
            {
                 List<Words> resourcesTable = connection.Table<Words>().ToList();

                if(resourcesTable.Count != 0)
                {
                    return await Task.FromResult(resourcesTable);
                }
                else
                {
                    resourcesTable = new List<Words>
                    {
                        new Words
                        {
                            UkrainianWords = "No Data",
                            EnglishWords ="No Data",
                            DateTime =DateTime.Now.ToString()
                        }
                    };
                    return await Task.FromResult(resourcesTable);


                }
            }
            catch
            {
                throw new Exception("Database reader error");
            }


        }
        public async Task<bool> DeleteDataTable(string name, params string[] input )
        {
            try
            {
                connection.Execute($"DELETE FROM {name} WHERE _Id = {input[0]}");
                connection.Execute($"UPDATE SQLITE_SEQUENCE SET SEQ = 0 WHERE NAME = '{name}'");
                return await Task.FromResult(true);
            }
            catch
            {
                throw new Exception("Database clean error");
            }
        }
        
    }
}
