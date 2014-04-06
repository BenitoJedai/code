using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.net;
using MultitouchFingerTools.Library;
using System.Xml.Linq;

namespace MultitouchFingerTools
{
    public static class ApplicationCanvasExtensionsForFlash
    {
        // this works under flas player :)

        public enum ConnectToSessionVariation
        {
            Flash
        }

        public static void ConnectToSession(this ApplicationCanvas that, ConnectToSessionVariation variation = ConnectToSessionVariation.Flash)
        {
            var nc = new NetConnection();

            var connected = false;

            Action<string> RaiseMessage =
                x =>
                {
                    that.About.Text = x + Environment.NewLine + that.About.Text;
                };

            Action<string> PostMessage =
                message =>
                {

                    RaiseMessage("drop: " + message);
                };

            #region AtNotifyBuildRocket
            that.AtNotifyBuildRocket +=
                (x, y) =>
                {
                    XElement BuildRocket = new DoubleVector2
                    {
                        X = x,
                        Y = y
                    };

                    PostMessage(
                        new XElement("Updates",
                            new XElement("BuildRocket", BuildRocket)
                       ).ToString()
                    );
                };
            #endregion


            #region AtNotifyVisualizeTouch
            that.AtNotifyVisualizeTouch +=
                 (x, y) =>
                 {
                     XElement VisualizeTouch = new DoubleVector2
                    {
                        X = x,
                        Y = y
                    };

                     PostMessage(
                         new XElement("Updates",
                             new XElement("VisualizeTouch", VisualizeTouch)
                        ).ToString()
                     );
                 };
            #endregion


            nc.netStatus +=
                e =>
                {
                    // http://stackoverflow.com/questions/10683595/cant-receive-netgroup-events

                    RaiseMessage("nc.netStatus: " + e.info.code);


                    if (e.info.code == "NetGroup.Connect.Success")
                    {
                        connected = true;
                        RaiseMessage("connected");
                        return;
                    }

                    if (e.info.code == "NetConnection.Connect.Success")
                    {
                        // http://kafkaris.com/blog/2011/04/03/local-peer-to-peer-communication-in-as3-with-rtmfp/
                        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/GroupSpecifier.html

                        var groupspec = new GroupSpecifier("goo");
                        groupspec.postingEnabled = true;
                        groupspec.routingEnabled = true;

                        // // Necessary to multicast over a NetStream. 
                        groupspec.multicastEnabled = true;

                        // // Must be enabled for LAN peer discovery to work 
                        groupspec.ipMulticastMemberUpdatesEnabled = true;

                        // http://help.adobe.com/en_US/flashmediaserver/ssaslr/WS486834a3d4bc74a45ce7a7ac126f44d8a30-8000.html
                        //groupspec.addIPMulticastAddress("225.225.0.1:30303");


                        // // Multicast address over which to exchange peer discovery. 
                        groupspec.addIPMulticastAddress("224.0.0.255:30000");


                        // Specify minimum GroupSpec version (FMS 4.5.2/Flash Player 11.5) 
                        groupspec.minGroupspecVersion = 2;

                        var group = new NetGroup(
                            nc,
                            groupspec.groupspecWithAuthorizations()
                            //groupspec.groupspecWithoutAuthorizations()
                        );

                        // http://stackoverflow.com/questions/10206097/netstream-send-not-working-with-netgroup-in-rtmfp

                        PostMessage =
                            message =>
                            {
                                if (connected)
                                {

                                    RaiseMessage("write: " + new { message.Length });
                                    //RaiseMessage("write: " + message);

                                    group.post(message);
                                }
                                else
                                {
                                    //RaiseMessage("skip: " + message);
                                }
                            };

                        //AtPostMessage += PostMessage;

                        group.netStatus +=
                            g =>
                            {

                                if (g.info.code == "NetGroup.Posting.Notify")
                                {
                                    // Type Coercion failed: cannot convert Object@60b6cb9 to LANMulticast_Components_MySprite1__f__AnonymousType0_1_33554444.

                                    var source = (string)g.info.message;

                                    RaiseMessage("group.netStatus: " + new { g.info.code, source });


                                    //Console.WriteLine("source: " + source);

                                    var xml = XElement.Parse(source);

                                    xml.Elements().Where(k => k.Name.LocalName == "BuildRocket").Elements().WithEach(
                                        ksource =>
                                        {
                                            //Console.WriteLine("BuildRocket: " + ksource);

                                            DoubleVector2 k = ksource;

                                            that.NotifyBuildRocket(k.X, k.Y);
                                        }
                                    );

                                    xml.Elements().Where(k => k.Name.LocalName == "VisualizeTouch").Elements().WithEach(
                                        ksource =>
                                        {
                                            //Console.WriteLine("VisualizeTouch: " + ksource);

                                            DoubleVector2 k = ksource;

                                            that.NotifyVisualizeTouch(k.X, k.Y);
                                        }
                                    );

                                    return;
                                }

                                RaiseMessage("group.netStatus: " + g.info.code);

                            };

                        return;
                    }
                };


            // X:\jsc.svn\examples\actionscript\FlashStratusDemo\FlashStratusDemo\ActionScript\OrcasFlashApplication.cs
            nc.connect("rtmfp:");
        }
    }


    public static class ApplicationCanvasExtensionsForOther
    {

        public enum ConnectToSessionVariation
        {
            Other
        }

        public static void ConnectToSession(this ApplicationCanvas that, ConnectToSessionVariation variation = ConnectToSessionVariation.Other)
        {

        }
    }
}
