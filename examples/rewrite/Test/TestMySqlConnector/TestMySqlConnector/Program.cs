using MySql.Data.MySqlClient;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.MySQL;
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
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140609/mysql
                // "C:\util\xampp-win32-1.8.0-VC9\xampp\mysql_start.bat"
                // ex = {"Authentication to host '192.168.1.211' for user 'arvo' using method
                // 'mysql_native_password' failed with message: Reading from the stream has failed."}



                var uri = new Uri("mysql://arvo:xxx@192.168.1.211");

                // 192.168.1.211:3306 arvo xxx
                var myConnectionString = "server=127.0.0.1;database=test;uid=root;";
                //var myConnectionString = "server=192.168.1.211;database=test;uid=arvo;password=xxx;";
                // DbConnection
                var myConnection = new MySQLConnection(myConnectionString);
                string selectQuery = "show databases";
                // DbCommand
                var myCommand = new MySQLCommand(selectQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                //var i = myCommand.ExecuteScalar();

                var a = new __DbDataAdapter { SelectCommand = myCommand };
                var t = new DataTable();
                //var ds = new DataSet();
                a.Fill(t);

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
