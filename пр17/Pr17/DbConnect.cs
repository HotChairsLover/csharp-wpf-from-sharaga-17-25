using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Pr17
{
    class DbConnect
    {
        public static MySqlDataReader Connection(string query)
        {
            string connectString = "server=localhost;port=3306;database=myDB;uid=root;password=root";
            MySqlConnection mySqlConnection = new MySqlConnection(connectString);
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
            return mySqlCommand.ExecuteReader();
        }
    }
}
