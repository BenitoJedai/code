using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public static class ServiceWorkerContainerExtensions
    {
        // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs

        // jsc wont yet allow to return Task from native apis?
        // need manual extensions for now?
        public static Task<ServiceWorkerRegistration> register(this ServiceWorkerContainer serviceworker)
        {
            var x = serviceworker.register(
                InternalInlineWorker.GetScriptApplicationSourceForInlineWorker(),
                null
            );


            return x.AsTask();
        }


        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerCache\TestServiceWorkerCache\Application.cs
        public static Task<ServiceWorker> activate(this ServiceWorkerContainer serviceworker)
        {
            var x = new TaskCompletionSource<ServiceWorker>();

            serviceworker.register().ContinueWith(
                t =>
                {
                    var registration = t.Result;

                    if (registration.installing != null)
                    {
                        var i = registration.installing;

                        i.onstatechange += delegate
                        {
                            if (i.state != "activated")
                                return;

                            x.SetResult(i);
                        };
                    }
                }
            );

            return x.Task;

        }

    }
}
