using System.Collections.Generic;
using System.Data;

namespace SQLiteLibrary
{
    public interface IDBUtills
    {
        DataTable ExecuteRead(string query, Dictionary<string, object> args = null);
        int ExecuteWrite(string query, Dictionary<string, object> args = null);
    }
}