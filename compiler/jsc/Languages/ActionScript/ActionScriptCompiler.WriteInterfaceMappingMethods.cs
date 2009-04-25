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

namespace jsc.Languages.ActionScript
{
	partial class ActionScriptCompiler
    {
		private void WriteInterfaceMappingMethods(Type z)
		{
			DebugBreak(z.ToScriptAttribute());

			// current interface exclusion implementation might not work well with abstract classes 

			Action<MethodInfo, MethodInfo, MethodInfo> WriteInterfaceMapping =
				(_InterfaceMethod, _ParamSignature, _TargetMethod) =>
				{
					DebugBreak(_TargetMethod.ToScriptAttributeOrDefault());

					var BaseMethod = z.BaseType.GetMethod(_InterfaceMethod);

					WriteMethodSignature(_InterfaceMethod, false,
						(BaseMethod != null && BaseMethod.IsAbstract) ?
						WriteMethodSignatureMode.OverridingImplementing :
						WriteMethodSignatureMode.Implementing
						, null, null, _ParamSignature);

					using (CreateScope())
					{
						WriteIdent();

						if (_InterfaceMethod.ReturnType != typeof(void))
						{
							WriteKeywordReturn();
							WriteSpace();
						}

						WriteThisReference();
						Write(".");

						#region prop
						{
							var prop = new PropertyDetector(_TargetMethod);

							if (_InterfaceMethod.GetParameters().Length == 1 && prop.SetProperty != null
								 && prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1)
							{
								Write(prop.SetProperty.Name);
								WriteAssignment();
								WriteSafeLiteral(_InterfaceMethod.GetParameters().Single().Name);
							}
							else if (prop.GetProperty != null
								  && prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0)
							{
								Write(prop.GetProperty.Name);
							}
							else
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
									WriteSafeLiteral(_InterfaceMethod.GetParameters()[i].Name);
								}
								Write(")");
							}
						}
						#endregion

						Write(";");
						WriteLine();
					}
				};



			WriteIdent();
			WriteCommentLine(DateTime.Now.ToString());

			foreach (var i in z.GetInterfaces())
			{
				WriteIdent();
				WriteCommentLine("interface " + i.Namespace + "::" + i.Name);

				var mapping = z.GetInterfaceMap(i);

				WriteIdent();
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
						WriteIdent();

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
									WriteIdent();

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
