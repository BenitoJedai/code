
using System;

using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;

using ScriptCoreLib;

using jsc.Script;
using jsc.Languages.JavaScript;

namespace jsc
{
	using ilbp = ILBlock.Prestatement;
	using ili = ILInstruction;
	using ilfsi = ILFlowStackItem;
	using ScriptCoreLib.Shared;
	using ScriptCoreLib.CSharp.Extensions;


	partial class IL2Script
	{
		protected static bool WriteMethodCustomBody(IdentWriter w, MethodBase m)
		{


			var EmbedGetFileNames = m.GetCustomAttributes<EmbedGetFileNamesAttribute>().FirstOrDefault();

			if (EmbedGetFileNames != null)
			{

				WriteMethodCustomBody_EmbedGetFileNames(w, m);

				return true;
			}

			return false;
		}

		private static void WriteMethodCustomBody_EmbedGetFileNames(IdentWriter w, MethodBase m)
		{
			w.WriteIdent();
			w.Write("return");
			w.Write(" ");
			w.Write("[");
			w.WriteLine();

			var a = EmbeddedResourcesExtensions.GetEmbeddedResources(null, m.DeclaringType.Assembly);



			for (int i = 0; i < a.Length; i++)
			{
				var source = a[i];

				w.WriteIdent();

				w.Write("'");
				w.WriteDecoratedLiteralString(source);
				w.Write("'");

				if (i < a.Length - 1)
					w.Write(",");

				w.WriteLine();
			}

			w.WriteIdent();
			w.Write("]");
			w.Write(";");
			w.WriteLine();
		}
	}

}