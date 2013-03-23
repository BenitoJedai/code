using android.content;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;


namespace ReinstallNotification.Activities
{
    [DefaultEvent("oninstall")]
    public sealed partial class ApplicationWebService_oninstall : Component
    {

        public event Action<string> oninstall
        {
            add
            {
                // based on X:\jsc.svn\examples\javascript\android\MultiMouse\com.abstractatech.multimouse\ApplicationWebService.sync_SelectContentUpdates.cs

                Console.WriteLine("ApplicationWebService_oninstall");

                // start polling!    

                // empty string means skip to the end
                var last_id = "";

                #region async_poll_oninstall
                var loop_index = 0;
                Action loop = null;

                loop = delegate
                {
                    loop_index++;

                    //talk.innerText = "#" + loop_index + " " + new { last_id };

                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                        delegate
                        {
                            this.applicationWebService1.async_poll_oninstall(
                                last_id: last_id,
                                yield: xml =>
                                {
                                    Console.WriteLine("async_poll_oninstall yield " + new { xml });

                                    var packageName = xml.Attribute("packageName").Value;

                                    value(packageName);
                                },
                                yield_last_id:
                                    id =>
                                    {
                                        // in stream mode this make a while to reach here
                                        last_id = id;

                                        // this would cause stackoverflow, yet since we are in 
                                        // clent-server "tail" call it aint.
                                        loop();
                                    }
                            );
                        }
                    ).StartTimeout(150);
                };

                loop();
                #endregion
            }
            remove
            {

            }
        }

    }

    public static class ApplicationWebServiceClientSideExtensions
    {
        public static void async_poll_oninstall(
            this ApplicationWebService service,
            string last_id, Action<XElement> yield, Action<string> yield_last_id)
        {
            // fallback
            service.poll_oninstall(last_id, yield, yield_last_id);
        }
    }

}
