using System.Collections.Generic;
using Vocabulary.Model;

namespace Vocabulary.Interface
{
    public interface IDataRepository
    {

        void AddInDataBase(string name, params string[] input);

        IEnumerable<Words> ReadDataBase();

        void DeleteDataTable(string name, params string[] input);
    }
}
