using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Threading
{
    // http://referencesource.microsoft.com/#WindowsBase/src/Base/System/Windows/Threading/Dispatcher.cs

    [Script(Implements = typeof(global::System.Windows.Threading.Dispatcher))]
    public class __Dispatcher
    {
        // 4.5
        public void Invoke(Action callback)
        {
            Invoke(method: callback);
        }

        public object Invoke(Delegate method, params object[] args)
        {
            // http://msdn.microsoft.com/en-us/library/cc647509.aspx

            if (method is Action)
            {
                ((Action)method)();

                return null;
            }

            throw new NotSupportedException();
        }
    }
}
