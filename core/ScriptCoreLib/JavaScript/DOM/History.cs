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
        public static Stack<Func<bool>> inline_unwind = new Stack<Func<bool>>();
    
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
