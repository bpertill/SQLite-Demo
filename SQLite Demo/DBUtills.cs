using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace SQLite_Demo
{
    public static class DBUtills
    {
        //TODO Add ConfigFile for Connectionstring
        private static readonly string Connectionstring = "Data Source=.\\DemoSQLiteDb.db ";

        public static DataTable ExecuteRead(string query, Dictionary<string, object> args = null)
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

        public static int ExecuteWrite(string query, Dictionary<string, object> args = null)
        {
            using var con = new SQLiteConnection(Connectionstring);
            con.Open();

            using var cmd = CreateCommandWithParameters(query, con, args);

            return cmd.ExecuteNonQuery();
        }

        public static SQLiteCommand CreateCommandWithParameters(string query, SQLiteConnection con, Dictionary<string, object> args = null)
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
