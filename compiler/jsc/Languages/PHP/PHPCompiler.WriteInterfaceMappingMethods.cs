using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
	{
		public enum WriteMethodSignatureMode
		{
			Delcaring,
			Implementing,
			Overriding,
			OverridingImplementing,
			ValueTypeConstructorAlias
		}

		private void WriteInterfaceMappingMethods(Type z)
		{
			DebugBreak(z.ToScriptAttribute());

			// current interface exclusion implementation might not work well with abstract classes 

			Action<MethodInfo, MethodInfo, MethodInfo> WriteInterfaceMapping =
				(_InterfaceMethod, _ParamSignature, _TargetMethod) =>
				{
					DebugBreak(_TargetMethod.ToScriptAttributeOrDefault());

					var BaseMethod = z.BaseType.GetMethod(_InterfaceMethod);

					WriteMethodSignature(
						z,
						_InterfaceMethod, false
						,
						(BaseMethod != null && BaseMethod.IsAbstract) ?
						WriteMethodSignatureMode.OverridingImplementing :
						WriteMethodSignatureMode.Implementing
						//, null, null, _ParamSignature
					);

					using (CreateScope())
					{
						WriteIndent();

						if (_InterfaceMethod.ReturnType != typeof(void))
						{
							WriteKeywordReturn();
							WriteSpace();
						}

						Write("$this");
						Write("->");

						#region prop
						{

							WriteDecoratedMethodName(_TargetMethod, false);
							Write("(");
							for (int i = 0; i < _InterfaceMethod.GetParameters().Length; i++)
							{
								if (i > 0)
								{
									Write(",");
									WriteSpace();
								}
								Write("$p" + i);
								//WriteSafeLiteral(_InterfaceMethod.GetParameters()[i].Name);
							}
							Write(")");
						}
						#endregion

						Write(";");
						WriteLine();
					}
				};



			WriteIndent();
			WriteCommentLine(DateTime.Now.ToString());

			foreach (var i in z.GetInterfaces())
			{
				WriteIndent();
				WriteCommentLine("interface " + i.Namespace + "::" + i.Name);

				var mapping = z.GetInterfaceMap(i);

				WriteIndent();
				WriteCommentLine(" mappings:");

				Action WriteInterfaceMappingDelayed = delegate { };

				for (int j = 0; j < mapping.InterfaceMethods.Length; j++)
				{
					var InterfaceMethod = mapping.InterfaceMethods[j];
					var InterfaceMethodDeclaringType =
						mapping.InterfaceType.IsGenericType ?
						mapping.InterfaceType.GetGenericTypeDefinition() :
						mapping.InterfaceType
						;

					var InterfaceMethodImplementation = (MethodInfo)MySession.ResolveImplementation(InterfaceMethodDeclaringType, InterfaceMethod,
						//AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveMethodOnly
						AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveBCLImplementation
						) ?? InterfaceMethod;

					var InterfaceMethodImplementationSignature = (MethodInfo)MySession.ResolveImplementation(InterfaceMethodDeclaringType, InterfaceMethod,
						AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveMethodOnly
						//AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveBCLImplementation
						) ?? InterfaceMethod;

					var TargetMethod = mapping.TargetMethods[j];



					if (TargetMethod.DeclaringType == z)
					{
						WriteIndent();

						if (InterfaceMethodImplementation == null)
						{
							BreakToDebugger("Interface Mapping Error: " +
								InterfaceMethod.DeclaringType.Name + "." + InterfaceMethod.Name +
								" -> " +
								TargetMethod.DeclaringType.Name + "." + TargetMethod.Name);
						}
						else
						{
							WriteCommentLine(" " +
								InterfaceMethod.DeclaringType.Name + "." + InterfaceMethod.Name + " = " +
								InterfaceMethodImplementation.DeclaringType.Name + "." + InterfaceMethodImplementation.Name +
								" -> this." + TargetMethod.Name);

							WriteInterfaceMappingDelayed +=
								delegate
								{
									WriteIndent();

									WriteCommentLine(" " +
										InterfaceMethodImplementation.DeclaringType.Name + "." + InterfaceMethodImplementation.Name + "_" + InterfaceMethodImplementation.MetadataToken
										);

									WriteInterfaceMapping(InterfaceMethodImplementation, InterfaceMethodImplementationSignature, TargetMethod);
								};
						}
					}
				}

				WriteInterfaceMappingDelayed();

				WriteLine();
			}

			WriteLine();
			//}




		}

	}
}
