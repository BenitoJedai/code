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
		Haxe,

		// Lua would be cool, only if we had our own interpreter too
		// there seems to be a lua2il compiler: http://www.lua.inf.puc-rio.br/luanet/lua2il/
		// a newer version: http://www.lua.inf.puc-rio.br/luaclr
		Lua
	}

}
