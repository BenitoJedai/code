using FlashTowerDefense.ActionScript.Actors;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
//using FlashTowerDefense.ActionScript;
using ScriptCoreLib.Shared.Lambda;
using System.Xml.Linq;

namespace com.abstractatech.games.ftd
{
    public sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = 512;
        public const int DefaultHeight = 512;

        public ApplicationSprite()
        {
        }

        public int EgoID = int.MaxValue.Random();

        // gc?
        NetConnection nc;
        GroupSpecifier groupspec;
        NetGroup group;

        public void ConnectToSession()
        {
            nc = new NetConnection();
            var connected = false;

            nc.netStatus +=
             e =>
             {
                 RaiseMessageFromFlash("nc.netStatus: " + e.info.code);

                 if (e.info.code == "NetGroup.Connect.Success")
                 {
                     connected = true;
                     RaiseAtConnect();
                     return;
                 }

                 if (e.info.code == "NetConnection.Connect.Success")
                 {
                     #region NetGroup
                     // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/GroupSpecifier.html
                     var groupname = "myGroup/groupX";
                     var groupip = "225.225.0.1:30000";

                     groupspec = new GroupSpecifier(groupname);
                     groupspec.ipMulticastMemberUpdatesEnabled = true;
                     //groupspec.multicastEnabled = true;
                     groupspec.postingEnabled = true;
                     groupspec.addIPMulticastAddress(groupip);
                     //groupspec.addIPMulticastAddress("224.0.0.254");

                     // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/NetGroup.html
                     group = new NetGroup(nc, groupspec.groupspecWithAuthorizations());

                     AtMessageToFlashNetGroup +=
                         message =>
                         {
                             if (connected)
                                 group.post(message);
                         };

                     group.netStatus +=
                         g =>
                         {
                             RaiseMessageFromFlash("group.netStatus: " + g.info.code);

                             #region NetGroup.Posting.Notify
                             if (g.info.code == "NetGroup.Posting.Notify")
                             {
                                 var source = (string)g.info.message;

                                 RaiseMessageFromFlash("message:" + source);

                                 try
                                 {
                                     var xml = XElement.Parse(source);

                                     FromNetGroup(xml);
                                 }
                                 catch
                                 {

                                 }
                             }
                             #endregion

                             #region NetGroup.Neighbor.Connect
                             if (g.info.code == "NetGroup.Neighbor.Connect")
                             {
                                 // 	Sent when a neighbor connects to this node. 
                                 // The info.neighbor:String property is the group address of the neighbor. 
                                 // The info.peerID:String property is the peer ID of the neighbor.

                                 var peerID = g.info.peerID();

                                 PeerArray.Add(peerID);
                             }
                             #endregion
                         };
                     #endregion

                     RaiseMessageFromFlash("NetGroup " + new { groupname, groupip });

                 }
             };

            nc.connect("rtmfp:");


        }


        #region AtConnect
        void RaiseAtConnect()
        {
            if (AtConnect != null)
                AtConnect();
        }


        public event Action AtConnect;
        #endregion

        #region AtMessageFromFlash
        void RaiseMessageFromFlash(string e)
        {
            if (AtMessageFromFlash != null)
                AtMessageFromFlash(e);
        }

        public event Action<string> AtMessageFromFlash;
        #endregion

        #region AtMessageToFlashNetGroup
        public void RaiseMessageToFlashNetGroup(XElement e)
        {
            RaiseMessageToFlashNetGroup(e.ToString());
        }

        public void RaiseMessageToFlashNetGroup(string e)
        {
            if (AtMessageToFlashNetGroup != null)
                AtMessageToFlashNetGroup(e);
        }

        event Action<string> AtMessageToFlashNetGroup;
        #endregion

        Dictionary<string, string> Names = new Dictionary<string, string>();

        List<string> PeerArray = new List<string>();
        List<PlayerWarrior> Players = new List<PlayerWarrior>();

        FlashTowerDefense.ActionScript.FlashTowerDefenseSized Map;

        public void CreateGame()
        {
            // Init

            Map = new FlashTowerDefense.ActionScript.FlashTowerDefenseSized(DefaultWidth, DefaultHeight);

            Map.GetWarzone().With(
                s =>
                {
                    var bg = (Sprite)s;

                    bg.graphics.clear();

                    bg.graphics.beginFill(0x808080, 0.01);
                    bg.graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);

                }
            );

            #region InitializeMap
            // we need synced enemies
            Map.CanAutoSpawnEnemies = false;
            Map.ReportDaysTimer.stop();

            // silence music for testing
            Map.ToggleMusic();
            Map.TeleportEgoNearTurret();


            Map.ShowMessage("Running in multiplayer mode!");
            #endregion

            //PeerArray.WithEach(
            //    id =>
            //    {
            //        var name = "user:" + id;

            //        var n = AddNewPlayer(name);

            //        Map.ShowMessage("Player is here: " + n.NetworkName);
            //    }
            //);

            Map.EgoMovedSlowTimer.timer +=
                delegate
                {
                    RaiseMessageToFlashNetGroup(
                        new XElement("EgoMovedSlowTimer",
                             new XAttribute("EgoID", "" + this.EgoID),
                             new XAttribute("x", "" + Convert.ToInt32(Map.Ego.x)),
                             new XAttribute("y", "" + Convert.ToInt32(Map.Ego.y))
                        )
                    );
                };

            Map.AttachTo(this);
        }

        private PlayerWarrior AddNewPlayer(string name)
        {
            var n = new PlayerWarrior
            {
                CanMakeFootsteps = false,
                RunAnimation = false,
                NetworkName = name,
                x = 100 + 100.Random(),
                y = 100 + 100.Random(),
                filters = new[] { new GlowFilter(0x8080ff) }
            }.AddTo(Players).AttachTo(Map.GetWarzone());

            n.Die +=
                delegate
                {
                    if (Players.Contains(n))
                    {
                        Map.ShowMessage("One of us has died!");
                    }
                };

            return n;
        }

        public void WhenReady(Action yield)
        {
            yield();
        }


        void FromNetGroup(XElement xml)
        {
            // Y:\jsc.svn\actionscript\Games\FlashTowerDefense.Multiplayer\Client\ActionScript\Client\FlashTowerDefenseClient.Implementation.cs

            if (xml.Name.LocalName == "SetMyName")
            {
                var EgoID = Convert.ToInt32(xml.Attribute("EgoID").Value);

                RaiseMessageFromFlash(EgoID + " : " + xml.Value);

                if (!Players.Any(k => k.NetworkId == EgoID))
                {
                    AddNewPlayer(xml.Value).NetworkId = EgoID;
                }
            }

            if (xml.Name.LocalName == "EgoMovedSlowTimer")
            {
                var EgoID = Convert.ToInt32( xml.Attribute("EgoID").Value);
                var x = Convert.ToInt32(xml.Attribute("x").Value);
                var y = Convert.ToInt32(xml.Attribute("y").Value);

                Players.Where(k => k.NetworkId == EgoID).WithEach(
                    p =>
                    {
                        p.WalkTo(x, y);
                    }
                );
            }
        }



        public void SetMyName(string p)
        {
            RaiseMessageToFlashNetGroup(
                new XElement("SetMyName",
                // jsc does not yet support int :)
                    new XAttribute("EgoID", "" + this.EgoID),
                    p
               )
            );
        }
    }

    internal static class dynamic
    {
        [Script(OptimizedCode = "return e.peerID;")]
        internal static string peerID<T>(this T e) where T : NetStatusEvent.dynamic
        {
            return default(string);
        }

        [Script(OptimizedCode = "return e.messageID;")]
        internal static string messageID<T>(this T e) where T : NetStatusEvent.dynamic
        {
            return default(string);
        }
    }
}
