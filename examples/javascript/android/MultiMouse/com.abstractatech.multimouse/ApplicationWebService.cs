using com.abstractatech.multimouse.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace com.abstractatech.multimouse
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {
        readonly PointerSync sync = new PointerSync();


        //void IPointerSync.Insert(XElement message)
        public void sync_Insert(XElement message)
        {
            var now = DateTime.Now;
            var ticksms = now.Ticks / 10000;



            sync.Insert(
                new PointerSyncQueries.Insert
                {
                    ticksms = ticksms,
                    message = message.ToString()
                }
            );

        }

        public void sync_SelectTransaction(Action<string> yield)
        {
            sync.SelectTransaction(
                id =>
                {
                    yield("" + id);
                }
            );
        }

        int sync_SelectContentUpdates_timeout = 500;
        int sync_SelectContentUpdates_waitmin = 100;
        int sync_SelectContentUpdates_waitrandom = 300;

        // good old buffering mode 
        public void sync_SelectContentUpdates(string last_id, Action<XElement> yield, Action<string> yield_last_id)
        {
            //             statement cannot be a load instruction (or is it a bug?)

            //type: com.abstractatech.multimouse.ApplicationWebService
            //offset: 0x00a5
            // method:Void sync_SelectContentUpdates(System.String, System.Action`1[System.Xml.Linq.XElement], System.Action`1[System.String])


            // jsc please implement int for web method calls, thanks :)
            var int_last_id = int.Parse(last_id);

            var sw = new Stopwatch();
            sw.Start();

            var random = new Random();


            // will this compile?


            while (sw.IsRunning)
            {
                var id = int_last_id;

                sync.SelectTransaction(
                    nid => id = (int)nid
                );

                //                type: System.Random
                //method: Int32 Next(Int32)
                var wait = sync_SelectContentUpdates_waitmin + random.Next(0, sync_SelectContentUpdates_waitrandom);

                //Console.WriteLine("SelectTransaction " + new { id, int_last_id, sw.ElapsedMilliseconds });
                if (id == int_last_id)
                {
                    Thread.Sleep(wait);
                }
                else
                {
                    // dont stop reading...
                    //sw.Stop();

                    var value = new PointerSyncQueries.SelectContentUpdates
                    {
                        FromTransaction = int_last_id,
                        ToTransaction = (int)id
                    };

                    sync.SelectContentUpdates(
                        value: value,
                        yield: message =>
                        {

                            yield(XElement.Parse(message));
                        }
                    );


                    int_last_id = (int)id;
                }

                if (sw.ElapsedMilliseconds >= sync_SelectContentUpdates_timeout)
                    sw.Stop();
            }

            yield_last_id("" + int_last_id);

        }
    }
}
