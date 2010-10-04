using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Desktop.Forms.Extensions;

namespace Designer1FormsJ
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            DesktopFormsExtensions.Launch(() => new ApplicationControl());
#else
#endif

        }
    }
}
