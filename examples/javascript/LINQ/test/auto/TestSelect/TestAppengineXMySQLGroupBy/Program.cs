using ScriptCoreLib.Query.Experimental;
using System;
using System.Data;
using System.Data.MySQL;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Reflection;
using ScriptCoreLib.Extensions;
using TestAppengineXMySQLGroupBy;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        #region MySQLConnection

        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

        // the idea is to test MySQL as we have LINQ to SQL also running in chrome now
        //var mysqld = @"C:\util\xampp-win32-1.8.0-VC9\xampp\mysql\bin\mysqld.exe";
        //// --standalone --console

        //var mysqldp = Process.Start(mysqld, " --standalone --console");

        // Additional information: WaitForInputIdle failed.  This could be because the process does not have a graphical interface.
        //mysqldp.WaitForInputIdle();
        Thread.Sleep(3000);

        // the safe way to hint we need to talk PHP dialect
        QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.MySQL;
        QueryExpressionBuilder.WithConnection =
            y =>
            {
                var DataSource = "file:xApplicationPerformance.xlsx.sqlite";
                var cc0 = new MySQLConnection(

                    new System.Data.MySQL.MySQLConnectionStringBuilder
                {

                    UserID = "transact",
                    Server = "173.194.249.56",
                    Password = "L11gutam3Andm3id",
                    SslMode = MySQLSslMode.Required,
                    CertificateFile = @"X:\Monese\network\certs\coresql\client.pfx",

                }.ToString()
                    //new MySQLConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
                );





                // Additional information: Authentication to host '' for user '' using method 'mysql_native_password' failed with message: Access denied for user ''@'asus7' (using password: NO)
                // Additional information: Unable to connect to any of the specified MySQL hosts.

                try
                {
                    cc0.Open();
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
               
               
              
            };
        #endregion


        new PerformanceResourceTimingData2ApplicationPerformance().Delete();

        new PerformanceResourceTimingData2ApplicationPerformance().Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectEnd = 9,
            connectStart = 5,
            Tag = "first insert"
        },

            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 111,
            Tag = "middle insert"
        },

            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 11,
            Tag = "Last insert, selected by group by"
        }
        );


        // https://code.google.com/p/chromium/issues/detail?id=369239&can=5&colspec=ID%20Pri%20M%20Iteration%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified

        var f = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()
            //orderby x.Key ascending
            // MYSQL and SQLITE seem to behave differently? in reverse actually!
            orderby x.connectEnd descending
            // { f = { c = 3, Tag = first insert } }

            //orderby x.Key ascending
            // { f = { c = 3, Tag = Last insert, selected by group by } }
            // { f = { c = 3, Tag = first insert } }
            group x by x.connectStart into gg
            select new
            {
                c = gg.Count(),
                // need orderby x.Key descending !
                gg.Last().Tag
            }

        ).FirstOrDefault();

        System.Console.WriteLine(
            new { f }
            );

        //mysqldp.CloseMainWindow();
        Debugger.Break();


    }
}
