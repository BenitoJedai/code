using ScriptCoreLib.JavaScript.BCLImplementation.System.Net;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
	public partial class __Task<TResult> : __Task
	{
		// X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs

		// called by 
		// Task.Run, 
		// Task.ctor
		// TaskFactory.StartNew

		public void InternalInitializeInlineWorker(
			//Func<object, TResult> function,
			Delegate function,

			object state,

			CancellationToken c,
			TaskCreationOptions o,
			TaskScheduler s,


			// X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
			// allow special callbacks
			Action<Worker, MessageEvent> yield = null
			)
		{
			var InternalThreadCounter = InternalInlineWorker.InternalThreadCounter;

			InternalInlineWorker.InternalThreadCounter++;



			Delegate xfunction = function;

			#region WriteLine
			Action<string> WriteLine =
				text =>
				{

					// () means we are setting the thread up... [] is the thread
					Console.WriteLine("(" + InternalThreadCounter + ") " + text);

				};
			#endregion

			//WriteLine("enter InternalInitializeInlineWorker");


			// whatif the delegate is Action?

			// X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
			// what happened? also, as interface cannot handle ull yet


			var MethodType = typeof(FuncOfObjectToObject).Name;



			// what if this is a GUI task?

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run

			#region MethodToken


			// X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs


			if (xfunction.Target != null)
				if (xfunction.Target != Native.self)
				{
					// X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs
					// X:\jsc.svn\examples\javascript\async\AsyncNonStaticHandler\AsyncNonStaticHandler\Application.cs

					var TargetType = xfunction.Target.GetType();

					//Console.WriteLine("InternalInitializeInlineWorker " + new { Target = function.Target.ToString(), TargetType });

					// X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
					Delegate InternalTaskExtensionsScope_function = (xfunction.Target as dynamic).InternalTaskExtensionsScope_function;
					// X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\TaskExtensions.cs

					if (InternalTaskExtensionsScope_function != null)
					{
						//MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).InternalMethodToken;

						xfunction = InternalTaskExtensionsScope_function;
					}
				}
			#endregion

			var MethodToken = ((__MethodInfo)xfunction.Method).InternalMethodToken;



			#region MethodTargetTypeIndex
			var MethodTargetTypeIndex = default(object);



			// shall we get level2 types accross
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141112
			var MethodTargetObjectDataTypes = default(object[]);

			var MethodTargetObjectData = default(object[]);
			var MethodTargetObjectDataProgress = default(__IProgress<object>[]);
			var MethodTargetObjectDataIsProgress = default(bool[]);

			// we need to send in also the this argument of the function, lets find whats the token of the type
			if (xfunction.Target != null)
				// functions bound to self/window are considered to be static
				if (xfunction.Target != Native.self)
				{
					// X:\jsc.svn\examples\javascript\async\AsyncNonStaticHandler\AsyncNonStaticHandler\Application.cs

					var TargetType = xfunction.Target.GetType();

					MethodTargetTypeIndex = __Type.GetTypeIndex("TargetType", TargetType);


					// lets hope all the fields are transferable!

					var MethodTargetSerializableMembers = FormatterServices.GetSerializableMembers(TargetType);

					//foreach (var item in MethodTargetSerializableMembers)
					//{
					//    // do we have usage/security information available from the analyzer?
					//    Console.WriteLine("__Task will share scope, field " + item.Name);
					//    // for scope sharing first we may see IEvent. that cannot be sent over
					//    // as it is not a primitive and not under our control either
					//    // how can we exclude it?
					//    // we should also know who exactyl can use it in the inner scope
					//}

					MethodTargetObjectData = FormatterServices.GetObjectData(xfunction.Target, MethodTargetSerializableMembers);

					// X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs
					MethodTargetObjectDataTypes = new object[MethodTargetObjectData.Length];
					MethodTargetObjectDataProgress = new __IProgress<object>[MethodTargetObjectData.Length];
					MethodTargetObjectDataIsProgress = new bool[MethodTargetObjectData.Length];

					for (int i = 0; i < MethodTargetObjectData.Length; i++)
					{
						var MemberName = MethodTargetSerializableMembers[i].Name;
						var MemberValue = MethodTargetObjectData[i];

						if (MemberValue != null)
						{
							//0:6967ms __Task scope { MemberName = foo, isString = false, isInt32 = false, idx = vgAABBwAgD2RX0Pk4wU2RQ } 
							// 0:5749ms __Task scope { MemberName = foo, isString = false, isInt32 = false, idx = vgAABBwAgD2RX0Pk4wU2RQ, MemberType = <Namespace>. }
							// 0:3093ms __Task scope { MemberName = row, isString = false, isInt32 = false, idx = type$_2LWH6_a6FzTCDOJSbur6JKQ, MemberType = <Namespace>.xRow } 

							// is it our type/secure or not? or is it primitive?
							var MemberType = MemberValue.GetType();

							var IsString = Expando.Of(MemberValue).IsString;
							//var isString = MemberType == typeof(string);
							//var isInt32 = MemberType == typeof(int);
							var IsNumber = Expando.Of(MemberValue).IsNumber;
							var IsByteArray = Expando.Of(MemberValue).IsByteArray;

							// are we to send typeIndex to the other side for member reconstruction?
							var TypeIndex = __Type.GetTypeIndex(MemberName, MemberType);

							MethodTargetObjectDataTypes[i] = TypeIndex;

							//0:4812ms __Task scope { MemberName = scope1, isString = false, isInt32 = false } view-source:40687
							//0:4814ms __Task scope { MemberName = e, isString = false, isInt32 = false } 



							// null it out as we are not able to clone that object for the other thread
							if (TypeIndex == null)
								if (!IsString)
									if (!IsNumber)
										if (!IsByteArray)
											MethodTargetObjectData[i] = null;

							// we do not know yet how to handle cloning events on level2
							var IsDelegate = MemberValue is Delegate;
							if (IsDelegate)
								MethodTargetObjectData[i] = null;



							#region xSemaphoreSlim
							// X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\Worker.cs

							// X:\jsc.svn\examples\javascript\async\Test\TestSemaphoreSlim\TestSemaphoreSlim\ApplicationControl.cs
							var xSemaphoreSlim = MemberValue as __SemaphoreSlim;
							if (xSemaphoreSlim != null)
							{
								// will sending a null over create the object on the other side?
								MethodTargetObjectData[i] = null;


								var MemberName0 = MemberName;
								Action<string> WriteLine0 = text =>
								{
									WriteLine("(" + MemberName0 + ") " + text);
								};


								// we need to entangle the fields in current and the new thread..
								// if we were to get the signal, how?

								// 1 or more

								//var xInternalVirtualWaitAsync = default(TaskCompletionSource<object>);

								// X:\jsc.svn\examples\javascript\async\test\TestSemaphoreSlimAwaitThenReleaseInWorker\TestSemaphoreSlimAwaitThenReleaseInWorker\Application.cs
								// is somebody already awaiting?
								var xInternalVirtualWaitAsync = xSemaphoreSlim.InternalVirtualWaitAsync0;

								xSemaphoreSlim.InternalVirtualWaitAsync += continuation =>
								{
									WriteLine0("enter xSemaphoreSlim.InternalVirtualWaitAsync, ui is now awaiting for signal");

									// we need a signal from the worker, or the ui
									xInternalVirtualWaitAsync = continuation;
								};



								var xSemaphoreSlim_InternalIsEntangled = false;

								// called by?
								#region yield_xSemaphoreSlim
								Action<Worker, MessageEvent> yield_xSemaphoreSlim =


								 // we want in. lets wait for messages from the thread..
								 //yield += (Worker worker, MessageEvent e) =>
								 (Worker worker, MessageEvent e) =>
								{
									dynamic data = e.data;
									string __xSemaphoreSlim = data.xSemaphoreSlim;

									if (__xSemaphoreSlim == null)
										return;


									if (__xSemaphoreSlim != MemberName0)
										return;

									//

									// now we have a port to send the release signal?
									//WriteLine("xSemaphoreSlim MessageEvent " + new { MemberName0 });

									// next will be a release from the worker?

									// we made contact!
									#region InternalVirtualRelease
									if (!xSemaphoreSlim_InternalIsEntangled)
									{
										WriteLine0("yield_xSemaphoreSlim, worker is now listening for release signal");

										xSemaphoreSlim_InternalIsEntangled = true;

										xSemaphoreSlim.InternalVirtualRelease += delegate
										{
											// um. we need to post a message to the other side.
											// do we have a port channel for it?
											// does the other side expect signals?

											//WriteLine("xSemaphoreSlim.InternalVirtualRelease " + new { MemberName0 });
											WriteLine0("xSemaphoreSlim.InternalVirtualRelease, ui is sending signal to worker, resync");

											// xSemaphoreSlim_ByteArrayFields
											#region xSemaphoreSlim_ByteArrayFields
											var xSemaphoreSlim_ByteArrayFields = new List<__Task.xByteArrayField>();


											var MethodTargetTypeSerializableMembers_index = 0;

											// MethodTargetSerializableMembers
											// MethodTargetTypeSerializableMembers

											foreach (FieldInfo item in MethodTargetSerializableMembers)
											{
												// how would we know if the array is a byte array?

												// FieldType is not exactly available yet
												//Console.WriteLine("worker resync candidate " + new { item.Name, item.FieldType, item.FieldType.IsArray });

												var item_value = item.GetValue(xfunction.Target);
												if (item_value != null)
												{
													var item_value_IsByteArray = Expando.Of(item_value).IsByteArray;

													if (item_value_IsByteArray)
													{
														var value = (byte[])item_value;

														xSemaphoreSlim_ByteArrayFields.Add(
															new __Task.xByteArrayField
															{
																index = MethodTargetTypeSerializableMembers_index,

																// keep name for diagnostics
																Name = item.Name,

																value = value
															}
														);

														WriteLine0("ui to worker resync xByteArrayField candidate " + new { item.Name, value.Length });
													}
												}


												MethodTargetTypeSerializableMembers_index++;
											}
											#endregion

											foreach (var p in e.ports)
											{
												// release 1
												p.postMessage(
													new
													{
														// X:\jsc.svn\examples\javascript\async\Test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs
														xSemaphoreSlim_ByteArrayFields = xSemaphoreSlim_ByteArrayFields.ToArray()
													}
												);
											}

										};

										return;

									}
									#endregion


									// now wait for release from worker?

									// we never get the message?
									//e.ports[0].onmessage += ee =>
									//{
									//	Console.WriteLine("xSemaphoreSlim port0 onmessage " + new { MemberName0 });
									//};



									//e.ports[1].onmessage += ee =>
									//{
									//	Console.WriteLine("xSemaphoreSlim port1 onmessage " + new { MemberName0 });
									//};

									// workaround?

									WriteLine0("yield_xSemaphoreSlim, worker sent a release signal to ui, resync");


									#region read xSemaphoreSlim_ByteArrayFields
									{
										__Task.xByteArrayField[] xSemaphoreSlim_ByteArrayFields = data.xSemaphoreSlim_ByteArrayFields;
										// 55779ms ui xSemaphoreSlim MessageEvent, resync, trigger InternalVirtualWaitAsync? {{ MemberName0 = bytes1sema, xInternalVirtualWaitAsync = [object Object], Length = 1 }}

										//Console.WriteLine("ui xSemaphoreSlim MessageEvent, resync, trigger InternalVirtualWaitAsync? " + new { MemberName0, xInternalVirtualWaitAsync, xSemaphoreSlim_ByteArrayFields.Length });

										// X:\jsc.svn\examples\javascript\async\test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs
										if (xSemaphoreSlim_ByteArrayFields != null)
											foreach (var item in xSemaphoreSlim_ByteArrayFields)
											{
												var xFieldInfo = (FieldInfo)MethodTargetSerializableMembers[item.index];

												// can we set the value?
												WriteLine("ui resync " + new
												{
													item.index,
													//item.Name,
													xFieldInfo = xFieldInfo.Name,
													item.value
												});

												xFieldInfo.SetValue(
													xfunction.Target,

													// null?
													item.value
												);

											}
									}
									#endregion


									// what happens if we wnt to signal but nobody is waiting?
									// X:\jsc.svn\examples\javascript\async\test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs
									// X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs




									if (xInternalVirtualWaitAsync == null)
									{
										WriteLine0("cannot signal, xInternalVirtualWaitAsync is null, why?");
									}
									else
									{
										xInternalVirtualWaitAsync.SetResult(null);
									}

								};
								#endregion



								yield += yield_xSemaphoreSlim;
							}
							#endregion


							// X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs
							var IsProgress = MemberValue is __IProgress<object>;
							if (IsProgress)
							{
								MethodTargetObjectDataProgress[i] = (__IProgress<object>)MemberValue;
								MethodTargetObjectData[i] = null;

								// let worker know we want progress reports sent back to us
								MethodTargetObjectDataIsProgress[i] = true;
							}

							// X:\jsc.svn\examples\javascript\async\test\TestScopeWithDelegate\TestScopeWithDelegate\Application.cs
							// lets make sure no delegates are at level2 either


							var scope2 = MethodTargetObjectData[i];

							if (IsString)
							{
								// see path yet?
								Console.WriteLine("string: " + new { MemberName, scope2 });
							}
							else if (xSemaphoreSlim != null)
							{
								// the first entangled method. should we look a the fields?
								//Console.WriteLine("xSemaphoreSlim: " + new { MemberName });
							}
							else
							{
								if (scope2 != null)
								{
									// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141112
									// x:\jsc.svn\examples\javascript\test\testhoptothreadpoolawaitable\testhoptothreadpoolawaitable\application.cs


									var scope2Type = scope2.GetType();
									var scope2TypeSerializableMembers = FormatterServices.GetSerializableMembers(scope2Type);

									if (scope2TypeSerializableMembers.Length > 0)
									{
										// are there any members?
										WriteLine("will inspect scope2 as " + new { MemberName, scope2TypeSerializableMembers.Length });
										var scope2ObjectData = FormatterServices.GetObjectData(scope2, scope2TypeSerializableMembers);

										// the hacky way. later we need to refactor this a lot.
										for (int ii = 0; ii < scope2ObjectData.Length; ii++)
										{
											var scope2FieldName = scope2TypeSerializableMembers[ii].Name;

											WriteLine("scope2: " + MemberName + "." + scope2FieldName);


											var scope2value = scope2ObjectData[ii];
											if (scope2value != null)
											{
												var scope2IsDelegate = scope2value is Delegate;
												if (scope2IsDelegate)
												{
													scope2ObjectData[ii] = null;

													WriteLine("scope2 delegate discarded " + new { MemberName, scope2FieldName });

												}
											}
										}

										// um. lets remove typeinfo? 
										// why? to get defaults?
										var scope2copy = FormatterServices.GetUninitializedObject(scope2Type);
										FormatterServices.PopulateObjectMembers(scope2copy, scope2TypeSerializableMembers, scope2ObjectData);
										MethodTargetObjectData[i] = scope2copy;
									}
								}
							}



							WriteLine(
								"Task scope " +
								new
								{
									MemberName,
									IsString,
									IsNumber,
									IsDelegate,
									IsProgress,

									// will 
									TypeIndex
								}
							);
						}
					}
				}
			#endregion



			// X:\jsc.svn\examples\javascript\test\TestTypeHandle\TestTypeHandle\Application.cs

			#region stateTypeHandleIndex
			var stateTypeHandleIndex = default(object);
			var stateType = default(Type);
			var state_ObjectData = default(object);

			if (state != null)
			{
				stateType = state.GetType();
				stateTypeHandleIndex = __Type.GetTypeIndex("state", stateType);
				var state_SerializableMembers = FormatterServices.GetSerializableMembers(stateType);
				// Failed to execute 'postMessage' on 'Worker': An object could not be cloned.
				state_ObjectData = FormatterServices.GetObjectData(state, state_SerializableMembers);
			}
			#endregion




			#region CreateWorker
			Action<string> CreateWorker = uri =>
			{
				// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
				// since we can start scope sharing, static sync may be turned off?

				#region xdata___string
				// X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
				dynamic xdata___string = new object();

				// how much does this slow us down?
				// connecting to a new scope, we need a fresh copy of everything
				// we can start with strings
				foreach (ExpandoMember nn in Expando.Of(InternalInlineWorker.__string).GetMembers())
				{
					if (nn.Value != null)
						xdata___string[nn.Name] = nn.Value;
				}
				#endregion


				var worker = new global::ScriptCoreLib.JavaScript.DOM.Worker(
					uri
				   //InternalInlineWorker.GetScriptApplicationSourceForInlineWorker()
				   //global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
				   );


				// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress

				// InternalInitializeInlineWorker { IsIProgress = false, state = [object Object] }
				// e = (a.state instanceof PWjgSJKGsjiGudzxTfGfaA);

				// jsc does not yet support is interface
				// function PWjgSJKGsjiGudzxTfGfaA() {}  PWjgSJKGsjiGudzxTfGfaA.TypeName = "IProgress_1";
				// we should add .Interfaces = []

				//Action<object> OnReportAction = default(__IProgress<object>).Report;
				//var OnReportMethod = OnReportAction.Method;


				// workaround until null as interface works.

				if (state == null)
					state = new object();


				// what about delegates to the interface? 
				#region IsIProgress
				var IsIProgress = state is __IProgress<object>;

				// InternalInitializeInlineWorker: { IsIProgress = true, state = [object Object] }
				var xProgress = default(__IProgress<object>);

				if (IsIProgress)
				{
					// X:\jsc.svn\examples\javascript\async\test\TestWorkerProgress\TestWorkerProgress\Application.cs
					xProgress = (__IProgress<object>)state;
					state = null;
					state_ObjectData = null;
					stateTypeHandleIndex = null;
				}
				#endregion

				//Console.WriteLine("InternalInitializeInlineWorker: " + new { IsIProgress, IsTuple2_Item1_IsIProgress, state });



				int responseCounter = 0;

				#region postMessage
				worker.postMessage(
					new
					{
						InternalThreadCounter,


						MethodTargetObjectDataIsProgress,

						MethodTargetObjectData,

						// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141112
						MethodTargetObjectDataTypes,

						MethodTargetTypeIndex,

						// set by ?
						MethodToken,
						MethodType,



						// X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
						// fields


						// are we in state machine hop?
						state_SerializableMembers = default(object),
						state_ObjectData,

						stateTypeHandleIndex,

						state = state,


						IsIProgress,
						//IsTuple2_Item1_IsIProgress,

						__string = (object)xdata___string
					}
					,
					 e =>
					 {
						 responseCounter++;

						 // what kind of write backs do we expect?
						 // for now it should be console only


						 //Console.WriteLine(
						 //    "InternalInitializeInlineWorker: new message! "
						 //    + new
						 //    {
						 //        data = string.Join(
						 //           ", ",
						 //           Expando.Of(e.data).GetMemberNames().Select(k => (string)k).ToArray()
						 //        )
						 //    }
						 //);

						 // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
						 dynamic zdata = e.data;


						 // is it the same for service worker?
						 #region AtWrite
						 string AtWrite = zdata.AtWrite;

						 if (!string.IsNullOrEmpty(AtWrite))
						 {
							 // thread is writing to console isnt it.
							 // we have requested to be notified of it on the main thread instead.

							 if (AtWrite.EndsWith("\r\n"))
								 Console.WriteLine(AtWrite.Substring(0, AtWrite.Length - 2));
							 else
								 Console.Write(AtWrite);
						 }
						 #endregion

						 #region __string
						 var zdata___string = (object)zdata.__string;
						 if (zdata___string != null)
						 {
							 #region __string
							 // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
							 dynamic target = InternalInlineWorker.__string;
							 var m = Expando.Of(zdata___string).GetMembers();

							 foreach (ExpandoMember nn in m)
							 {
								 Console.WriteLine("Worker has sent changes " + new { nn.Name });

								 target[nn.Name] = nn.Value;
							 }
							 #endregion
						 }
						 #endregion


						 //zdata.MethodTargetObjectDataProgressReport = new { ProgressEvent, i };

						 #region MethodTargetObjectDataProgressReport
						 {
							 // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs
							 // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
							 dynamic MethodTargetObjectDataProgressReport = zdata.MethodTargetObjectDataProgressReport;
							 if ((object)MethodTargetObjectDataProgressReport != null)
							 {
								 object ProgressEvent = MethodTargetObjectDataProgressReport.ProgressEvent;
								 int ii = MethodTargetObjectDataProgressReport.ii;

								 MethodTargetObjectDataProgress[ii].Report(
									 ProgressEvent
								);
							 }
						 }
						 #endregion


						 #region ContinueWithResult
						 {
							 // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
							 dynamic ContinueWithResult = zdata.ContinueWithResult;
							 if ((object)ContinueWithResult != null)
							 {
								 responseCounter++;

								 // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

								 var ResultTypeIndex = default(object);
								 var ResultObjectData = default(object);

								 object Result = ContinueWithResult.Result;

								 // primitives wont have index
								 if (Result != null)
								 {
									 // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs

									 ResultTypeIndex = ContinueWithResult.ResultTypeIndex;
									 ResultObjectData = ContinueWithResult.ResultObjectData;
								 }

								 // are we getting multiple responses?
								 Console.WriteLine("Task ContinueWithResult " + new { responseCounter, ResultTypeIndex, Result });

								 if (ResultTypeIndex != null)
								 {
									 // X:\jsc.svn\examples\javascript\async\test\TestTaskRunReturnToString\TestTaskRunReturnToString\Application.cs
									 // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
									 dynamic self = Native.self;

									 var ResultType = Type.GetTypeFromHandle(new __RuntimeTypeHandle((IntPtr)self[ResultTypeIndex]));
									 var zResult = FormatterServices.GetUninitializedObject(ResultType);
									 var zResultTypeSerializableMembers = FormatterServices.GetSerializableMembers(ResultType);


									 // available for flash too yet?
									 FormatterServices.PopulateObjectMembers(
										 zResult,
										 zResultTypeSerializableMembers,
										 (object[])ResultObjectData
									 );

									 var xResult = new TaskCompletionSource<object>();
									 //xResult.SetResult(Result);
									 xResult.SetResult(zResult);
									 this.InternalSetCompleteAndYield((TResult)(object)xResult.Task);

								 }
								 else
								 {
									 // :7170ms Task ContinueWithResult { ResultTypeIndex = type$rfUTAxaiVTOCOkKbE6i0hg } 

									 //0:7506ms Task ContinueWithResult view-source:40742
									 //0:7508ms { Result = [object Object] } 

									 var xResult = new TaskCompletionSource<object>();
									 //xResult.SetResult(Result);
									 xResult.SetResult(Result);
									 this.InternalSetCompleteAndYield((TResult)(object)xResult.Task);
								 }
							 }
						 }
						 #endregion


						 #region yield
						 {
							 // X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
							 // this should be disabled if we hopped to ui? how?
							 // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
							 dynamic xyield = zdata.yield;
							 if ((object)xyield != null)
							 {

								 object value = xyield.value;

								 // X:\jsc.svn\examples\javascript\async\test\TestSemaphoreSlimAwaitThenReleaseInWorker\TestSemaphoreSlimAwaitThenReleaseInWorker\Application.cs

								 Console.WriteLine("__Task.InternalStart inner complete (pre bugcheck) " + new { yield = new { value } });

								 // needs more tests.
								 // is causing trouble for older tests?

								 this.InternalDispose = delegate
								 {
									 Console.WriteLine("at InternalDispose");
									 worker.terminate();
								 };

								 this.InternalSetCompleteAndYield((TResult)value);



								 // when to terminate???
								 //w.terminate();

							 }
						 }
						 #endregion


						 #region __IProgress_Report
						 if (xProgress != null)
						 {
							 // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
							 dynamic __IProgress_Report = zdata.__IProgress_Report;

							 if ((object)__IProgress_Report != null)
							 {
								 object value = __IProgress_Report.value;




								 //Console.WriteLine("InternalInitializeInlineWorker Report: " + new { __IProgress_Report = new { value } });


								 xProgress.Report(value);
							 }
						 }
						 #endregion


						 // X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
						 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
						 #region  HopToUIAwaitable
						 // where can we send the signal?

						 #endregion

						 if (yield != null)
							 yield(worker, e);

					 }
				);
				#endregion

			};
			#endregion



			#region InternalStart
			this.InternalStart = delegate
			{
				// X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs



				#region GetScriptApplicationSourceForInlineWorker
				var u = InternalInlineWorker.GetScriptApplicationSourceForInlineWorker();

				//GetScriptApplicationSourceForInlineWorker { value = view-source#worker }

				if (u == Worker.ScriptApplicationSource + "#worker")
				{
					#region base not redirected
					if (Native.document.baseURI == Native.document.location.href)
					{
						// X:\jsc.svn\examples\javascript\async\Test\TestDownloadStringTaskAsync\TestDownloadStringTaskAsync\Application.cs

						// X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
						//Console.WriteLine("Document base not redirected...");

						// its about time to cache the inline worker source. if we have not done it before that is..

						var sw = Stopwatch.StartNew();

						if (InternalWorkerSourceToBlobCache.ContainsKey(u))
						{
							// in this case, lets continue in the blob..

							WriteLine("worker source is in cache... pending");

							InternalWorkerSourceToBlobCache[u].ContinueWith(
								task =>
								{
									WriteLine("worker source is in cache... " + new { sw.ElapsedMilliseconds });


									CreateWorker(task.Result);
								}
							);


							return;
						}

						// otherwise lets download, once...
						// X:\jsc.svn\examples\javascript\async\Test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs


						WriteLine("will download worker source into cache...");

						var pending = new TaskCompletionSource<string>();

						InternalWorkerSourceToBlobCache[u] = pending.Task;


						// 4.0 compatible, using 4.5 feats
						new __WebClient().DownloadStringTaskAsync(u).ContinueWith(
							task =>
							{
								WriteLine("will download worker source into cache... done " + new { task.Result.Length, sw.ElapsedMilliseconds });

								var aFileParts = new[] { task.Result };
								var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" }); // the blob
								var url = URL.createObjectURL(oMyBlob);

								pending.SetResult(url);

								CreateWorker(url);
							}
						);

						return;
					}
					#endregion

					// see also
					// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs

					{

						// tested by?
						Console.WriteLine("Document base redirected...");

						var w = new WebClient();


						// continue with?
						w.DownloadStringCompleted +=
							(sender, args) =>
							{

								var aFileParts = new[] { args.Result };
								var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" }); // the blob
								var url = URL.createObjectURL(oMyBlob);

								InternalInlineWorker.ScriptApplicationSourceForInlineWorker = url;

								u = InternalInlineWorker.GetScriptApplicationSourceForInlineWorker();

								CreateWorker(u);
							};

						// Failed to load resource: the server responded with a status of 400 (Bad Request) http://192.168.1.75:24275/:view-source
						w.DownloadStringAsync(
							new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
						);

						return;
					}
				}
				#endregion


				CreateWorker(u);
			};
			#endregion

		}

		static Dictionary<string, Task<string>> InternalWorkerSourceToBlobCache = new Dictionary<string, Task<string>>();





		public __Task()
		{

		}

		// called by?
		//public __Task(Func<object, TResult> function, object state)
		public __Task(Delegate function, object state)
		{
			// X:\jsc.svn\examples\javascript\async\test\TestTaskRun\TestTaskRun\Application.cs

			InternalInitializeInlineWorker(
				function,
				state,
				default(CancellationToken),
				default(TaskCreationOptions),
				TaskScheduler.Default
			 );
		}







	}
}
