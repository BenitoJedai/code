using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
	// Src/Compilers/CSharp/Source/Lowering/AsyncRewriter/AsyncStateMachine.cs	


	// http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.iasyncstatemachine.aspx
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Runtime.CompilerServices/IAsyncStateMachine.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/CompilerServices/IAsyncStateMachine.cs

	[Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.IAsyncStateMachine")]
	public interface __IAsyncStateMachine
	{
		// types marked by this interface are special, jumpable,
		// thus jsc should embed analysis intel in it to know what this type is about

		// X:\jsc.svn\examples\javascript\test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
		// X:\jsc.svn\examples\javascript\test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs
		// x:\jsc.svn\examples\javascript\async\test\testeditandcontinue\testeditandcontinue\application.cs
		// X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs
		// X:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs
		// x:\jsc.svn\examples\javascript\chrome\extensions\chromeextensionhoptotab\chromeextensionhoptotab\application.cs

		void MoveNext();


		// does jsc know about replicas?
		// Configures the state machine with a heap-allocated replica.
		void SetStateMachine(
			__IAsyncStateMachine stateMachine
		);
	}

}
