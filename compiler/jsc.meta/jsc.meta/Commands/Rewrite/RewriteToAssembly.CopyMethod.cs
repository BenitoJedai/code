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
		public void CopyMethod(
			AssemblyBuilder a,
			ModuleBuilder m,
			MethodInfo source,
			TypeBuilder t,
			VirtualDictionary<Type, Type> tc,
			VirtualDictionary<MethodInfo, MethodInfo> mc,
			VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache,
			VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
			VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
			VirtualDictionary<string, string> NameObfuscation)
		{
			// sanity check!

			if (mc.BaseDictionary.ContainsKey(source))
				return;

			// Unknown runtime implemented delegate method

			var MethodName =
				//(source == this._assembly.EntryPoint) ||
				(source.GetMethodBody() == null || (source.Attributes & MethodAttributes.Virtual) == MethodAttributes.Virtual) ?
				source.Name : NameObfuscation[source.Name];

			var km = t.DefineMethod(MethodName, source.Attributes, source.CallingConvention, tc[source.ReturnType], source.GetParameters().Select(kp => tc[kp.ParameterType]).ToArray());

			km.SetImplementationFlags(source.GetMethodImplementationFlags());

			mc[source] = km;

			var MethodBody = source.GetMethodBody();

			if (MethodBody == null)
				return;

			var ExceptionHandlingClauses = MethodBody.ExceptionHandlingClauses.ToArray();



			MethodBase mb = source;

			var kmil = km.GetILGenerator();


			if (source == this._assembly.EntryPoint)
			{
				// we found the entrypoint
				if (this.codeinjecton != null)
				{
					WriteEntryPointCodeInjection(a, m, kmil, t, tc, mc, TypeFieldCache, ConstructorCache, MethodCache);

					// we have changed the IL offsets!
				}

				a.SetEntryPoint(km);
			}

			var x = new ILTranslationExtensions.EmitToArguments
			{
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

				AfterInstruction =
					e =>
					{
					

					},

				// we need to redirect any typerefs and methodrefs!
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
				TranslateTargetType = TargetType => tc[TargetType],
				TranslateTargetField = TargetField => TypeFieldCache[TargetField.DeclaringType].SingleOrDefault(k => k.Name == NameObfuscation[TargetField.Name]) ?? TargetField,
				TranslateTargetMethod = TargetMethod => MethodCache[TargetMethod],
				TranslateTargetConstructor = TargetConstructor => ConstructorCache[TargetConstructor],
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

			mb.EmitTo(kmil, x);


			// we need to emit the try/catch blocks too!

		}



	}
}
