#define this_applicationWebService1

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidNFCEvents
{
    [DisplayName("nfc")]
    [DefaultEvent("onnfc")]
    public partial class ApplicationControl_onnfc : Component
    {
        // inspired by
        // X:\jsc.svn\examples\javascript\android\forms\ReinstallNotification\ReinstallNotification\ApplicationWebService_oninstall.cs

        public ApplicationControl_onnfc()
        {
            InitializeComponent();
        }

        public IApplicationWebService_poll_onnfc service { get; set; }


        public event Action<int> onnfc_syncframe;

        public event Action<string> onnfc
        {
            add
            {
                // based on X:\jsc.svn\examples\javascript\android\MultiMouse\com.abstractatech.multimouse\ApplicationWebService.sync_SelectContentUpdates.cs

                Console.WriteLine("ApplicationWebService_onnfc");

                // start polling!    

                // empty string means skip to the end
                var last_id = "";


                // we need the partial build succeed
                // so that we can add the control to toolbox
                Action invoke = async delegate
                {
                    //loop_index++;

                    //talk.innerText = "#" + loop_index + " " + new { last_id };

                    var poll = true;
                    var syncframe = 0;

                    while (poll)
                    {
                        if (onnfc_syncframe != null)
                            onnfc_syncframe(syncframe);

                        syncframe++;
                        await Task.Delay(150);

                        // what about ref?
                        var index = await service.poll_onnfc(
                                last_id: last_id,
                                yield: xml =>
                                {

                                    var xid = xml.Attribute("id").Value;

                                    Console.WriteLine();
                                    Console.WriteLine("onnfc yield " + new { xid });

                                    value(xid);
                                }
                            );

                        // in stream mode this make a while to reach here
                        last_id = index;

                        // this would cause stackoverflow, yet since we are in 
                        // clent-server "tail" call it aint.

                        poll = service != null;
                    }

                };

                invoke();

            }
            remove
            {

            }
        }

    }
}
