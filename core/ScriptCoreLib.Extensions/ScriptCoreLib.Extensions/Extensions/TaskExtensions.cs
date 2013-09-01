using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{



    public static class TaskExtensions
    {
        sealed class InternalTaskExtensionsScope<TSource, TResult> where TSource : class
        {
            [Obsolete("Special hint for JavaScript runtime, until scope sharing is implemented..")]
            public Func<TSource, TResult> InternalTaskExtensionsScope_function;

            public TResult f(object e)
            {
                return this.InternalTaskExtensionsScope_function((TSource)e);
            }
        }

        [Obsolete]
        public static Task<TResult> StartNew<TSource, TResult>(this TaskFactory<TResult> that, TSource state, Func<TSource, TResult> function) where TSource : class
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\TaskRunExperiment\TaskRunExperiment\ApplicationControl.cs


            var x = new InternalTaskExtensionsScope<TSource, TResult> { InternalTaskExtensionsScope_function = function };

            return Task<TResult>.Factory.StartNew(x.f, (object)state);
        }


        public static Task<TResult> StartNew<TSource, TResult>(this TaskFactory that, TSource state, Func<TSource, TResult> function) where TSource : class
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\TaskRunExperiment\TaskRunExperiment\ApplicationControl.cs
            // X:\jsc.svn\examples\javascript\WebCamToGIFAnimation\WebCamToGIFAnimation\Application.cs

            var x = new InternalTaskExtensionsScope<TSource, TResult> { InternalTaskExtensionsScope_function = function };

            return Task<TResult>.Factory.StartNew(x.f, (object)state);
        }

        //cancellationToken: default(CancellationToken),
        //         creationOptions: TaskCreationOptions.LongRunning,
        //         scheduler: TaskScheduler.Default

        public static Task<TResult> StartNew<TSource, TResult>(this TaskFactory that,
            TSource state,
            Func<TSource, TResult> function,
            CancellationToken cancellationToken,
            TaskCreationOptions creationOptions,
            TaskScheduler scheduler
            ) where TSource : class
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\MandelbrotFormsControl\MandelbrotFormsControl\Library\MandelbrotComponent.cs
            // X:\jsc.svn\examples\javascript\forms\TaskRunExperiment\TaskRunExperiment\ApplicationControl.cs


            var x = new InternalTaskExtensionsScope<TSource, TResult> { InternalTaskExtensionsScope_function = function };

            return Task<TResult>.Factory.StartNew(
                x.f, 
                (object)state,
                cancellationToken,
                creationOptions,
                scheduler
            );
        }

    }
}
