using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;
using System;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;

namespace LANMulticast.Components
{
    public sealed class MySprite1 : Sprite
    {
        // http://www.flashrealtime.com/local-flash-peer-to-peer-communication-over-lan-without-stratus/
        // http://flashrealtime.com/demos/p2pchatlocal/P2PChatLocal.mxml

        bool connected = false;


        public MySprite1()
        {
            var nc = new NetConnection();

            nc.netStatus +=
                e =>
                {
                    RaiseMessage("nc.netStatus: " + e.info.code);


                    if (e.info.code == "NetGroup.Connect.Success")
                    {
                        connected = true;
                        RaiseMessage("connected");
                        return;
                    }

                    if (e.info.code == "NetConnection.Connect.Success")
                    {
                        var groupspec = new GroupSpecifier("myGroup/groupOne");
                        groupspec.postingEnabled = true;
                        groupspec.ipMulticastMemberUpdatesEnabled = true;
                        groupspec.addIPMulticastAddress("225.225.0.1:30303");

                        var group = new NetGroup(nc, groupspec.groupspecWithAuthorizations());


                        Action<string> PostMessage =
                            message =>
                            {
                                if (connected)
                                {

                                    RaiseMessage("write: " + x);

                                    group.post(message);
                                }
                                else
                                {
                                    RaiseMessage("skip: " + x);
                                }
                            };

                        AtPostMessage += PostMessage;

                        group.netStatus +=
                            g =>
                            {
                                RaiseMessage("group.netStatus: " + g.info.code);

                                if (g.info.code == "NetGroup.Posting.Notify")
                                {
                                    // Type Coercion failed: cannot convert Object@60b6cb9 to LANMulticast_Components_MySprite1__f__AnonymousType0_1_33554444.

                                    var k = (string)g.info.message;

                                    RaiseMessage("read: " + k);
                                }
                            };

                        return;
                    }
                };

            nc.connect("rtmfp:");

        }

        public void RaiseMessage(string e)
        {
            if (AtMessage != null)
                AtMessage(e);
        }

        public event Action<string> AtMessage;

        public void PostMessage(string e)
        {
            if (AtPostMessage != null)
                AtPostMessage(e);
        }

        public event Action<string> AtPostMessage;

    }
}
