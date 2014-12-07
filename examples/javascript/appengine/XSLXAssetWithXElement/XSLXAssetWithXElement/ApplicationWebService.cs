using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;
using System.Data.MySQL;


namespace XSLXAssetWithXElement
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {


        //Implementation not found for type import :
        //type: System.Data.SQLite.SQLiteCommand
        //method: Void .ctor(System.String, System.Data.SQLite.SQLiteConnection)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        //assembly: W:\XSLXAssetWithXElement.ApplicationWebService.exe
        //type: XSLXAssetWithXElement.Data.Book1+Sheet1+Queries, XSLXAssetWithXElement.ApplicationWebService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        //offset: 0x0006
        // method:System.Threading.Tasks.Task Create(System.Data.SQLite.SQLiteConnection)

        static ApplicationWebService()
        {
            // ex = {"Could not load file or assembly 'ScriptCoreLib.Ultra, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.":"ScriptCoreLib.Ultra, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null"}

            //{ var r = typeof(ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda); }
            { var r = typeof(System.Data.SQLite.SQLiteConnection); }
            { var r = typeof(ScriptCoreLib.Library.StringConversions); }

#if DEBUG
            #region QueryExpressionBuilder.WithConnection
            QueryExpressionBuilder.WithConnection =
                y =>
                {
                    // jsc should imply it?

                    var cc = new SQLiteConnection(
                        new SQLiteConnectionStringBuilder
                        {
                            DataSource = "file:Book1.xlsx.sqlite"
                        }.ToString()
                    );

                    cc.Open();
                    y(cc);
                    cc.Dispose();
                };
            #endregion
#else

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
                        var QDataSource = "XSLXAssetWithXElement";

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


#endif
        }

        public void WebMethod2()
        {
            //  <h2> <i>Could not load file or assembly 'ScriptCoreLib.Extensions

            // Additional information: Invalid connection string: invalid URI

            var x = new XSLXAssetWithXElement.Data.Book1Sheet1();

            // shall the key route to gethashcode?

            // ex.Message = "SQL logic error or missing database\r\ntable Book1.Sheet1 has no column named Sheet2"

            var data = new Data.Book1Sheet1Row { Zoo = 77, Element = new XElement("foo"), Sheet2 = Data.Sheet2.EUR, Flag = true, Sheet14 = Data.Sheet2.ZEN };
            var newKey = x.Insert(data);

            var data2 = new Data.Book1Sheet1Row { Zoo = 77, Element = new XElement("foo"), Sheet2 = Data.Sheet2.EUR, Flag = false, Sheet14 = Data.Sheet2.ZEN };
            // implemented for appengine yet?
            var newKey2 = x.InsertAsync(data2);
            // newKey2 = Id = 0x00000001, Status = WaitingForActivation, Method = "{null}", Result = "{Not yet computed}"


            //Implementation not found for type import :
            //type: System.Threading.Tasks.Task

            //method: System.Threading.Tasks.Task`1[TResult[]] WhenAll[TResult](System.Threading.Tasks.Task`1[TResult][])

            //Did you forget to add the [Script] attribute?
            //Please double check the signature!


            // implemented for appengine yet?
            var last3 = x.FirstOrDefaultAsync();
            // last3 = Id = 0x00000001, Status = RanToCompletion, Method = "{null}", Result = "1, , 77, PGZvbyAvPg==, True, 0, 2, 0, , 2014-12-07 7:20:24 PM"


            //new SQLiteDataAdapter().sele
            // Unable to cast object of type 'System.String' to type 'System.Xml.Linq.XElement
            //var y = x.SelectAllAsEnumerable().ToArray();
            var y = x.AsEnumerable().ToArray();

            Console.WriteLine(new { y.Length });


        }

    }
}
