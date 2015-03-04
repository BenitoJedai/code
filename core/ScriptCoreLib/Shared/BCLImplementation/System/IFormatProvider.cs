using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IFormatProvider))]
    public interface __IFormatProvider
    {
		// x:\jsc.svn\examples\javascript\test\test46anonymoustypetostring\test46anonymoustypetostring\class1.cs

		object GetFormat(Type formatType);
    }
}
