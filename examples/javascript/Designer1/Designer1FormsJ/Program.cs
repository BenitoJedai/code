using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Desktop.Forms.Extensions;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;

namespace Designer1FormsJ
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            DesktopFormsExtensions.Launch(() => new ApplicationControl());
#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif

        }
    }
}
