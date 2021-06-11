using System.Collections.Generic;

namespace Vocabulary.Model
{
    public interface IDataRepository
    {

        void AddInDataBase(string name, params string[] input);

        IEnumerable<Words> ReadDataBase();

        void DeleteDataTable(string name, params string[] input);
    }
}
