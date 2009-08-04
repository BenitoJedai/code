using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{

	/// <summary>
	/// A class can be marked to be translated into a target langage
	/// </summary>
	public enum ScriptType
	{
		Unknown,
		Java,
		JavaScript,
		PHP,
		C,
		Batch,

		VisualBasic,
		ActionScript,
		CSharp2,

		// talk about meta programming :) shall jsc support haxe?
		Haxe
	}

}
