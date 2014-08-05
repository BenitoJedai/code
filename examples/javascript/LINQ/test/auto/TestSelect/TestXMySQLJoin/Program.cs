using ScriptCoreLib.Query.Experimental;
using System;
using System.Data;
using System.Data.MySQL;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Reflection;
using ScriptCoreLib.Extensions;
using TestXMySQLJoin;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs
        #region MySQLConnection

        // the idea is to test MySQL as we have LINQ to SQL also running in chrome now
        var mysqld = @"C:\util\xampp-win32-1.8.0-VC9\xampp\mysql\bin\mysqld.exe";
        // --standalone --console

        var mysqldp = Process.Start(mysqld, " --standalone --console");

        // Additional information: WaitForInputIdle failed.  This could be because the process does not have a graphical interface.
        //mysqldp.WaitForInputIdle();
        Thread.Sleep(5500);

        // the safe way to hint we need to talk PHP dialect
        QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.MySQL;
        QueryExpressionBuilder.WithConnection =
            y =>
            {
                var DataSource = "file:xApplicationPerformance.xlsx.sqlite";
                var cc0 = new MySQLConnection(

                    new System.Data.MySQL.MySQLConnectionStringBuilder
                {


                    UserID = "root",
                    Server = "127.0.0.1",

                }.ToString()
                //new MySQLConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
                );





                // Additional information: Authentication to host '' for user '' using method 'mysql_native_password' failed with message: Access denied for user ''@'asus7' (using password: NO)
                // Additional information: Unable to connect to any of the specified MySQL hosts.
                var st = Stopwatch.StartNew();

                while (true)
                    try
                    {
                        Thread.Sleep(300);

                        cc0.Open();
                        break;
                    }
                    catch
                    {
                        if (st.ElapsedMilliseconds > 6000)
                            // give up
                            throw;
                    }

                #region use db
                {
                    var a = Assembly.GetExecutingAssembly().GetName();


                    // SkipUntilIfAny ???
                    var QDataSource = a.Name + ":" + DataSource.SkipUntilIfAny("file:").TakeUntilIfAny(".xlsx.sqlite");

                    // QDataSource.Length = 76
                    var QLengthb = QDataSource.Length;

                    // Database	64
                    cc0.CreateCommand("CREATE DATABASE IF NOT EXISTS `" + QDataSource + "`").ExecuteScalar();
                    cc0.CreateCommand("use `" + QDataSource + "`").ExecuteScalar();
                }
                #endregion

                y(cc0);

                cc0.Dispose();
            };
        #endregion



        new PerformanceResourceTimingData2ApplicationPerformance().Insert(
           new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            Tag = "first insert"
        }
       );

        new PerformanceResourceTimingData2ApplicationResourcePerformance().Insert(
             new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
        {
            connectStart = 5,
            Tag = "first insert"
        }
         );

        // Additional information: You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near '== `y`.`connectStart`
        var q = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()
            join y in new PerformanceResourceTimingData2ApplicationResourcePerformance() on x.connectStart equals y.connectStart
            select new
            {
                field1 = x.connectStart,
                field2 = y.connectStart,
                field3 = y.connectStart,
            }

        );

        var f = q.FirstOrDefault();

        Console.WriteLine(new { f });
        // { f = { field1 = 5, field2 = 5, field3 = 5 } }

        mysqldp.CloseMainWindow();

        Debugger.Break();

    }
}
