extern alias global_scle;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppEngineUserAgentLoggerWithXSLXAsset
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {
        #region appengine

        //Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.ArgumentException: Illegal characters in path.
        //   at System.IO.Path.CheckInvalidPathChars(String path, Boolean checkAdditional)
        //   at System.IO.Path.NormalizePath(String path, Boolean fullCheck, Int32 maxPathLength, Boolean expandShortPaths)
        //   at System.IO.Path.GetFullPathInternal(String path)
        //   at System.IO.DirectoryInfo.Init(String path, Boolean checkHost)
        //   at System.IO.DirectoryInfo..ctor(String path)
        //   at ScriptCoreLib.Reflection.Options.ParameterDispatcherExtensions.AsParameterTo(String value, Object e, FieldInfo f) in x:\jsc.svn\core\ScriptCoreLib.Reflection.Options\ScriptCoreLib.Reflection.Options\ParameterDispatcherExtensions.cs:line 244

        //0001 02000058 AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService::<module>.SHA12e7f9b931303fcbae1427b5e9cb94063fc864d21@2130839642
        //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\files
        //C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe  -encoding UTF-8 -cp Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.3\lib\impl\*;C:\util\appengine-java-sdk-1.8.3\lib\shared\* -d "Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release" @"Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\files"
        //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\AppEngineUserAgentLoggerWithXSLXAsset\Design\Book1_Sheet1__SelectAllAsEnumerable_closure.java:26: incompatible types
        //found   : java.lang.Object
        //required: long
        //        row0.Key = /* unbox Book1Sheet1Key */_arg0.get_Item("Key");
        //                                                           ^
        //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\AppEngineUserAgentLoggerWithXSLXAsset\Design\Book1_Sheet2__SelectAllAsEnumerable_closure.java:26: incompatible types
        //found   : java.lang.Object
        //required: long
        //        row0.Key = /* unbox Book1Sheet2Key */_arg0.get_Item("Key");

        // X:\jsc.svn\examples\java\Test\TestUnboxEnum\TestUnboxEnum\Class1.cs

        //        InternalWebMethodInfo.AddField { FieldName = field_ClientTime, FieldValue =  }
        //Nov 25, 2013 12:41:23 AM com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver openConnection
        //SEVERE: Could not allocate a connection
        //com.mysql.jdbc.exceptions.jdbc4.CommunicationsException: Communications link failure

        //The last packet sent successfully to the server was 0 milliseconds ago. The driver has not received any packets from the server.
        //        at sun.reflect.NativeConstructorAccessorImpl.newInstance0(Native Method)

        // "C:\util\xampp-win32-1.8.0-VC9\xampp\mysql_start.bat"

        //Caused by: java.lang.RuntimeException: Table 'book1.xlsx.sqlite.sheet1' doesn't exist
        //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteCommand.ExecuteNonQuery(__SQLiteCommand.java:294)
        //        at AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1_Sheet1_Queries.Insert(Book1_Sheet1_Queries.java:56)



        #endregion

        public int ScreenWidth;
        public int ScreenHeight;

        public string ClientTime;

        #region WebServiceHandler
        [EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(
                               DesignerSerializationVisibility.Hidden)]
        [Obsolete("experimental")]
        public WebServiceHandler WebServiceHandler { set; get; }
        #endregion

        public Task<DataTable> GetVisitHeadersFor(Design.Book1Sheet1Key visit)
        {
            var visitkey = "" + (long)visit;

            // we need a diagram showing us
            // how much faster will we make this call if
            // we move the filtering from web app into database

            // y[visitkey].SelectAllAsEnumerable();

            return (
                from k in new Design.Book1.Sheet2()
                where k.Sheet1 == visitkey
                select k
            ).AsDataTable().ToTaskResult();

        }



        public class NotifyTuple
        {
            public DataTable DataSource;

            public Design.Book1Sheet1Key visit;
        }

        public Task<NotifyTuple> Notfiy()
        {

            // https://code.google.com/p/googleappengine/issues/detail?id=803

            var x = new Design.Book1.Sheet1();



            var now = DateTime.Now;
            var ServiceTime = now.ToString();

            var visit = x.Insert(
                new Design.Book1Sheet1Row
                {
                    // jsc experience should auto detect, 
                    // implicit column types

                    // should we infer the type?
                    // should we use dynamic? dynamic has no intellisense

                    ScreenWidth = "" + this.ScreenWidth,
                    ScreenHeight = "" + this.ScreenHeight,


                    // not available for AppEngine?
                    // http://stackoverflow.com/questions/8787463/how-to-identify-ip-address-of-requesting-client

                    IPAddress = WebServiceHandler.Context.Request.UserHostAddress,

                    // we are now logging all headers
                    //UserAgent = WebServiceHandler.Context.Request.UserAgent,

                    ClientTime = this.ClientTime,
                    ServiceTime = ServiceTime,
                }
            );

            //visit.Sheet2().

            var y = new Design.Book1.Sheet2();



            //y[visit].Insert();
            //visit.Sheet2().Insert();

            var h = this.WebServiceHandler.Context.Request.Headers;

            foreach (var item in h.AllKeys)
            {
                // InsertRange
                y.Insert(
                   new Design.Book1Sheet2Row
                   {
                       HeaderKey = item,
                       HeaderValue = h[item],

                       // can jsc auto bind to key? 
                       Sheet1 = "" + (long)visit
                   }
               );
            }


            #region auto
            // [FileNotFoundException: Could not load file or assembly 'ScriptCoreLib.Extensions, Version=1.0.0.0, C
            // [FileNotFoundException: Could not load file or assembly 'System.Data.SQLite, Version=1.0.89.0,
            var ref_SQLite = typeof(global::System.Data.SQLite.SQLiteCommand);

            var ref_ScriptCoreLib_Extensions = typeof(global_scle::ScriptCoreLib.Extensions.DataExtensions);
            #endregion

            return new NotifyTuple
            {
                DataSource = x.SelectAllAsDataTable(),
                visit = visit
            }.ToTaskResult();
        }

    }

    public static class X
    {
#if DEBUG

        //public static IEnumerable<Design.Book1Sheet2Row> Where(this Design.Book1.Sheet2 data, Expression<Func<Design.Book1Sheet2Row, bool>> f)
        public static IQueryable<Design.Book1Sheet2Row> Where(
            this Design.Book1.Sheet2 data,
            Expression<Func<Design.Book1Sheet2Row, bool>> f
            )
        {
            // http://www.codeproject.com/Tips/468215/Difference-Between-IEnumerable-and-IQueryable

            //.Lambda #Lambda1<System.Func`2[AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet2Row,System.Boolean]>(AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet2Row $k)
            //{
            //    $k.Sheet1 == .Constant<AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService+<>c__DisplayClass0>(AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService+<>c__DisplayClass0).visitkey
            //}

            var zf = f.Compile();

            // Error	5	Cannot implicitly convert type 'System.Collections.Generic.IEnumerable<AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet2Row>' to 'System.Linq.IQueryable<AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet2Row>'. An explicit conversion exists (are you missing a cast?)	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs	203	20	AppEngineUserAgentLoggerWithXSLXAsset

            return data.SelectAllAsEnumerable().Where(zf).AsQueryable();
        }
#else
        public static IEnumerable<Design.Book1Sheet2Row> Where(this Design.Book1.Sheet2 data, Func<Design.Book1Sheet2Row, bool> f)
        {
            return data.SelectAllAsEnumerable().Where(f);
        }
#endif



        public static IEnumerable<Design.Book1Sheet2Row> XSelectAllAsEnumerable(this Design.Book1.Sheet2 data)
        {
            var x = data.SelectAllAsDataTable();

            return x.Rows.AsEnumerable().Select(
                r =>
                {
                    Console.WriteLine("Book1Sheet2Key:");

                    //var KeyType = r.get
                    var KeyObject = r["Key"];
                    var KeyType = KeyObject.GetType();

                    Console.WriteLine(new { KeyObject, KeyType.FullName });

                    //            Caused by: java.lang.ClassCastException: java.lang.Integer cannot be cast to java.lang.Long
                    //at AppEngineUserAgentLoggerWithXSLXAsset.X._XSelectAllAsEnumerable_b__1(X.java:69)

                    var Key = (Design.Book1Sheet2Key)Convert.ToInt64(KeyObject);

                    return new Design.Book1Sheet2Row
                    {
                        Key = Key,
                        HeaderKey = (string)r["HeaderKey"],
                        HeaderValue = (string)r["HeaderValue"],
                        Sheet1 = (string)r["Sheet1"],
                    };
                }

            );

        }

        public static DataTable AsDataTable(this IEnumerable<Design.Book1Sheet2Row> source)
        {

            var x = new DataTable();

            x.Columns.Add("Key");
            x.Columns.Add("HeaderKey");
            x.Columns.Add("HeaderValue");
            x.Columns.Add("Sheet1");

            foreach (var item in source)
            {
                x.Rows.Add(
                    item.Key,
                    item.HeaderKey,
                    item.HeaderValue,
                    item.Sheet1
                );
            }

            return x;
        }
    }
}
