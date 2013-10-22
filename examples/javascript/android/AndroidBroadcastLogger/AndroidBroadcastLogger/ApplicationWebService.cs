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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndroidBroadcastLogger
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        //public MyDataSource DataSource;

        // sending field as cookie - cuts of at ; inside xml escapes..

        public Task<MyDataSource> DataSource_poll(MyDataSource DataSource)
        {
            // first timer null
            if (DataSource == null)
                DataSource = new MyDataSource();

            DataSource.poll();

            // Set-Cookie:InternalFields=field_DataSource=<_02000006>%0d%0a  <_0400000b>1</_0400000b>%0d%0a  <_0400000c>&lt;DataTable TableName=""&gt;%0d%0a  &lt;Columns&gt;%0d%0a    &lt;DataColumn ReadOnly="true"&gt;xml&lt;/DataColumn&gt;%0d%0a  &lt;/Columns&gt;%0d%0a  &lt;DataRow&gt;%0d%0a    &lt;DataColumn&gt;&amp;lt;fake&amp;gt;data { last_id = 0, Count = 0 }&amp;lt;/fake&amp;gt;&lt;/DataColumn&gt;%0d%0a  &lt;/DataRow&gt;%0d%0a&lt;/DataTable&gt;</_0400000c>%0d%0a  <_0400000d>1000</_0400000d>%0d%0a  <_0400000e>10</_0400000e>%0d%0a  <_0400000f>30</_0400000f>%0d%0a</_02000006>; 
            // Cookie GetValues { value = field_DataSource=<_02000006>%0d%0a  <_0400000b>1</_0400000b>%0d%0a  <_0400000c>&lt }


            return DataSource.ToTaskResult();
        }

        public Task DataSource_addfake()
        {


            ApplicationWebServiceExtensions.History.Add(
                new XElement("fake", "data " + new { ApplicationWebServiceExtensions.History.Count })
            );

            return new object().ToTaskResult();
        }
    }

    public static class DoEventsExtension
    {
        public static void DoEvents(this int wait)
        {
            var waitTimer = new Stopwatch();

            waitTimer.Start();

            while (waitTimer.ElapsedMilliseconds < wait)
            {
                System.Windows.Forms.Application.DoEvents();
                Thread.Yield();
                //Thread.Sleep(1);
            }
        }
    }
    static class ApplicationWebServiceExtensions
    {
        // Error	3	The parameter modifier 'ref' cannot be used with 'this' 	X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs	31	38	AndroidBroadcastLogger

        public static List<XElement> History = new List<XElement>();

        public static void poll(this MyDataSource DataSource)
        {
            Console.WriteLine("enter poll " + new { DataSource.last_id });

            if (DataSource.last_id < 0)
            {
                DataSource.last_id = History.Count;
                return;
            }


            var sw = new Stopwatch();
            sw.Start();

            var random = new Random();


            // will this compile?


            while (sw.IsRunning)
            {
                var id = History.Count;



                if (id == DataSource.last_id)
                {
                    var wait = DataSource.sync_SelectContentUpdates_waitmin
                        + random.Next(0, DataSource.sync_SelectContentUpdates_waitrandom);

                    wait.DoEvents();


                }
                else
                {



                    History.ToArray().Skip(DataSource.last_id).Take(id - DataSource.last_id).WithEach(
                        xml =>
                        {

                            Console.WriteLine("yield " + new { xml });
                            DataSource.yield(xml);

                            // force end of stream for now.
                            // as we are not using event stream yet
                            sw.Stop();
                        }
                    );

                    DataSource.last_id = id;
                }

                if (sw.ElapsedMilliseconds >= DataSource.sync_SelectContentUpdates_timeout)
                    sw.Stop();
            }
        }
    }

    public class MyDataSource
    {
        public int last_id = -1;

        public DataTable data;

        public int sync_SelectContentUpdates_timeout = 1000;
        public int sync_SelectContentUpdates_waitmin = 10;
        public int sync_SelectContentUpdates_waitrandom = 30;

        public void yield(XElement value)
        {
            if (data == null)
            {
                data = new DataTable { };
                data.Columns.Add(new DataColumn { ColumnName = "xml", ReadOnly = true });

            }

            // An exception of type 'System.IndexOutOfRangeException' occurred in System.Data.dll but was not handled in user code
            // Additional information: Cannot find column 0.

            var row = data.NewRow();

            row[0] = value.ToString();

            // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRowCollection.Add(System.Object[])]
            data.Rows.Add(row);
        }
    }
}
