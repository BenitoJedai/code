using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLibJava.BCLImplementation.System.Globalization
{
	[Script(Implements = typeof(global::System.Globalization.CultureInfo))]
	internal class __CultureInfo : __ICloneable, __IFormatProvider
	{
        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }
    }
}
