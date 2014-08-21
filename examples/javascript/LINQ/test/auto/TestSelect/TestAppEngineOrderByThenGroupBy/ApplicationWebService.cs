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
using ScriptCoreLib.Query.Experimental;
using System.Data.MySQL;

namespace TestAppEngineOrderByThenGroupBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140817/appengine

        // step 1. run it under 199
        // step 2. add a button do show sql syntax
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\JVMCLRSyntaxOrderByThenGroupBy\Program.cs

        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRAsync\TestJVMCLRAsync\Program.cs
        // can we send in the caller IButtonProxy ?
        // as long the interface is async, one way we could do it.
        // if it allows a continuation we would have
        // to reinit our state
        // this would be possible only if we encrypt and sign
        // our state
        // as we cannot trust the other device to not change our expected state

        static ApplicationWebService()
        {
            // jsc should not try to do cctor on client side

            //........................................................1464:02:01 RewriteToAssembly error: System.NotImplementedException: { SourceMethod = Void <.cctor > b__0(System.Action`1[System.Data.IDbConnection]) }
            //at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.WebServiceForJavaScript.WriteMethod(MethodInfo SourceMethod) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebServiceForJavaScript.cs:line 520

            #region MySQLConnection

            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

            // the safe way to hint we need to talk PHP dialect
            QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.MySQL;
            QueryExpressionBuilder.WithConnection =
                y =>
            {
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
                    var QDataSource = "TestAppEngineOrderByThenGroupBy";

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
        }

        public async Task<string> WebMethod2()
        //public void WebMethod2(Action<string> yield)
        {

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
                orderby x.connectEnd ascending
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

            // f will be null, as no connection is available!
            return new { message = "ok", f }.ToString();
            //yield(new { message = "ok" }.ToString());
        }
    }
}
