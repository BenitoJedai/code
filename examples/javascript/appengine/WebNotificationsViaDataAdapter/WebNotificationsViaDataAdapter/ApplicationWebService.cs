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
using WebNotificationsViaDataAdapter.Design;
using WebNotificationsViaDataAdapter.Schema;

namespace WebNotificationsViaDataAdapter
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public Task<string[]> this[long delayfrom, long delayto]
        {
            get
            {
                var data = ScriptedNotifications.GetDataTable();



                var foo = new FooTable();

                foo.Insert(
                    new FooTableQueries.InsertFoo { delay = 700, text = "text via db" }
                );

                var foodata = foo.Select();


                var merge = new DataTable();

                merge.Merge(data);

                //Additional information: <target>.delay and <source>.delay have conflicting properties: DataType property mismatch.
                merge.Merge(foodata);

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
