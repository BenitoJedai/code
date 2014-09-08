using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebNotificationsViaDataAdapter.Design;
using WebNotificationsViaDataAdapter.Schema;
using ScriptCoreLib.Query.Experimental;
using System.Data.SQLite;

namespace WebNotificationsViaDataAdapter
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class ApplicationWebService : Component
    {
        //02000027 WebNotificationsViaDataAdapter.AssetsLibrary.<0200000dArray\+ConvertToString>
        //02000028 WebNotificationsViaDataAdapter.Design.ScriptedNotificationsV2ScriptedNotificationsKey
        //02000029 ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda

        static ApplicationWebService()
        {
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\ApplicationWebService.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140908

            #region QueryExpressionBuilder.WithConnection
            QueryExpressionBuilder.WithConnection =
                y =>
            {
                var cc = new SQLiteConnection(
                    new SQLiteConnectionStringBuilder { DataSource = "file:WebNotificationsViaDataAdapter.xlsx.sqlite" }.ToString()
                );

                cc.Open();
                y(cc);
                cc.Dispose();
            };
            #endregion

        }

        // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\ApplicationWebService.cs
        // restart of vs helps to resolve ScriptedNotificationsV2ScriptedNotificationsRow

        public Task<DataTable> __FooTable_Insert(
            ScriptedNotificationsV2ScriptedNotificationsRow[] value
            )
        {
            new ScriptedNotificationsV2ScriptedNotifications().Insert(
                value
            );


            //value.Select(
            //    new ScriptedNotificationsV2.ScriptedNotifications().Insert
            //).ToArray();


            return __FooTable_Select();
        }


#if V1
        public Task<DataTable> __FooTable_Insert(FooTable.InsertFoo[] value)
        {
            FooTable rw = new FooTable();


            Console.WriteLine("delete");
            // first delete and then add new content
            rw.Delete();

            value.WithEach(x =>
                {
                    Console.WriteLine("insert " + new { x.delay, x.text });

                    rw.Insert(x);
                }
            );

            return __FooTable_Select();
        }
#endif

        // called by FooTableDesigner_Load
        public Task<DataTable> __FooTable_Select()
        {
            var n = new ScriptedNotificationsV2ScriptedNotifications();

            #region auto reset and reinit?
            if (n.Count() == 0)
            {
                ScriptedNotificationsV2.GetDataSet().Tables[0].Rows.AsEnumerable().WithEach(
                    x => n.Insert(x)
                );
            }
            #endregion

            // why cannot we do a direct AsDataTable ?
            return n.AsEnumerable().AsDataTable().AsResult();


#if V1
            var ro = ScriptedNotifications.GetDataTable();
            var rw = new FooTable().Select();

            var merge = new DataTable();



            // X:\jsc.svn\examples\java\JVMCLRDataTableMerge\JVMCLRDataTableMerge\Program.cs
            Console.WriteLine("before Merge ro");
            merge.Merge(ro);

            if (rw.Rows.Count > 0)
            {
                Console.WriteLine("before Merge rw");


                // Additional information: <target>.delay and <source>.delay have conflicting properties: DataType property mismatch.

                merge.Merge(rw);
            }

            // merge with ro data

            var columnNames = merge.Columns.AsEnumerable().Select(k => k.ColumnName).ToArray();

            var distinct = merge.DefaultView.ToTable(
                distinct: true,
                columnNames: columnNames
            );


            //            ToTable: { c = delay, value = 500 }
            //ToTable: { c = delay, xvalue = 500 }
            //ToTable: { c = text, value = howdy! }
            //ToTable: { c = text, xvalue = howdy! }
            //ToTable: { c = delay, value = 5000 }
            //ToTable: { c = delay, xvalue = 5000 }
            //ToTable: { c = text, value = how can we help you? }
            //ToTable: { c = text, xvalue = how can we help you? }
            //ToTable: { c = delay, value = 25000 }
            //ToTable: { c = delay, xvalue = 25000 }
            //ToTable: { c = text, value = do you like our site? }
            //ToTable: { c = text, xvalue = do you like our site? }
            //__FooTable_Select
            //{ ColumnName = delay, value =  }
            //{ ColumnName = text, value =  }
            //{ ColumnName = delay, value =  }
            //{ ColumnName = text, value =  }
            //{ ColumnName = delay, value =  }
            //{ ColumnName = text, value =  }

            //var xml = ScriptCoreLib.Library.StringConversionsForDataTable.ConvertToString(
            //    distinct
            //);

            //Console.WriteLine(new { xml });

            return distinct.ToTaskResult();
#endif

        }


        // http://192.168.43.252:11528/xml/get_Item Failed to load resource: the server responded with a status of 500 (Internal Server Error)
        // tested by ?
        public Task<string[]> this[long delayfrom, long delayto]
        {
            get
            {

                var n = new ScriptedNotificationsV2ScriptedNotifications();

                #region auto reset and reinit?
                if (n.Count() == 0)
                {
                    ScriptedNotificationsV2.GetDataSet().Tables[0].Rows.AsEnumerable().WithEach(
                        x => n.Insert(x)
                    );
                }
                #endregion

                return Enumerable.ToArray(

                    from row in n.AsEnumerable()

                    let delay = row.delay

                    where delay >= delayfrom
                    where delay <= delayto

                    select row.text
                ).ToTaskResult();



#if V1
                var merge = __FooTable_Select().Result;

                // !! merge now has a Debug Visualizer, pause here to inspect

                return Enumerable.ToArray(
                    from row in merge.Rows.AsEnumerable()

                    where !string.IsNullOrEmpty((string)row["delay"])


                    let delay = Convert.ToInt64((string)row["delay"])
                    let text = (string)row["text"]

                    where delay >= delayfrom
                    where delay <= delayto

                    select text
                ).ToTaskResult();
#endif

            }
        }

    }
}
