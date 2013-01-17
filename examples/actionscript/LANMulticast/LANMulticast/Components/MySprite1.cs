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
            nc.asyncError +=
                e =>
                {
                    RaiseMessage("nc.asyncError: " + e.errorID);
                };

            nc.ioError +=
               e =>
               {
                   RaiseMessage("nc.ioError: " + e.errorID);
               };

            nc.securityError +=
           e =>
           {
               RaiseMessage("nc.securityError: " + e.errorID);
           };

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
                        // http://forums.adobe.com/message/2774620
                        // Click on the 'Administration' tab and Enable UPnP if it is disabled.

                        // http://book.zi5.me/books/read/2473/295
                        // this does not simply work anymore???

                        var groupspec = new GroupSpecifier("myGroup/groupOne");
                        //groupspec.multicastEnabled = true;
                        groupspec.addIPMulticastAddress("239.254.254.1:30000");
                        groupspec.ipMulticastMemberUpdatesEnabled = true;
                        groupspec.postingEnabled = true;
                        //groupspec.serverChannelEnabled = true;


                        //groupspec.addIPMulticastAddress("225.225.0.1:30303");

                        var group = new NetGroup(nc, groupspec.groupspecWithAuthorizations());
                        //var group = new NetGroup(nc, groupspec.groupspecWithoutAuthorizations());


                        group.deactivate +=
                            ee =>
                            {
                                RaiseMessage("group.deactivate");
                            };

                        Action<string> PostMessage =
                            message =>
                            {
                                if (connected)
                                {

                                    RaiseMessage("write: " + message);

                                    group.post(message);
                                    group.sendToAllNeighbors(message);

                                }
                                else
                                {
                                    RaiseMessage("skip: " + message);
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

            // http://stackoverflow.com/questions/5332762/rtmfp-and-firewalls-routers
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
