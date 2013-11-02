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
        FooTable rw = new FooTable();

        public Task __FooTable_Insert(FooTable.InsertFoo[] value)
        {
            Console.WriteLine("delete");
            // first delete and then add new content
            rw.Delete();

            value.WithEach(x =>
                {
                    Console.WriteLine("insert " + new { x.delay, x.text });

                    rw.Insert(x);
                }
            );

            return "".ToTaskResult();
        }

        public Task<DataTable> __FooTable_Select()
        {
            var ro = ScriptedNotifications.GetDataTable();
            var rw = new FooTable().Select();

            var merge = new DataTable();



            // 
            merge.Merge(ro);

            if (rw.Rows.Count > 0)
                merge.Merge(rw);

            // merge with ro data
            var distinct = merge.DefaultView.ToTable(
                distinct: true,
                columnNames: merge.Columns.AsEnumerable().Select(k => k.ColumnName).ToArray()
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


                    let delay = Convert.ToInt64(row["delay"])
                    let text = (string)row["text"]

                    where delay >= delayfrom
                    where delay <= delayto

                    select text
                ).ToTaskResult();
            }
        }

    }
}
