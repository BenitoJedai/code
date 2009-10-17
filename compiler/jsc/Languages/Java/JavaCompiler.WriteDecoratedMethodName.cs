
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
		public override void WriteDecoratedMethodName(MethodBase z, bool q)
		{
			if (q)
				Write("\"");

			if (z.Name == "ToString" && !z.IsStatic)
				Write("toString");
			else if (z.Name == "Equals" && !z.IsStatic)
				Write("equals");
			else if (z.Name == "GetHashCode" && !z.IsStatic)
				Write("hashCode");
			else
			{
				// an assembly can have multiple entrypoints
				// one for .net and one for translated java
				if (z.DeclaringType.Assembly.EntryPoint == z || z.Name == "Main")
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


    }
}
