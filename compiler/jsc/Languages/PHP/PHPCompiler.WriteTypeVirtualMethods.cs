using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

using jsc.CodeModel;

using ScriptCoreLib;

namespace jsc.Script.PHP
{

	partial class PHPCompiler
	{

		private void WriteTypeVirtualMethods(Type owner, ScriptAttribute za)
		{
			// find the virtual name or names

			if (za.HasNoPrototype)
				return;

			List<Type> t = new List<Type>(owner.GetInterfaces());


			bool b = false;

			foreach (Type x in t)
			{

				InterfaceMapping z = owner.GetInterfaceMap(x);

				int ix = 0;

				foreach (MethodInfo zm in z.TargetMethods)
				{

					MethodBase a = z.InterfaceMethods[ix];

					MethodBase u = z.TargetMethods[ix];

					// since this is php, we cannot share method bodies, we must clone them
					// wrapper? forwarder?

					WriteCommentLine(z.InterfaceType.FullName);
					WriteCommentLine(a.Name);
					WriteCommentLine(u.Name);
					// WriteMethodSignature(owner, a, false);
					// WriteMethodBody(u);

					b = true;

					ix++;
				}




			}

			if (b)
				WriteLine();

		}

	}
}
