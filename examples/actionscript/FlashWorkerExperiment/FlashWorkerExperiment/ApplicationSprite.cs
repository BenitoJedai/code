using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.Text;

namespace FlashWorkerExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        // X:\jsc.svn\examples\actionscript\async\Test\TestAsync\TestAsync\ApplicationSprite.cs
        // X:\jsc.svn\examples\actionscript\async\AsyncWorkerTask\AsyncWorkerTask\ApplicationSprite.cs
        // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

        [Obsolete("redirected to getSharedProperty if user cross threads?")]
        public static MessageChannel zfromWorker { get; set; }

        public ApplicationSprite()
        {
            // http://www.yeahbutisitflash.com/?p=5469

            // http://forums.adobe.com/message/5745066

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131006-air

            //System.ArgumentException: Parameter count does not match passed in argument value count.

            //        ReferenceError: Error #1065: Variable flash.system::WorkerDomain is not defined.
            //at FlashWorkerExperiment::ApplicationSprite()[S:\web\FlashWorkerExperiment\ApplicationSprite.as:31]

            //{ isSupported = true }
            // http://esdot.ca/site/2012/intro-to-as3-workers-hello-world


            //   The swf version should be 22 and above.



            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/Worker.html
            //  For mobile platforms, concurrency is supported in AIR on Android but not in AIR on iOS. 
            // You can use the static isSupported property to check whether concurrency is supported before attempting to use it.
            if (Worker.current.isPrimordial)
            {
                //{ os = Windows 7, version = WIN 13,0,0,133, WorkerDomain = true, Worker = true, isPrimordial = true, length = 519498 }
                // before start
                // after start { w = [object Worker] }
                // main: { i = 0, isPrimordial = true, current = true }
                // { data = ready? 
                // in worker: { i = 0, isPrimordial = false, current = true }
                // in worker: { i = 1, isPrimordial = true, current = false } }click!
                // { data = hi from worker { data = hi from UI } }

                // http://forums.adobe.com/thread/1171498

                var t = new TextField
                {
                    text = new
                    {
                        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/Capabilities.html
                        Capabilities.os,
                        Capabilities.version,

                        WorkerDomain = WorkerDomain.isSupported,
                        Worker = Worker.isSupported,
                        Worker.current.isPrimordial,

                        this.loaderInfo.bytes.length
                    }.ToString(),

                    autoSize = TextFieldAutoSize.LEFT
                };

                t.AttachTo(this);

                //this.stage.loaderInfo.uncaughtErrorEvents.uncaughtError +=
                this.loaderInfo.uncaughtErrorEvents.uncaughtError +=

                    e =>
                    {
                        // http://www.adobe.com/support/flashplayer/downloads.html

                        t.appendText(

                          new { e.errorID, e.text, e.error }.ToString()

                          );
                    };

                if (WorkerDomain.isSupported)
                {
                    // http://jacksondunstan.com/articles/1968
                    // "C:\util\flex_sdk_4.6\frameworks\libs\player\11.9\playerglobal.swc"
                    // "C:\util\air3-9_sdk_sa_win\frameworks\libs\player\11.9\playerglobal.swc"
                    // "C:\util\air13_sdk_win\frameworks\libs\player\13.0\playerglobal.swc"
                    // can we create natives of it yet?
                    // call c:\util\jsc\bin\jsc.meta.exe RewriteToActionScriptNatives /SWCFiles:"C:\util\flex_sdk_4.6\frameworks\libs\player\11.1\playerglobal.swc" /SWCFiles:"C:\util\flex_sdk_4.6\frameworks\libs\framework.swc"  /SWCFiles:"C:\util\flex_sdk_4.6\frameworks\libs\mx\mx.swc" /OutputAssembly:c:\util\jsc\bin\ScriptCoreLib.ActionScript.Natives.dll  /DisableResolveExternalType:true  /DisableWorkerDomain 
                    // call c:\util\jsc\bin\jsc.meta.exe RewriteToActionScriptNatives /SWCFiles:"C:\util\air13_sdk_win\frameworks\libs\player\13.0\playerglobal.swc" /SWCFiles:"C:\util\flex_sdk_4.6\frameworks\libs\framework.swc"  /SWCFiles:"C:\util\flex_sdk_4.6\frameworks\libs\mx\mx.swc" /OutputAssembly:c:\util\jsc\bin\ScriptCoreLib.ActionScript.AIRNatives.dll  /DisableResolveExternalType:true  /DisableWorkerDomain 

                    // should flash natives gen get a copy of the playerglobal and use it instead?

                    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/WorkerDomain.html
                    var w = WorkerDomain.current.createWorker(
                        this.loaderInfo.bytes
                    );

                    // http://jacksondunstan.com/articles/2401

                    //  channel1 = WorkerDomain.current.createMessageChannel(worker0);
                    var toWorker = Worker.current.createMessageChannel(w);
                    var fromWorker = w.createMessageChannel(Worker.current);

                    // http://esdot.ca/site/2012/intro-to-as3-workers-hello-world
                    w.setSharedProperty("toWorker", toWorker);
                    w.setSharedProperty("fromWorker", fromWorker);

                    // http://jcward.com/AIR+3.9+Workers+Beta
                    fromWorker.channelMessage +=
                        e =>
                        {
                            var data = (string)fromWorker.receive();

                            t.appendText(

                                "\n " + new { data }.ToString()

                                );
                        };

                    t.appendText(
                              "\n before start"
                              );

                    // { isSupported = true, isPrimordial = true, length = 485141 }
                    w.start();


                    t.appendText(
                              "\n after start " + new { w }
                              );

                    var list = WorkerDomain.current.listWorkers();

                    for (int i = 0; i < list.length; i++)
                    {

                        t.appendText(
                                  "\n main: " + new
                                  {
                                      i,
                                      list[i].isPrimordial,
                                      current = list[i] == Worker.current
                                  }
                         );
                    }

                    t.click +=
                        delegate
                        {
                            t.appendText(

                            "click!"


                             );

                            toWorker.send("hi from UI");
                        };
                }
            }
            else
            {
                var toWorker = (MessageChannel)Worker.current.getSharedProperty("toWorker");

                // VerifyError: Error #1014: Class flash.system::Worker could not be found.

                //WorkerDomain = true, Worker = true, isPrimordial = true, length = 517003 }
                //before start
                //after start { w = [object Worker] }{ data = ready? }click!{ data = hi from worker { data = hi from UI } }


                // http://forums.adobe.com/thread/1411606?tstart=0
                //Mobile Workers (concurrency) - Android
                //Introduced as a beta feature in AIR 3.9, we've continued to improve this feature based on your feedback for its official release in AIR 4.





                // what about automagic sync, jsc detects and marks such properties?
                var xfromWorker = (MessageChannel)Worker.current.getSharedProperty("fromWorker");

                if (xfromWorker != null)
                {
                    var w = new StringBuilder();


                    //{ WorkerDomain = true, Worker = true, isPrimordial = true, length = 519030 }
                    // before start
                    // after start { w = [object Worker] }
                    // main: { i = 0, isPrimordial = true, current = true }
                    // { data = ready? 
                    // in worker: { i = 0, isPrimordial = false, current = true }
                    // in worker: { i = 1, isPrimordial = true, current = false } }click!
                    // { data = hi from worker { data = hi from UI } }click!
                    // { data = hi from worker { data = hi from UI } }

                    var list = WorkerDomain.current.listWorkers();

                    for (int i = 0; i < list.length; i++)
                    {

                        w.Append(
                                  "\n in worker: " + new
                                  {
                                      i,
                                      list[i].isPrimordial,
                                      current = list[i] == Worker.current
                                  }
                         );
                    }

                    xfromWorker.send("ready? " + w.ToString());
                }


                toWorker.channelMessage +=
                    e =>
                    {
                        var data = (string)toWorker.receive();
                        // now what?

                        xfromWorker.send("hi from worker " + new { data });
                    };

            }
        }

    }
}
