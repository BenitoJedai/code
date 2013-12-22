using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class History
    {
        public readonly object state;

        public void back()
        {
        }

        public void pushState(object data)
        {

        }

        public void replaceState(object data)
        {

        }


    }


    [Script]
    public class HistoryScope
    {
        // tested by
        // X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

        public static Stack<Func<bool>> inline_unwind = new Stack<Func<bool>>();
        public static Stack<object> inline_unwind_data = new Stack<object>();

    }

    [Obsolete("This class should live in ScriptCoreLib.Extensions, yet for now it has to stay here to keep identity between applications...")]
    [Script]
    public class HistoryScope<T>
    {
        public T __state;

        public T state { get { return __state; } }

        public Func<TaskCompletionSource<HistoryScope<T>>> __TaskCompletionSource;

        public TaskCompletionSource<HistoryScope<T>> TaskCompletionSource
        {
            get
            {
                return __TaskCompletionSource();
            }
        }

    }
}
