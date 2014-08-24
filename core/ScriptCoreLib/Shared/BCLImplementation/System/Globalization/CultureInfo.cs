using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Resources
{
    // http://referencesource.microsoft.com/#mscorlib/system/globalization/cultureinfo.cs
    [Script(Implements = typeof(global::System.Globalization.CultureInfo))]
    internal class __CultureInfo : __ICloneable, __IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }
    }
}
