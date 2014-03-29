using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class File : Blob
    {
        public readonly string type;
        public readonly string name;

    }
}
