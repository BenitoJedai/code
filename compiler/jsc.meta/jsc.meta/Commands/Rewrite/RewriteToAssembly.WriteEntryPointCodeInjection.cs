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
		private void WriteEntryPointCodeInjection(
	AssemblyBuilder a,
	ModuleBuilder m,
	ILGenerator kmil,
	TypeBuilder t,
	VirtualDictionary<Type, Type> tc,
	VirtualDictionary<MethodInfo, MethodInfo> mc,
	VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache,
	VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
	VirtualDictionary<MethodInfo, MethodInfo> MethodCache)
		{
			var codeinjectonparams = this.codeinjectonparams(this._assembly);

			// we need to actually track the method being called (IL)
			// and the variables to make them literal constants

			// we should copy all relevant types too...
			// we have some dependency issues! need to fix them!
			foreach (var k in from p in this.codeinjecton.Method.GetParameters()
							  let ParameterType = p.ParameterType
							  where ParameterType.Assembly == this.codeinjecton.Method.DeclaringType.Assembly
							  select ParameterType
							  )
			{
				CopyType(k, a, m, tc, TypeFieldCache, ConstructorCache, MethodCache, t);
			}


			CopyType(this.codeinjecton.Method.DeclaringType, a, m, tc, TypeFieldCache, ConstructorCache, MethodCache, t);



			//Copy(a, this.codeinjecton.Method, t, tc, mc, TypeFieldCache);


			Action<Action> notification =
				_this_codeinjection =>
				{
					Console.WriteLine("this assembly was modified by RewriteToAssembly");

					_this_codeinjection();
				};

			var ea = new ILTranslationExtensions.EmitToArguments();

			// it is not actually an instance method
			// so we cannot load this to call _this_codeinjection
			// it is a static method


			ea[OpCodes.Ret] = delegate { };
			ea[OpCodes.Ldarg_0] = delegate { };


			ea[OpCodes.Callvirt] =
				e =>
				{
					if (e.i.StackBeforeStrict[0].SingleStackInstruction.OpCode == OpCodes.Ldarg_0)
					{
						// we are calling a method on this
						// we assume it is _this_codeinjection

						if (this.codeinjecton.Method.GetParameters().Length != codeinjectonparams.Length)
							throw new InvalidDataException("codeinjectonparams");

						foreach (var p in codeinjectonparams)
						{
							if (p is string)
							{
								e.il.Emit(OpCodes.Ldstr, (string)p);
							}
							else if (p is object)
							{
								// the object constructor better be there
								// can we serialize this object into IL?
								e.il.EmitInstance(p,
									new ILTranslationContext
									{
										ConstructorCache = ConstructorCache,
										MethodCache = MethodCache,
										TypeCache = tc,
										TypeFieldCache = TypeFieldCache
									}
								);
							}
							else throw new NotSupportedException();
						}

						e.il.Emit(OpCodes.Call, mc[this.codeinjecton.Method]);

						return;
					}

					// rewire this.Invoke to codeinjection.Method
					e.Default();
				};

			notification.EmitTo(kmil, ea);
		}


	}
}
