using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.WebHeaderCollection))]
    public class __WebHeaderCollection : __NameValueCollection
    {
    }
}
