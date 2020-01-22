using System;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace RefactorThis.Models
{
    // Very basic Database connection.
    public class Database
    {

        private const string ConnectionString = "Data Source=C:/Users/Alex/Downloads/RefactorThis_Dev/RefactorThis/App_Data/products.db";
        private SqliteConnection Connection { get; set; }
        public SqliteCommand Command { get; set; }
        public SqliteDataReader reader { get; private set; }

        // Start the connection in constructor
        public Database()
        {
            Connection = new SqliteConnection(ConnectionString);
            Connection.Open();
            // I had command in here because there was some SQL injection prevention I was trying to get to work which ended up failing so I reverted it.
            Command = Connection.CreateCommand();
        }


        // prevent duplication code
        public void Query(string sql)
        {
            try
            {
                Command.CommandText = sql;
                reader = Command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Close connection on destructor.
        ~Database()
        {
            Connection.Close();
        }


    }
}