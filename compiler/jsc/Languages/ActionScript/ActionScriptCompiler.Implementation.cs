using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
	partial class ActionScriptCompiler
	{
		// lets use for instead of while where we can
		public override bool SupportsForStatements
		{
			get { return true; }
		}

		public override bool SupportsInlineAssigments
		{
			get { return true; }
		}

		public override bool SupportsInlineArrayInit
		{
			get { return true; }
		}

		public override bool SupportsInlineThisReference
		{
			get { return true; }
		}

		public override bool SupportsInlineExceptionVariable
		{
			get { return true; }
		}

		public override bool SupportsAbstractMethods
		{
			get { return false; }
		}
		public override bool SupportsCustomArrayEnumerator
		{
			get { return true; }
		}

		public override bool IsUTF8SupportedInLiterals()
		{
			return true;
		}

		public override ScriptCoreLib.ScriptType GetScriptType()
		{
			return ScriptCoreLib.ScriptType.ActionScript;
		}



		private Action WriteTypeStaticConstructor(Type z, ScriptAttribute za)
		{
			var cctor = z.GetStaticConstructor();

			if (cctor == null)
				return null;

			WriteXmlDoc(cctor);
			WriteMethodSignature(z, cctor, false);
			WriteMethodBody(cctor, MethodBodyFilter);
			WriteLine();

			return delegate
			{
				WriteIdent();
				WriteDecoratedTypeName(z);
				Write(".");
				WriteDecoratedMethodName(cctor, false);
				Write("();");
				WriteLine();
			};
		}


		public MethodBase ResolveMethod(MethodBase m)
		{
			return
				(m.DeclaringType.ToScriptAttribute() == null
							? ResolveImplementationMethod(m.DeclaringType, m)
							: m);
		}

		public MethodBase ResolveMethod(Type t, MethodBase m)
		{
			var s = m.DeclaringType.ToScriptAttributeOrDefault();

			return
				(s == null
							? ResolveImplementationMethod(t, m)
							: m);
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

		public override void WriteTypeSignature(Type z, ScriptCoreLib.ScriptAttribute za)
		{
			DebugBreak(za);


			WriteIdent();

			if (ScriptAttribute.IsAnonymousType(z) || (za != null && za.Implements != null))
			{
				// private for developers but public for the runtime

				Write("public ");
			}
			else
			{
				// as3 cannot have assembly private types

				Write("public ");

			}

			if (z.IsSealed)
				Write("final ");

			if (z.IsInterface)
				Write("interface ");
			else
				Write("class ");

			WriteDecoratedTypeName(z);

			if (z.IsClass)
			{
				var BaseTypeImplementation =
					z.BaseType == typeof(object) ? z.BaseType :
					MySession.ResolveImplementation(z.BaseType) ?? z.BaseType;

				#region extends
				if (BaseTypeImplementation != typeof(object) && BaseTypeImplementation != null)
				{
					Write(" extends ");



					ScriptAttribute ba = ScriptAttribute.Of(BaseTypeImplementation, true);

					if (ba == null)
						throw new NotSupportedException("extending object has no attribute");

					if (ba.ImplementationType != null)
					{
						WriteDecoratedTypeNameOrImplementationTypeName(ba.ImplementationType, false, false, IsFullyQualifiedNamesRequired(z, ba.ImplementationType));
					}
					else// if (ba.Implements == null)
						WriteDecoratedTypeNameOrImplementationTypeName(BaseTypeImplementation, false, false, IsFullyQualifiedNamesRequired(z, BaseTypeImplementation));
					//else
					//    Write(GetDecoratedTypeName(BaseTypeImplementation, false));

				}
				#endregion
			}

			#region implements
			var timp = z.GetInterfaces();

			if (timp.Length > 0)
			{
				int i = 0;

				DebugBreak(za);

				foreach (var v in timp)
				{
					var timpv = v;

					// ignore interfaces which are not visible to scripting
					if (timpv.ToScriptAttribute() == null)
					{
						timpv = MySession.ResolveImplementation(timpv);

						if (timpv == null)
							continue;
					}

					if (i++ > 0)
						Write(", ");
					else
					{
						// http://livedocs.adobe.com/specs/actionscript/3/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=as3_specification189.html
						// http://livedocs.adobe.com/specs/actionscript/3/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=as3_specification100.html#wp127562

						if (z.IsInterface)
							Write(" extends ");
						else
							Write(" implements ");
					}

					WriteDecoratedTypeNameOrImplementationTypeName(timpv, false, true, IsFullyQualifiedNamesRequired(z, timpv));
					//WriteDecoratedTypeNameOrImplementationTypeName(timpv);
				}
			}
			#endregion

			WriteLine();
		}










		public override void WriteSelf()
		{
			Write("_this");
		}



		public override Type[] GetActiveTypes()
		{
			throw new NotImplementedException();
		}

		public override Type ResolveImplementation(Type t)
		{
			return MySession.ResolveImplementation(t);
		}

		public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m)
		{
			return MySession.ResolveImplementation(t, m);
		}

		public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m, string alias)
		{
			return MySession.ResolveMethod(m, t, alias);
		}

		public override void WriteTypeConstructionVerified()
		{
			Write("{}");
		}





		public override string GetDecoratedTypeName(Type x, bool bExternalAllowed)
		{
			if (x.IsEnum)
				return "int";

			Func<Type, string> GetShortName =
				z =>
				{
					if (z.IsArray)
						return "Array";

					// convert c# type to actionscript typename literal

					if (NativeTypes.ContainsKey(z))
						return NativeTypes[z];


					return GetSafeLiteral(z.Name);
				};

			var p = x;
			var s = "";

			do
			{
				if (string.IsNullOrEmpty(s))
					s = GetShortName(p);
				else
					s = GetShortName(p) + "_" + s;

				p = p.DeclaringType;
			}
			while (p != null);

			if (ScriptAttribute.IsAnonymousType(x))
			{
				s = GetSafeLiteral(x.Assembly.ManifestModule.ScopeName) + s + ("_" + x.MetadataToken);
			}

			return s;
		}

		public override string GetDecoratedTypeNameWithinNestedName(Type z)
		{
			// used when writing the source file

			return GetDecoratedTypeName(z, false);
		}



		public override void WriteDecoratedMethodName(System.Reflection.MethodBase z, bool q)
		{
			if (q)
				WriteQuote();

			try
			{
				// tostring is a special method
				if (z.Name == "ToString" && z.GetParameters().Length == 0)
				{
					Write("toString");
					return;
				}


				// todo: should use base62 encoding here

				var s = z.DeclaringType.ToScriptAttribute();
				if (s != null && s.IsNative || z.ToScriptAttributeOrDefault().NoDecoration)
					WriteSafeLiteral(z.Name);
				else
					WriteSafeLiteral(z.Name + "_" + z.MetadataToken);

			}
			finally
			{
				if (q)
					WriteQuote();
			}
		}

		public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
		{
			WriteIdent();

			Write("var ");
			WriteVariableName(u.DeclaringType, u, v);
			Write(":");

			WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true, IsFullyQualifiedNamesRequired(u.DeclaringType, v.LocalType));

			if (v.LocalType.IsValueType && !v.LocalType.IsPrimitive && !v.LocalType.IsEnum)
			{
				var z = MySession.ResolveImplementation(v.LocalType) ?? v.LocalType;

				// define default ctor
				if (z.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null) == null)
					Break("valuetype " + z.ToString() + " - " + z.Namespace + "." + z.Name + " must define a default .ctor");


				WriteAssignment();
				WriteKeywordSpace(Keywords._new);
				WriteDecoratedTypeNameOrImplementationTypeName(z, true, true, IsFullyQualifiedNamesRequired(u.DeclaringType, z));
				Write("()");
			}

			WriteLine(";");
		}



		public bool IsFullyQualifiedNamesRequired(Type context, Type subject)
		{
			if (context != subject && context.Name == subject.Name)
				return true;

			// there is a field with the same name as the type we would be importing
			if (context.GetField(subject.Name) != null)
				return true;

			return GetImportTypes(context).Any(i => i.Name == subject.Name || (i.Namespace != null && i.Namespace.EndsWith("." + subject.Name)));
		}

	}
}
