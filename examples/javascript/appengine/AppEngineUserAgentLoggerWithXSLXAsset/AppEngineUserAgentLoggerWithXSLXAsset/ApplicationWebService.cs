extern alias global_scle;
using AppEngineUserAgentLoggerWithXSLXAsset.Design;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;
using System.Data.MySQL;

namespace AppEngineUserAgentLoggerWithXSLXAsset
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {
        #region appengine


        //        InternalWebMethodInfo.AddField { FieldName = field_ClientTime, FieldValue =  }
        //Nov 25, 2013 12:41:23 AM com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver openConnection
        //SEVERE: Could not allocate a connection
        //com.mysql.jdbc.exceptions.jdbc4.CommunicationsException: Communications link failure

        //The last packet sent successfully to the server was 0 milliseconds ago. The driver has not received any packets from the server.
        //        at sun.reflect.NativeConstructorAccessorImpl.newInstance0(Native Method)

        // "C:\util\xampp-win32-1.8.0-VC9\xampp\mysql_start.bat"

        #endregion

        public long ScreenWidth;
        public long ScreenHeight;

        public string ClientTime;

        #region WebServiceHandler
        [EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(
                               DesignerSerializationVisibility.Hidden)]
        [Obsolete("experimental")]
        public WebServiceHandler WebServiceHandler { set; get; }
        #endregion

        static ApplicationWebService()
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAppEngineOrderByThenGroupBy\ApplicationWebService.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140908

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
                        var QDataSource = "AppEngineUserAgentLoggerWithXSLXAsset";

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

            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\ApplicationWebService.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140908

        }


        //public async Task<DataTable> GetVisitHeadersFor(Book1BSheet1Key visit)
        public Task<DataTable> GetVisitHeadersFor(Book1BSheet1Key visit)
        {
            Console.WriteLine("enter GetVisitHeadersFor");
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140908

            //at ScriptCoreLibJava.BCLImplementation.System.Diagnostics.__Debugger.Break(__Debugger.java:32)
            //at ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder_1.SetException(__AsyncTaskMethodBuilder_1.java:49)

            //at ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.Where(QueryExpressionBuilder.java:137)
            //at ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.Where(QueryExpressionBuilder.java:137)
            //at ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.Where(QueryExpressionBuilder.java:137)

            return (
                from k in new Design.Book1BSheet2()
                where k.Sheet1 == visit
                select k
            ).AsDataTable().AsResult();



            // we need a diagram showing us
            // how much faster will we make this call if
            // we move the filtering from web app into database


            // if the client side were to do this,
            // all call sites would need automatically be sent to the server
            // including the ctor
        }



        public class NotifyTuple
        {
            public DataTable DataSource;

            public Design.Book1BSheet1Key visit;
        }

        public async Task Reset()
        {
            Console.WriteLine("Reset");

            new AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1BSheet1().Delete();

            //return new AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1B.Sheet1.Queries().WithConnection(
            //    c =>
            //    {

            //        // http://www.w3schools.com/sql/sql_drop.asp
            //        var _Sheet1 = new SQLiteCommand("drop table `" + AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1B.Sheet1.Queries.QualifiedTableName + "`", (SQLiteConnection)c).ExecuteNonQuery();
            //        var _Sheet2 = new SQLiteCommand("drop table `" + AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1B.Sheet2.Queries.QualifiedTableName + "`", (SQLiteConnection)c).ExecuteNonQuery();

            //        return default(object).AsResult();
            //    }
            //);
        }

        public Task<NotifyTuple> Notfiy()
        {
            Console.WriteLine("server in Notify");

            // https://code.google.com/p/googleappengine/issues/detail?id=803

            //var ds = Design.Book1.GetDataSet().Tables.
            var x = new Design.Book1BSheet1();



            var now = DateTime.Now;
            var ServiceTime = now.ToString();

            Console.WriteLine("server in Notify, Insert");
            var visit = x.Insert(
                new Design.Book1BSheet1Row
                {
                    // jsc experience should auto detect, 
                    // implicit column types

                    // should we infer the type?
                    // should we use dynamic? dynamic has no intellisense

                    ScreenWidth = this.ScreenWidth,
                    ScreenHeight = this.ScreenHeight,


                    // not available for AppEngine?
                    // http://stackoverflow.com/questions/8787463/how-to-identify-ip-address-of-requesting-client

                    IPAddress = WebServiceHandler.Context.Request.UserHostAddress,

                    // we are now logging all headers
                    //UserAgent = WebServiceHandler.Context.Request.UserAgent,

                    ClientTime = this.ClientTime,
                    ServiceTime = ServiceTime,
                }
            );

            // 2AD6F7000000000
            Console.WriteLine(new { visit });

            //visit.Sheet2().

            #region Headers
            var y = new Design.Book1BSheet2();



            //y[visit].Insert();
            //visit.Sheet2().Insert();

            var h = this.WebServiceHandler.Context.Request.Headers;

            foreach (var item in h.AllKeys)
            {
                // InsertRange
                y.Insert(
                   new Design.Book1BSheet2Row
                   {
                       HeaderKey = item,
                       HeaderValue = h[item],

                       // can jsc auto bind to key? 
                       // what if the sheet we are referring to is in another dll/nuget?
                       // what if there are tuples and triplets that start to bind data for us?

                       Sheet1 = visit
                   }
               );
            }
            #endregion




            var DataSource = x.AsDataTable();

            Console.WriteLine("server in Notify, DataSource " + new { DataSource.Rows.Count });

            Console.WriteLine("server in Notify, exiting");

            return new NotifyTuple
            {
                DataSource = DataSource,
                visit = visit
            }.AsResult();
        }

    }


}
