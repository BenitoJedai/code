using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using LightsOut.ActionScript.Shared;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;

namespace LightsOut.ActionScript.Client
{
    partial class TeamPlay
    {
        LightsOut Map;

        public Action<string> ShowMessage;


        bool InitializeMapDone;

        private void InitializeMap()
        {
            if (InitializeMapDone)
                return;

            InitializeMapDone = true;

            stage.scaleMode = StageScaleMode.NO_SCALE;

            Map = new LightsOut();

            Map.NetworkClick += Messages.Click;

            Map.GameResetByLocalPlayer +=
                delegate
                {
                    SendMap();
                };


            var MyColor = 0xffffff.Random().ToInt32();

            Map.mouseMove +=
                e =>
                {

                    Messages.MouseMove(e.stageX.ToInt32(), e.stageY.ToInt32(), MyColor);
                };

            
            Map.AttachTo(this);
        }

    }
}
