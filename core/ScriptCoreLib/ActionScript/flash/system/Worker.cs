using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/Worker.html
    [Script(IsNative = true)]
    public class Worker : EventDispatcher
    {
        // Note: Native Extensions are not supported for background and secondary workers.

        // X:\jsc.svn\examples\actionscript\air\AIRAudioWorker\AIRAudioWorker\ApplicationSprite.cs

        // Each additional worker is created from a separate swf. 

        public static bool isSupported { get; set; }

        public bool isPrimordial { get; private set; }

        public static Worker current { get; private set; }

        public void start()
        {
        }


        public MessageChannel createMessageChannel(Worker w)
        {
            // X:\jsc.svn\examples\actionscript\FlashWorkerExperiment\FlashWorkerExperiment\ApplicationSprite.cs   

            return default(MessageChannel);
        }


        public object getSharedProperty(string key)
        {
            return default(object);
        }

        public void setSharedProperty(string key, object value)
        {
            // There are five types of objects that are an exception to the rule that objects aren't shared between workers:

            //Worker
            //MessageChannel
            //shareable ByteArray (a ByteArray object with its shareable property set to true
            //Mutex
            //Condition

        }
    }
}
