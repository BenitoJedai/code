using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    #region referencesource

    //http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerContainer.idl

    #endregion

    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerContainer")]
    [Obsolete("experimental")]
    public class ServiceWorkerContainer
    {
        // tested by?

        public readonly ServiceWorker installing;
        public readonly ServiceWorker waiting;
        public readonly ServiceWorker active;
        public readonly ServiceWorker controller;

        // Task ?
        readonly IPromise ready;

        //Promise register(ScalarValueString url, optional Dictionary options);
        public IPromise register(string url, object options) { return null; }

        //Promise unregister(optional ScalarValueString scope = "/*");
        public IPromise unregister(string scope = "/*") { return null; }
    }



}
