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
    }
}
