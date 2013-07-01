using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class FileList
    {
        public uint length;

        public File this[uint i]
        {
            get
            {
                return default(File);
            }
        }
    }
}
