using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Data.SQLite;

namespace Cadeteria.Entidades
{
    public static class SQLiteData
    {
        private static SQLiteConnection sql_con;
        private static string dataDir = "Data Source = " + Path.Combine(Directory.GetCurrentDirectory() + "\\Data\\data.db");

        public static string DataDir { get => dataDir; set => dataDir = value; }
        public static SQLiteConnection Sql_con { get => sql_con; set => sql_con = value; }

        public static SQLiteDataReader ExecuteSQLiteQuery(string query)
        {
            
            SQLiteCommand sql_cmd = Sql_con.CreateCommand();
            sql_cmd.CommandText = query;
            SQLiteDataReader sql_rdr = sql_cmd.ExecuteReader();
            return sql_rdr;
        }

        public static void ExecuteSQLiteNonQuery(string query)
        {

            SQLiteCommand sql_cmd = Sql_con.CreateCommand();
            sql_cmd.CommandText = query;
            sql_cmd.ExecuteNonQuery();
        }

        public static void OpenConnection()
        {
            Sql_con = new SQLiteConnection(DataDir);
            Sql_con.Open();
        }

        public static void CloseConnection()
        {
            Sql_con.Close();
        }

    }
}
