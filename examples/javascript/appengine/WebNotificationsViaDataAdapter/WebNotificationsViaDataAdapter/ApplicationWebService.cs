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

namespace WebNotificationsViaDataAdapter
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class ApplicationWebService : Component
    {

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

        public Task<DataTable> __FooTable_Select()
        {
            //java.lang.RuntimeException: Sequence contains no elements
            //        at com.google.appengine.tools.development.DevAppServerModulesFilter.doDirectRequest(DevAppServerModulesFilter.java:368)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__DefinedError.NoElements(__DefinedError.java:26)
            //        at com.google.appengine.tools.development.DevAppServerModulesFilter.doDirectModuleRequest(DevAppServerModulesFilter.java:351)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable.First(__Enumerable.java:1247)
            //        at com.google.appengine.tools.development.DevAppServerModulesFilter.doFilter(DevAppServerModulesFilter.java:116)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable.First(__Enumerable.java:1223)
            //        at org.mortbay.jetty.servlet.ServletHandler$CachedChain.doFilter(ServletHandler.java:1157)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataRow.set_Item(__DataRow.java:38)
            //        at org.mortbay.jetty.servlet.ServletHandler.handle(ServletHandler.java:388)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataTable.Merge(__DataTable.java:108)
            //        at org.mortbay.jetty.security.SecurityHandler.handle(SecurityHandler.java:216)
            //        at WebNotificationsViaDataAdapter.ApplicationWebService.__FooTable_Select(ApplicationWebService.java:82)

            var ro = ScriptedNotifications.GetDataTable();
            var rw = new FooTable().Select();

            var merge = new DataTable();



            // X:\jsc.svn\examples\java\JVMCLRDataTableMerge\JVMCLRDataTableMerge\Program.cs
            Console.WriteLine("before Merge ro");
            merge.Merge(ro);

            if (rw.Rows.Count > 0)
            {
                Console.WriteLine("before Merge rw");

                merge.Merge(rw);
            }

            // merge with ro data


            //:\WebNotificationsViaDataAdapter.ApplicationWebService\staging.java\web\files
            //:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe  -encoding UTF-8 -cp V:\WebNotificationsViaDataAdapter.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.3\lib\impl\*;C:\util\appengine-java-sdk-1.8.3\lib\shared\* -d "V:\WebNotificationsViaDataAdapter.ApplicationWebService\staging.java\web\release" @"V:\WebNotificationsViaDataAdapter.ApplicationWebService\staging.java\web\files"
            //:\WebNotificationsViaDataAdapter.ApplicationWebService\staging.java\web\java\WebNotificationsViaDataAdapter\ApplicationWebService.java:111: ToTable(boolean,java.lang.String[]) in ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataView cannot be applied to (int,java.lang.String[])
            //       table3 = view9.ToTable(num8, stringArray10);
            //                     ^

            var columnNames = merge.Columns.AsEnumerable().Select(k => k.ColumnName).ToArray();

            var distinct = merge.DefaultView.ToTable(
                distinct: true,
                columnNames: columnNames
            );

            return distinct.ToTaskResult();
        }

        public Task<string[]> this[long delayfrom, long delayto]
        {
            get
            {
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
            }
        }

    }
}
