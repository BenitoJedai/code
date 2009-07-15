
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.Java
{
	// http://www.javacamp.org/javavscsharp/constructor.html
	// http://www4.ncsu.edu/~kaltofen/courses/Languages/JavaExamples/cpp_vs_java/
	public partial class JavaCompiler : Script.CompilerCLike
	{
		public static string FileExtension = "java";

		public override ScriptType GetScriptType()
		{
			return ScriptType.Java;
		}

		public override Type[] GetActiveTypes()
		{
			return MySession.Types;
		}

		public override bool CompileToSingleFile
		{
			get
			{
				return false;
			}
		}

		public override bool IsUTF8SupportedInLiterals()
		{
			return true;
		}

		public readonly AssamblyTypeInfo MySession;

		public JavaCompiler(TextWriter xw, AssamblyTypeInfo xs)
			: base(xw)
		{

			MySession = xs;

			CreateInstructionHandlers();

		}







		private void WriteTypeStaticAccessor()
		{
			Write(".");
		}



		public override Type ResolveImplementation(Type t)
		{
			return MySession.ResolveImplementation(t); ;
		}

		public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
		{
			return MySession.ResolveImplementation(t, m);
		}

		public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
		{
			return MySession.ResolveMethod(m, t, alias);
		}














		public override void WriteXmlDoc(MethodBase m)
		{
			if (this.XmlDoc != null)
			{
				DebugBreak(m);

				XmlNode n = GetXMLNodeForMethod(m);

				if (n != null && n["summary"] != null)
				{
					string Summary = n["summary"].InnerText.Trim();

					WriteBlockComment(Summary);
				}
				else
				{
					//WriteJavaDoc(MethodSig);
				}
			}
		}





		public override void WriteMethodSignature(MethodBase m, bool dStatic)
		{
			DebugBreak(ScriptAttribute.Of(m));

			WriteIdent();

			if (m.IsAbstract && !m.DeclaringType.IsInterface)
				WriteKeywordSpace(Keywords._abstract);

			int flags = (int)m.GetMethodImplementationFlags();

			// http://blogs.msdn.com/ricom/archive/2004/05/05/126542.aspx
			// http://msdn2.microsoft.com/en-us/library/system.reflection.methodimplattributes.aspx
			if ((flags & (int)MethodImplAttributes.Synchronized) == (int)MethodImplAttributes.Synchronized)
				Write("synchronized ");

			// it seems delegates cannot call to private or protected
			// static methods
			// so we make them public...
			if (m.IsPublic || m.IsStatic)
				WriteKeywordPublic();
			else
			{
				if (m.IsFamily)
					Write("protected ");
				else
					WriteKeywordPrivate();
			}

			if (m.IsStatic || dStatic)
				this.WriteKeywordSpace(Keywords._static);
			else
			{
				if (m is MethodInfo)
				{
					if (m.IsFinal || !m.IsVirtual)
						Write("final ");
				}
			}

			if (ScriptIsPInvoke(m))
			{
				Write("native ");
			}

			if (m is MethodInfo)
			{
				MethodInfo mi = m as MethodInfo;

				//WriteDecoratedTypeName(mi.ReturnType);
				WriteDecoratedTypeNameOrImplementationTypeName(mi.ReturnType, true, true);

				//Write(GetDecoratedTypeNameWithinNestedName( mi.ReturnType));
				WriteSpace();
			}

			if (m.IsInstanceConstructor())
				Write(GetDecoratedTypeName(m.DeclaringType, false, true, true, true));
			else
				WriteDecoratedMethodName(m, false);

			Write("(");
			WriteMethodParameterList(m);

			Write(")");

			WriteMethodSignatureThrows(m);

			if (m.IsAbstract || ScriptIsPInvoke(m))
				WriteLine(";");
			else
				WriteLine();

		}

		public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
		{
			WriteIdent();

			WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true);
			WriteSpace();

			//WriteVariableType(v.LocalType, true);

			WriteVariableName(u.DeclaringType, u, v);

			WriteLine(";");
		}



		public void WriteExternalMethod(string p, MethodBase m)
		{
			if (p.Contains("*"))
			{
				foreach (PropertyInfo v in m.DeclaringType.GetProperties())
				{
					if (v.GetGetMethod() == m || v.GetSetMethod() == m)
					{
						Write(p.Replace("*", v.Name));

						return;
					}
				}

				throw new NotSupportedException("The use of * is only allowed on properties to capture its name.");

			}
			else
			{
				Write(p);
			}
		}

		protected override bool IsTypeCastRequired(Type e, ILFlow.StackItem s)
		{
			// next stop - MethodCallParameterTypeCast

			if (e == typeof(byte) || (e.IsEnum && Enum.GetUnderlyingType(e) == typeof(byte)))
				return true;

			if (e == typeof(uint) || (e.IsEnum && Enum.GetUnderlyingType(e) == typeof(uint)))
				return true;

			return false;
		}

		public override void MethodCallParameterTypeCast(Type context, Type p)
		{
			if (p == typeof(uint) || (p.IsEnum && Enum.GetUnderlyingType(p) == typeof(uint)))
			{
				Write("(int)");
				return;
			}

			if (p == typeof(byte) || (p.IsEnum && Enum.GetUnderlyingType(p) == typeof(byte)))
			{
				Write("(byte)");
				return;
			}

			Write("(");
			WriteDecoratedTypeName(p);
			Write(")");
		}

		private void WriteTypeOrExternalTargetTypeName(Type m)
		{


			string x = ScriptGetExternalTarget(m);

			if (x == null)
				Write(GetDecoratedTypeName(m, false, true, true, true));
			else
				Write(x);

		}


		public void WriteTypeNameAsMemberName(Type e)
		{
			if (e.IsArray)
			{
				Write("ArrayOf");

				WriteTypeNameAsMemberName(e.GetElementType());

				return;
			}

			WriteDecoratedTypeName(e);
		}

		public override void WriteDecoratedMethodName(MethodBase z, bool q)
		{
			if (q)
				Write("\"");

			if (z.Name == "ToString" && !z.IsStatic)
				Write("toString");
			else
			{
				if (z.Name == "Main" && z.IsStatic)
				{
					// java wants main to be lowercased
					Write("main");
				}
				else if (z.Name == "op_Implicit")
				{


					Type rt = ((MethodInfo)z).ReturnType;

					if (rt == z.DeclaringType)
					{
						// name clash?

						Write("Of");

						//Write("From");
						//WriteTypeNameAsMemberName(z.GetParameters()[0].ParameterType);
					}
					else
					{
						Write("To");

						if (rt.IsPrimitive)
							Write("_");

						WriteTypeNameAsMemberName(rt);

					}

				}
				else
				{
					WriteSafeLiteral(z.Name);
				}

			}

			if (q)
				Write("\"");
		}

		private void WriteMethodSignatureThrows(MethodBase m)
		{
			DebugBreak(ScriptAttribute.Of(m));

			List<Type> list = GetMethodExceptions(m);

			if (list.Count > 0)
			{
				WriteSpace();
				WriteKeywordThrows();

				for (int i = 0; i < list.Count; i++)
				{
					if (i > 0)
						Write(", ");

					WriteVariableType(list[i], false);

				}
			}
		}



		public override bool EmitTryBlock(ILBlock.Prestatement p)
		{
			if (p.Block.IsTryBlock)
			{

				WriteIdent();
				WriteLine("try");


				ILBlock.PrestatementBlock b = p.Block.Prestatements;

				bool _pop = false;
				bool _leave = b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First;

				EmitScope(b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last));


			}
			else if (p.Block.IsHandlerBlock)
			{


				WriteIdent();



				ILBlock.PrestatementBlock b = p.Block.Prestatements;

				bool _pop = b.First == OpCodes.Pop && (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
				bool _leave =
					b.Last == OpCodes.Endfinally
				||
					(b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

				b = b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

				b.RemoveNopOpcodes();

				if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
				{
					DebugBreak(p.DeclaringMethod.ToScriptAttribute());

					Write("catch (");

					if (p.Block.Clause.CatchType == typeof(object))
					{
						Write("java.lang.Throwable");
						WriteSpace();
						WriteExceptionVar();
					}
					else
					{
						var ExceptionType = MySession.ResolveImplementation(p.Block.Clause.CatchType) ?? p.Block.Clause.CatchType;
						var ExceptionTypeAttribute = ExceptionType.ToScriptAttribute();

						if (ExceptionTypeAttribute != null && ExceptionTypeAttribute.ImplementationType != null)
							Write(GetDecoratedTypeName(ExceptionTypeAttribute.ImplementationType, true));
						else
							Write(GetDecoratedTypeName(ExceptionType, true));

						WriteSpace();

						ILBlock.Prestatement set_exc = p.Block.Prestatements.PrestatementCommands[0];

						if (set_exc.Instruction.TargetVariable == null)
							Write("__ExceptionValue");
						else
							WriteVariableName(p.Block.OwnerMethod.DeclaringType, p.Block.OwnerMethod, set_exc.Instruction.TargetVariable);

						// remove the set command if there is one
						if (set_exc.Instruction.TargetVariable != null)
							b.PrestatementCommands.RemoveAt(0);

					}


					WriteLine(")");

					EmitScope(b);
				}
				else
				{
					WriteLine("finally");
					EmitScope(b);
				}

				// additional space
				WriteLine();
			}
			else
			{
				return false;
			}

			return true;

		}






		public override void WriteSelf()
		{
			Write("that");
		}



		public override void WriteMethodParameterList(MethodBase m)
		{
			ParameterInfo[] mp = m.GetParameters();

			ScriptAttribute ma = ScriptAttribute.Of(m);

			bool bStatic = (ma != null && ma.DefineAsStatic);

			if (bStatic)
			{
				if (m.IsStatic)
				{
					Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
				}


				DebugBreak(ma);


				ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

				if (sa.Implements == null)
				{
					WriteDecoratedTypeName(m.DeclaringType);

				}
				else
				{
					WriteDecoratedTypeName(sa.Implements);
				}

				// this parameter is on the argument list

				WriteSpace();
				WriteSelf();
			}

			for (int mpi = 0; mpi < mp.Length; mpi++)
			{
				if (mpi > 0 || bStatic)
				{
					Write(",");
					WriteSpace();
				}

				ParameterInfo p = mp[mpi];

				ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);

				// why would we want to write the actual typename of BCL type?
				// it wont be around!
				// maybe for strings and such?
				// but they have to define implementation type then




				if (za.Implements != null && m.DeclaringType == p.ParameterType)
					WriteDecoratedTypeNameOrImplementationTypeName(za.Implements, true, true);
				else
					WriteDecoratedTypeNameOrImplementationTypeName(p.ParameterType, true, true);


				WriteSpace();




				Write(p.Name);
			}
		}

		public void WriteVariableType(Type t, bool bSpace)
		{

			Write(GetDecoratedTypeName(t, true, true, true));

			if (bSpace)
				WriteSpace();
		}


		public override void WriteTypeFields(Type z, ScriptAttribute za)
		{
			FieldInfo[] zf = GetAllFields(z);

			foreach (FieldInfo zfn in zf)
			{
				// external class cannot have static variables inside a type
				// should be defined outside as global static instead
				if (za.HasNoPrototype && !zfn.IsStatic)
					continue;

				if (zfn.IsLiteral)
					continue;

				WriteIdent();
				WriteTypeFieldModifier(zfn);

				WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true);
				WriteSpace();

				//WriteVariableType(zfn.FieldType, true);
				WriteSafeLiteral(zfn.Name);

				if (zfn.IsStatic && zfn.IsInitOnly)
				{
					ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

					if (ci.Length == 1)
					{
						ILBlock cctor = new ILBlock(ci[0]);
						ILBlock.Prestatement assign = cctor.GetStaticFieldFinalAssignment(zfn);

						if (assign != null)
						{
							WriteAssignment();

							EmitFirstOnStack(assign);
						}
					}
				}

				WriteLine(";");
			}
		}

		public override void WriteTypeFieldModifier(FieldInfo zfn)
		{
			if (zfn.IsPublic)
				WriteKeywordPublic();
			else
			{
				if (zfn.IsFamily)
					Write("protected ");
				else
					WriteKeywordPrivate();
			}

			if (zfn.IsInitOnly)
				WriteKeywordFinal();

			if (zfn.IsStatic)
				this.WriteKeywordSpace(Keywords._static);

			if (zfn.IsNotSerialized)
				Write("transient ");
		}

		#region keywords


		private void WriteKeywordImport()
		{
			Write("import ");
		}


		private void WriteKeywordFinal()
		{
			Write("final ");
		}

		private void WriteKeywordPrivate()
		{
			Write("private ");
		}

		private void WriteKeywordPublic()
		{
			Write("public ");
		}

	
		private void WriteKeywordThrows()
		{
			Write("throws ");
		}

		#endregion

		public override string GetDecoratedTypeNameWithinNestedName(Type z)
		{
			return GetDecoratedTypeName(z, false, false, false, true);
		}

		public override void WriteTypeSignature(Type z, ScriptAttribute za)
		{
			WriteIdent();



			//if (za.Implements != null || z.IsPublic || z.IsNestedPublic || z.ToScriptAttribute() == null)
			WriteKeywordPublic();
			//else
			//    WriteKeywordPrivate();


			if (z.IsAbstract && !z.IsSealed && !z.IsInterface)
				WriteKeywordSpace(Keywords._abstract);

			if (z.IsSealed)
				Write("final ");
			else
			{
				// Shall we seal all nonused objects?
			}

			if (z.IsInterface)
				WriteKeywordSpace(Keywords._interface);
			else
				WriteKeywordSpace(Keywords._class);

			if (za.Implements == null)
				Write(GetDecoratedTypeNameWithinNestedName(z));
			else
				Write(GetDecoratedTypeName(z, false, true, true, true));


			#region extends
			if (z.BaseType != typeof(object) && z.BaseType != null)
			{

				WriteSpace();
				WriteKeywordSpace(Keywords._extends);

				var _BaseType = ResolveImplementation(z.BaseType) ?? z.BaseType;

				ScriptAttribute ba = ScriptAttribute.Of(_BaseType, true);

				if (ba == null)
					Break("extending object has no attribute");


				if (ba.Implements == null)
					WriteDecoratedTypeName(_BaseType);
				else
					Write(GetDecoratedTypeName(_BaseType, false));

			}
			#endregion

			#region implements
			Type[] timp = z.GetInterfaces();

			if (timp.Length > 0)
			{
				WriteSpace();
				WriteKeywordSpace(z.IsInterface ? Keywords._extends : Keywords._implements);

				int i = 0;

				DebugBreak(za);

				foreach (Type timpv in timp)
				{
					if (i++ > 0)
						Write(", ");

					WriteDecoratedTypeNameOrImplementationTypeName(timpv);
				}
			}
			#endregion

			WriteLine();
		}

		public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv)
		{
			WriteDecoratedTypeNameOrImplementationTypeName(timpv, false, false);
		}

		/// <summary>
		/// tries to use the implementation name
		/// </summary>
		/// <param name="timpv"></param>
		/// <param name="favorTargetType"></param>
		private void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType)
		{
			//[Script(Implements = typeof(global::System.Boolean),
			//    ImplementationType=typeof(java.lang.Integer))]


			Type iType = ResolveImplementation(timpv);

			if (iType != null)
			{
				if (favorTargetType)
				{
					if (ScriptAttribute.OfProvider(iType).ImplementationType != null)
						iType = null;
				}
			}

			if (iType == null)
				Write(GetDecoratedTypeName(timpv, true, favorPrimitives, true));
			else
				Write(GetDecoratedTypeName(iType, true));
		}

	

		public void WriteDecoratedMethodParameter(ParameterInfo p, Type ExpectedType)
		{
			if (ExpectedType == typeof(object) && (p.ParameterType.IsEnum || p.ParameterType.IsPrimitive))
			{
				this.WriteOpCodesBox(
					p.ParameterType,
					null,
					delegate
					{
						WriteDecoratedMethodParameter(p);
					}
					, false
				);

				return;
			}

			// revert to common implementation
			WriteDecoratedMethodParameter(p);
		}

		public override void WriteDecoratedMethodParameter(ParameterInfo p)
		{
			Write(p.Name);
		}

		string ToJavaTypeName(string e)
		{
			e = e.Replace("`", "_");
			e = e.Replace("<", "_");
			e = e.Replace(">", "_");

			return e;
		}

		// http://www.idevelopment.info/data/Programming/java/miscellaneous_java/Java_Primitive_Types.html
		public override string GetDecoratedTypeName(Type type, bool bExternalAllowed)
		{
			return GetDecoratedTypeName(type, bExternalAllowed, true);
		}

		public string GetDecoratedTypeName(Type type, bool bExternalAllowed, bool bUsePrimitives)
		{
			return GetDecoratedTypeName(type, bExternalAllowed, bUsePrimitives, true);
		}

		public string GetDecoratedTypeName(Type type, bool bExternalAllowed, bool bUsePrimitives, bool bChopNestedParents)
		{
			return GetDecoratedTypeName(type, bExternalAllowed, bUsePrimitives, bChopNestedParents, false);
		}

		public string GetDecoratedTypeName(Type type, bool bExternalAllowed, bool bUsePrimitives, bool bChopNestedParents, bool bDisableArrayToObjectRewrite)
		{
			if (type == null)
				return "null";

			if (type.IsEnum)
			{
				// http://stackoverflow.com/questions/503806/in-c-can-i-use-reflection-to-determine-if-an-enum-type-is-int-byte-short-etc
				return GetDecoratedTypeName(Enum.GetUnderlyingType(type), false);
			}

			if (type.IsArray)
			{
				return GetDecoratedTypeName(type.GetElementType(), bExternalAllowed, bUsePrimitives, bChopNestedParents) + "[]";
			}

			if (!bDisableArrayToObjectRewrite)
			{
				if (type.ToScriptAttributeOrDefault().Implements == typeof(Array))
					return GetDecoratedTypeName(typeof(object), false);
			}



			ScriptAttribute a = ScriptAttribute.Of(type, true);


			Type __impl = ResolveImplementation(type);

			if (bExternalAllowed && a == null && __impl != null)
			{
				a = ScriptAttribute.Of(__impl);
			}


			if (bExternalAllowed && a != null && a.ExternalTarget != null)
			{
				return a.ExternalTarget;
			}
			else
			{

				if (type.IsNested)
				{
					#region nested
					List<string> x = new List<string>();

					Type p = type;

					if (bChopNestedParents && a != null && a.IsNative)
						return type.Name;

					while (p != null)
					{
						x.Add(ToJavaTypeName(p.Name));


						p = p.DeclaringType;
					}



					x.Reverse();

					// if they are native java inner types
					// we need to use .

					// custom nested classes are refactored as
					// separate classes by _

					if (a == null)
						BreakToDebugger("typename");

					return string.Join(
						a.IsNative
						? "."
						: "_", x.ToArray());
					#endregion

				}
				else
				{
					if (bUsePrimitives)
					{
						if (type == typeof(void)) return "void";

						else if (type == typeof(int)) return "int";
						else if (type == typeof(uint)) return "int";

						else if (type == typeof(byte)) return "byte";
						else if (type == typeof(sbyte)) return "byte";

						else if (type == typeof(short)) return "short";
						else if (type == typeof(ushort)) return "short";


						else if (type == typeof(double)) return "double";
						else if (type == typeof(bool)) return "boolean";
						else if (type == typeof(long)) return "long";



						else if (type == typeof(char)) return "char";
						else if (type == typeof(float)) return "float";
						else if (type == typeof(double)) return "double";

						if (type.IsArray)
						{
							if (type.GetElementType() == typeof(sbyte))
								return "byte[]";
							if (type.GetElementType() == typeof(byte))
								return "byte[]";
							if (type.GetElementType() == typeof(int))
								return "int[]";
							if (type.GetElementType() == typeof(uint))
								return "int[]";
							else if (type.GetElementType() == typeof(float))
								return "float[]";
						}
					}
					else
					{
						// http://www.dotnetspider.com/tutorials/Datatypes.aspx
						// box for java

						if (type == typeof(int))
							return "Integer";
						if (type == typeof(long))
							return "Long";
						if (type == typeof(sbyte))
							return "Byte";
						if (type == typeof(short))
							return "Short";
						if (type == typeof(float))
							return "Float";
					}

					if (__impl != null)
						if (__impl.ToScriptAttributeOrDefault().ImplementationType == null)
							return ToJavaTypeName(__impl.Name);

					return ToJavaTypeName(type.Name);
				}
			}
		}






		public override bool SupportsInlineArrayInit
		{
			get
			{
				return true;
			}
		}

		public override bool SupportsForStatements
		{
			get
			{
				return true;
			}
		}

		public override bool SupportsInlineThisReference
		{
			get
			{
				return true;
			}
		}

		public override bool SupportsInlineAssigments
		{
			get
			{
				return true;
			}
		}

		public override void WriteReturnParameter(ILBlock.Prestatement _p, ILInstruction _i)
		{
			if (_i.OwnerMethod is MethodInfo)
			{
				if ((_i.OwnerMethod as MethodInfo).ReturnType == typeof(bool))
				{
					if (_i.InlineAssigmentValue != null)
					{
						if (_i.InlineAssigmentValue.Instruction.IsStoreLocal)
						{

							WriteReturnParameter(_p, _i.InlineAssigmentValue.Instruction.StackBeforeStrict[0].SingleStackInstruction);

							return;
						}
					}

					if (_i == OpCodes.Ldc_I4_0)
					{
						WriteKeywordFalse();

						return;
					}

					if (_i == OpCodes.Ldc_I4_1)
					{
						WriteKeywordTrue();

						return;
					}
				}
			}

			base.WriteReturnParameter(_p, _i);
		}

		public override bool SupportsInlineExceptionVariable
		{
			get
			{
				return true;
			}
		}

		public override void WriteTypeConstructionVerified()
		{
			Write("new Object()");
		}

		public override void WriteInstanceOfOperator(ILInstruction value, Type type)
		{
			EmitInstruction(null, value);

			Write(" instanceof ");

			WriteDecoratedTypeName(type);
		}

		//protected override bool IsTypeCastRequired(Type e, ILFlow.StackItem s)
		//{
		//    if (e == typeof(int) && s.SingleStackInstruction.TargetInteger != null)
		//        return false;

		//    return true;
		//}


		//protected override bool WriteTypeOf(ILBlock.Prestatement p, ILInstruction i)
		//{
		//    Emit(p, i.StackBeforeStrict[0]);

		//    Write(".class");

		//    return true;
		//}

		/// <summary>
		/// a special opcode emit mode
		/// </summary>
		bool WriteCall_DebugTrace_Assign_Active;

		public override void WriteCall_DebugTrace_AfterAssign(MethodInfo m, ILBlock.Prestatement p)
		{
			if (p.Instruction.TargetVariable == null)
				return;

			WriteIdent();
			WriteDecoratedMethodName(m, false);
			Write("(");

			Write("\"");

			Write(" [ ");
			WriteVariableName(p.Instruction.OwnerMethod.DeclaringType, p.Instruction.OwnerMethod, p.Instruction.TargetVariable);
			Write(" ] \" + ");
			WriteVariableName(p.Instruction.OwnerMethod.DeclaringType, p.Instruction.OwnerMethod, p.Instruction.TargetVariable);

			Write(")");
			WriteLine(";");
		}

		public override void WriteCall_DebugTrace_Assign(MethodInfo m, ILBlock.Prestatement p)
		{
			if (WriteCall_DebugTrace_Assign_Active)
				return;

			WriteIdent();
			WriteDecoratedMethodName(m, false);
			Write("(");

			Write("\"");

			WriteCall_DebugTrace_Assign_Active = true;
			WriteLine_NewLineEnabled = false;
			WriteIdent_Enabled = false;

			EmitInstruction(p, p.Instruction);

			WriteIdent_Enabled = true;
			WriteLine_NewLineEnabled = true;
			WriteCall_DebugTrace_Assign_Active = false;

			Write("\"");

			Write(")");
			WriteLine(";");
		}


	}
}
