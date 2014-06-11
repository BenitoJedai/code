using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;
using ScriptCoreLib.Shared.Data.Diagnostics;

namespace TestSelectGroupByAndConstant
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");


        public ApplicationWebService()
        {
            Console.WriteLine(
                new { Thread.CurrentThread.ManagedThreadId }
                );

            // can we override CLR SQLite to MySQL? or even use both of them?

            //Data.PerformanceResourceTimingData2.ApplicationPerformance.Queries.


            IDbConnectionExtensions.VirtualCreateCommand =
            (IDbConnection c, string CommandText) =>
            {
                var sql = ScriptCoreLib.PHP.Data.SQLiteToMySQLConversion.Convert(CommandText);
                var x = c.CreateCommand();

                Console.WriteLine(new { sql });
                x.CommandText = sql;
                return x;
            };
            WithConnectionLambda.VirtualWithConnection =
                DataSource =>
                {
                    Console.WriteLine("enter VirtualWithConnection");

                    return y =>
                    {
                        // DeclaringType = {Name = "Queries" FullName = "TestSelectGroupByAndConstant.Data.PerformanceResourceTimingData2+ApplicationPerformance+Queries"}
                        // y = {Method = {System.Threading.Tasks.Task Create(System.Data.SQLite.SQLiteConnection)}}
                        // 
                        Console.WriteLine("enter y VirtualWithConnection");

                        // y = {Method = {System.Threading.Tasks.Task Create(System.Data.IDbConnection)}}



                        var csb = new System.Data.MySQL.MySQLConnectionStringBuilder
                        {
                            UserID = "root",
                            Server = "127.0.0.1",
                        };

                        var c0 = new System.Data.MySQL.MySQLConnection(csb.ToString());
                        c0.Open();


                        // Error	3	The type or namespace name 'MySQL' does not exist in the namespace 'System.Data' (are you missing an assembly reference?)	X:\jsc.internal.svn\compiler\jsx.reflector\ReflectorWindow.AddWebApplication.cs	26	19	jsx.reflector
                        // TargetFrameworkAttribute

                        //Additional information: You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'AUTOINCREMENT, 


                        // Additional information: Unknown database 'file:performanceresourcetimingdata2.xlsx.sqlite-1'

                        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs
                        c0.CreateCommand("CREATE DATABASE IF NOT EXISTS `" + DataSource + "-1`").ExecuteScalar();
                        c0.CreateCommand("use `" + DataSource + "-1`").ExecuteScalar();


                        // Additional information: Unknown column 'EventTime' in 'field list'
                        var ret = y(c0);


                        c0.Close();

                        return ret;
                    };
                };
        }


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            //new Data.PerformanceResourceTimingData2.ApplicationPerformance().Clear();

            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\WithConnectionLambda.cs
            //new Data.PerformanceResourceTimingData2.ApplicationPerformance.Queries().WithConnection


            //Data.PerformanceResourceTimingData2.ApplicationPerformance.Queries.Create(

            var keys = new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                // does the insert report the values to us?
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-0) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1), domComplete = 5 }
            );










            var LoginCount = 33;




            // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs

            // http://www.sqlite.org/lang_select.html#fromclause
            // http://stackoverflow.com/questions/774475/what-joins-does-sqlite-support
            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                        //from offset in Enumerable.Range(33, 3)
                        //select new
                        //{
                        //    x.Key,
                        //    x.EventTime,
                        //    x.domComplete,
                        //    offset
                        //} into x

                    group x by new { x.domComplete } into g
                    //group x by new { x.offset } into g
                    select new
                    {
                        //offset = g.Key.offset,

                        // need to select atleast one field!
                        Count = g.Count(),

                        LoginCount
                    };


            // 26cc:0001 AsDataTable { ElapsedMilliseconds = 177, IsAttached = True, caller = AsDataTable<TElement> at offset 59 in file:line:column X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsDataTable.cs:159:13
            var dt = q.AsDataTable();

            //var qa = q.ToArray();

            Debugger.Break();

        }


    }
}
