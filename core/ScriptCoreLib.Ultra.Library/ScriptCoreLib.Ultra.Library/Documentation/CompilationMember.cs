using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public abstract class CompilationMember
	{
		protected XElement Data { get; set; }

		

		public CompilationType DeclaringType { get; private set; }


		public CompilationMember(CompilationType Context, XElement Data)
		{
			this.Data = Data;

			this.DeclaringType = Context;


		}
	}
}
