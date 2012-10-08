using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace org.elasticdroid
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            //[dx] If you really intend to build a core library -- which is only
            //[dx] appropriate as part of creating a full virtual machine
            //[dx] distribution, as opposed to compiling an application -- then use
            //[dx] the "--core-library" option to suppress this error message.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/20120807

            global::jsc.AndroidLauncher.Launch(
                 typeof(ApplicationActivity)
            );
        }
    }
}
