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


                        PostMessage =
                            message =>
                            {
                                if (connected)
                                {

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
                                RaiseMessage("group.netStatus: " + g.info.code);

                                if (g.info.code == "NetGroup.Posting.Notify")
                                {
                                    // Type Coercion failed: cannot convert Object@60b6cb9 to LANMulticast_Components_MySprite1__f__AnonymousType0_1_33554444.

                                    var source = (string)g.info.message;
                                    var xml = XElement.Parse(source);

                                    xml.Elements().Where(k => k.Name.LocalName == "BuildRocket").Select(k => (DoubleVector2)k).WithEach(
                                        k =>
                                        {
                                            that.NotifyBuildRocket(k.X, k.Y);
                                        }
                                    );

                                    xml.Elements().Where(k => k.Name.LocalName == "VisualizeTouch").Select(k => (DoubleVector2)k).WithEach(
                                        k =>
                                        {
                                            that.NotifyVisualizeTouch(k.X, k.Y);
                                        }
                                    );
                                }
                            };

                        return;
                    }
                };

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
