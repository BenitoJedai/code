using android.content;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml.Linq;


namespace ReinstallNotification.Activities
{

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        // jsc does not yet work with nested types correctly here
        static AtInstall ref0;

        // http://stackoverflow.com/questions/7470314/receiving-package-install-and-uninstall-events


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            Console.WriteLine("AtInstall event registered!");

            // Send it back to the caller.
            y(e);
        }



        #region poll_oninstall
        int sync_SelectContentUpdates_timeout = 5000;
        int sync_SelectContentUpdates_waitmin = 100;
        int sync_SelectContentUpdates_waitrandom = 300;


        // jsc could upgrade this method to use EventSource?
        // async yield?
        public void poll_oninstall(string last_id, Action<XElement> yield, Action<string> yield_last_id)
        {
            Console.WriteLine("enter poll_oninstall " + new { last_id });

            if (last_id == "")
            {
                yield_last_id("" + AtInstall.History.Count);
                return;
            }



            var int_last_id = int.Parse(last_id);

            var sw = new Stopwatch();
            sw.Start();

            var random = new Random();


            // will this compile?


            while (sw.IsRunning)
            {
                var id = int_last_id;

                //sync.SelectTransaction(
                //    nid => id = (int)nid
                //);

                id = AtInstall.History.Count;


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

                    //var value = new PointerSyncQueries.SelectContentUpdates
                    //{
                    //    FromTransaction = int_last_id,
                    //    ToTransaction = (int)id
                    //};

                    //sync.SelectContentUpdates(
                    //    value: value,
                    //    yield: message =>
                    //    {

                    //        yield(XElement.Parse(message));
                    //    }
                    //);

                    Console.WriteLine("raise oninstall " + new { int_last_id, id });

                    AtInstall.History.ToArray().Skip(int_last_id).Take(id - int_last_id).WithEach(
                        packageName =>
                        {
                            var xml = new XElement("oninstall", new XAttribute("packageName", packageName));

                            // raise oninstall { int_last_id = 1, id = 2 }

                            Console.WriteLine("yield " + new { xml });
                            yield(xml);
                        }
                    );

                    int_last_id = (int)id;
                }

                if (sw.ElapsedMilliseconds >= sync_SelectContentUpdates_timeout)
                    sw.Stop();
            }

            yield_last_id("" + int_last_id);
        }
        #endregion

    }


}
