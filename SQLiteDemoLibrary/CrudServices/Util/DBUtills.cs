using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteLibrary.CrudServices
{
    public class DBUtills : IDBUtills
    {
        public DBUtills(IConfiguration config)
        {
            Connectionstring = config.GetConnectionString("Default");
        }
        private readonly string Connectionstring;

        public async Task<DataTable> ExecuteRead(string query, Dictionary<string, object> args = null)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;
              
            using var con = new SQLiteConnection(Connectionstring);
            await con.OpenAsync();

            using var cmd = CreateCommandWithParameters(query, con, args);

            using var da = new SQLiteDataAdapter(cmd);
            var dt = new DataTable();
            await Task.Run(() => da.Fill(dt));
            da.Dispose();
            return dt;
        }

        public async Task<int> ExecuteWrite(string query, Dictionary<string, object> args = null)
        {
            using var con = new SQLiteConnection(Connectionstring);
            await con.OpenAsync();

            using var cmd = CreateCommandWithParameters(query, con, args);

            return await cmd.ExecuteNonQueryAsync();
        }

        private SQLiteCommand CreateCommandWithParameters(string query, SQLiteConnection con, Dictionary<string, object> args = null)
        {
            var cmd = new SQLiteCommand(query, con);
            if (args != null)
            {
                foreach (KeyValuePair<string, object> entry in args)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
            }
            return cmd;
        }
    }
}
