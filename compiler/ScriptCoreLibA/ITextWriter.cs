using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib
{
    public interface ITextWriter
    {
        void Write(string e);

        void WriteLine(string e);
    }
}
