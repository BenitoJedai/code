using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IFormattable))]
    internal interface __IFormattable
    {
		// http://stackoverflow.com/questions/27493541/elvis-operator-and-string-formatter-in-c-6

		string ToString(string format, IFormatProvider formatProvider);
    }
}
