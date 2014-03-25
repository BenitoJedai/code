using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;
using System.Data.SQLite;
using System.Diagnostics;

namespace AppEngineUserAgentLoggerWithXSLXAsset
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //var x = new Design.Book1B.Sheet1();


            //new SQLiteConnectionStringBuilder { DataSource = "file:foo" }.AsWithConnection()(
            //    c =>
            //    {
            //        Design.Book1B.Sheet1.Queries.Create(c);



            //        var zcmd = new SQLiteCommand(
            //             Design.Book1B.Sheet1.Queries.InsertCommandText,
            //             c
            //        );

            //        var r = new Design.Book1BSheet1Row();

            //        // ex = {"The specified name does not exist: @ScreenHeight"}

            //        Design.Book1B.Sheet1.Queries.InsertParameterScreenWidth(zcmd, r);
            //        Design.Book1B.Sheet1.Queries.InsertParameterScreenHeight(zcmd, r);
            //        Design.Book1B.Sheet1.Queries.InsertParameterClientTime(zcmd, r);
            //        Design.Book1B.Sheet1.Queries.InsertParameterIPAddress(zcmd, r);
            //        Design.Book1B.Sheet1.Queries.InsertParameterServiceTime(zcmd, r);
            //        Design.Book1B.Sheet1.Queries.InsertParameterTag(zcmd, r);
            //        Design.Book1B.Sheet1.Queries.InsertParameterTimestamp(zcmd, r);

            //        //0.ax
            //        zcmd.ExecuteNonQuery();

            //        //  public int LastInsertRowID();
            //        var uu = zcmd.LastInsertRowID();
            //        var uuu = c.LastInsertRowId;

            //        //                    0x0053 . call           [mscorlib] System.Threading.Tasks.TaskCompletionSource`1<Int64>.get_Task() : Task`1<Int64>
            //        //0x0058 ret 


            //        var z = Design.Book1B.Sheet1.Queries.Insert(c,
            //            new Design.Book1BSheet1Row()
            //            );

            //        // z.Result = 175770952473247744
            //        var zz = z.Result;

            //        Debugger.Break();
            //    }
            //);

            //var visit = x.Insert(
            //    new Design.Book1BSheet1Row
            //    {
            //        // jsc experience should auto detect, 
            //        // implicit column types

            //        // should we infer the type?
            //        // should we use dynamic? dynamic has no intellisense

            //        //ScreenWidth = this.ScreenWidth,
            //        //ScreenHeight = this.ScreenHeight,


            //        //// not available for AppEngine?
            //        //// http://stackoverflow.com/questions/8787463/how-to-identify-ip-address-of-requesting-client

            //        //IPAddress = WebServiceHandler.Context.Request.UserHostAddress,

            //        //// we are now logging all headers
            //        ////UserAgent = WebServiceHandler.Context.Request.UserAgent,

            //        //ClientTime = this.ClientTime,
            //        //ServiceTime = ServiceTime,
            //    }
            //);


            //xQueries_Insert
            //xQueries_Insert { ColumnName = ScreenWidth }
            //xQueries_Insert { ColumnName = ScreenHeight }
            //xQueries_Insert { ColumnName = IPAddress }
            //xQueries_Insert { ColumnName = ServiceTime }
            //xQueries_Insert { ColumnName = ClientTime }
            //xQueries_Insert { ColumnName = Tag }
            //xQueries_Insert { ColumnName = Timestamp }


            // 2AD6F7000000000
            // visit = 167582013207871488
            //Console.WriteLine(new { visit });


            //var x = new ApplicationWebService { ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width };
            //var y = x.Notfiy();

            ////Error	1	The best overloaded method match for 'AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService.GetVisitHeadersFor(AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet1Key)' has some invalid arguments	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\Program.cs	16	21	AppEngineUserAgentLoggerWithXSLXAsset
            ////Error	2	Argument 1: cannot convert from 'System.Data.DataTable' to 'AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet1Key'	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\Program.cs	16	42	AppEngineUserAgentLoggerWithXSLXAsset


            //var z = x.GetVisitHeadersFor(y.Result.visit);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
