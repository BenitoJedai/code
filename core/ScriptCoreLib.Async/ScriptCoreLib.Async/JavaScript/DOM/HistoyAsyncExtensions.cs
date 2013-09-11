using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    public static class HistoyAsyncExtensions
    {
        public static TaskAwaiter<HistoryScope<T>> GetAwaiter<T>(this HistoryScope<T> scope)
        {
            return scope.TaskCompletionSource.Task.GetAwaiter();
        }
    }
}
