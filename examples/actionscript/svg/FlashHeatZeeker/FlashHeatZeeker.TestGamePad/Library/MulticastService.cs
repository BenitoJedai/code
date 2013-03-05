using FlashHeatZeeker.Core.Library;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FlashHeatZeeker.TestGamePad.Library
{
    public static class MulticastService
    {
        public static void InitializeConnection(
            this KeySample key, bool WriteMode = true, bool ReadMode = false,
            TextField text = null,

            Action<Action<string>> yield_PostMessage = null,
            Action<XElement> yield_Notify = null
            )
        {
            //var that = new { About };

            var nc = new NetConnection();

            var connected = false;

            Action<string> RaiseMessage =
                x =>
                {
                    if (text != null)
                    {
                        //text.text = x + Environment.NewLine + text.text;
                        text.text = x;
                    }
                };

            Action<string> PostMessage =
                message =>
                {

                    RaiseMessage("drop: " + message);
                };


            if (WriteMode)
            {
                var sync = new ScriptCoreLib.ActionScript.flash.utils.Timer(1000 / 60);
                var syncid = 0;

                var was = -1;

                sync.timer += delegate
                {
                    if (key.value == was)
                        if (was == 0)
                            return;

                    syncid++;

                    PostMessage(
                        new XElement("sync",
                           new XElement("KeySample",
                               new XAttribute("value", "" + key.value),
                               new XAttribute("forcex", "" + key.forcex),
                               new XAttribute("forcey", "" + key.forcey),
                               new XAttribute("syncid", "" + syncid)
                          )

                        ).ToString()
                       );

                    was = key.value;
                };

                sync.start();
            }

            //that.AtNotifyVisualizeTouch +=
            //     (x, y) =>
            //     {
            //         XElement VisualizeTouch = new DoubleVector2
            //         {
            //             X = x,
            //             Y = y
            //         };

            //         PostMessage(
            //             new XElement("Updates",
            //                 new XElement("VisualizeTouch", VisualizeTouch)
            //            ).ToString()
            //         );
            //     };


            nc.netStatus +=
                e =>
                {
                    RaiseMessage("nc.netStatus: " + e.info.code);


                    if (e.info.code == "NetGroup.Connect.Success")
                    {
                        connected = true;
                        RaiseMessage("connected, looking for long range coms... (7sec delay) might need to reset android wifi!");
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

                                    RaiseMessage("write: " + message);

                                    group.post(message);
                                }
                                else
                                {
                                    RaiseMessage("skip: " + message);
                                }
                            };

                        if (yield_PostMessage != null)
                            yield_PostMessage(PostMessage);

                        //if (WriteMode)
                        //{
                        //    PostMessage(
                        //          new XElement("KeySample",
                        //              new XAttribute("value", "" + key.value)
                        //         ).ToString()
                        //      );
                        //}
                        //AtPostMessage += PostMessage;

                        group.netStatus +=
                            g =>
                            {

                                if (g.info.code == "NetGroup.Posting.Notify")
                                {
                                    // Type Coercion failed: cannot convert Object@60b6cb9 to LANMulticast_Components_MySprite1__f__AnonymousType0_1_33554444.

                                    var source = (string)g.info.message;

                                    //Console.WriteLine("source: " + source);
                                    RaiseMessage("source: " + source);

                                    var xml = XElement.Parse(source);

                                    //xml.Elements().Where(k => k.Name.LocalName == "BuildRocket").Elements().WithEach(
                                    //    ksource =>
                                    //    {
                                    //        //Console.WriteLine("BuildRocket: " + ksource);

                                    //        DoubleVector2 k = ksource;

                                    //        that.NotifyBuildRocket(k.X, k.Y);
                                    //    }
                                    //);

                                    if (yield_Notify != null)
                                    {
                                        yield_Notify(xml);
                                    }

                                    if (ReadMode)
                                    {
                                        //xml.Elements().Where(k => k.Name.LocalName == "KeySample").WithEach(
                                        xml.Elements("KeySample").WithEach(
                                            ksource =>
                                            {
                                                var value = int.Parse(ksource.Attribute("value").Value);
                                                var forcex = double.Parse(ksource.Attribute("forcex").Value);
                                                var forcey = double.Parse(ksource.Attribute("forcey").Value);

                                                //RaiseMessage("value: " + value);

                                                key.value = value;
                                                key.forcex = forcex;
                                                key.forcey = forcey;

                                                //Console.WriteLine("VisualizeTouch: " + ksource);
                                                //new XElement("value", "" + k.value)

                                                //DoubleVector2 k = ksource;

                                                //that.NotifyVisualizeTouch(k.X, k.Y);
                                            }
                                        );
                                    }

                                    return;
                                }

                                RaiseMessage("group.netStatus: " + g.info.code);

                            };

                        return;
                    }
                };

            nc.connect("rtmfp:");
        }
    }
}
