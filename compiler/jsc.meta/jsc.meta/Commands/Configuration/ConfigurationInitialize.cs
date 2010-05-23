using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Library;
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using jsc;

namespace jsc.meta.Commands.Configuration
{
    [Description("The installer will invoke this. This is a background installer.")]
    public class ConfigurationInitialize : CommandBase
    {
        public override void Invoke()
        {
            Action<string> WithText = Text =>
            {
                Console.WriteLine(Text);
            };

            var DisposeNotification = AsNotification(value => WithText += value);

            WithText("Creating project templates...");



            new ConfigurationCreateProjectTemplates().Invoke();

            // even if we shipped with precompiled scriptcorelib's we did not
            // precompile everything :)

            // here we could have had optional demos precompiled?

            DisposeNotification();
        }

    }
}
