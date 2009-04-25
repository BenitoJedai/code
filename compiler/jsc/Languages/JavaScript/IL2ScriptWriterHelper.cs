using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using ScriptCoreLib.CSharp.Extensions;


namespace jsc
{
	using ilbp = ILBlock.Prestatement;
	using ili = ILInstruction;
	using ilfsi = ILFlow.StackItem;
	using System.IO;

	using jsc.Languages.JavaScript;
	using ScriptCoreLib.Tools;

	public class IL2ScriptWriterHelper
	{
		readonly IdentWriter w;

		public const string Member_Prototype = "prototype";

		public void WriteAccessor()
		{
			w.Write(".");
		}

		public void WriteMetadataMember()
		{
			w.Write(ScriptCoreLib.ScriptAttribute.MetadataMember);
		}

		public void WritePrototype()
		{
			w.Write(Member_Prototype);
		}

		public IL2ScriptWriterHelper(IdentWriter w)
		{
			this.w = w;
		}

		public void WriteAssignment()
		{
			WriteOptionalSpace();
			w.Write("=");
			WriteOptionalSpace();
		}

		public void DOMCopyMember(Type o, MethodBase d, MethodBase s)
		{
			w.WriteIdent();
			w.Helper.WritePrototypeAlias(o);
			w.Helper.WriteAccessor();
			//w.WriteDecorated(o);
			//w.Write(".prototype.");
			w.WriteDecoratedMethodName(d);

			WriteAssignment();

			w.Helper.WritePrototypeAlias(o);
			w.Helper.WriteAccessor();
			//w.WriteDecorated(o);
			//w.Write(".prototype.");
			w.WriteDecoratedMethodName(s);
			WriteTerminator();
			w.WriteLine();
		}


		//public void DOMAttribute(string name, params string[] args)
		//{
		//    w.WriteIdent();
		//    w.Write("/** @attribute ");
		//    w.Write(name);

		//    if (args.Length > 0)
		//    {
		//        w.Write("(");


		//        w.Write("'" + String.Join("', '", args) + "'");

		//        w.Write(")");
		//    }

		//    w.Write(" */");
		//    w.WriteLine();

		//}

		public void WriteCreateObject()
		{
			w.Write("{}");
		}

		public void WriteCreateArray(params string[] e)
		{
			w.Write("[");

			for (int i = 0; i < e.Length; i++)
			{
				if (i > 0)
				{
					WriteDelimiter();
				}

				w.Write(e[i]);
			}

			w.Write("]");
		}

		public void DOMCreateType(string externaltarget, ilbp p, ili i, ilfsi[] s)
		{
			w.Write("new ");
			w.Write(externaltarget);
			w.Write("(");
			w.WriteParameters(p, i, s, 0, null);
			w.Write(")");
		}

		public void DOMCreateType(Type t)
		{
			w.Write("new ");
			w.WriteDecorated(t);
			w.Write("()");
		}

		public void DOMDefineVariable(MethodBase i, LocalVariableInfo loc)
		{
			w.WriteIdent();
			w.Write("var ");
			w.WriteDecorated(i, loc);
			WriteTerminator();
			w.WriteLine();
		}

		public void DOMDefineThisVariable()
		{
			w.WriteIdent();
			w.Write("var ");
			w.WriteSelf();
			WriteOptionalSpace();
			w.Write("=");
			WriteOptionalSpace();
			w.Write("this");
			WriteTerminator();
			w.WriteLine();
		}

		public void DOMDefineVariableAndInitializeNew(string n, Type t)
		{
			w.Write("var ");
			w.Write(n);
			WriteOptionalSpace();
			w.Write("=");
			WriteOptionalSpace();
			DOMCreateType(t);
		}

		public void DOMReturn()
		{
			w.Write("return");
		}

		public void DOMReturnExpression(ThreadStart a)
		{
			DOMReturn();

			if (a != null)
			{
				w.Write(" ");
				a();
			}


		}

		public void DOMReturnVariable(string n)
		{
			DOMReturnExpression(
				delegate { w.Write(n); }
			);

		}

		public void DOMStatement(params ThreadStart[] e)
		{
			foreach (ThreadStart z in e)
			{
				w.WriteIdent();

				z();

				WriteTerminator();

				w.WriteLine();
			}
		}

		/// <summary>
		/// should be optional, and considered as a packing option
		/// </summary>
		public void WriteOptionalSpace()
		{
			w.WriteSpace();
		}

		public void DOMWriteTerminator(params ThreadStart[] e)
		{
			foreach (ThreadStart z in e)
			{
				z(); WriteTerminator();
			}
		}

		public void WriteOptionalIdent()
		{
			w.WriteIdent();
		}

		public void WriteOptionalNewline()
		{
			w.WriteLine();
		}

		public void WriteTerminator()
		{
			w.Write(";");
		}

		public void DOMInvokeMethod(string o,
			ILBlock.Prestatement p, ILInstruction i, ILFlow.StackItem[] s, int offset, MethodBase m)
		{

			w.Write(o);
			w.Write(".");
			w.WriteDecoratedMemberInfo(m);
			w.Write("(");
			w.WriteParameters(p, i, s, 0, m);
			w.Write(")");
		}

		public void DOMDefineNamedType(Type t)
		{
			w.WriteIdent();
			w.Write("function ");
			w.WriteDecorated(t);
			w.Write("()");
			WriteOptionalSpace();
			w.Write("{}");
		}

		public void DOMWriteCatchParameter()
		{
			w.Write("__exc");
		}


		public void DOMDefineNamedType(Type t, Type basetype)
		{
			w.WriteCommentLine(t.FullName);

			w.WriteIdent();
			w.Write("function ");
			w.WriteDecorated(t);
			w.WriteLine("(){};");

			var sa = ScriptCoreLib.ScriptAttribute.Of(t);


			w.WriteMemberAssignment(IdentWriter.GetGUID64(t),
				new
				{
					TypeName = IdentWriter.GetSafeLiteral(
						// show original typename via reflection
						// instead of the type that implements it
						// in current language
						(sa != null ? sa.Implements ?? t : t).Name
					),
					Assembly = (LiteralString)IdentWriter.GetGUID64(t.Assembly.ManifestModule.ModuleVersionId)
				}
			);

			/*
			w.WriteIdent();
			w.WriteDecorated(t);
			w.Helper.WriteAccessor();
			w.Write("TypeName");
			w.Helper.WriteAssignment();

			w.WriteQoute(true, () => w.WriteSafeLiteral(t.Name));
			w.WriteLine(";");
			*/

			//w.WriteBeginScope();

			//WriteBaseConstructorCallIfAny(basetype);

			//w.EndScopeAndTerminate();
			//w.WriteLine();
		}

		public void WritePrototypeAlias(Type z)
		{
			w.Write("type$");
			w.WriteDecorated(z);
		}
		//public void WriteBaseConstructorCallIfAny(Type basetype)
		//{
		//    if (basetype != typeof(object) && basetype != null)
		//    {
		//        w.WriteIdent();

		//        w.Write(IL2Script.CopyMembers);
		//        w.Write("(this, ");
		//        DOMCreateType(basetype);
		//        w.WriteLine(");");

		//        //DOMDefineVariableAndInitializeNew("__base", basetype);
		//        //WriteTerminator();

		//        //w.WriteIdent();
		//        //w.WriteLine("for (var i in __base) if (this[i] == void(0)) this[i] = __base[i]; ");
		//        //w.WriteIdent();

		//        //w.WriteLine("if (this['toString'] == void(0)) this['toString'] = __base['toString']; ");
		//    }
		//}

		public void DOMAnonymousMethodCall(MethodBase i)
		{

			w.WriteIdent();
			w.Write("(");
			w.Write("function");
			w.WriteHint(i.DeclaringType.FullName + "." + i.Name);
			w.Write("()");
			w.WriteLine();

			w.WriteBeginScope();

			IL2Script.EmitBody(w, i, false);

			w.WriteEndScope();

			w.WriteIdent();
			w.Write(")");
			w.Write("()");

		}

		public void DOMCallScopeAsFunction(params ThreadStart[] e)
		{
			w.Write("(");
			w.Write("(");
			w.Write("function");
			w.Write("()");
			w.Write("{");
			this.DOMWriteTerminator(e);
			w.Write("}");
			w.Write(")");
			w.Write("()");
			w.Write(")");
		}


		public void WriteDelimiter()
		{
			w.Write(",");

			WriteOptionalSpace();
		}

		public void WriteBaseTypeConstructor(Type z)
		{
			w.Write("basector$");
			w.WriteDecoratedType(z, false);
		}




		public void WriteWrappedConstructor(ConstructorInfo zc)
		{
			w.Write("ctor$");
			w.WriteDecoratedMemberInfo(zc);
		}


		public void DOMCreateAndInvokeConstructor(
			Type type, MethodBase ctor,
			ILBlock.Prestatement p, ILInstruction i, ILFlow.StackItem[] s)
		{
			// fixme: call ctor$...

			w.Write("new ");

			this.WriteWrappedConstructor((ConstructorInfo)ctor);

			w.Write("(");
			w.WriteParameters(p, i, s, 0, ctor);
			w.Write(")");


		}

		public void DefineAndAssignPrototype(Type z)
		{
			w.WriteIdent();
			w.Write("var ");
			w.Helper.WritePrototypeAlias(z);
			w.Helper.WriteAssignment();
			w.WriteDecorated(z);
			w.Helper.WriteAccessor();
			w.Helper.WritePrototype();

			var BaseType = w.Session.ResolveImplementation(z.BaseType) ?? z.BaseType;
			var ScriptAttribute = BaseType.ToScriptAttribute();

			if (BaseType != null && BaseType != typeof(object))
				if (ScriptAttribute != null)
					if (!ScriptAttribute.IsNative)
						if (!ScriptAttribute.HasNoPrototype)
							if (ScriptAttribute.Implements != typeof(object))
							{
								// we are now supporing the as and is operators


								w.Helper.WriteAssignment();

								w.Write("new");
								w.WriteSpace();
								w.WriteDecorated(BaseType);
								w.Write("()");

							}
			w.WriteLine(";");

			w.WriteIdent();
			w.Helper.WritePrototypeAlias(z);
			w.Helper.WriteAccessor();
			w.Write("constructor");
			w.Helper.WriteAssignment();
			w.WriteDecorated(z);
			w.WriteLine(";");

		}

		public void DefineTypeInheritanceConstructor(Type z, Type basetype)
		{
			w.WriteIdent();
			w.Write("var ");
			w.Helper.WriteBaseTypeConstructor(z);
			w.Helper.WriteAssignment();
			w.Write("$ctor$");
			w.Write("(");

			if (basetype != null && basetype != typeof(object))
			{
				Type t = w.Session.ResolveImplementation(basetype) ?? basetype;

				//w.Write("/* " + t.FullName + " */");

				w.Helper.WriteBaseTypeConstructor(t);
			}
			else
			{
				w.Write("null");
			}

			w.Write(", null, ");
			w.Helper.WritePrototypeAlias(z);
			w.WriteLine(");");
		}

		public void DefineTypeInheritanceConstructor(Type z, ConstructorInfo zc, Type basetype)
		{
			w.WriteIdent();
			w.Write("var ");
			w.Helper.WriteWrappedConstructor(zc);

			if (zc.GetParameters().Length == 0)
			{
				// this supports the Activator.CreateInstance function
				// prototype.constructor.ctor = Activator Constructor with no params

				w.Helper.WriteAssignment();

				w.Write(IdentWriter.GetGUID64(z.GUID));
				w.Helper.WriteAccessor();
				w.Write("ctor");

			}

			w.Helper.WriteAssignment();
			w.Write("$ctor$");
			w.Write("(");

			if (basetype != null && basetype != typeof(object))
			{
				Type t = w.Session.ResolveImplementation(basetype) ?? basetype;

				//w.Write("/* " + t.FullName + " */");

				w.Helper.WriteBaseTypeConstructor(t);
			}
			else
			{
				w.Write("null");
			}

			if (zc == null)
				w.Write(", null, ");
			else
			{
				w.Write(", '");
				w.WriteDecoratedMethodName(zc);
				w.Write("', ");
			}

			w.Helper.WritePrototypeAlias(z);
			w.WriteLine(");");
		}

		public void DefineTypeMemberMethodHeader(Type z, MethodBase zc)
		{
			w.WriteIdent();
			w.Helper.WritePrototypeAlias(z);
			w.Helper.WriteAccessor();
			w.WriteDecoratedMemberInfo(zc);
			w.Helper.WriteAssignment();
			w.Write("function ");
			w.Write("(");
			w.WriteDecoratedParameterArray(zc.GetParameters());
			w.Write(")");
			w.WriteLine();
		}

		public void DefineTypeMemberMethodAs(Type z, MethodBase zc, MethodBase target)
		{
			w.WriteIdent();
			w.Helper.WritePrototypeAlias(z);
			w.Helper.WriteAccessor();
			w.WriteDecoratedMemberInfo(zc);
			w.Helper.WriteAssignment();
			w.Helper.WritePrototypeAlias(target.DeclaringType);
			w.Helper.WriteAccessor();
			w.WriteDecoratedMemberInfo(target);
			w.WriteLine(";");

		}
	}
}
