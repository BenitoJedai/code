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

	public partial class ShadowIAsyncStateMachine
	{
		public string TypeName;
		public int state;

		public class xStringField
		{
			public string FieldName;

			public string value;
		}

		// first steps to get async string sharing to work
		// jsc should add analysis so a runtime could know if its worth to send a value over to the other side
		public List<xStringField> StringFields = new List<xStringField>();


		public static ShadowIAsyncStateMachine FromContinuation(
			Action continuation,

			//Error	CS0177	The out parameter 'AsyncStateMachineSource' must be assigned to before control leaves the current method	TestSwitchToServiceContextAsync	X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs	284
			//out IAsyncStateMachine AsyncStateMachineSource // = default(IAsyncStateMachine)

			ref Action<int> MoveNext
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

			Console.WriteLine(new { AsyncStateMachineType, AsyncStateMachineStateField });

			if (AsyncStateMachineType == null)
				return null;

			if (AsyncStateMachineStateField == null)
				return null;

			s.TypeName = AsyncStateMachineType.FullName;
			s.state = (int)AsyncStateMachineStateField.GetValue(AsyncStateMachineSource);

			Console.WriteLine(new { s.state, s.TypeName });

			MoveNext =
				NextState =>
				{
					Console.WriteLine("enter MoveNext " + new { NextState });

					// TypeError: ref$f[0]._4gAABnvBZz_auB7QhDebdPQ is not a function
					AsyncStateMachineStateField.SetValue(AsyncStateMachineSource, NextState);
					AsyncStateMachineSource.MoveNext();
				};

			return s;
		}
	}

}
