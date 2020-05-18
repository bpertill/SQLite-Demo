using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SQLiteLibrary.CrudServices
{
    /// <summary>
    /// Only needed if Dapper is not used
    /// </summary>
    public interface IDBUtills
    {

        DataTable ExecuteRead(string query, Dictionary<string, object> args = null);
        int ExecuteWrite(string query, Dictionary<string, object> args = null);
    }
}