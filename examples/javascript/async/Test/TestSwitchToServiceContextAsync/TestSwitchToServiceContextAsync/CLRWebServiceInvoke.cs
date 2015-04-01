using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSwitchToServiceContextAsync;

namespace TestSwitchToServiceContextAsync
{
	public class CLRWebServiceInvoke
	{
		[Obsolete("jsc needs to send encrypted Type and FieldInfo init ops ")]
		public static async Task<ShadowIAsyncStateMachine> Invoke(Type service, ShadowIAsyncStateMachine that)
		{
			Console.WriteLine("looking for the type...");

			Func<string, string> DecoratedString =
				x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");

			var xAsyncStateMachineType = service.Assembly.GetTypes().FirstOrDefault(
				x =>
				{
					// safety check 1
					if (!(typeof(IAsyncStateMachine).IsAssignableFrom(x)))
						return false;

					// we need a rsa lookup table?
					// or a special one time pad?

					Console.WriteLine(new { x.FullName, x.Name });

					// js wont know declaringtype name
					return DecoratedString("<Namespace>." + x.Name) == DecoratedString(that.TypeName);
				}
			);

			var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
			//var NewStateMachine = Activator.CreateInstance(xAsyncStateMachineType);
			var NewStateMachineI = NewStateMachine as IAsyncStateMachine;

			#region 1__state
			xAsyncStateMachineType.GetFields(
					  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
				  ).WithEach(
				   AsyncStateMachineSourceField =>
				   {
					   // we need to populate the data for the debugger?

					   //var SourceField_value = AsyncStateMachineSourceField.GetValue(NewStateMachine);

					   // it is a new type.
					   Console.WriteLine(new { AsyncStateMachineSourceField });

					   if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
					   {
						   AsyncStateMachineSourceField.SetValue(
							   NewStateMachineI,
							   that.state
							);
					   }

					   // field names/ tokens need to be encrypted like typeinfo.

					   // or, are we supposed to initialize a string value here?
					   var xStringField = that.StringFields.AsEnumerable().FirstOrDefault(
						   f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
					   );

					   if (xStringField != null)
					   {
						   // once we are to go back to client. we need to reverse it?

						   AsyncStateMachineSourceField.SetValue(
							   NewStateMachineI,
							   xStringField.value
							);
						   // next xml?
						   // before lets send our strings back with the new state!
						   // what about exceptions?
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
