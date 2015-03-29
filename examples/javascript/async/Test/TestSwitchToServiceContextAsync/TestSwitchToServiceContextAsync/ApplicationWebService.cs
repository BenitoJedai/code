using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestSwitchToServiceContextAsync
{
	/// <summary>
	/// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public class ApplicationWebService
	{
		// first let server send the default state to client, then start context hopping...
		// are we able to reference this byref yet into worker ? 
		public SharedProgram shared = new SharedProgram { text = "hello from server" };



		public async Task<ShadowIAsyncStateMachine> Invoke(ShadowIAsyncStateMachine that)
		{
			var xAsyncStateMachineTypeName = that.TypeName;


			// do we trust the caller?
			// can we jump?

			// is the typename RSA signed?
			Console.WriteLine(new { that.state, that.TypeName });

			Console.WriteLine("looking for the type...");

			var xAsyncStateMachineType = typeof(SharedProgram).Assembly.GetTypes().FirstOrDefault(
				x =>
				{
					// safety check 1
					if (!(typeof(IAsyncStateMachine).IsAssignableFrom(x)))
						return false;

					// we need a rsa lookup table?
					// or a special one time pad?

					Console.WriteLine(new { x.FullName, x.Name });

					// js wont know declaringtype name
					return ("<Namespace>." + x.Name).Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_")
					== xAsyncStateMachineTypeName.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");
				}
			);

			//{ state = 0, TypeName = <Namespace>._Invoke_d__3 }
			//looking for the type...
			//{ FullName = <>f__AnonymousType6`2 }
			//{ FullName = <>f__AnonymousType4`1 }
			//{ FullName = <>f__AnonymousType7`2 }
			//{ FullName = <>f__AnonymousType2`2 }
			//{ FullName = <>f__AnonymousType3`1 }
			//{ FullName = <>f__AnonymousType5`1 }
			//{ FullName = <>f__AnonymousType0`2 }
			//{ FullName = <>f__AnonymousType11`1 }
			//{ FullName = <>f__AnonymousType8 }
			//{ FullName = <>f__AnonymousType9`1 }
			//{ FullName = <>f__AnonymousType10`1 }
			//{ FullName = <>f__AnonymousType1`1 }
			//{ FullName = TestSwitchToServiceContextAsync.ApplicationWebService }
			//{ FullName = TestSwitchToServiceContextAsync.HopFromService }
			//{ FullName = TestSwitchToServiceContextAsync.HopToService }
			//{ FullName = TestSwitchToServiceContextAsync.Application }
			//{ FullName = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine }
			//{ FullName = TestSwitchToServiceContextAsync.SharedProgram }
			//{ FullName = TestSwitchToServiceContextAsync.xConsole }
			//{ FullName = TestSwitchToServiceContextAsync.Program }
			//{ FullName = TestSwitchToServiceContextAsync.ApplicationWebService+<>c__DisplayClass1_0 }
			//{ FullName = TestSwitchToServiceContextAsync.ApplicationWebService+<Invoke>d__1 }
			//{ FullName = TestSwitchToServiceContextAsync.Application+<>c__DisplayClass0_0 }
			//{ FullName = TestSwitchToServiceContextAsync.Application+<>c__DisplayClass0_1 }
			//{ FullName = TestSwitchToServiceContextAsync.Application+<>c }
			//{ FullName = TestSwitchToServiceContextAsync.Application+<<-ctor>b__1_0>d }
			//{ FullName = TestSwitchToServiceContextAsync.SharedProgram+<Invoke>d__3 }
			//{ FullName = TestSwitchToServiceContextAsync.Application+<>c__DisplayClass0_0+<<-cctor>b__3>d }

			// xAsyncStateMachineType = {Name = "<Invoke>d__3" FullName = "TestSwitchToServiceContextAsync.SharedProgram+<Invoke>d__3"}

			var NewStateMachine = Activator.CreateInstance(xAsyncStateMachineType);
			var NewStateMachineI = NewStateMachine as IAsyncStateMachine;

			#region 1__state
			xAsyncStateMachineType.GetFields(
					  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
				  ).WithEach(
				   AsyncStateMachineSourceField =>
				   {
					   if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
					   {
						   AsyncStateMachineSourceField.SetValue(
							   NewStateMachineI,
							   that.state
							);

					   }
				   }
			  );
			#endregion

			var s = default(ShadowIAsyncStateMachine);

			var reset = new AutoResetEvent(false);

			// we only care about the new thread we will be creating... skip other threads if any?
			// or should we care about sync context?
			HopFromService.VirtualOnCompleted =
				(Action continuation) =>
				{
					Action<ShadowIAsyncStateMachine> MoveNext = null;
					s = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);

					// should be the same state machine!
					Console.WriteLine("time to jump back? yes " + new { s.state, s.TypeName });

					reset.Set();
				};



			// are we on a chrome server?
			new Thread(
				delegate ()
				{
					// this will take step into next await.
					NewStateMachineI.MoveNext();
				}
			).Start();


			reset.WaitOne();

			Console.WriteLine("time to jump back? " + new { xAsyncStateMachineType, that.state });

			return s;
		}
	}
}
