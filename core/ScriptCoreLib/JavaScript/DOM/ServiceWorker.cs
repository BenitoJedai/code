using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    #region referencesource

    // http://www.chromium.org/blink/serviceworker/testing

    // https://developer.mozilla.org/en-US/docs/Mozilla/Projects/Social_API/Service_worker_API_reference
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorker.idl


    // https://jakearchibald.github.io/isserviceworkerready/
    // https://github.com/slightlyoff/ServiceWorker
    // http://www.chromestatus.com/features/6561526227927040

    // http://www.w3.org/TR/service-workers/
    // http://www.chromium.org/blink/serviceworker
    
    #endregion

    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorker")]
    [Obsolete("experimental")]
    public class ServiceWorker : Worker
    {
        // ?
        // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/AbstractWorker.idl

        // how dos this compare to extensions, webviews?




        // in 2007 we had google gears, it got discontiued.
        // lets ait and see if this ServiceWorker feature from chrome delivers as promised 


        // how does it relate to Task.Run, Worker and SharedWorker?
        // this will replace AppCache

        // works only on https? does jsc inline server support SSL?
    }



}
