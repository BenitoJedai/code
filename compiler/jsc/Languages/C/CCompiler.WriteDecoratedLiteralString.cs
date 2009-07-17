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
		public override void WriteDecoratedLiteralString(string z)
		{
			char[] b = z.ToCharArray();

			foreach (char x in b)
				if (IsVisibleCharacter(x))
				{
					MyWriter.Write(x);
				}
				else
				{
					MyWriter.Write(@"\x{0:x2}", (byte)x);

					// it seems like C compiler needs some qoute magic
					// to break out of the hex code read mode
					// crazy huh?
					// http://www.velocityreviews.com/forums/t445817-how-to-write-a-byte-string-as-a-char.html

					this.WriteQuote();
					this.WriteQuote();
				}

		}
	}


}
