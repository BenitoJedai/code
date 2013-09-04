using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    //[Script(Implements = typeof(global::System.Progress))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Progress`1")]
    internal class __Progress<T> : __IProgress<T>
    {
        Action<T> handler;

        public __Progress(Action<T> handler)
        {
            this.handler = handler;
        }

        protected virtual void OnReport(T value)
        {
            handler(value);
        }

        void __IProgress<T>.Report(T value)
        {
            OnReport(value);
        }
    }
}
