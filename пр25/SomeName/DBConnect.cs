using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SomeName
{
    class DBConnect
    {
        public static MySqlDataReader Select(string query)
        {
            string connection = "server=localhost;port=3306;database=mydb;uid=root;password=root";

            MySqlConnection myConnect = new MySqlConnection(connection);
            myConnect.Open();

            MySqlCommand command = new MySqlCommand(query, myConnect);
            return command.ExecuteReader();
        }
    }
}
