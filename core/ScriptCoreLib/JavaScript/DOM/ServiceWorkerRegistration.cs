using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // DOM is not actually the correct namespace, DOM is unavailable within workers isnt it?

    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerRegistration.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/push_messaging/ServiceWorkerRegistrationPush.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/notifications/ServiceWorkerRegistrationNotifications.idl


    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerRegistration")]
    [Obsolete("experimental")]
    public class ServiceWorkerRegistration
    {
        //  [CallWith=ScriptState] Promise showNotification(DOMString title, optional NotificationOptions options);
        //[Script(ExternalTarget = "ServiceWorkerRegistration.showNotification")]
        //public static IPromise<object> showNotification(string title, object options)
        //{
        //    return null;
        //}



        // https://github.com/slightlyoff/ServiceWorker/issues/421

        // Service Workers are started and kept alive by their relationship to events, not documents. This design borrows heavily from developer and vendor experience with Shared Workers and Chrome Background Pages.

        // http://www.w3.org/TR/push-api/#idl-def-PushRegistrationManager
        // http://w3c.github.io/push-api/
        //public readonly PushManager pushManager;
        // service register! {{ pushManager = null, ElapsedMilliseconds = 32346 }}
        // X:\jsc.svn\examples\javascript\test\TestPushManager\TestPushManager\Application.cs
        // https://github.com/w3c/push-api/issues/47
        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/push_messaging/PushManager.idl
        [Obsolete("not available yet?")]
        public readonly object pushManager;
        //public readonly object pushRegistrationManager;


        // http://www.chromium.org/blink/serviceworker/service-worker-faq
        // chrome://serviceworker-internals
        // chrome://inspect/#service-workers

        public readonly ServiceWorker installing;
        public readonly ServiceWorker waiting;
        // current ?

        public readonly ServiceWorker active;

        public string scope;


        // onupdatefound

    }
}
