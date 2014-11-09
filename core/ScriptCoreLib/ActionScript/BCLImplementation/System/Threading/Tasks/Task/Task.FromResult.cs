using ScriptCoreLib.ActionScript.flash.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{

    internal partial class __Task
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Delay.cs

        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            var x = new TaskCompletionSource<TResult>();
            x.SetResult(result);
            return x.Task;
        }
    }
}
