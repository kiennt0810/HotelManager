using System;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbHelper
    {
        private static MySqlConnection connection;

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = "server=localhost;user id=root;password=123456789;port=3306;database=hotelmanager1;"
                };
            }
            return connection;
        }
        // public static MySqlDataReader ExecQuery(string query)
        // {
        //     MySqlDataReader command = new MySqlDataReader(query, connection);
        //     return command.ExecuteReader();
        // }
    }
}