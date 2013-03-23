using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace ReinstallNotification
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {

        // http://stackoverflow.com/questions/7470314/receiving-package-install-and-uninstall-events

        [IntentFilter(Action = Intent.ACTION_PACKAGE_REPLACED)]
        [IntentFilter(Action = Intent.ACTION_PACKAGE_INSTALL)]
        public class AtInstall : BroadcastReceiver
        {
            public override void onReceive(Context arg0, Intent arg1)
            {
                var context = ThreadLocalContextReference.CurrentContext;

            }
        }


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

    }
}
