using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMySqlConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var myConnectionString = "server=127.0.0.1;database=test;uid=root;";
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                string selectQuery = "show databases";
                MySqlCommand myCommand = new MySqlCommand(selectQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                var i = myCommand.ExecuteScalar();
                Console.WriteLine(i);
                myCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
