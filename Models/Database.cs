using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace RefactorThis.Models
{
    public class Database
    {

        private const string ConnectionString = "Data Source=App_Data/products.db";
        private var Connection;
        private var Command;
        

        public Database()
        {
            Connection = NewConnection();
            Connection.open();
        }

        public SqliteConnection NewConnection()
        {
            return new SqliteConnection(ConnectionString);
        }


   
        public Query(string sql)
        {
            
            try
            {
                
                Command = conn.CreateCommand();
                Command.CommandText = sql;
                var result = cmd.ExecuteReader();
            }
            catch (MySqlException e)
            {
                return e.message;

            }

            return result;
        }

        ~Database()
        {
            Connection.close();
        }


    }
}