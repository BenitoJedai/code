using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {
		/// <summary>
		/// Some parameters can be nameless which are used by delegates and these parameters are not used
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public override string GetDecoratedMethodParameter(ParameterInfo p)
		{
			if (string.IsNullOrEmpty(p.Name))
				return "__" + p.Position;

			return GetSafeLiteral(p.Name);
		}

    }
}
