using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSwitchToServiceContextAsync;
using TestSwitchToServiceContextAsync.Design;
using TestSwitchToServiceContextAsync.HTML.Pages;

namespace TestSwitchToServiceContextAsync
{
	// json tranferable, jsc should make all methods static, if no interfaces and a sealed class?
	public sealed class ArrayList<T>
	{
		//public T[] items = new T[0];
		internal T[] items = new T[0];

		// if we were to implement an interface via a member,
		// such methods need to be inlined. jsc analysis up for it yet?
	}

	public static class ArrayListExtensions
	{
		public static IEnumerable<T> AsEnumerable<T>(this ArrayList<T> that)
		{
			return that.items.AsEnumerable();
		}

		public static ArrayList<T> Add<T>(this ArrayList<T> that, T value)
		{
			var x = that.items.ToList();
			x.Add(value);

			that.items = x.ToArray();

			return that;
		}
	}

	public partial class ShadowIAsyncStateMachine
	{
		// allow chrome extension to send the source, for to hop into iframe
		// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs
		public string code;

		public string TypeName;
		public int state;

		public class xStringField
		{
			public string FieldName;

			public string value;
		}

		// first steps to get async string sharing to work
		// jsc should add analysis so a runtime could know if its worth to send a value over to the other side

		// for chrome trasnferables.. store as array instead?
		//public List<xStringField> StringFields = new List<xStringField>();
		public ArrayList<xStringField> StringFields = new ArrayList<xStringField>();


		// first test by 
		// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTab\ChromeExtensionHopToTab\Application.cs
		//public class ResumeableContinuation
		public struct ResumeableContinuation
		{
			public Action<ShadowIAsyncStateMachine> MoveNext;

			public ShadowIAsyncStateMachine shadowstate;
		}

		public static ResumeableContinuation ResumeableFromContinuation(Action continuation)
		{
			// testing autoinit...
			var value = default(ResumeableContinuation);
			var MoveNext = value.MoveNext;

			// can we take byref of a field yet in js?
			//value.shadowstate = FromContinuation(continuation, ref value.MoveNext);
			value.shadowstate = FromContinuation(continuation, ref MoveNext);

			// when can we context switch a byref field? 
			value.MoveNext = MoveNext;

			//02000036 TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine
			//{ Location =
			// assembly: W:\ChromeExtensionHopToTab.Application.exe
			// type: TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine, ChromeExtensionHopToTab.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
			// offset: 0x0013
			//  method:ResumeableContinuation ResumeableFromContinuation(System.Action) }
			//script: error JSC1000: Method: ResumeableFromContinuation, Type: TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine; emmiting failed : System.NotImplementedException: { ParameterType = System.Action`1[TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine]&, p = [0x0018] stfld      +0 -2{[0x0009]
			//   at jsc.IdentWriter.JavaScript_WriteParameters(Prestatement p, ILInstruction i, ILFlowStackItem[] s, Int32 offset, MethodBase m) in X:\jsc.internal.git\compiler\jsc\Languages\IdentWriter.cs:line 846
			//   at jsc.IL2ScriptGenerator.OpCode_call_override(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s, MethodBase m) in X:\jsc.internal.git\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.cs:line 410

			return value;
		}



		[Obsolete("jsc async rewriter wont like byref that much")]
		public static ShadowIAsyncStateMachine FromContinuation(
			Action continuation,

			//Error	CS0177	The out parameter 'AsyncStateMachineSource' must be assigned to before control leaves the current method	TestSwitchToServiceContextAsync	X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs	284
			//out IAsyncStateMachine AsyncStateMachineSource // = default(IAsyncStateMachine)

			ref Action<ShadowIAsyncStateMachine> MoveNext
			)
		{
			var AsyncStateMachineSource = default(IAsyncStateMachine);

			Console.WriteLine(new { continuation });
			Console.WriteLine(new { continuation.Method });
			Console.WriteLine(new { continuation.Target });

			var f = continuation.Target.GetType().GetFields(
				  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
			  );


			//var AsyncStateMachineSource = continuation.Target as IAsyncStateMachine;
			var AsyncStateMachineType = default(Type);
			var AsyncStateMachineFields = default(FieldInfo[]);

			var AsyncStateMachineStateField = default(FieldInfo);

			if (continuation.Target is IAsyncStateMachine)
			{
				AsyncStateMachineSource = (IAsyncStateMachine)continuation.Target;
			}


			var s = new ShadowIAsyncStateMachine
			{
			};

			#region AtAsyncStateMachineSourceField

			Action<FieldInfo> AtAsyncStateMachineSourceField = AsyncStateMachineSourceField =>
			{
				var value = AsyncStateMachineSourceField.GetValue(AsyncStateMachineSource);


				var xString = value as string;
				if (xString != null)
				{
					// look there is a string we should initialize on the other side.

					s.StringFields.Add(
						new xStringField
						{
							FieldName = AsyncStateMachineSourceField.Name,
							value = xString
						}
					);

				}

				if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
				{
					AsyncStateMachineStateField = AsyncStateMachineSourceField;
				}

				Console.WriteLine(new { AsyncStateMachineSourceField, value });
			};
			#endregion



			if (AsyncStateMachineSource == null)
			{
				f.WithEach(
					SourceField =>
					{
						var SourceField_value = SourceField.GetValue(continuation.Target);
						Console.WriteLine(new { SourceField, value = SourceField_value });

						// could it be, we are already enumerating asyncstatemachine ?

						var m_stateMachine = SourceField_value as IAsyncStateMachine;
						if (m_stateMachine != null)
						{
							//Error CS1628  Cannot use ref or out parameter 'AsyncStateMachineSource' inside an anonymous method, lambda expression, or query expression TestSwitchToServiceContextAsync X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs    219

							AsyncStateMachineSource = m_stateMachine;
							AsyncStateMachineType = m_stateMachine.GetType();

							AsyncStateMachineFields = AsyncStateMachineType.GetFields(
								System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
							);

							AsyncStateMachineFields.WithEach(AtAsyncStateMachineSourceField);
						}

					}
				);
			}
			else
			{
				AsyncStateMachineType = AsyncStateMachineSource.GetType();

				// inline mode?

				f.WithEach(AtAsyncStateMachineSourceField);
			}

			// does it look like in JVM?

			//enter VirtualOnCompleted..
			//{{ continuation = [object Object] }}
			//{{ Method = {{ InternalMethodToken = MycABsh3zjevXeqty6qy4w }} }}
			//{{ Target = [object Object] }}
			//{{ SourceField = zstateMachine, value = [object Object] }}
			//{{ AsyncStateMachineSourceField = __t__builder, value = [object Object] }}
			//{{ AsyncStateMachineSourceField = __1__state, value = 0 }}
			//{{ AsyncStateMachineSourceField = e, value = hello from server }}
			//{{ AsyncStateMachineSourceField = __u__1, value = [object Object] }}
			//{{ AsyncStateMachineSourceField = __u__2, value = null }}
			//{{ SourceField = yield, value = [object Object] }}

			// js seems to have the same issue where, struct fields are not inited?
			// for js, the ctor needs to do it? or can we do it on prototype level? 

			Console.WriteLine("FromContinuation " + new { AsyncStateMachineType, AsyncStateMachineStateField });

			if (AsyncStateMachineType == null)
				return null;

			if (AsyncStateMachineStateField == null)
				return null;

			s.TypeName = AsyncStateMachineType.FullName;
			s.state = (int)AsyncStateMachineStateField.GetValue(AsyncStateMachineSource);

			Console.WriteLine("FromContinuation " + new { s.state, s.TypeName });


			Func<string, string> DecoratedString =
				x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");


			MoveNext =
				that =>
				{
					var NextState = that.state;

					Console.WriteLine("enter MoveNext " + new { NextState, StringFields = that.StringFields.AsEnumerable().Count() });

					AsyncStateMachineSource.GetType().GetFields(
					  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
					  ).WithEach(
					   AsyncStateMachineSourceField =>
					   {
						   var xStringField = that.StringFields.AsEnumerable().FirstOrDefault(
							  ff => DecoratedString(ff.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
						  );

						   if (xStringField != null)
						   {
							   // once we are to go back to client. we need to reverse it?

							   AsyncStateMachineSourceField.SetValue(
								   AsyncStateMachineSource,
								   xStringField.value
								);
							   // next xml?
							   // before lets send our strings back with the new state!
							   // what about exceptions?
						   }
					   }
				   );

					// TypeError: ref$f[0]._4gAABnvBZz_auB7QhDebdPQ is not a function
					AsyncStateMachineStateField.SetValue(AsyncStateMachineSource, NextState);
					AsyncStateMachineSource.MoveNext();
				};

			return s;
		}
	}

}
