using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

using jsc.CodeModel;

using ScriptCoreLib;

namespace jsc.Script.PHP
{

	partial class PHPCompiler : jsc.Script.CompilerCLike
	{
		public static string FileExtension = "php";

		public override ScriptType GetScriptType()
		{
			return ScriptType.PHP;
		}




		public readonly AssamblyTypeInfo MySession;

		public PHPCompiler(TextWriter xw, AssamblyTypeInfo xs)
			: base(xw)
		{

			MySession = xs;
			CreateInstructionHandlers();

		}




		private void WriteTypeStaticAccessor()
		{
			Write("::");
		}





		public override Type ResolveImplementation(Type t)
		{
			return MySession.ResolveImplementation(t);
		}

		public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
		{
			return MySession.ResolveImplementation(t, m); ;
		}

		public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
		{
			return MySession.ResolveMethod(m, t, alias); ;

		}


		public override void WriteExceptionVar()
		{
			Write("$__exc");
		}



		public void WriteDecoratedField(FieldInfo z, bool p)
		{
			if (p)
				Write("$");

			bool noDec = false;

			if (z.DeclaringType.IsSerializable)
				noDec = true;

			ScriptAttribute sa = ScriptAttribute.Of(z.DeclaringType, false);

			if (sa != null && sa.HasNoPrototype)
				noDec = true;

			// instance members do not clash, nor should they be redefined

			if (!z.IsStatic)
				noDec = true;

			if (noDec)
			{

				WriteSafeLiteral(z.Name);
			}
			else
			{
				WriteDecoratedFieldVerified(z);
			}

		}











		public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
		{
			try
			{
				bool bBase = false;

				if (m.IsInstanceConstructor())
					if (i != null) 
					// CLR 4 seems to now tell that static ctor IsConstructor
					{
						var _BaseType = i.OwnerMethod.DeclaringType.BaseType;
						_BaseType = ResolveImplementation(_BaseType) ?? _BaseType;

						if (m.DeclaringType == _BaseType)
						{

							bBase = true;
						}
						else
							Break("If it was a native constructor, it should be remapped via InternalConstructor attribute.Cannot call constructor : " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ".");
					}

				ScriptAttribute ma = ScriptAttribute.OfProvider(m);
				bool dStatic = ma != null && ma.DefineAsStatic;



				ILFlow.StackItem[] s = i == null ? null : i.StackBeforeStrict;

				int offset = 1;



				if (m.IsStatic || dStatic || bBase)
				{
					if (bBase)
					{
						WriteTypeBaseType();
						Write("::");

					}



					offset = !m.IsStatic && (dStatic || bBase) ? 1 : 0;

				}
				else
				{


					Emit(p, s[0]);

					Write("->");
				}


				WriteMethodName(m);
				WriteParameterInfoFromStack(m, p, s, offset);
			}
			catch
			{
				Break("cannot write method call");
			}
		}

		private void WriteTypeBaseType()
		{
			Write("parent");
		}

		public override void WriteNativeNoExceptionMethodName(MethodBase m)
		{
			Write("@");

			base.WriteNativeNoExceptionMethodName(m);
		}

		private void WriteMethodName(MethodBase m)
		{


			if (m.IsInstanceConstructor())
				Write("__construct");
			else
			{
				if (IsToStringMethod(m))
				{
					Write("__toString");
				}
				else
					WriteDecoratedMethodName(m, false);
			}

		}












		public override Type[] GetActiveTypes()
		{
			return MySession.Types;
		}



		public TypeInfo TypeInfoOf(Type z)
		{
			TypeInfo v = new TypeInfo(z);

			v.TargetFileNameHandler = delegate(TypeInfo a)
			{
				return "inc/" + a.AssamblyFileName + "/class." + a.Win32TypeName + ".php";
			};

			v.ResolvedValue = ResolveImplementation(v.Value) ?? v.Value;
			v.ResolvedBaseTypes = Enumerable.ToArray(
				from k in v.ReferencedBaseTypes
				let r = ResolveImplementation(k) ?? k
				select r.IsGenericType ? r.GetGenericTypeDefinition() : r
			).ToArray();

			return v;
		}





		public void WriteImport(TypeInfo z)
		{
			if (z.IsScript || z.IsCompilerGenerated)
			{
				WriteLine("require_once '" + z.TargetFileName + "';");
			}
		}

		private void WriteTypeStaticConstructor(Type z, bool defMode)
		{
			ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

			foreach (ConstructorInfo m in ci)
			{
				if (defMode)
				{
					WriteMethodSignature(z, m, false);
					WriteMethodBody(m);
					WriteLine();

				}
				else
				{
					WriteIdent();

					WriteHint(m);

					WriteMethodCallVerified(null, null, m);
					WriteLine(";");
					WriteLine();

				}

			}


		}

		private void WriteHint(ConstructorInfo m)
		{
			WriteLine("/* " + m.DeclaringType.FullName + " :: " + m.Name + " */");
		}



		private void WriteTypeInstanceConstructors(Type z)
		{
			ConstructorInfo[] zci = z.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

			if (zci.Length > 1)
			{
				Break("PHP does not support multiple constructors, type: " + z.FullName);
			}

			foreach (ConstructorInfo zc in zci)
			{
				WriteIdent();
				WriteCommentLine(zc.DeclaringType.FullName + ".ctor");
				WriteMethodSignature(z, zc, false);
				WriteMethodBody(zc);

			}
			WriteLine();
		}

		public override void WriteTypeFields(Type z, ScriptAttribute za)
		{
			FieldInfo[] zf = z.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

			foreach (FieldInfo zfn in zf)
			{
				// external class cannot have static variables inside a type
				// should be defined outside as global static instead
				if (za.HasNoPrototype && !zfn.IsStatic)
					continue;


				WriteIdent();
				WriteTypeFieldModifier(zfn);



				WriteDecoratedField(zfn, true);
				Write(";");
				WriteLine();
			}

			WriteLine();
		}


		public override void WriteTypeFieldModifier(FieldInfo zfn)
		{


			if (zfn.IsStatic)
			{
				Write("static");
				WriteSpace();
			}
			else if (zfn.IsPublic)
			{
				Write("public");
			}
			else
			{
				Write("var");
			}

			WriteSpace();
		}

		public override void WriteTypeInstanceMethods(Type z, ScriptAttribute za)
		{
			MethodInfo[] mx = base.GetAllInstanceMethods(z);

			foreach (MethodInfo m in mx)
			{
				// for now we skip such methods
				if (z.IsAnonymousType() && m.Name == "Equals")
					continue;

				if (z.IsAnonymousType() && m.Name == "GetHashCode")
					continue;

				ScriptAttribute ma = ScriptAttribute.Of(m);




				bool dStatic = ma != null && ma.DefineAsStatic;

				if (dStatic)
				{
					continue;
				}

				if (ma != null && ma.IsNative)
					continue;


				WriteMethodHint(m);


				WriteMethodSignature(z, m, dStatic);


				if (z.IsInterface ||
					(m.IsAbstract && !z.IsInterface && z.IsAbstract)
				)
				{
					WriteCommentLine("abstract method");
				}
				else
				{
					WriteMethodBody(m);
				}

				WriteLine();
			}
		}











		public override void WriteMethodSignature(Type compiland, MethodBase m, bool dStatic)
		{
			WriteMethodSignature(compiland, m, dStatic, WriteMethodSignatureMode.Delcaring);
		}

		public void WriteMethodSignature(Type compiland, MethodBase m, bool dStatic, WriteMethodSignatureMode Mode)
		{

			WriteIdent();

			if (compiland.IsInterface)
			{
				if (m.IsPublic)
					WriteKeywordSpace(Keywords._public);
			}
			else
			{
				if (compiland.IsAbstract)
					if (m.IsAbstract)
					{
						if (Mode == WriteMethodSignatureMode.Delcaring)
							WriteKeywordSpace(Keywords._abstract);
					}
			}

			Write("function ");
			WriteMethodName(m);

			Write("(");
			WriteMethodParameterList(m);
			Write(")");


			if (compiland.IsInterface ||
				(m.IsAbstract && !compiland.IsInterface && compiland.IsAbstract) && (Mode == WriteMethodSignatureMode.Delcaring)
				)
				WriteLine(";");
			else
				WriteLine();
		}

		public override void WriteMethodSignature(MethodBase m, bool dStatic)
		{
			CompilerBase.BreakToDebugger("obsolete");

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


				if (ScriptParameterByValAttribute.IsDefined(m, typeof(ScriptParameterByValAttribute))
					|| ScriptParameterByValAttribute.IsDefined(m.DeclaringType, typeof(ScriptParameterByValAttribute)))
				{

				}
				else
				{
					if (ScriptParameterByRefAttribute.IsDefined(m, typeof(ScriptParameterByRefAttribute)))
					{
						WriteByRef();
					}
					else
					{
						if (Debugger.IsAttached)
							CompilerBase.WriteVisualStudioMessage(MessageType.warning, 1002, m.DeclaringType.FullName + "." + m.Name + " : consider ScriptParameterByRefAttribute");

						WriteByRef();

					}
				}

				WriteSelf();
			}

			for (int mpi = 0; mpi < mp.Length; mpi++)
			{
				if (mpi > 0 || bStatic)
				{
					Write(",");
					WriteSpace();
				}

				bool bByRef = false;

				if (mp[mpi].ParameterType.IsByRef ||
					mp[mpi].ParameterType.IsArray)
				{
					//if (ma.Implements != null)
					//{
					//    if (ma.Implements.IsByRef)
					//    {
					bByRef = true;
					//    }
					//}
				}

				if (ScriptParameterByRefAttribute.IsDefined(mp[mpi], typeof(ScriptParameterByRefAttribute))) bByRef = true;

				if (bByRef)
					WriteByRef();

				if (ma != null && ma.OptimizedCode != null && !ma.UseCompilerConstants)
					Write("$" + mp[mpi].Name);
				else
					WriteDecoratedMethodParameter(mp[mpi]);

			}
		}

		public override string GetDecoratedMethodParameter(ParameterInfo p)
		{
			return "$p" + p.Position;
		}

		private void WriteByRef()
		{
			Write("&");
		}

		public override void WriteSelf()
		{
			Write("$this");
		}





		public override void EmitPrestatement(ILBlock.Prestatement p)
		{


			#region opt-out


			if (IsOptOut(p))
				return;


			if (p.Instruction == OpCodes.Ret)
			{
				if (p.Next == null)
				{
					if (p.Instruction.StackBeforeStrict.Length == 0)
					{
						return;
					}
				}
			}
			#endregion



			#region if
			ILIfElseConstruct iif = p.Instruction.InlineIfElseConstruct;

			if (iif != null)
			{
				EmitIfBlock(p, iif);

				return;
			}
			#endregion



			WriteIdent();


			try
			{
				EmitInstruction(p, p.Instruction);
				WriteLine(";");
			}
			catch (SkipThisPrestatementException exc)
			{
				WriteLine();
			}
			catch
			{
				throw;
			}

		}

		private static bool IsOptOut(ILBlock.Prestatement p)
		{
			bool bOptOut = false;

			// nop call wont do any good
			if (p.Instruction == OpCodes.Nop)
				bOptOut = true;


			if (p.Instruction.OpCode == OpCodes.Call)
			{
				if (p.Instruction.TargetConstructor != null)
				{
					// we wont call object() constructor excplicitly
					if (p.Instruction.TargetConstructor.DeclaringType == typeof(object))
						bOptOut = true;


					if (p.Instruction.TargetConstructor.DeclaringType == p.Instruction.OwnerMethod.DeclaringType.BaseType)
					{
						ScriptAttribute a = ScriptAttribute.Of(p.Instruction.OwnerMethod.DeclaringType, true);

						// if construct type is equal to current base type and
						// we do have script attribute and we are external object
						// we skip?
						if (a != null && a.InternalConstructor)
							bOptOut = true;

					}
				}
			}
			return bOptOut;
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
					Write("catch (Exception ");
					WriteExceptionVar();
					WriteLine(")");

					EmitScope(b);
				}
				else
				{
					Write("catch (Exception ");
					WriteExceptionVar();
					WriteLine(")");
					WriteScopeBegin();

					WriteIdent();
					Write("$__" + p.Block.Clause.TryOffset);
					WriteSpace();
					Write("=");
					WriteSpace();
					WriteExceptionVar();
					WriteLine(";");

					WriteScopeEnd();
					WriteLine();

					EmitPrestatementBlock(b);

					WriteIdent();
					Write("if (isset(");
					Write("$__" + p.Block.Clause.TryOffset);
					WriteLine("))");

					WriteScopeBegin();

					WriteIdent();
					Write("throw ");
					Write("$__" + p.Block.Clause.TryOffset);
					WriteLine(";");

					WriteScopeEnd();



				}

			}
			else
			{
				return false;
			}

			return true;
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


		public override bool DoWriteStaticMethodHint
		{
			get
			{
				return true;
			}
		}


		public override void WriteTypeConstructionVerified()
		{
			Write("new stdClass()");
		}

		public override void WriteInstanceOfOperator(ILInstruction value, Type type)
		{
			EmitInstruction(null, value);

			Write(" instanceof ");

			WriteDecoratedTypeName(type);
		}
	}
}
