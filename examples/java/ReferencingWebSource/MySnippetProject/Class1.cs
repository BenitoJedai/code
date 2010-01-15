using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MySnippetProject
{
	[Description("The source code for this class is available via HTTP GET")]
	public class Class1
	{
		public string Invoke(string e)
		{
			return "hello world: " + e;
		}
	}
}
