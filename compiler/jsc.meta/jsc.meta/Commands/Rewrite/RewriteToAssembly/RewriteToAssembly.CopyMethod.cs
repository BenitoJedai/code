using System;
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
			VirtualDictionary<Type, Type> TypeCache,
				VirtualDictionary<FieldInfo, FieldInfo> FieldCache,
			VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
			VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
			VirtualDictionary<string, string> NameObfuscation,

			Assembly PrimarySourceAssembly,
			Delegate codeinjecton,
			Func<Assembly, object[]> codeinjectonparams,


			Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,

			Action<MethodInfo, MethodBuilder, Func<ILGenerator>> BeforeInstructions

			)
		{
			// sanity check!

			if (MethodCache.BaseDictionary.ContainsKey(source))
				return;

			// Unknown runtime implemented delegate method

			var MethodName =
				//(source == this._assembly.EntryPoint) ||
				(source.GetMethodBody() == null || (source.Attributes & MethodAttributes.Virtual) == MethodAttributes.Virtual) ?
				source.Name : NameObfuscation[source.Name];

			// !! fixme
			// How to: Define a Generic Method with Reflection Emit
			// http://msdn.microsoft.com/en-us/library/ms228971.aspx
			var Parameters =
				 TypeCache[source.GetParameterTypes()];

			if (Parameters.Contains(null))
				throw new InvalidOperationException();

			var km = t.DefineMethod(
				MethodName, source.Attributes, source.CallingConvention, TypeCache[source.ReturnType], Parameters);

			// synchronized?
			km.SetImplementationFlags(source.GetMethodImplementationFlags());

			source.GetParameters().CopyTo(km);

			// should we copy attributes? should they be opt-out?
			var TypeAttributes = source.GetCustomAttributes(false);

			foreach (var item in TypeAttributes)
			{
				// call a callback?
				km.DefineAttribute(item, item.GetType());
			}

			MethodCache[source] = km;

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
							, TypeCache,
							ConstructorCache,
							MethodCache,
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
				TypeCache,
				FieldCache,
				//TypeFieldCache,
				ConstructorCache,
				MethodCache,
				NameObfuscation,
				ILOverride,
				ExceptionHandlingClauses
			);



			mb.EmitTo(kmil, x);


			// we need to emit the try/catch blocks too!

		}

		public static ILTranslationExtensions.EmitToArguments CreateMethodBaseEmitToArguments(
			MethodBase SourceMethod,
			VirtualDictionary<Type, Type> TypeCache,
			VirtualDictionary<FieldInfo, FieldInfo> FieldCache,
			VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
			VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
			VirtualDictionary<string, string> NameObfuscation,
			Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
			ExceptionHandlingClause[] ExceptionHandlingClauses)
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

							if ((ex.Flags & ExceptionHandlingClauseOptions.Finally) == ExceptionHandlingClauseOptions.Finally)
							{
								if (ex.HandlerOffset == e.i.Offset)
								{
									//Console.WriteLine(".finally");
									e.il.BeginFinallyBlock();
								}


							}
							else
							{
								if (ex.HandlerOffset == e.i.Offset)
								{
									//Console.WriteLine(".catch");
									e.il.BeginCatchBlock(ex.CatchType);
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
				#region TranslateBranchOffset
				TranslateBranchOffset =
					(i, o) =>
					{
						Func<ILInstruction, ILInstruction> NextInstruction = ii => o < 0
							? (ii.Offset < (i.Offset + o) ? null : ii.Prev)
							: (ii.Offset >= (i.Offset + o) ? null : ii.Next);

						var Selection = new List<ILInstruction>();

						#region Selection
						var p = NextInstruction(i);
						while (p != null)
						{
							Selection.Add(p);
							p = NextInstruction(p);
						}
						#endregion

						var Leave_s_Count = Selection.Count(k => k.OpCode == OpCodes.Leave_S);
						var Leave_s_Fixup = Leave_s_Count * (Math.Sign(o) * 3);

						//Console.WriteLine(new { i.Location, Leave_s_Count, Leave_s_Fixup });

						return o + Leave_s_Fixup;
					},
				#endregion

				TranslateTargetType = TypeCache,
				TranslateTargetField = FieldCache,
				TranslateTargetMethod = MethodCache,
				TranslateTargetConstructor = ConstructorCache,
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


			if (ILOverride != null)
				ILOverride(SourceMethod, x);
			return x;
		}



	}
}
