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
	partial class CCompiler 
	{
		private void WriteTypeCastAndEmit(CodeEmitArgs e, Type tc)
		{
			Write("((");
			Write(GetDecoratedTypeName(tc, true, true));
			Write(")");
			EmitFirstOnStack(e);
			Write(")");
		}
	}


}
