using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Query.Experimental;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.MySQL;

namespace TestAppEngineInsert
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        static ApplicationWebService()
        {
            // jsc should not try to do cctor on client side
            // X:\jsc.svn\examples\javascript\Test\TestWebServiceStaticConstructor\TestWebServiceStaticConstructor\ApplicationWebService.cs



            #region MySQLConnection

            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

            // the safe way to hint we need to talk PHP dialect
            QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.MySQL;
            QueryExpressionBuilder.WithConnection =
                y =>
            {
                Console.WriteLine("enter WithConnection");

                //var DataSource = "file:xApplicationPerformance.xlsx.sqlite";
                var cc0 = new MySQLConnection(

                    new System.Data.MySQL.MySQLConnectionStringBuilder
                {
                    //Database = 

                    UserID = "root",
                    Server = "127.0.0.1",

                    //SslMode = MySQLSslMode.VerifyFull

                    //ConnectionTimeout = 3000

                }.ToString()
                //new MySQLConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
                );





                // Additional information: Authentication to host '' for user '' using method 'mysql_native_password' failed with message: Access denied for user ''@'asus7' (using password: NO)
                // Additional information: Unable to connect to any of the specified MySQL hosts.
                cc0.Open();

                #region use db
                {
                    //var a = Assembly.GetExecutingAssembly().GetName();


                    // SkipUntilIfAny ???
                    //var QDataSource = a.Name + ":" + DataSource.SkipUntilIfAny("file:").TakeUntilIfAny(".xlsx.sqlite");
                    var QDataSource = "TestAppEngineInsert";

                    // QDataSource.Length = 76
                    var QLengthb = QDataSource.Length;

                    // Database	64
                    cc0.CreateCommand("CREATE DATABASE IF NOT EXISTS `" + QDataSource + "`").ExecuteScalar();
                    cc0.CreateCommand("use `" + QDataSource + "`").ExecuteScalar();
                }
                #endregion

                y(cc0);


                // jsc java does the wrong thing here
                cc0.Close();
                //cc0.Dispose();
                Console.WriteLine("exit WithConnection");
            };
            #endregion
        }

        public void WebMethod2(Action<string> yield)
        {
            // ThreadLocal SynchronizationContext aware ConnectionPool?
            var n = new PerformanceResourceTimingData2ApplicationPerformance();

            var rid = n.Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 13,

                // conversion done in AddParameter
                // .stack rewriter needs to store struct. can we create new byref struct parameters?
                //EventTime = DateTime.Now.AddDays(-0),

                // conversion done in Insert?
                z = new XElement("goo", "foo")
            }
            );

            // { LastInsertRowId = 2 }
            Console.WriteLine("after insert " + new { rid });


            var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

            Console.WriteLine(new { c, rid });

            // I/System.Console( 7320): {{ c = 18, rid = 18 }}
            //return new { c, rid }.ToString();
            yield(
                "TestAndroidInsert " + new { c, rid }.ToString()
                );

        }

    }
}
