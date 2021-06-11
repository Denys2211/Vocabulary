using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Vocabulary.Model;

namespace Vocabulary.Services
{
    public interface IDataRepository<T>
    {

        Task<bool> AddInDataBase(string name, params string[] input);

        Task<IEnumerable<Words>>  ReadDataBase(bool forceRefresh = false);

        Task<bool> DeleteDataTable(string name, params string[] input);
    }
}
