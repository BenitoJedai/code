using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    // http://dotnet.org.za/kevint/pages/Flags.aspx
    [Flags]
    public enum SerializedDataFormat
    {
        none = 0,
        json = 1,
        xml = 2,
        tlv = 4
    }

    /// <summary>
    /// Will generate an entrypoint to this class with the default value of field 'DefaultData'
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptApplicationEntryPointAttribute : Attribute
    {
        public bool IsClickOnce;

        public SerializedDataFormat Format = SerializedDataFormat.json | SerializedDataFormat.xml;
    }

}
