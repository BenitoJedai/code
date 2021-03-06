﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://googledevelopers.blogspot.com/2014/10/promises-in-google-apis-javascript.html

    // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Promise.webidl
    // https://code.google.com/p/dart/source/browse/third_party/WebCore/core/dom/Promise.idl?spec=svn26952&r=26952
    // https://chromium.googlesource.com/chromium/blink/+/072dd87acc8859c3a35441f24b1486faa12efe42/Source/core/dom/Promise.idl

    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Threading/Tasks/Promise.cs

    // chosing the prefix of I to prevent intellisense pollution. 
    // this feature in C# is to be available as async/await Task<>
    // this might be what Windows Runtime API def feels within .NET def
    // how does it relate to Nullable<> ?

    // ScriptPromise 

    // http://blog.slaks.net/2015-01-05/introducing-promises/
    // http://blog.slaks.net/2015-01-08/comparing-different-languages-promises-frameworks/

    [Script(HasNoPrototype = true, ExternalTarget = "Promise")]
    public class IPromise
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\java\util\concurrent\Future.cs
    }

    [Script(HasNoPrototype = true, ExternalTarget = "Promise")]
    public class IPromise<T> : IPromise
    {

        // https://github.com/mozilla-services/services-central-legacy/blob/master/browser/devtools/shared/Promise.jsm
        // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

        // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-promise-objects
        // jsc shall translate to IFunction here
        public void then(Action<T> onSuccess)
        {
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

        }

        //public IPromise<TResult> then<TResult>(Func<T, TResult> onSuccess)
        //{
        //    // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs
        //    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerAssetCache\TestServiceWorkerAssetCache\Application.cs

        //    return null;
        //}

        // what if different types objects are to be returned? 
        public IPromise<object> then(Func<T, object> onSuccess)
        {
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs
            // X:\jsc.svn\examples\javascript\test\TestServiceWorkerAssetCache\TestServiceWorkerAssetCache\Application.cs

            return null;
        }


        public IPromise<object> then(Func<T, object> onSuccess, Func<T, object> onError)
        {
            return null;
        }

        public void then(Action<T> onSuccess, Action<object> onError)
        {

        }


        //): Uncaught SyntaxError: Unexpected token catch at http://83.191.214.142:1030/view-source:6824
        //  f.catch(Yh8ABlQajj_aYV3DTlUa8mQ(FAMABBoZ8jSxXtI20l3A6g));
        // android webview wont like .catch
        // X:\jsc.svn\examples\javascript\test\TestFunctionDecorationForCatch\TestFunctionDecorationForCatch\Class1.cs

        public void @catch(Action<object> onError)
        {
            // tested by?
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

        }


#if NET_45
        // csharp compiler wont allow us do define api within 4.0 and use in 4.5 the way we need to..
 

        [Script(DefineAsStatic = true)]
        public IPromiseAwaiter<T> GetAwaiter()
        {
            var t = new TaskCompletionSource<T>();

            var p = new IPromiseAwaiter<T> { Task = t.Task };

            then(t.SetResult);

            return p;
        }
#endif
    }

#if NET_45
    [Script]
    public class IPromiseAwaiter<T> : global::System.Runtime.CompilerServices.INotifyCompletion<>
    {
        // Error	3	'ScriptCoreLib.JavaScript.DOM.IPromiseAwaiter<ScriptCoreLib.JavaScript.DOM.KeyPair>' does not implement 'System.Runtime.CompilerServices.INotifyCompletion'	X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs	87	27	TestWebCryptoAsync


        public Task<T> Task;

        // CLR seems to do the oppisite for now..
        // later this might be the place to synchronize context data between worker threads..
        public bool IsCompleted { get { return Task.IsCompleted; } }

        public void OnCompleted(Action<T> continuation)
        {
            Task.ContinueWith(
                task =>
                {
                    continuation(task.Result);
                }
            );


        }

        public T GetResult() { return Task.Result; } // Nop. It exists purely because the compiler pattern demands it.
    }
#endif
}
