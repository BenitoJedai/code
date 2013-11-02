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
        public Task<DataTable> __FooTable_Select()
        {
            var ro = ScriptedNotifications.GetDataTable();
            var rw = new FooTable().Select();

            var merge = new DataTable();



            // 
            merge.Merge(ro);

            if (rw.Rows.Count > 0)
                merge.Merge(rw);

            return merge.ToTaskResult();
        }

        public Task<string[]> this[long delayfrom, long delayto]
        {
            get
            {
                var ro = ScriptedNotifications.GetDataTable();



                var rw = new FooTable().Select();

                //foo.Insert(
                //    new FooTableQueries.InsertFoo { delay = 700, text = "text via db" }
                //);



                var merge = new DataTable();

                merge.Merge(ro);

                //Additional information: <target>.delay and <source>.delay have conflicting properties: DataType property mismatch.

                if (rw.Rows.Count > 0)
                    merge.Merge(rw);

                // !! merge now has a Debug Visualizer, pause here to inspect
                ////Debugger.Break();

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
