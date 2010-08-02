using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jsc.meta.Library.VolumeFunctions;
using System.IO;

namespace jsc.meta.Commands.Test
{
    public class TestVirtualDriveCleanup : CommandBase
    {
        public override void Invoke()
        {

            using (var x = new DirectoryInfo(Environment.CurrentDirectory).ToVirtualDrive())
            {
                // invoked via LoaderStrategy!
                // Configuration.ConfigurationDisposeSubst.Monitor(x);

                Environment.FailFast("Exited while working with a virtual drive - " + x.VirtualDrive);
            }
        }

    }
}
