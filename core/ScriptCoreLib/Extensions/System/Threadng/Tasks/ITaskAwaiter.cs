using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Runtime.CompilerServices
{
    //Error	3	'System.Threading.Tasks.ITaskAwaiter<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage>' does not implement '
    //System.Runtime.CompilerServices.INotifyCompletion'	X:\jsc.svn\examples\javascript\AsyncImageTask\AsyncImageTask\Application.cs	57	21	AsyncImageTask

    //[TypeForwardedFrom("mscorlib")]
    //public interface INotifyCompletion
    //{

    //    void OnCompleted(Action continuation);
    //}

    //public interface ITaskAwaiter<TResult> : INotifyCompletion
    //{
    //    // Error	4	'X.XAwaiter' does not implement 'System.Runtime.CompilerServices.INotifyCompletion'	X:\jsc.svn\examples\javascript\AsyncImageTask\AsyncImageTask\Application.cs	57	21	AsyncImageTask


    //    bool IsCompleted { get; set; }


    //    TResult GetResult();
    //}
}
