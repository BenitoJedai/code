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

        public static Task Delay(int millisecondsDelay)
        {
            Console.WriteLine("enter __Task.Delay");

            var x = new TaskCompletionSource<object>();


            var timer = new Timer(millisecondsDelay);

            timer.timer +=
                delegate
                {
                    timer.stop();

                    Console.WriteLine("continue __Task.Delay");
                    x.SetResult(null);
                };

            timer.start();

            Console.WriteLine("exit __Task.Delay");
            return x.Task;
        }
    }
}
