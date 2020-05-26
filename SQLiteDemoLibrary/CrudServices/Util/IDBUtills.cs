using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace SQLiteLibrary.CrudServices
{
    /// <summary>
    /// Only needed if Dapper is not used
    /// </summary>
    public interface IDBUtills
    {
        Task<DataTable> ExecuteRead(string query, Dictionary<string, object> args = null);
        Task<int> ExecuteWrite(string query, Dictionary<string, object> args = null);
    }
}