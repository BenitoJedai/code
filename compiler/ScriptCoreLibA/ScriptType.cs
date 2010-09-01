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

		// Visual Basic 6 seems to fade into irrelavancy... this language target will probably target vb.net 
		// this is currently prioritized as nice to have
		VisualBasic,
        FSharp,

		ActionScript,

		CSharp2,

		// talk about meta programming :) shall jsc support haxe?
		Haxe,

		// Lua would be cool, only if we had our own interpreter too
		// there seems to be a lua2il compiler: http://www.lua.inf.puc-rio.br/luanet/lua2il/
		// a newer version: http://www.lua.inf.puc-rio.br/luaclr
		Lua,

        /// <summary>
        /// For the WebGL
        /// </summary>
        GLSL,

        /// <summary>
        /// For WPF via DirectX
        /// </summary>
        HLSL,

        Perl,

        Ruby,

        
        ObjectiveC
	}

}
