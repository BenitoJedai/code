using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.CancelEventArgs))]
    public class __CancelEventArgs : __EventArgs
    {
        public bool Cancel { get; set; }

        public __CancelEventArgs() : this(false)
        {
//            { Namespace__ = W:\web\ScriptCoreLib\Shared\BCLImplementation\System\__EventArgs.as, TargetDirectory = W:\web }
//24a4:02:01 after worker yield...

//Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.AggregateException: One or more errors occurred. ---> System.NotSupportedException: Unable to transform overloaded constructors to a single constructor via optional parameters for ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel.__CancelEventArgs
//   at  .?   .  ..ctor(Type , Boolean )
//   at  .?   .    (Type )
        }

        public __CancelEventArgs(bool cancel)
        {
            this.Cancel = cancel;
        }
    }
}
