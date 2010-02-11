using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript;

namespace Ultra1.Common
{
	public class Class1
	{
		public static string ToString(string x, string y)
		{
			return "{ " + x + ": " + y + " }";
		}
	}

	public interface IUltraData1
	{
		string Data1 { get; set; }
	}

	public interface IUltraPolyglot
	{
		event Action Clicked;
		event Action MyLoaded;

		string Status { get; set; }
	}

	public class FieldClass1
	{
		public string FieldX;
	}
}
