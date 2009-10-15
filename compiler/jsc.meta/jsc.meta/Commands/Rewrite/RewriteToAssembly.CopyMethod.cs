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

			if (source.GetMethodBody() == null)
				return;

			MethodBase mb = source;

			var kmil = km.GetILGenerator();

	
			if (source == this._assembly.EntryPoint)
			{
				// we found the entrypoint
				if (this.codeinjecton != null)
				{
					WriteEntryPointCodeInjection(a, m, kmil, t, tc, mc, TypeFieldCache, ConstructorCache, MethodCache);
				}

				a.SetEntryPoint(km);
			}

			mb.EmitTo(kmil,
				new ILTranslationExtensions.EmitToArguments
				{
					// we need to redirect any typerefs and methodrefs!
					TranslateTargetType = TargetType => tc[TargetType],
					TranslateTargetField = TargetField => TypeFieldCache[TargetField.DeclaringType].SingleOrDefault(k => k.Name == NameObfuscation[TargetField.Name]) ?? TargetField,
					TranslateTargetMethod = TargetMethod => MethodCache[TargetMethod],
					TranslateTargetConstructor = TargetConstructor => ConstructorCache[TargetConstructor],
				}
			);

		
			// we need to emit the try/catch blocks too!

		}



	}
}
