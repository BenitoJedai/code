using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.FileAPI
{
    [Script(HasNoPrototype = true)]
    public class FileList
    {
        public File this[uint i]
        {
            get
            {
                return default(File);
            }
        }
    }
}
