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
        // Please note that support for creating universal IPA binaries will only be available in the new compiler. The legacy compiler is not (and will not be)
        // compatible with iOS 64-bit. Because of this, it will be removed with version 16 of the AIR SDK. 


        // do we have air workers for ipad2/ios2 yet?
        // https://bugbase.adobe.com/index.cfm?event=bug&id=3683920
        // http://forum.starling-framework.org/topic/air-sdk-16-beta


        // what happens if we throw in the worker?


        // X:\jsc.svn\examples\javascript\WebWorkerExperiment\WebWorkerExperiment\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\ServiceWorker.cs

        // how does this relate to ServiceWorker?
        // Task.Run?

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
