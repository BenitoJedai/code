using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/DedicatedWorkerGlobalScope.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/WorkerGlobalScopeCrypto.idl

    [Script(HasNoPrototype = true)]
    public class WorkerGlobalScope : IEventTarget
    {

        // readonly attribute Crypto crypto;
        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/Crypto.idl
        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/SubtleCrypto.idl


        public readonly WorkerLocation location;



        #region setTimeout
        public int setTimeout(string code, int time)
        {
            return default(int);
        }

        public int setTimeout(IFunction code, int time)
        {
            return default(int);
        }

        [Script(DefineAsStatic = true)]
        internal int setTimeout(System.Action code, int time)
        {
            return setTimeout(((BCLImplementation.System.__Delegate)((object)code)).InvokePointer, time);
        }
        #endregion

        #region setInterval
        public int setInterval(string code, int time)
        {
            return default(int);
        }

        public int setInterval(IFunction code, int time)
        {
            return default(int);
        }

        [Script(DefineAsStatic = true)]
        internal int setInterval(System.Action code, int time)
        {
            return setInterval(((BCLImplementation.System.__Delegate)((object)code)).InvokePointer, time);
        }
        #endregion


        public void clearTimeout(int i)
        {

        }

        public void clearInterval(int i)
        {

        }


        public string encodeURIComponent(string e)
        {
            return default(string);
        }

        public string decodeURIComponent(string e)
        {
            return default(string);
        }


        public string escape(string e)
        {
            return default(string);
        }

        public string unescape(string e)
        {
            return default(string);
        }

        // http://www.infoq.com/news/2010/02/Web-SQL-Database
        public Database openDatabase(
            string name = "database.sqlite",
            string version = "1.0",
            //string version = "",
            string displayName = "Web SQL",

            // AppCache allows 5MB, how much for db?
            ulong estimatedSize = 2 * 1024 * 1024,

            Action<Database> creationCallback = null
            )
        {
            return default(Database);
        }
    }

    [Script(HasNoPrototype = true)]
    public class WorkerLocation
    {
        public readonly string href;
    }


    [Script(HasNoPrototype = true)]
    public class DedicatedWorkerGlobalScope : WorkerGlobalScope
    {
        #region event onmessage
        public event System.Action<MessageEvent> onmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "message");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "message");
            }
        }
        #endregion

        #region event onfirstmessage
        public event System.Action<MessageEvent> onfirstmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {

                System.Action<MessageEvent> yield = null;

                yield = e =>
                {
                    value(e);


                    this.onmessage -= yield;
                };

                this.onmessage += yield;

            }
            [Script(DefineAsStatic = true)]
            remove
            {
            }
        }
        #endregion

        public void postMessage(object message) { }
    }
}
