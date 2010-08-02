using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
    {
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
            // override?
			WriteKeywordSpace(Keywords._function);
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
			throw new NotSupportedException();
		}

    }
}
