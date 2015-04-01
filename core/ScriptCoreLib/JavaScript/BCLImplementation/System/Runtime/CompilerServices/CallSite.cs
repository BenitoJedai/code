using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
	// see: http://msdn.microsoft.com/en-us/library/System.Runtime.CompilerServices.CallSite.aspx
	// http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Actions/CallSite.cs
	// https://github.com/mono/mono/blob/master/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/CallSite.cs
	// https://github.com/mono/mono/blob/master/mcs/tools/cil-strip/Mono.Cecil/CallSite.cs
	// https://github.com/mono/mono/blob/master/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/CallSiteOps.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Runtime/CompilerServices/CallSite.cs

	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs

	// 
	[Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite))]
	internal class __CallSite
	{

		// tested by
		// X:\jsc.svn\examples\javascript\Test\Test435Dynamic\Test435Dynamic\Application.cs
		// X:\jsc.svn\examples\javascript\Test\Test435CoreAsDynamic\Test435CoreAsDynamic\Class1.cs


		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/uint8clampedarray
		// roslyn dynamic can only be used on merge mode?


		// can we move it into Shared?
		// ActionSctipt is also on board?


		public CallSiteBinder Binder { get; set; }
	}

	[Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite<>))]
	internal class __CallSite<T> : __CallSite
	{


		public T Target;



		public static __CallSite<T> Create(CallSiteBinder binder)
		{
			// T is Func<CallSite, object, IFunction, object>

			//Console.WriteLine("__CallSite.Create");

			// see also: X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\Microsoft\CSharp\RuntimeBinder\Binder.cs

			#region __GetIndexBinder
			{
				var xGetIndexBinder = (object)binder as __GetIndexBinder;
				if (xGetIndexBinder != null)
				{
					var r = new Func<__CallSite, object, object, object>(
						(site, subject, key) =>
						{
							// X:\jsc.svn\examples\javascript\test\TestDynamicGetIndex\TestDynamicGetIndex\Application.cs

							// tested by?
							var x = subject as DynamicObject;
							if (x != null)
							{
								//Console.WriteLine("__SetMemberBinder DynamicObject");
								var result = default(object);

								if (x.TryGetIndex((GetIndexBinder)(object)xGetIndexBinder, new[] { key }, out result))
								{
									return result;
								}
							}

							//Console.WriteLine("__CallSite __GetIndexBinder " + new { subject, key });

							var value = ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMember(
								subject, key
							);

							//var value = new IFunction("subject", "name", "return subject[name];").apply(null,
							//    subject,
							//    GetMember.Name
							//);

							return value;
						}
					);
					return r;
				}
			}
			#endregion


			#region __SetIndexBinder
			{
				var __SetIndexBinder = (object)binder as __SetIndexBinder;
				if (__SetIndexBinder != null)
				{
					//0x006e . ldsfld                     [TestDynamicSetIndexer] TestDynamicSetIndexer.Application+<.ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string, string) -> Object>
					//0x0073 . ldfld                      [System.Core] System.Runtime.CompilerServices.CallSite`1[[System.Func`5[[System.Runtime.CompilerServices.CallSite, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]].Target : (CallSite, object, string, string) -> Object
					//0x0078 . . ldsfld                   arg1 <- [TestDynamicSetIndexer] TestDynamicSetIndexer.Application+<.ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string, string) -> Object>
					//0x007d . . . ldloc.2                arg2 <- loc.2 : object
					//0x007e . . . . ldstr                str0 <- "preview:"
					//0x0083 . . . . . ldloc.0            str1 <- loc.0 : string
					//0x0084 . . . . call                 arg3 <- [mscorlib] System.String.Concat(str0 : string, str1 : string) : String
					//0x0089 . . . . . ldloc.1            arg4 <- loc.1 : string
					//0x008a . callvirt                   [mscorlib] System.Func`5.Invoke(arg1 : CallSite, arg2 : object, arg3 : string, arg4 : string) : Object
					//0x008f pop 
					//0x0090 . ldsfld                     [TestDynamicSetIndexer] TestDynamicSetIndexer.Application+<.ctor>o__SiteContainer0.<>p__Site2 : CallSite`1<(CallSite, Type, object) -> void>
					//0x0095 brtrue.s 
					//0x0095 -> 0x0097 0x00da 


					var r = new Func<__CallSite, object, object, object, object>(
						(site, subject, key, value) =>
						{
							//var x = subject as DynamicObject;
							//if (x != null)
							//{
							//    //Console.WriteLine("__SetMemberBinder DynamicObject");

							//    if (x.TrySetIndex((SetMemberBinder)(object)SetMember, value))
							//    {
							//        return null;
							//    }
							//}

							//#region special rule - boundary DOM / BCL
							//if (subject == Native.Window)
							//{
							//    var xx = value as Delegate;

							//    if (xx != null)
							//    {
							//        value = IFunction.OfDelegate(xx);
							//    }
							//}
							//#endregion

							//Console.WriteLine("__CallSite SetMember " + new { subject, SetMember.name, value });

							ScriptCoreLib.JavaScript.Runtime.Expando.InternalSetMember(
								subject,
								key,
								value
							);

							//new IFunction("subject", "name", "value", "subject[name] = value;").apply(null,
							//    subject,
							//    SetMember.Name,
							//    value
							//);

							return null;
						}
					);
					return r;
				}
			}
			#endregion

			#region SetMember
			{
				var SetMember = (object)binder as __SetMemberBinder;
				if (SetMember != null)
				{
					var r = new Func<__CallSite, object, object, object>(
						(site, subject, value) =>
						{
							var x = subject as DynamicObject;
							if (x != null)
							{
								//Console.WriteLine("__SetMemberBinder DynamicObject");

								if (x.TrySetMember((SetMemberBinder)(object)SetMember, value))
								{
									return null;
								}
							}

							#region special rule - boundary DOM / BCL
							if (subject == Native.window)
							{
								var xx = value as Delegate;

								if (xx != null)
								{
									value = IFunction.OfDelegate(xx);
								}
							}
							#endregion

							//Console.WriteLine("__CallSite SetMember " + new { subject, SetMember.name, value });

							ScriptCoreLib.JavaScript.Runtime.Expando.InternalSetMember(
								subject,
								SetMember.Name,
								value
							);

							//new IFunction("subject", "name", "value", "subject[name] = value;").apply(null,
							//    subject,
							//    SetMember.Name,
							//    value
							//);

							return null;
						}
					);
					return r;
				}
			}
			#endregion

			#region GetMember
			{
				// X:\jsc.svn\examples\javascript\test\TestDynamicToArray\TestDynamicToArray\Application.cs

				var GetMember = (object)binder as __GetMemberBinder;
				if (GetMember != null)
				{
					var r = new Func<__CallSite, object, object>(
						(site, subject) =>
						{
							#region DynamicObject
							var x = subject as DynamicObject;
							if (x != null)
							{
								//Console.WriteLine("__SetMemberBinder DynamicObject");
								var result = default(object);

								if (x.TryGetMember((GetMemberBinder)(object)GetMember, out result))
								{
									return result;
								}
							}
							#endregion

							//Console.WriteLine("__CallSite GetMember " + new { subject, GetMember.name });

							// what does CLR do when we do . on null?
							if (subject == null)
								return null;

							var value = ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMember(
								subject, GetMember.Name
							);

							//var value = new IFunction("subject", "name", "return subject[name];").apply(null,
							//    subject,
							//    GetMember.Name
							//);

							return value;
						}
					);
					return r;
				}
			}
			#endregion

			#region Convert
			{
				var Convert = (object)binder as __Binder.__Convert;
				if (Convert != null)
				{
					var r = new Func<__CallSite, object, object>(
						(site, value) =>
						{
							//Console.WriteLine("__CallSite Convert " + new { value });

							// should we do some reflection and conversion?
							return value;
						}
					);
					return r;
				}
			}
			#endregion

			#region __InvokeMemberBinder
			// X:\jsc.svn\examples\javascript\Test\TestDynamicCall\TestDynamicCall\Application.cs
			{
				//0:57ms { Name = alert, Count = 2 }
				//view-source:42763 0:59ms { target = [object Window], Name = alert, arg1 = hello world }

				var xInvokeMemberBinder = (object)binder as __InvokeMemberBinder;
				if (xInvokeMemberBinder != null)
				{
					var Count = xInvokeMemberBinder.argumentInfo.Count();

					// ldc.i4                       InvokeMember(... flags: (CSharpBinderFlags) ResultDiscarded = 256 (0x00000100)
					var IsReturnVoid = xInvokeMemberBinder.flags == global::Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags.ResultDiscarded;

					Console.WriteLine(new { xInvokeMemberBinder.Name, xInvokeMemberBinder.ReturnType, IsReturnVoid, Count });

					//foreach (var item in xInvokeMemberBinder.argumentInfo)
					//{
					//    Console.WriteLine(
					//        new { item }
					//        );
					//}

					//0:37ms { Name = alert, Count = 3 }

					if (Count == 4)
					{
						if (IsReturnVoid)
						{
							var r = new Action<__CallSite, object, object, object, object>(
								 (site, target, arg1, arg2, arg3) =>
								 {

									 Console.WriteLine(
										 new { target, xInvokeMemberBinder.Name, arg1, arg2, arg3 }
										 );

									 var __value = IFunction.Of(target, xInvokeMemberBinder.Name).apply(target,
										 arg1, arg2, arg3
									 );


									 //return __value;
								 }
							 );

							return r;
						}
						else
						{
							// [TestDynamicCall] TestDynamicCall.Application+<_ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string) -> void>
							var r = new Func<__CallSite, object, object, object, object, object>(
								(site, target, arg1, arg2, arg3) =>
								{

									Console.WriteLine(
										new { target, xInvokeMemberBinder.Name, arg1, arg2, arg3 }
										);

									var __value = IFunction.Of(target, xInvokeMemberBinder.Name).apply(target,
										arg1, arg2, arg3
									);


									return __value;
								}
							);
							return r;
						}
					}


					if (Count == 3)
					{
						// [TestDynamicCall] TestDynamicCall.Application+<_ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string) -> void>
						var r = new Func<__CallSite, object, object, object, object>(
							(site, target, arg1, arg2) =>
							{

								Console.WriteLine(
									new { target, xInvokeMemberBinder.Name, arg1, arg2 }
									);

								return null;
							}
						);
						return r;
					}

					if (Count == 2)
					{
						// [TestDynamicCall] TestDynamicCall.Application+<_ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string) -> void>


						if (IsReturnVoid)
						{

							var r = new Action<__CallSite, object, object>(
								(site, target, arg1) =>
								{
									Console.WriteLine(
										 new { target, xInvokeMemberBinder.Name, arg1 }
										 );

									// PgAABOHIvzeIVHiwt04BEQ.Target.PSAABv_ap6j2DT0O2SaNyVw(PgAABOHIvzeIVHiwt04BEQ, c, 'hello world');
									var __value = IFunction.Of(target, xInvokeMemberBinder.Name).apply(target,
										arg1
									);


									//return __value;
								}
							);
							return r;
						}
						else
						{
							var r = new Func<__CallSite, object, object, object>(
						   (site, target, arg1) =>
						   {
							   Console.WriteLine(
									new { target, xInvokeMemberBinder.Name, arg1 }
									);

							   // PgAABOHIvzeIVHiwt04BEQ.Target.PSAABv_ap6j2DT0O2SaNyVw(PgAABOHIvzeIVHiwt04BEQ, c, 'hello world');
							   var __value = IFunction.Of(target, xInvokeMemberBinder.Name).apply(target,
								   arg1
							   );


							   return __value;
						   }
					   );
							return r;
						}
					}

					if (Count == 1)
					{
						if (IsReturnVoid)
						{
							var r = new Action<__CallSite, object>(
								  (site, target) =>
								  {

									  Console.WriteLine(
										  new { target, xInvokeMemberBinder.Name }
										  );

									  var __value = IFunction.Of(target, xInvokeMemberBinder.Name).apply(target);

								  }
							  );
							return r;
						}
						else
						{
							// [TestDynamicCall] TestDynamicCall.Application+<_ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string) -> void>
							var r = new Func<__CallSite, object, object>(
								(site, target) =>
								{

									Console.WriteLine(
										new { target, xInvokeMemberBinder.Name }
										);

									var __value = IFunction.Of(target, xInvokeMemberBinder.Name).apply(target);

									return __value;
								}
							);
							return r;
						}
					}
				}
			}
			#endregion




			#region __UnaryOperationBinder
			{
				var xUnaryOperationBinder = (object)binder as __UnaryOperationBinder;
				if (xUnaryOperationBinder != null)
				{

					var r = new Func<__CallSite, object, bool>(
					   (site, target) =>
					   {

						   Console.WriteLine(
							   new
							   {
								   target,
								   xUnaryOperationBinder.operation,
								   xUnaryOperationBinder.flags
								   //, xUnaryOperationBinder.argumentInfo.ToArray().Length 
							   }
							   );


						   if (xUnaryOperationBinder.operation == global::System.Linq.Expressions.ExpressionType.IsTrue)
						   {
							   //  0:46ms { target = true, operation = 83, flags = 0 }
							   // now what?
							   return global::System.Collections.Comparer.Default.Compare(target, true) == 0;
						   }


						   throw new NotImplementedException(new { binder, xUnaryOperationBinder.operation }.ToString());
					   }
				   );

					return r;
				}
			}
			#endregion




			#region __BinaryOperationBinder
			{
				var xBinaryOperationBinder = (object)binder as __BinaryOperationBinder;
				if (xBinaryOperationBinder != null)
				{
					var r = new Func<__CallSite, object, string, bool>(
					   (site, target, value) =>
					   {

						   Console.WriteLine(
							   new
							   {
								   xBinaryOperationBinder.operation,
								   xBinaryOperationBinder.flags,

								   target,
								   value
								   //, xBinaryOperationBinder.argumentInfo.ToArray().Length 
							   }
							   );


						   if (xBinaryOperationBinder.operation == global::System.Linq.Expressions.ExpressionType.Equal)
						   {
							   // Uncaught Error: NotImplementedException: { binder = BinaryOperationBinder, operation = 13 }

							   // now what?
							   // 0:45ms { operation = 13, flags = 0, target = foo, value = foo }

							   return global::System.Collections.Comparer.Default.Compare(target, value) == 0;
						   }


						   throw new NotImplementedException(new { binder, xBinaryOperationBinder.operation }.ToString());
					   }
				   );

					return r;

				}
			}
			#endregion

			Console.WriteLine(new { binder });

			throw new NotImplementedException("__CallSite.Create " + new { binder });

		}

		public static implicit operator __CallSite<T>(Delegate Target)
		{
			// crude casting.
			// this will work in JavaScript.

			return new __CallSite<T> { Target = (T)(object)Target };
		}

	}


}
