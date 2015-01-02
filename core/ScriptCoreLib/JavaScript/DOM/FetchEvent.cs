﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs
    // http://www.w3.org/TR/service-workers/#fetch-event-interface
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/FetchEvent.idl
    // https://chromium.googlesource.com/chromium/blink.git/+/master/Source/modules/serviceworkers/FetchEvent.h
    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs


    [Script(HasNoPrototype = true, ExternalTarget = "FetchEvent")]
    public class FetchEvent : IEvent
    {
        // https://developer.mozilla.org/en-US/docs/Web/API/ExtendableEvent

        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerFetchHTML\TestServiceWorkerFetchHTML\Application.cs

        public readonly Request request;


        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerCache\TestServiceWorkerCache\Application.cs
        [Obsolete("not available yet?")]
        public readonly ServiceWorkerClient client;
        public readonly bool isReload;



        // client side web service like activity?

        //void respondWith((Response or Promise<Response>) r);

        // how can we translate Task to Promise?
        // can we then update jsc to do it automagically?
        // https://chromium.googlesource.com/chromium/blink.git/+/master/Source/modules/serviceworkers/RespondWithObserver.h
        public void respondWith(Response r) { }
        public void respondWith(IPromise<object> r) { }
    }

}