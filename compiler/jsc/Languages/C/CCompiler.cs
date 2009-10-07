using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Linq;

using IntPtr = global::System.IntPtr;

using ScriptCoreLib;

using jsc.Script;
using ScriptCoreLib.CSharp.Extensions;


namespace jsc.Languages.C
{
	partial class CCompiler : CompilerCLike
	{
		public static string FileExtension = "c";


		public string HeaderFileName;

		public bool IsHeaderOnlyMode;

		public readonly AssamblyTypeInfo MySession;

		public CCompiler(TextWriter xw, AssamblyTypeInfo xs)
			: base(xw)
		{

			MySession = xs;

			CreateInstructionHandlers();
		}


	
	

		public override ScriptType GetScriptType()
		{
			return ScriptType.C;
		}

	
		private void WriteTypeInstanceConstructors(Type z)
		{
			ConstructorInfo[] zci = GetAllInstanceConstructors(z);

			foreach (ConstructorInfo zc in zci)
			{
				WriteLine();

				WriteMethodHint(zc);
				WriteMethodSignature(zc, false);

				if (WillEmitMethodBody())
					WriteMethodBody(zc);

			}

			WriteLine();
		}

		private void WriteTypeDefPrototype(Type e)
		{
			if (IsHeaderOnlyMode)
			{

				ScriptAttribute a = ScriptAttribute.Of(e);


				if (a == null || !a.HasNoPrototype)
				{
					WriteIdent();
					Write("typedef struct tag_" + GetDecoratedTypeName(e, false));

					string _pname = GetPointerName(e);

					WriteLine(" *" + _pname + ";");
				}
			}
		}


		private void WriteTypeDef(Type e)
		{
			if (!e.IsAbstract)
			{
				ScriptAttribute a = ScriptAttribute.Of(e);

				//if (a.Implements != null)
				//    return;

				WriteLine();

				if (IsHeaderOnlyMode)
				{
					string _typename = GetDecoratedTypeName(e, false, false);
					string _pname = GetPointerName(e);

					#region instance struct

					if (a == null || !a.HasNoPrototype)
					{
						#region typedef
						WriteIdent();
						WriteLine("typedef struct tag_" + GetDecoratedTypeName(e, false));

						using (CreateScope(false))
						{
							Stack<Type> u = new Stack<Type>();

							Type p = e;

							while (p != typeof(object))
							{
								u.Push(p);
								p = p.BaseType;
							}

							while (u.Count > 0)
							{
								p = u.Pop();

								FieldInfo[] fields = GetAllFields(p);

								if (fields.Length == 0)
								{
									WriteIdent();
									WriteLine("void* __dummy;");

								}
								else
								{
									foreach (FieldInfo field in fields)
									{
										if (field.IsStatic)
											continue;

										WriteIdent();

										if (field.FieldType != typeof(string) && !field.FieldType.IsArray && !field.FieldType.IsPrimitive && field.FieldType.IsClass)
										{
											Write("struct tag_" + GetDecoratedTypeName(field.FieldType, false));
											Write("*");
										}
										else
										{
											Write(GetDecoratedTypeName(field.FieldType, false, true));
										}

										WriteSpace();
										Write(field.Name);
										WriteLine(";");
									}
								}
							}
						}
						WriteLine(" " + _typename + ", *" + _pname + ";");
						#endregion
					}



					WriteLine("#define __new_" + _typename + "(count) \\");
					WriteLine("    (" + _pname + ") malloc(sizeof(" + _typename + ") * count)");

					WriteLine();

					#endregion
				}
			}
		}

		public string GetPointerName(Type e)
		{


			ScriptAttribute a = ScriptAttribute.Of(e);

			if (a == null || a.PointerName == null)
			{
				string _typename = GetDecoratedTypeName(e, false, false);
				string _pname = "LP" + _typename;

				return _pname;
			}
			else
				return a.PointerName;

		}

		public override void WriteTypeConstructionVerified(CodeEmitArgs e, Type mtype, MethodBase m, ScriptAttribute mza)
		{
			string type = GetDecoratedTypeName(mtype, false, false);

			if (e.OpCode == OpCodes.Newobj)
			{
				WriteDecoratedMethodName(m, false);
				Write("(");
			}


			ScriptAttribute a = ScriptAttribute.Of(mtype);

			if (a == null)
			{
				a = ScriptAttribute.Of(ResolveImplementation(mtype));
			}

			if (a == null)
			{
				Write("(" + type + "*) malloc(sizeof(" + type + ")");

				if (e.OpCode == OpCodes.Newarr)
				{
					Write(" * ");

					EmitFirstOnStack(e);
				}

				Write(")");
			}
			else
			{
				Write("__new_" + type + "(");
				if (e.OpCode == OpCodes.Newarr)
				{
					EmitFirstOnStack(e);
				}
				else
				{
					Write("1");
				}
				Write(")");
			}




			if (e.OpCode == OpCodes.Newobj)
			{
				WriteParameters(e.p, m, e.i.StackBeforeStrict, 0, m.GetParameters(), true, ",");

				Write(")");
			}
		}

		public override void WriteTypeSignature(Type z, ScriptAttribute za)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteTypeFields(Type z, ScriptAttribute za)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteTypeFieldModifier(FieldInfo zfn)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void WriteXmlDoc(MethodBase m)
		{
			if (this.XmlDoc != null)
			{
				ParameterInfo[] param = m.GetParameters();

				string MethodSig = "M:" + m.DeclaringType.Namespace + "." + m.DeclaringType.Name + "." + m.Name;

				if (param.Length > 0)
				{
					int i = 0;
					MethodSig += "(";

					foreach (ParameterInfo MethodParam in param)
					{
						if (i++ > 0)
							MethodSig += ",";

						MethodSig += MethodParam.ParameterType.FullName;
					}

					MethodSig += ")";
				}

				XmlNode n = this.XmlDoc.SelectSingleNode(@"//members/member[@name='" + MethodSig + "']");

				if (n != null)
				{
					string Summary = n["summary"].InnerText.Trim();

					WriteBlockComment(Summary);
				}
				else
				{
					//   WriteJavaDoc(MethodSig);
				}
			}
		}

		public override Predicate<ILBlock.Prestatement> MethodBodyFilter
		{
			get
			{
				return
				 delegate(ILBlock.Prestatement p)
				 {
					 // note that instance constructor returns pointer to instance

					 #region remove redundant returns
					 if (p.Instruction != null)
						 if (p.Instruction == OpCodes.Ret)
							 if (p.Instruction.Next == null)
								 if (p.Instruction.StackBeforeStrict.Length == 0)
									 if (!p.Instruction.OwnerMethod.IsInstanceConstructor())
									 {
										 return true;
									 }
					 #endregion

					 return false;
				 };
			}
		}


		public override void WriteTypeInstanceMethods(Type z, ScriptAttribute za)
		{
			MethodInfo[] mx = GetAllInstanceMethods(z);

			int idx = 0;

			foreach (MethodInfo m in mx)
			{
				ScriptAttribute ma = ScriptAttribute.Of(m);

				bool dStatic = AlwaysDefineAsStatic || (ma != null && ma.DefineAsStatic);

				if (ma != null && (ma.IsNative || ma.ExternalTarget != null))
					continue;

				if (ma == null && !m.IsStatic && (za.HasNoPrototype))
					continue;

				if (idx++ > 0)
					WriteLine();

				WriteMethodHint(m);
				WriteXmlDoc(m);
				WriteMethodSignature(m, dStatic);

				if (WillEmitMethodBody())
					if (!ScriptIsPInvoke(m))
						if (!m.IsAbstract) WriteMethodBody(m, MethodBodyFilter);

			}
		}



		public override string GetDecoratedTypeName(Type z, bool bExternalAllowed)
		{
			return GetDecoratedTypeName(z, bExternalAllowed, false);
		}

		public override bool WillEmitMethodBody()
		{
			return !this.IsHeaderOnlyMode;
		}

		public override void WriteMethodSignature(MethodBase m, bool dStatic)
		{
			WriteIdent();

			if (m is MethodInfo)
			{
				MethodInfo mi = m as MethodInfo;

				//WriteDecoratedTypeName(mi.ReturnType);
				Write(GetDecoratedTypeName(mi.ReturnType, true, true));
				//Write(GetDecoratedTypeNameWithinNestedName( mi.ReturnType));

			}
			else
			{
				Write(GetDecoratedTypeName(m.DeclaringType, true, true));
			}

			WriteSpace();

			WriteDecoratedMethodName(m, false);

			Write("(");
			WriteMethodParameterList(m);
			Write(")");

			if (!WillEmitMethodBody())
			{
				Write(";");
			}


			WriteLine();
		}

		public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
		{
			ScriptAttribute s = ScriptAttribute.Of(m.DeclaringType);

			if (s == null)
			{
				WriteDecoratedMethodName(ResolveImplementationMethod(m.DeclaringType, m), false);
			}
			else
			{
				WriteDecoratedMethodName(m, false);
			}

			int offset = 1;

			if (m.IsStatic)
				offset = 0;

			WriteParameterInfoFromStack(m, p, i.StackBeforeStrict, offset);
		}



		public override bool AlwaysDefineAsStatic
		{
			get
			{
				return true;
			}
		}

		public bool HideParameterNameInHeaderFiles = true;

		public override void WriteMethodParameterList(MethodBase m)
		{
			ParameterInfo[] mp = m.GetParameters();

			ScriptAttribute ma = ScriptAttribute.Of(m);

			bool bStatic = (!m.IsStatic && AlwaysDefineAsStatic) || (ma != null && ma.DefineAsStatic);

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
					Write(GetDecoratedTypeName(m.DeclaringType, true, true));

				}
				else
				{
					Write(GetDecoratedTypeName(sa.Implements, true, true));
				}

				if (this.IsHeaderOnlyMode)
				{
					if (!HideParameterNameInHeaderFiles)
					{
						WriteSpace();
						Write("/* ");
						WriteSelf();
						Write(" */");
					}
				}
				else
				{
					WriteSpace();
					WriteSelf();
				}
			}
			else
			{
				if (mp.Length == 0)
					Write("void");
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

				if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
					Write(GetDecoratedTypeName(p.ParameterType, true, true));
				else
					Write(GetDecoratedTypeName(za.Implements, true, true));

				if (this.IsHeaderOnlyMode)
				{
					if (!HideParameterNameInHeaderFiles)
					{
						WriteSpace();
						Write("/* ");
						Write(p.Name);
						Write(" */");
					}
				}
				else
				{
					WriteSpace();
					Write(p.Name);
				}

			}
		}

		public override void WriteSelf()
		{
			Write("that");
		}

		public override void EmitPrestatement(ILBlock.Prestatement p)
		{
			WriteIdent();
			//WriteBoxedComment(p.Instruction.Flow.ToString());

			EmitInstruction(p, p.Instruction);
			WriteLine(";");
		}



		public override Type[] GetActiveTypes()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override Type ResolveImplementation(Type t)
		{
			return MySession.ResolveImplementation(t); ;
		}

		public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
		{
			return MySession.ResolveImplementation(t, m); ;
		}

		public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
		{
			return MySession.ResolveMethod(m, t, alias);
		}

		public override bool EmitTryBlock(ILBlock.Prestatement p)
		{
			// no exceptions in c, so used only for paired actions

			BreakToDebugger("no try/catch in c");


			return true;
		}

		public override void WriteDecoratedMethodName(MethodBase z, bool q)
		{

			if (q)
				Write("\"");

			ScriptAttribute s = ScriptAttribute.Of(z);

			bool x = true;

			if (s != null)
			{
				if (s.NoDecoration)
					x = false;
			}

			s = ScriptAttribute.Of(z.DeclaringType);

			if (s != null)
			{
				if (s.IsNative)
					x = false;
			}

			if (x)
			{
				Write(GetDecoratedTypeName(z.DeclaringType, true, false));
				Write("_");
			}

			Write(z.Name.Replace(".", "_"));

			if (z.Name == "op_Implicit")
			{
				Write("_");
				WriteDecoratedTypeName(((MethodInfo)z).ReturnType);
			}

			if (x && (!z.IsStatic || z.GetParameters().Length > 0))
			{
				if (IsOverloadedMethod(z))
				{
					Write("_");
					Write(TokenToString(z.MetadataToken));
				}
			}

			if (q)
				Write("\"");
		}



		public override void WriteDecoratedMethodParameter(ParameterInfo p)
		{
			Write(p.Name);
		}




		public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
		{
			WriteIdent();
			Write(GetDecoratedTypeName(v.LocalType, false, true));
			WriteSpace();
			WriteVariableName(u.DeclaringType, u, v);

			WriteLine(";");
		}

		protected override bool IsTypeCastRequired(Type e, ILFlow.StackItem s)
		{
			// we shall behave like AlwaysDoTypeCastOnParameters
			return true;
		}

		public override bool AlwaysDoTypeCastOnParameters
		{
			get
			{
				// we shall rely on IsTypeCastRequired and MethodCallParameterTypeCast instead
				return false;
			}
		}

		public override bool IsBooleanSupported
		{
			get
			{
				return false;
			}
		}

		public override bool WillReturnPointerToThisOnConstructorReturn
		{
			get
			{
				return true;
			}
		}



		protected override void WriteTypeCast(Type type)
		{
			ScriptAttribute u = ScriptAttribute.Of(type);

			Type p = type;

			if (u != null && u.Implements != null)
				p = u.Implements;

			WriteBoxedComment("typecast");

			Write("(");
			Write(GetDecoratedTypeName(p, true, true));
			Write(")");
		}

		public override bool SupportsForStatements
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

		public override void WriteTypeConstructionVerified()
		{
			this.Write("/*newobj*/ malloc(4)");
		}

		public override void WriteKeywordNull()
		{
			this.Write("NULL");
		}
	}


}
