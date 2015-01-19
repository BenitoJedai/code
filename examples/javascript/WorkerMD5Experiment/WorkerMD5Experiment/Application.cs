using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkerMD5Experiment;
using WorkerMD5Experiment.Design;
using WorkerMD5Experiment.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Diagnostics;
using System.Threading;

namespace WorkerMD5Experiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            new IHTMLButton { "do MD5" }.AttachToDocument().onclick +=
                async a =>
                {
                    var data = "whats the hash for this?";

                    var z = await Task.Run(
                        delegate
                        {
                            // 20140629 level1 scope sharing!

                            var bytes = Encoding.UTF8.GetBytes(data);

                            var s = Stopwatch.StartNew();

                            // { data = "{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 41 }" }

                            // { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1268 }
                            // { i = 255, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 170 }

                            // {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 245, ManagedThreadId = 10 }}
                            // laptop {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 439, ManagedThreadId = 10 }}

                            // on red server. how fast is the laptop?
                            // laptop wont trust the server ssl?
                            // certs are configured via certmgr.msc 
                            // after export and import the laptop should now be able to trust the ssl?

                            var scope = new { data };

                            for (int i = 0; i < 0x1000; i++)
                            {

                                var hash = bytes.ToMD5Bytes();
                                var hex = hash.ToHexString();

                                //scope = new { data = new { i, hex, s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount }.ToString() };
                                scope = new { data = new { i, hex, s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId }.ToString() };

                            }


                            return scope;
                        }
                    );

                    // show proof of work
                    //a.Element.innerText = z.data;


                    new IHTMLPre {
                        // ProcessorCount allows to know if we are on our lite laptop or the server
                        new { Environment.ProcessorCount, z.data }
                    }.AttachToDocument();

                    // {{ ProcessorCount = 8, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 246, ManagedThreadId = 10 }} }}
                    //{ { ProcessorCount = 8, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 240, ManagedThreadId = 11 } } } }
                    //{ { ProcessorCount = 8, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 240, ManagedThreadId = 12 } } } }

                    // high profile/ac mode
                    //{ { ProcessorCount = 4, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 445, ManagedThreadId = 10 } } } }
                    //{ { ProcessorCount = 4, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 426, ManagedThreadId = 11 } } } }
                    //{ { ProcessorCount = 4, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 422, ManagedThreadId = 12 } } } }

                    // battery/no power
                    // {{ ProcessorCount = 4, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1253, ManagedThreadId = 13 }} }}
                    // {{ ProcessorCount = 4, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1350, ManagedThreadId = 14 }} }}
                    // {{ ProcessorCount = 4, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1550, ManagedThreadId = 15 }} }}


                };

        }

    }
}
