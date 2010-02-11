using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using jsc.meta.Library;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;
using jsc.meta.Library.Templates;
using jsc;
using jsc.Library;
using jsc.Languages.IL;
using ScriptCoreLib.JavaScript;
using jsc.meta.Commands.Rewrite.Templates;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument
	{
		const string __in_Delegate = "__in_Delegate";
		const string __proxy_Interface = "__proxy_Interface";
		const string __in_Method = "__in_Method";
		const string __out_Method = "__out_Method";
		const string __out_MethodDelayed = "__out_MethodDelayed";
		const string __out_MethodClosure = "__out_MethodClosure";

		const string _context = "context";
		const string _callback = "callback";
		const string _this = "_this";
		const string _token = "_token";

		const string _ToType = "ToType";
		const string _FromType = "FromType";
		const string _Invoke = "Invoke";

		private void WriteExternalInterface(
			Type source,
			RewriteToAssembly.TypeRewriteArguments a,
			RewriteToAssembly r,

			Func<object, object[], object> ExternalCall,

			Action<MethodBuilderInfo> ExternalCallback

			)
		{




			var TypeCache = r.RewriteArguments.context.TypeCache;
			var MethodCache = r.RewriteArguments.context.MethodCache;

			var x = GetExternalInterfaceMethodsFromType(source);

			var Interfaces = x.Select(k => k.DeclaringType).Where(k => k.IsInterface).Distinct().Select((k, i) => new { k, i }).ToArray();
			var Delegates = x.SelectMany(k => k.GetSignatureTypes()).Where(k => k.IsDelegate()).Distinct().Select((k, i) => new { k, i, Invoke = k.GetMethod("Invoke") }).ToArray();

			var Methods = Interfaces.SelectMany(k => k.k.GetMethods()).Concat(Delegates.Select(k => k.Invoke)).Select((k, i) => new { k, i }).ToArray();
			var MethodsLocal = x.Where(k => k.DeclaringType == source).Select((k, i) => new { k, i }).ToArray();
			var MethodsIncoming = Methods.Concat(MethodsLocal).Select((k, i) => new { k.k, i }).ToArray();


			var __out = new Dictionary<MethodInfo, MethodBuilder>();
			var __out_field = new Dictionary<MethodInfo, FieldBuilder>();

			#region __out_Method(__out_field)
			foreach (var item in Methods)
			{
				var f = a.Type.DefineField(__out_Method + item.i + _callback, typeof(string), FieldAttributes.Assembly);

				__out_field[item.k] = f;
			}

			var InitializeParameters = Enumerable.Range(0, Methods.Length).Select(k => typeof(string)).ToArray();

			var Initialize = a.Type.DefineMethod(__in_Method, MethodAttributes.Public, CallingConventions.Standard,
				typeof(void),
				InitializeParameters
			);

			Initialize.DefineAttribute(
				new ObfuscationAttribute
				{
					Feature = "meta method: we are now able to call the external interface"
				}
				,
				typeof(ObfuscationAttribute)
			);

			if (ExternalCallback != null)
				ExternalCallback(new MethodBuilderInfo { Method = Initialize, Parameters = InitializeParameters });

			{
				var il = Initialize.GetILGenerator();

				foreach (var item in Methods)
				{
					Initialize.DefineParameter(item.i + 1, ParameterAttributes.None, item.k.Name);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg, (short)(item.i + 1));

					il.Emit(OpCodes.Stfld, __out_field[item.k]);
				}

				il.Emit(OpCodes.Ret);
			}


			#endregion

			var __ExternalCall = MethodCache[ExternalCall.Method];

			#region __out_Method
			foreach (var item in Methods)
			{
				var m = a.Type.DefineMethod(
					// javac will complain!
					__out_Method + item.i, MethodAttributes.Public | MethodAttributes.Final,

					typeof(void) == item.k.ReturnType ? typeof(void) : typeof(string),

					Enumerable.Range(0, item.k.GetParameters().Length + 1).Select(k => typeof(string)).ToArray()
				);

				__out[item.k] = m;

				m.DefineAttribute(
					new ObfuscationAttribute
					{
						Feature = "out method: " + item.k.DeclaringType.FullName + "." + item.k.Name
					}
					,
					typeof(ObfuscationAttribute)
				);

				m.DefineParameter(1, ParameterAttributes.None, item.k.DeclaringType.IsInterface ? _this : _callback);

				foreach (var p in item.k.GetParameters())
				{
					m.DefineParameter(p.Position + 2, ParameterAttributes.None, p.Name);
				}

				var il = m.GetILGenerator();

				#region loc0 = arguments
				il.DeclareLocal(typeof(object[]));

				il.Emit(OpCodes.Ldc_I4, item.k.GetParameters().Length + 2);
				il.Emit(OpCodes.Newarr, typeof(object));
				il.Emit(OpCodes.Stloc_0);

				il.Emit(OpCodes.Ldloc_0);
				il.Emit(OpCodes.Ldc_I4_0);
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, __out_field[item.k]);
				il.Emit(OpCodes.Stelem_Ref);

				il.Emit(OpCodes.Ldloc_0);
				il.Emit(OpCodes.Ldc_I4_1);
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Stelem_Ref);

				var ParameterTypes = item.k.GetParameterTypes().Select((k, i) => new { k, i }).ToArray();

				foreach (var p in ParameterTypes)
				{
					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ldc_I4, p.i + 2);
					il.Emit(OpCodes.Ldarg, (short)(p.i + 2));
					il.Emit(OpCodes.Stelem_Ref);
				}
				#endregion

				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldloc_0);

				il.Emit(OpCodes.Call, __ExternalCall);

				if (item.k.ReturnType == typeof(void))
					il.Emit(OpCodes.Pop);
				else
					il.Emit(OpCodes.Castclass, typeof(string));

				il.Emit(OpCodes.Ret);

			}
			#endregion

			// any caching should be done by ToType?
			var __in_ToType = new Dictionary<Type, MethodBuilder>();


			#region Interfaces
			foreach (var item in Interfaces)
			{
				var proxy = a.Type.DefineNestedType(
					__proxy_Interface + item.i,
					TypeAttributes.Sealed | TypeAttributes.Class | TypeAttributes.NestedPublic,
					null,
					new[] { TypeCache[item.k] }
				);

				var proxy_ctor = proxy.DefineDefaultConstructor(MethodAttributes.Public);

				var proxy_context = proxy.DefineField(_context, TypeCache[source], FieldAttributes.Public);
				var proxy_this = proxy.DefineField(_this, typeof(string), FieldAttributes.Public);

				#region ToType
				var proxy_ToType = proxy.DefineMethod(
					_ToType,
					MethodAttributes.Static | MethodAttributes.Public,
					TypeCache[item.k],
					new[] { TypeCache[source], typeof(string) }
				);

				proxy_ToType.DefineParameter(1, ParameterAttributes.None, _context);
				proxy_ToType.DefineParameter(2, ParameterAttributes.None, _this);

				__in_ToType[item.k] = proxy_ToType;

				{
					var il = proxy_ToType.GetILGenerator();

					var loc = il.DeclareLocal(proxy);

					il.Emit(OpCodes.Newobj, proxy_ctor);
					il.Emit(OpCodes.Stloc_0);

					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Stfld, proxy_context);

					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Stfld, proxy_this);

					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ret);

				}
				#endregion

				foreach (var m in item.k.GetMethods())
				{
					var proxy_method = proxy.DefineMethod(
						m.Name,
						m.Attributes & ~MethodAttributes.Abstract,
						m.CallingConvention,
						TypeCache[m.ReturnType],
						m.GetParameterTypes().Select(k => TypeCache[k]).ToArray()
					);

					var il = proxy_method.GetILGenerator();

					if (m.ReturnType.IsDelegate() || m.ReturnType.IsInterface)
					{
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, proxy_context);
					}


					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, proxy_context);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, proxy_this);

					foreach (var p in m.GetParameters())
					{
						// can we get the remote this/callback or do we need to generate one?
						il.Emit(OpCodes.Ldnull);
					}

					il.Emit(OpCodes.Call, __out[m]);



					if (m.ReturnType.IsDelegate() || m.ReturnType.IsInterface)
					{
						il.Emit(OpCodes.Call, __in_ToType[m.ReturnType]);
					}

					il.Emit(OpCodes.Ret);
				}

				r.TypeCreated +=
					tt =>
					{
						if (tt.SourceType != a.SourceType)
							return;

						proxy.CreateType();
					};
			}
			#endregion

			#region Delegates
			foreach (var item in Delegates)
			{
				var proxy = a.Type.DefineNestedType(__in_Delegate + item.i, TypeAttributes.Sealed | TypeAttributes.Class | TypeAttributes.NestedPublic | TypeAttributes.Class, null, null);

				var proxy_ctor = proxy.DefineDefaultConstructor(MethodAttributes.Public);

				var proxy_context = proxy.DefineField(_context, TypeCache[source], FieldAttributes.Public);
				var proxy_callback = proxy.DefineField(_callback, typeof(string), FieldAttributes.Public);


				var proxy_Invoke = proxy.DefineMethod(_Invoke, MethodAttributes.Public, TypeCache[item.Invoke.ReturnType], item.Invoke.GetParameterTypes().Select(k => TypeCache[k]).ToArray());

				item.Invoke.GetParameters().CopyTo(proxy_Invoke);


				{

					var il = proxy_Invoke.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, proxy_context);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, proxy_callback);

					foreach (var p in item.Invoke.GetParameters())
					{
						// can we get the remote this/callback or do we need to generate one?
						il.Emit(OpCodes.Ldnull);
					}

					il.Emit(OpCodes.Call, __out[item.Invoke]);
					il.Emit(OpCodes.Ret);
				}
				#region ToType
				var proxy_ToType = proxy.DefineMethod(
					_ToType,
					MethodAttributes.Static | MethodAttributes.Public,
					TypeCache[item.k],
					new[] { TypeCache[source], typeof(string) }
				);

				proxy_ToType.DefineParameter(1, ParameterAttributes.None, _context);
				proxy_ToType.DefineParameter(2, ParameterAttributes.None, _callback);

				{
					var il = proxy_ToType.GetILGenerator();

					var loc = il.DeclareLocal(proxy);

					il.Emit(OpCodes.Newobj, proxy_ctor);
					il.Emit(OpCodes.Stloc_0);

					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Stfld, proxy_context);

					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Stfld, proxy_callback);

					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ldftn, proxy_Invoke);
					il.Emit(OpCodes.Newobj, r.RewriteArguments.context.ConstructorCache[item.k.GetConstructors().Single()]);
					il.Emit(OpCodes.Ret);
				}
				#endregion

				__in_ToType[item.k] = proxy_ToType;

				proxy.CreateType();
			}
			#endregion

			// delegates!!

			// string to interface/delegate conversion!

			#region __in_Method
			foreach (var item in MethodsIncoming)
			{
				var ParameterTypes = item.k.GetParameterTypes().Select((k, i) => new { k, i }).ToArray();

				var mParameters = Enumerable.Range(0, item.k.GetParameters().Length + (item.k.DeclaringType.IsInterface ? 1 : 0)).Select(k => typeof(string)).ToArray();

				var m = a.Type.DefineMethod(
					__in_Method + item.i, MethodAttributes.Public | MethodAttributes.Final,

					typeof(void) == item.k.ReturnType ? typeof(void) : typeof(string),

					mParameters
				);

				m.DefineAttribute(
					new ObfuscationAttribute
					{
						Feature = "in method: " + item.k.DeclaringType.FullName + "." + item.k.Name
					}
					,
					typeof(ObfuscationAttribute)
				);


				if (item.k.DeclaringType.IsInterface)
					m.DefineParameter(1, ParameterAttributes.None, _this);

				foreach (var p in item.k.GetParameters())
				{
					m.DefineParameter(p.Position + (item.k.DeclaringType.IsInterface ? 2 : 1), ParameterAttributes.None, p.Name);
				}

				if (ExternalCallback != null)
					ExternalCallback(new MethodBuilderInfo { Method = m, Parameters = mParameters });

				var il = m.GetILGenerator();

				if (item.k.DeclaringType == source)
				{
					// we should rewire old implementation to here...

					#region local
					m.DefineAttribute(
						new ObfuscationAttribute
						{
							Feature = "in method: local" // delegate or interface
						}
						,
						typeof(ObfuscationAttribute)
					);

					il.Emit(OpCodes.Ldarg_0);


					foreach (var p in ParameterTypes)
					{
						if (p.k.IsDelegate() || p.k.IsInterface)
						{
							il.Emit(OpCodes.Ldarg_0);
						}

						il.Emit(OpCodes.Ldarg, (short)(p.i + 1));

						if (p.k.IsDelegate() || p.k.IsInterface)
						{
							il.Emit(OpCodes.Call, __in_ToType[p.k]);
						}
					}

					il.Emit(OpCodes.Call, MethodCache[item.k]);
					il.Emit(OpCodes.Ret);
					#endregion

					continue;
				}

				il.EmitCode(() => { throw new NotImplementedException(); });
			}
			#endregion

		}

		public static MethodInfo[] GetExternalInterfaceMethodsFromType(Type source)
		{
			var _Distinct = new List<Type>();

			Func<Type, bool> _DistinctFilter =
				t =>
				{
					if (_Distinct.Contains(t))
						return false;

					_Distinct.Add(t);

					return true;
				};

			return Enumerable.ToArray(
						from m in source.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)

						// we should block overriding methods like applet.init but allow interface methods...
						where !m.IsVirtualMethod()

						let dependencies = GetExternalInterfaceMethods(m, _DistinctFilter).ToArray()

						// we need to check dependencies
						where dependencies.All(k => k.GetSignatureTypes().All(IsTypeSupportedForExternalInterface))

						select dependencies
					).SelectMany(k => k).Distinct().ToArray();
		}

		internal static IEnumerable<MethodInfo> GetExternalInterfaceMethods(MethodInfo m, Func<Type, bool> _DistinctFilter)
		{
			// start with the actual method
			yield return m;


			// then all the methods found in interfaces
			foreach (var item in m.GetSignatureTypes().Where(k => k.IsInterface).Where(_DistinctFilter).SelectMany<Type, MethodInfo>(t => t.GetMethods().SelectMany<MethodInfo, MethodInfo>(mm => GetExternalInterfaceMethods(mm, _DistinctFilter))))
			{
				yield return item;
			}
		}


		private static bool IsTypeSupportedForExternalInterface(Type t)
		{
			// hard to not support void :D
			if (t == typeof(void))
				return true;

			if (t == typeof(string))
				return true;

			if (t.IsDelegate())
				return true;

			if (t.IsInterface)
			{
				// fixme: is the interface defined java/actionscript only? then no!

				return true;
			}

			return false;
		}



		public class ExternalInterfaceProvider
		{
			// the method above shall be refactored here

		}

		public class ExternalInterfaceConsumer
		{
			public Type SourceType;

			public MethodInfo[] SourceTypeMethods;

			// this is our Sprite or Applet
			public TypeBuilder DeclaringType;

			// this is the method we shall call in the ctor...
			public MethodBuilder __out_Method_init;

			// we need to resolve merged and rewritten types
			public RewriteToAssembly.AssemblyRewriteArguments RewriteArguments;
			public RewriteToAssembly Rewrite;

			public class DefineMethodArguments
			{
				public MethodBuilder Method;

				public string LocalName;
				public string RemoteName;
				public Type ReturnType;
				public Type[] ParameterTypes;

				public TypeBuilder DeclaringType;
				public FieldBuilder DeclaringTypeContext;

			}

			public Func<DefineMethodArguments, MethodBuilder> DefineMethod;


			public readonly Dictionary<MethodInfo, MethodBuilder> OutgoingMethodCache = new Dictionary<MethodInfo, MethodBuilder>();

			public FieldBuilder OutgoingInterfaceField;
			public FieldBuilder OutgoingDelayedField;
			public FieldBuilder OutgoingDirectField;
			public TypeBuilder OutgoingInterfaceType;
			public TypeBuilder OutgoingDelayedType;
			public TypeBuilder OutgoingDirectType;

			public Dictionary<Type, MethodBuilder> __proxy_ToType = new Dictionary<Type, MethodBuilder>();
			public Dictionary<Type, MethodBuilder> __proxy_FromType = new Dictionary<Type, MethodBuilder>();


			public void Implement()
			{

				var TypeCache = this.RewriteArguments.context.TypeCache;
				var MethodCache = this.RewriteArguments.context.MethodCache;


				this.__out_Method_init = DeclaringType.DefineMethod(RewriteToJavaScriptDocument.__out_Method + "_init", MethodAttributes.Private, CallingConventions.Standard, typeof(void), new Type[0]);
				var __out_Method_tick = DeclaringType.DefineMethod(RewriteToJavaScriptDocument.__out_Method + "_tick", MethodAttributes.Private, CallingConventions.Standard, typeof(void), new Type[0]);

				// first we need to export our callbacks
				// then we need to start polling
				// in level1 we can assume there aren't any



				this.OutgoingInterfaceType = this.DeclaringType.DefineNestedType(
					RewriteToJavaScriptDocument.__out_Method + "InterfaceType",
					TypeAttributes.NestedPublic | TypeAttributes.Abstract | TypeAttributes.Interface, null);

				this.OutgoingDelayedType = this.DeclaringType.DefineNestedType(RewriteToJavaScriptDocument.__out_Method + "DelayedType", TypeAttributes.NestedPublic | TypeAttributes.Sealed, null, new Type[] { 
					this.OutgoingInterfaceType 
				});
				//this.OutgoingDelayedType.AddInterfaceImplementation(this.OutgoingInterfaceType);
				this.OutgoingDirectType = this.DeclaringType.DefineNestedType(RewriteToJavaScriptDocument.__out_Method + "DirectType", TypeAttributes.NestedPublic | TypeAttributes.Sealed, null, new Type[] { 
					this.OutgoingInterfaceType 
				});
				//this.OutgoingDirectType.AddInterfaceImplementation(this.OutgoingInterfaceType);

				var OutgoingDelayedTypeConstructor = this.OutgoingDelayedType.DefineDefaultConstructor(MethodAttributes.Public);
				var OutgoingDirectTypeConstructor = this.OutgoingDirectType.DefineDefaultConstructor(MethodAttributes.Public);


				var OutgoingDelayedTypeContext = this.OutgoingDelayedType.DefineField("Context", TypeCache[DeclaringType], FieldAttributes.Public);
				var OutgoingDirectTypeContext = this.OutgoingDirectType.DefineField("Context", TypeCache[DeclaringType], FieldAttributes.Public);


				this.OutgoingInterfaceField = this.DeclaringType.DefineField(RewriteToJavaScriptDocument.__out_Method + "Interface", this.OutgoingInterfaceType, FieldAttributes.Public);
				this.OutgoingDelayedField = this.DeclaringType.DefineField(RewriteToJavaScriptDocument.__out_Method + "Delayed", this.OutgoingDelayedType, FieldAttributes.Public);
				this.OutgoingDirectField = this.DeclaringType.DefineField(RewriteToJavaScriptDocument.__out_Method + "Direct", this.OutgoingDirectType, FieldAttributes.Public);


				var Retry = RewriteArguments.context.MethodCache[((Action<Action>)__InternalElementProxy.Retry).Method];


				var x = GetExternalInterfaceMethodsFromType(SourceType);

				var Interfaces = x.Select(k => k.DeclaringType).Where(k => k.IsInterface).Distinct().Select((k, i) => new { k, i }).ToArray();
				var Delegates = x.SelectMany(k => k.GetSignatureTypes()).Where(k => k.IsDelegate()).Distinct().Select((k, i) => new { k, i, Invoke = k.GetMethod("Invoke") }).ToArray();

				var Methods = Interfaces.SelectMany(k => k.k.GetMethods()).Concat(Delegates.Select(k => k.Invoke)).Select((k, i) => new { k, i }).ToArray();
				var MethodsLocal = x.Where(k => k.DeclaringType == SourceType).Select((k, i) => new { k, i }).ToArray();
				var MethodsOutgoing = Methods.Concat(MethodsLocal).Select((k, i) => new { k.k, i }).ToArray();

				SourceTypeMethods = MethodsLocal.Select(k => k.k).ToArray();


				var __out_Method_Parameters = Enumerable.Range(0, Methods.Length).Select(k => typeof(string)).ToArray();

				#region meta __out_method
				var __out_Method = this.DefineMethod(
					new DefineMethodArguments
					{
						Method = this.OutgoingDirectType.DefineMethod(
							RewriteToJavaScriptDocument.__out_Method,
							MethodAttributes.Public,
							CallingConventions.Standard,
							typeof(void),
							__out_Method_Parameters
						),

						LocalName = RewriteToJavaScriptDocument.__out_Method,
						RemoteName = RewriteToJavaScriptDocument.__in_Method,
						ReturnType = typeof(void),
						ParameterTypes = __out_Method_Parameters,

						DeclaringType = this.OutgoingDirectType,
						DeclaringTypeContext = OutgoingDirectTypeContext
					}
				);
				#endregion

				Action InvokeLater = delegate { };


				#region Interfaces
				foreach (var item in Interfaces)
				{
					var proxy = this.DeclaringType.DefineNestedType(
						__proxy_Interface + item.i,
						TypeAttributes.Sealed | TypeAttributes.Class | TypeAttributes.NestedPublic,
						null,
						new Type[] {
							TypeCache[item.k] 
						}
					);

					var proxy_ctor = proxy.DefineDefaultConstructor(MethodAttributes.Public);


					var proxy_lookup_Type = TypeCache[typeof(InternalLookup._Consumer)];

					var proxy_lookup = this.DeclaringType.DefineField(
						__proxy_Interface + item.i + "lookup",

						proxy_lookup_Type,

						FieldAttributes.Public
					);


					
					var proxy_context = proxy.DefineField(_context, this.DeclaringType, FieldAttributes.Public);
					var proxy_this = proxy.DefineField(_this, typeof(string), FieldAttributes.Public);
					
					
					#region ToType
					
					var proxy_ToType = proxy.DefineMethod(
						_ToType,
						MethodAttributes.Static | MethodAttributes.Public,
						TypeCache[item.k],
						new[] { this.DeclaringType, typeof(string) }
					);

					proxy_ToType.DefineParameter(1, ParameterAttributes.None, _context);
					proxy_ToType.DefineParameter(2, ParameterAttributes.None, _this);

					__proxy_ToType[item.k] = proxy_ToType;

					{



						var il = proxy_ToType.GetILGenerator();
						var il_a = new jsc.Languages.IL.ILTranslationExtensions.EmitToArguments
						{
							TranslateTargetMethod = MethodCache,

							TranslateTargetConstructor =
								ctor =>
								{
									if (ctor.DeclaringType == typeof(InternalToTypeReturnTypeImplementation))
										return proxy_ctor;

									return this.RewriteArguments.context.ConstructorCache[ctor];
								},


							TranslateTargetType =
								tt =>
								{
									if (tt == typeof(InternalToTypeContext))
										return this.DeclaringType;

									if (tt == typeof(InternalToTypeReturnType))
										return TypeCache[item.k];

									if (tt == typeof(InternalToTypeReturnTypeImplementation))
										return proxy;

									return TypeCache[tt];
								},

							TranslateTargetField =
								ff =>
								{
									if (ff.DeclaringType == typeof(InternalToTypeReturnTypeImplementation))
									{
										if (ff.Name == _context)
											return proxy_context;

										if (ff.Name == _this)
											return proxy_this;

									}

									if (ff.DeclaringType == typeof(InternalToTypeContext))
									{
										if (ff.Name == "lookup")
											return proxy_lookup;
									}

									throw new NotSupportedException();
								}

						};

						((Func<InternalToTypeContext, string, InternalToTypeReturnType>)InternalToType.ToType).Method.EmitTo(il, il_a);





					}
					
					#endregion

					#region FromType
					
					var proxy_FromType = proxy.DefineMethod(
						_FromType,
						MethodAttributes.Static | MethodAttributes.Public,
						typeof(string),
						new[] { this.DeclaringType, TypeCache[item.k] }
					);

					proxy_FromType.DefineParameter(1, ParameterAttributes.None, _context);
					proxy_FromType.DefineParameter(2, ParameterAttributes.None, _this);

					__proxy_FromType[item.k] = proxy_FromType;

					{
						var il = proxy_FromType.GetILGenerator();


						#region LazyConstructor
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, proxy_lookup);
						il.Emit(OpCodes.Call, MethodCache[((Func<InternalLookup._Consumer, InternalLookup._Consumer>)InternalLookup._Consumer.LazyConstructor).Method]);
						il.Emit(OpCodes.Stfld, proxy_lookup);
						#endregion

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, proxy_lookup);

						il.Emit(OpCodes.Ldarg_1);
						il.Emit(OpCodes.Call, MethodCache[((Func<InternalLookup._Consumer, object, string>)InternalLookup.FromType).Method]);

						il.Emit(OpCodes.Ret);

					}
					#endregion

					#region Methods
					foreach (var m_ in item.k.GetMethods())
					{
						var m = m_;

						var proxy_method = proxy.DefineMethod(
							m.Name,
							m.Attributes & ~MethodAttributes.Abstract,
							m.CallingConvention,
							TypeCache[m.ReturnType],
							m.GetParameterTypes().Select(k => TypeCache[k]).ToArray()
						);

						var il = proxy_method.GetILGenerator();

						InvokeLater +=
							delegate
							{
								if (m.ReturnType.IsDelegate() || m.ReturnType.IsInterface)
								{
									il.Emit(OpCodes.Ldarg_0);
									il.Emit(OpCodes.Ldfld, proxy_context);
								}


								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Ldfld, proxy_context);
								il.Emit(OpCodes.Ldfld, this.OutgoingInterfaceField);

								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Ldfld, proxy_this);

								foreach (var p in m.GetParameters())
								{
									// can we get the remote this/callback or do we need to generate one?
									il.Emit(OpCodes.Ldnull);
								}

								il.Emit(OpCodes.Call, this.OutgoingMethodCache[m]);



								if (m.ReturnType.IsDelegate() || m.ReturnType.IsInterface)
								{
									il.Emit(OpCodes.Call, __proxy_ToType[m.ReturnType]);
								}

								il.Emit(OpCodes.Ret);
							};
					}
					
					#endregion



					this.NestedTypesCreated +=
						delegate
						{
							proxy.CreateType();
						};
				}


				#endregion



				#region __in_Method

				var Exports = new Dictionary<MethodInfo, MethodBuilder>();
				var ExportDelegates = new Dictionary<MethodInfo, ConstructorInfo>();
				var ExportFields = new Dictionary<MethodInfo, FieldInfo>();

				foreach (var item in Methods)
				{
					var m = this.DeclaringType.DefineMethod(
						RewriteToJavaScriptDocument.__in_Method + item.i,
						MethodAttributes.Public,
						CallingConventions.Standard,
							item.k.ReturnType == typeof(void) ? typeof(void) : typeof(string),
						Enumerable.Range(0, item.k.GetParameters().Length + 1).Select(k => typeof(string)).ToArray()
					);

					Exports[item.k] = m;

					#region parameter names
					m.DefineParameter(1, ParameterAttributes.None,
						item.k.DeclaringType.IsInterface ? _this : _callback
					);

					foreach (var p in item.k.GetParameters().Select((k, i) => new { k, i }))
					{
						m.DefineParameter(p.i + 2, ParameterAttributes.None, p.k.Name);
					}
					#endregion


					m.DefineAttribute(
						new ObfuscationAttribute
						{
							Feature = "in method: " + item.k.DeclaringType.FullName + "." + item.k.Name
						}
						,
						typeof(ObfuscationAttribute)
					);

					// we need to find the interface/delegate mentioned by token

					var il = m.GetILGenerator();

					if (this.__proxy_ToType.ContainsKey(item.k.DeclaringType))
					{
						var __proxy_ToType = this.__proxy_ToType[item.k.DeclaringType];

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldarg_1);

						il.Emit(OpCodes.Call, __proxy_ToType);

						foreach (var p in item.k.GetParameters())
						{
							il.Emit(OpCodes.Ldarg, (short)(p.Position + 2));
						}

						il.Emit(OpCodes.Call, MethodCache[item.k]);

						il.Emit(OpCodes.Ret);
					}
					else
					{
						il.EmitCode(() => { throw new NotImplementedException(); });

					}


					var __Delegate_ctor = DefineAnonymousDelegate(DeclaringType,
						new MethodBuilderInfo
						{
							Method = m,
							Parameters = item.k.GetParameterTypes()
						}
					);

					ExportDelegates[item.k] = __Delegate_ctor;

					ExportFields[item.k] = DeclaringType.DefineField(
						RewriteToJavaScriptDocument.__out_Method + item.i + _callback,
						typeof(string), FieldAttributes.Public
					);

				}
				#endregion

				#region __out_Method_init
				{
					var il = this.__out_Method_init.GetILGenerator();

					var loc0 = il.DeclareInitializedLocal(this.OutgoingDelayedType, OutgoingDelayedTypeConstructor);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldloc, (short)loc0.LocalIndex);
					il.Emit(OpCodes.Stfld, this.OutgoingDelayedField);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, this.OutgoingDelayedField);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Stfld, OutgoingDelayedTypeContext);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, this.OutgoingDelayedField);
					il.Emit(OpCodes.Stfld, this.OutgoingInterfaceField);


					var loc1 = il.DeclareInitializedLocal(this.OutgoingDirectType, OutgoingDirectTypeConstructor);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldloc, (short)loc1.LocalIndex);
					il.Emit(OpCodes.Stfld, this.OutgoingDirectField);


					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, this.OutgoingDirectField);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Stfld, OutgoingDirectTypeContext);

					foreach (var item in Methods)
					{
						il.Emit(OpCodes.Ldarg_0);

						il.Emit(OpCodes.Ldarg_0);

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldftn, Exports[item.k]);
						il.Emit(OpCodes.Newobj, ExportDelegates[item.k]);

						il.Emit(OpCodes.Call, MethodCache[((Func<__InternalElementProxy, Delegate, string>)__InternalElementProxy.__ExportDelegate).Method]);

						il.Emit(OpCodes.Stfld, ExportFields[item.k]);
						// we should actually export our instance methods
					}

					// init implementations!

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldftn, __out_Method_tick);
					il.Emit(OpCodes.Newobj, typeof(Action).GetConstructors().Single());

					il.Emit(OpCodes.Call, Retry);

					il.Emit(OpCodes.Ret);
				}
				#endregion


				#region __out_Method_tick
				{
					var il = __out_Method_tick.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);

					// load callback methods here
					il.Emit(OpCodes.Ldfld, this.OutgoingDirectField);

					foreach (var item in Methods)
					{
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, ExportFields[item.k]);

						// we should actually export our instance methods
					}

					il.Emit(OpCodes.Call, __out_Method);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, this.OutgoingDirectField);
					il.Emit(OpCodes.Stfld, this.OutgoingInterfaceField);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Call, MethodCache[((Action<__InternalElementProxy>)__InternalElementProxy.__SetElementLoaded).Method]);

					il.Emit(OpCodes.Ret);
				}
				#endregion




				#region __out_Method
				foreach (var item in MethodsOutgoing)
				{
					// interfaces and Callbacks should have an additional argument!

					var _DirectReturnType = item.k.ReturnType == typeof(void) ? typeof(void) : typeof(string);
					var _DirectParameters = Enumerable.Range(0,
						((item.k.DeclaringType.IsInterface || item.k.DeclaringType.IsDelegate()) ? 1 : 0)
						+ item.k.GetParameters().Length
					).Select(k => typeof(string)).ToArray();


					var _Direct = DefineMethod(

						new DefineMethodArguments
						{
							Method = this.OutgoingDirectType.DefineMethod(
								RewriteToJavaScriptDocument.__out_Method + item.i,

								MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.NewSlot,

								CallingConventions.Standard,
								_DirectReturnType,
								_DirectParameters
							),


							LocalName = RewriteToJavaScriptDocument.__out_Method + item.i,
							RemoteName = RewriteToJavaScriptDocument.__in_Method + item.i,
							ReturnType = _DirectReturnType,
							ParameterTypes = _DirectParameters,

							DeclaringType = this.OutgoingDirectType,
							DeclaringTypeContext = OutgoingDirectTypeContext
						}
					);



					var _Interface = this.OutgoingInterfaceType.DefineMethod(
						RewriteToJavaScriptDocument.__out_Method + item.i,
						MethodAttributes.Abstract | MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot,
						CallingConventions.Standard,
						item.k.ReturnType == typeof(void) ? typeof(void) : typeof(string),
						_DirectParameters
					);





					// Delayed implementation should be in a different interface
					// once we are loaded we ditch the delayed interface!

					#region _Delayed
					var _Delayed = this.OutgoingDelayedType.DefineMethod(
						RewriteToJavaScriptDocument.__out_Method + item.i,
						MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.NewSlot,
						CallingConventions.Standard,
						item.k.ReturnType == typeof(void) ? typeof(void) : typeof(string),
						_DirectParameters
					);

					if (item.k.ReturnType == typeof(void))
					{
						#region Closure
						var Closure = this.OutgoingDelayedType.DefineNestedType(RewriteToJavaScriptDocument.__out_MethodClosure + item.i,
							TypeAttributes.NestedPublic, null
						);

						var Closure_ctor = Closure.DefineDefaultConstructor(MethodAttributes.Public);
						var Closure_context = Closure.DefineField(_context, TypeCache[DeclaringType], FieldAttributes.Public);

						var Closure_Invoke = Closure.DefineMethod(
							_Invoke,
							MethodAttributes.Public,
							CallingConventions.Standard,
							typeof(void),
							new Type[0]
						);

						var Closure_Invoke_il = Closure_Invoke.GetILGenerator();

						var il = _Delayed.GetILGenerator();
						var loc0 = il.DeclareInitializedLocal(Closure, Closure_ctor);


						il.Emit(OpCodes.Ldloc, (short)loc0.LocalIndex);
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, OutgoingDelayedTypeContext);
						il.Emit(OpCodes.Stfld, Closure_context);

						Closure_Invoke_il.Emit(OpCodes.Ldarg_0);
						Closure_Invoke_il.Emit(OpCodes.Ldfld, Closure_context);
						Closure_Invoke_il.Emit(OpCodes.Ldfld, OutgoingDirectField);

						foreach (var p in _DirectParameters.Select((k, i) => new { k, i }).ToArray())
						{
							var f = Closure.DefineField("_" + p.i, p.k, FieldAttributes.Public);

							il.Emit(OpCodes.Ldloc, (short)loc0.LocalIndex);
							il.Emit(OpCodes.Ldarg, (short)(1 + p.i));
							il.Emit(OpCodes.Stfld, f);

							Closure_Invoke_il.Emit(OpCodes.Ldarg_0);
							Closure_Invoke_il.Emit(OpCodes.Ldfld, f);

						}

						Closure_Invoke_il.Emit(OpCodes.Call, _Direct);
						Closure_Invoke_il.Emit(OpCodes.Ret);

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, OutgoingDelayedTypeContext);

						il.Emit(OpCodes.Ldloc, (short)loc0.LocalIndex);
						il.Emit(OpCodes.Ldftn, Closure_Invoke);
						il.Emit(OpCodes.Newobj, typeof(Action).GetConstructors().Single());

						il.Emit(OpCodes.Call, MethodCache[((Action<__InternalElementProxy, Action>)__InternalElementProxy.__AfterElementLoaded).Method]);
						il.Emit(OpCodes.Ret);

						Closure.CreateType();
						#endregion

					}
					else
					{
						_Delayed.GetILGenerator().EmitCode(() => { throw new NotSupportedException(); });
					}
					#endregion



					// on top of that we need one to translate to strings...

					_Direct.DefineAttribute(
						new ObfuscationAttribute
						{
							Feature = "out method: " + item.k.DeclaringType.FullName + "." + item.k.Name
						}
						,
						typeof(ObfuscationAttribute)
					);

					_Interface.DefineAttribute(
						new ObfuscationAttribute
						{
							Feature = "out method: " + item.k.DeclaringType.FullName + "." + item.k.Name
						}
						,
						typeof(ObfuscationAttribute)
					);


					OutgoingMethodCache[item.k] = _Interface;
				}
				#endregion

				InvokeLater();

				this.NestedTypesCreated +=
					delegate
					{
						// important!
						// if we implement an interface which is a nested type
						// then the declaring types need call CreateType first!
						// the rewriter should also remember this rule!

						// BCL should provide .CreateTypeWheneverYouFeelLike();

						this.OutgoingInterfaceType.CreateType();
						this.OutgoingDelayedType.CreateType();
						this.OutgoingDirectType.CreateType();
					};


			}

			public Action NestedTypesCreated;
		}


	}

	namespace Templates
	{
		[Obfuscation(Feature = "invalidmerge")]
		internal class InternalToTypeReturnTypeImplementation : InternalToTypeReturnType
		{
			public InternalToTypeContext context;
			public string _this;
		}

		[Obfuscation(Feature = "invalidmerge")]
		internal interface InternalToTypeReturnType
		{

		}

		[Obfuscation(Feature = "invalidmerge")]
		internal class InternalToTypeContext
		{
			public InternalLookup._Consumer lookup;
		}

		internal class InternalToType
		{

			public static InternalToTypeReturnType ToType(InternalToTypeContext context, string _this)
			{
				context.lookup = InternalLookup._Consumer.LazyConstructor(context.lookup);

				var local = (InternalToTypeReturnType)InternalLookup.ToType(context.lookup, _this);

				if (local != null)
					return local;

				return new InternalToTypeReturnTypeImplementation { context = context, _this = _this };
			}


		}
	}
}
