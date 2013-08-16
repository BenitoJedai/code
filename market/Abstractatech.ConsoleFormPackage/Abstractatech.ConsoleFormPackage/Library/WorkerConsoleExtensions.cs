using Abstractatech.ConsoleFormPackage.Library;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class WorkerConsoleExtensions
    {
        public static DedicatedWorkerGlobalScope RedirectConsoleOutput(this DedicatedWorkerGlobalScope worker)
        {
            {
                #region ConsoleFormWriter
                var w = new ConsoleForm_TextWriter();

                var o = Console.Out;

                Console.SetOut(w);

                w.AtWrite =
                     x =>
                     {
                         worker.postMessage(x);
                     };

                w.AtWriteLine =
                    x =>
                    {
                        worker.postMessage(x + Environment.NewLine);
                    };
                #endregion
            }

            return worker;

        }
    }
}
