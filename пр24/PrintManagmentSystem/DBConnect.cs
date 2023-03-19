using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PrintManagmentSystem
{
    class DBConnect
    {
        public static MySqlDataReader Connection(string query)
        {
            string connectString = "server=localhost;port=3310;database=myDB;uid=root;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectString);
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
            return mySqlCommand.ExecuteReader();
        }
    }
}
