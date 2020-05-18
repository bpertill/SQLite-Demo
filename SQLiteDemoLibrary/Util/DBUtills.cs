using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace SQLiteLibrary
{
    public class DBUtills : IDBUtills
    {
        public DBUtills()
        {
            Connectionstring = LoadConnectionString();
        }
        private readonly string Connectionstring;
        private string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        public DataTable ExecuteRead(string query, Dictionary<string, object> args = null)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;

            using var con = new SQLiteConnection(Connectionstring);
            con.Open();

            using var cmd = CreateCommandWithParameters(query, con, args);

            using var da = new SQLiteDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public int ExecuteWrite(string query, Dictionary<string, object> args = null)
        {
            using var con = new SQLiteConnection(Connectionstring);
            con.Open();

            using var cmd = CreateCommandWithParameters(query, con, args);

            return cmd.ExecuteNonQuery();
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
