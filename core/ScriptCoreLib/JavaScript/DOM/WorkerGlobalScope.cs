using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/WorkerGlobalScope.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/WorkerGlobalScope.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/performance/WorkerGlobalScopePerformance.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/performance/WorkerPerformance.idl

    [Script(HasNoPrototype = true)]
    public class WorkerGlobalScope : IEventTarget
    {
        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerAssetCache\TestServiceWorkerAssetCache\Application.cs
        // https://fetch.spec.whatwg.org/#dom-global-fetch
        //[NewObject] Promise<Response> fetch(RequestInfo input, optional RequestInit init);
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\FetchEvent.cs
        // https://code.google.com/p/chromium/issues/detail?id=408041
        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/fetch/WorkerFetch.idl
        public IPromise<Response> fetch(object input) { return null; }


        public Crypto crypto;
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
        // what about service worker?
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

}
