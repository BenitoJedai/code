
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

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
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
					WriteKeywordSpace(Keywords._protected);
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
						WriteKeywordSpace(Keywords._final);
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


    }
}
