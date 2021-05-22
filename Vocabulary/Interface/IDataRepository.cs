using System.Collections.Generic;
using Vocabulary.Model;

namespace Vocabulary.Interface
{
    public interface IDataRepository
    {

        void AddInDataBase(string name, params string[] inpu);

        IEnumerable<Words> ReadDataBase();

        void DeleteDataTable(string name);
    }
}
