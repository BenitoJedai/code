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
		private void WriteTypeDefPrototype(Type e)
		{
			if (IsHeaderOnlyMode)
			{
				if (e.IsAbstract && e.IsSealed)
					return;

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

	}


}
