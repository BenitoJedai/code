using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace TextScreenSaver
{
    interface IAssemblyReferenceToken :
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
    }

}
