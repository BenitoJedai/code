using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using System;
using System.Linq;

using FlashTowerDefense.ActionScript;

using FlashTowerDefense.Shared;
using FlashTowerDefense.ActionScript.Actors;
using System.Collections.Generic;
using FlashTowerDefense.ActionScript.Assets;

using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.Lambda;

namespace FlashTowerDefense.ActionScript.Client
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint(Width = Width, Height = Height)]
    [SWF(width = Width, height = Height)]
    public partial class FlashTowerDefenseClient : Sprite
    {
        public const int Width = FlashTowerDefense.DefaultWidth + 240;
        public const int Height = FlashTowerDefense.DefaultHeight;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashTowerDefenseClient()
        {
            if (stage == null)
                this.addedToStage +=
                    delegate
                    {
                        Initialize();
                    };
            else
                Initialize();
        }

      
     

        private void Initialize()
        {

            var c = NonobaAPI.MakeMultiplayer(stage
                //, "192.168.3.102"
                    //, "192.168.1.119"
                );


            var MyEvents = new SharedClass1.RemoteEvents();
            var MyMessages = new SharedClass1.RemoteMessages
            {
                Send = e => c.SendMessage(e.i, e.args)
            };



            var MyImplementation = new Implementation
            {
                NetworkEvents = MyEvents,
                NetworkMessages = MyMessages
            };

            MyImplementation.InitializeEvents();

            Func<Message, bool> Dispatch =
                e =>
                {
                    var type = (SharedClass1.Messages)int.Parse(e.Type);

                    if (MyEvents.Dispatch(type,
                          new SharedClass1.RemoteEvents.DispatchHelper
                          {
                              GetLength = i => e.length,
                              GetInt32 = e.GetInt,
                              GetDouble = e.GetNumber,
                              GetString = e.GetString,
                          }
                      ))
                        return true;

                    return false;
                };

            #region message
            c.Message +=
                e =>
                {
                    var Dispatched = false;

                    try
                    {
                        Dispatched = Dispatch(e.message);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("error at dispatch " + e.message.Type);

                        throw ex;
                    }

                    if (Dispatched)
                        return;

                    System.Console.WriteLine("not on dispatch: " + e.message.Type);

                };
            #endregion

            c.Disconnect +=
                 delegate
                 {
                     if (MyImplementation.Map == null)
                         throw new Exception("Cannot connect to server at the moment");

                     MyImplementation.Map.ShowMessage("Disconnected!");
                     MyImplementation.Map.CanAutoSpawnEnemies = true;

                     foreach (var v in MyImplementation.Players.ToArray())
                     {
                         v.RemoveFrom(MyImplementation.Players).AddDamage(v.Health);
                     }
                 };



            c.Init +=
                delegate
                {
                    MyImplementation.Map = new FlashTowerDefense();
                    MyImplementation.InitializeMap();
                    MyImplementation.Map.AttachTo(this);
                };

        }

  
    }
}