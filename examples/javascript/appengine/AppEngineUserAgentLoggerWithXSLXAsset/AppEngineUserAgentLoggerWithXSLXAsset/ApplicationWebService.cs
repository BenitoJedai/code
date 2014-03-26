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

namespace AppEngineUserAgentLoggerWithXSLXAsset
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {

        void References()
        {
            // [FileNotFoundException: Could not load file or assembly 'ScriptCoreLib.Extensions, Version=1.0.0.0, C
            // [FileNotFoundException: Could not load file or assembly 'System.Data.SQLite, Version=1.0.89.0,
            { var ref_SQLite = typeof(global::System.Data.SQLite.SQLiteCommand); }
            { var ref_ScriptCoreLib_Extensions = typeof(global_scle::ScriptCoreLib.Extensions.DataExtensions); }
        }

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

        public Task<DataTable> GetVisitHeadersFor(Book1BSheet1Key visit)
        {
            return
                from k in new Design.Book1B.Sheet2()
                where k.Sheet1 == visit
                select k;


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

        public Task Reset()
        {
            Console.WriteLine("Reset");

            return new AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1B.Sheet1.Queries().WithConnection(
                c =>
                {

                    // http://www.w3schools.com/sql/sql_drop.asp
                    var _Sheet1 = new SQLiteCommand("drop table `" + AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1B.Sheet1.Queries.QualifiedTableName + "`", (SQLiteConnection)c).ExecuteNonQuery();
                    var _Sheet2 = new SQLiteCommand("drop table `" + AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1B.Sheet2.Queries.QualifiedTableName + "`", (SQLiteConnection)c).ExecuteNonQuery();

                    return default(object).AsResult();
                }
            );
        }

        public Task<NotifyTuple> Notfiy()
        {
            Console.WriteLine("server in Notify");

            // https://code.google.com/p/googleappengine/issues/detail?id=803

            //var ds = Design.Book1.GetDataSet().Tables.
            var x = new Design.Book1B.Sheet1();



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
            var y = new Design.Book1B.Sheet2();



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
