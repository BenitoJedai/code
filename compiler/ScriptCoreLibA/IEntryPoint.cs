using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
	/// <summary>
	/// The jsc compiler will invoke specific entrypoints in the target assembly.
	/// This will enable to fill existing placeholder files with data.
	/// </summary>
	public interface IEntryPoint
	{
		void Define(string filename, string content);

		string this[string filename] { set; }
	}
}
