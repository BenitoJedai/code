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
using System.Diagnostics;

namespace jsc.meta.Commands.Configuration
{
    [Description("The installer will invoke this. This is a background installer.")]
    public class ConfigurationInitialize : CommandBase
    {
        public bool AttachDebugger;

        public override void Invoke()
        {
            if (this.AttachDebugger)
                Debugger.Launch();

            try
            {
                InternalInvoke();
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message, "Error",
                     System.Windows.MessageBoxButton.OK,
                      System.Windows.MessageBoxImage.Error
                );
            }
        }

        private void InternalInvoke()
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
