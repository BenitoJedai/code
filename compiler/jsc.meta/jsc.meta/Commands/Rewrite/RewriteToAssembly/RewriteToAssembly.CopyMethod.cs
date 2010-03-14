﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;

namespace jsc.meta.Commands.Rewrite
{
	public partial class RewriteToAssembly
	{
		internal static void CopyMethod(
			AssemblyBuilder a,
			ModuleBuilder m,
			MethodInfo source,
			TypeBuilder t,
			VirtualDictionary<string, string> NameObfuscation,
			Assembly PrimarySourceAssembly,
			Delegate codeinjecton,
			Func<Assembly, object[]> codeinjectonparams,


			Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,

			Action<MethodInfo, MethodBuilder, Func<ILGenerator>> BeforeInstructions,
			ILTranslationContext context
			)
		{
			// sanity check!

			if (context.MethodCache.BaseDictionary.ContainsKey(source))
				return;

			// Unknown runtime implemented delegate method
			// Operation could destabilize the runtime.
			// http://www.fuzzydev.com/blogs/dotnet/archive/2006/06/10/Operation_could_destabilize_the_runtime.aspx

			var MethodName =
				//(source == this._assembly.EntryPoint) ||
				(source.GetMethodBody() == null || (source.Attributes & MethodAttributes.Virtual) == MethodAttributes.Virtual) ?
				source.Name : NameObfuscation[source.Name];

			// !! fixme
			// How to: Define a Generic Method with Reflection Emit
			// http://msdn.microsoft.com/en-us/library/ms228971.aspx
			var Parameters =
				 context.TypeCache[source.GetParameterTypes()];

			if (Parameters.Contains(null))
				throw new InvalidOperationException();


			var km = t.DefineMethod(
				MethodName, source.Attributes, source.CallingConvention,
				context.TypeCache[source.ReturnType],
				Parameters

			);

			context.MethodCache[source] = km;

			//Console.WriteLine("Method: " + km.Name);

			if (source.IsGenericMethodDefinition)
			{
				var ga = source.GetGenericArguments();
				var gp = km.DefineGenericParameters(ga.Select(k => k.Name).ToArray());

			}



			// synchronized?
			km.SetImplementationFlags(source.GetMethodImplementationFlags());

			source.GetParameters().CopyTo(km);

			// should we copy attributes? should they be opt-out?
			foreach (var item in source.GetCustomAttributes(false).Select(kk => kk.ToCustomAttributeBuilder()))
			{
				km.SetCustomAttribute(item(context));
			}

			var MethodBody = source.GetMethodBody();

			if (MethodBody == null)
				return;

			var ExceptionHandlingClauses = MethodBody.ExceptionHandlingClauses.ToArray();



			MethodBase mb = source;

			var kmil = km.GetILGenerator();
			var kmil_Dirty = false;

			if (BeforeInstructions != null)
				BeforeInstructions(source, km,
					delegate
					{
						kmil_Dirty = true;
						return kmil;
					}
				);

			// BeforeInstructions has replaced the IL
			// they may be calling a copy of us?
			if (kmil_Dirty)
				return;

			if (PrimarySourceAssembly != null)
				if (source == PrimarySourceAssembly.EntryPoint)
				{
					// we found the entrypoint
					if (codeinjecton != null)
					{
						WriteEntryPointCodeInjection(
							a, m, kmil, t
							, context.TypeCache,
							context.ConstructorCache,
							context.MethodCache,
							PrimarySourceAssembly,
							codeinjecton,
							codeinjectonparams
							);

						// we have changed the IL offsets!
					}

					a.SetEntryPoint(km);
				}

			var x = CreateMethodBaseEmitToArguments(
				source,

				NameObfuscation,
				ILOverride,
				ExceptionHandlingClauses,
				context
			);



			mb.EmitTo(kmil, x);


			// we need to emit the try/catch blocks too!

		}

		public static ILTranslationExtensions.EmitToArguments CreateMethodBaseEmitToArguments(
			MethodBase SourceMethod,
			VirtualDictionary<string, string> NameObfuscation,
			Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
			ExceptionHandlingClause[] ExceptionHandlingClauses,
			ILTranslationContext context)
		{
			var x = new ILTranslationExtensions.EmitToArguments
			{
				#region BeforeInstruction
				BeforeInstruction =
					e =>
					{

						foreach (var ex in ExceptionHandlingClauses)
						{
							if (ex.TryOffset == e.i.Offset)
							{
								//Console.WriteLine(".try");
								e.il.BeginExceptionBlock();

							}

							if (ex.HandlerOffset == e.i.Offset)
							{
								// http://blogs.msdn.com/clrteam/archive/2009/03/23/exceptions-out-of-fault-finally.aspx

								if (ex.Flags == ExceptionHandlingClauseOptions.Finally)
								{
									//Console.WriteLine(".finally");
									e.il.BeginFinallyBlock();


								}
								else if (ex.Flags == ExceptionHandlingClauseOptions.Clause)
								{

									//Console.WriteLine(".catch");
									e.il.BeginCatchBlock(context.TypeCache[ex.CatchType]);


								}
								else if (ex.Flags == ExceptionHandlingClauseOptions.Fault)
								{

									//Console.WriteLine(".catch");
									e.il.BeginFaultBlock();


								}
								else
								{
									throw new NotImplementedException();
								}
							}
						}

						foreach (var ex in ExceptionHandlingClauses)
						{


							if ((ex.Flags & ExceptionHandlingClauseOptions.Finally) == ExceptionHandlingClauseOptions.Finally)
							{

								if ((ex.HandlerOffset + ex.HandlerLength) == e.i.Offset)
								{

									//Console.WriteLine(".endfinally");
									e.il.EndExceptionBlock();
								}
							}
							else
							{

								if ((ex.HandlerOffset + ex.HandlerLength) == e.i.Offset)
								{
									//Console.WriteLine(".endcatch");
									e.il.EndExceptionBlock();

								}
							}
						}
					},
				#endregion


				AfterInstruction =
					e =>
					{


					},

				// we need to redirect any typerefs and methodrefs!
				


				TranslateTargetType = context.TypeCache,
				TranslateTargetField = context.FieldCache,
				TranslateTargetMethod = context.MethodCache,
				TranslateTargetConstructor = context.ConstructorCache,
			};

			x[OpCodes.Endfinally] =
				e =>
				{
				};

			x[OpCodes.Leave_S] =
				e =>
				{
					// see: http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/afc3b34b-1d42-427c-880f-1f6372ed81ca

					// MethodBuilder.Emit is too nice and always writes .leave for us.
					// As such we need not to write this twice
				};

			x[OpCodes.Leave] =
				e =>
				{
					// see: http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/afc3b34b-1d42-427c-880f-1f6372ed81ca

					// MethodBuilder.Emit is too nice and always writes .leave for us.
					// As such we need not to write this twice
				};


			if (ILOverride != null)
				ILOverride(SourceMethod, x);
			return x;
		}



	}
}
