using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
    // Src/Compilers/CSharp/Source/Lowering/AsyncRewriter/AsyncStateMachine.cs	


    // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.iasyncstatemachine.aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.IAsyncStateMachine")]
    public interface __IAsyncStateMachine
    {
		// X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs

		// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Runtime.CompilerServices/IAsyncStateMachine.cs

		void MoveNext();


        // does jsc know about replicas?
        // Configures the state machine with a heap-allocated replica.
        void SetStateMachine(
            __IAsyncStateMachine stateMachine
        );
    }

}
