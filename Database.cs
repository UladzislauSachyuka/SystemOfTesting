using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOfTesting
{
    internal class Database
    { 
        MySqlConnection connect = new MySqlConnection ("server=localhost;port=8889;username=root;password=root;database=hello");
      
        public void OpenConnection()    
        {
            if(connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        public void CloseConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }

        public MySqlConnection getConnection()
        {
            return connect; 
        }
    }
}
