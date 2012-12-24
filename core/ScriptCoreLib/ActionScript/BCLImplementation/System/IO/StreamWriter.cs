using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.StreamWriter))]
	internal class __StreamWriter : __TextWriter
	{
        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }
    }
}
