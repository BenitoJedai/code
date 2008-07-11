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


            Action<int> AddScore =
                e =>
                {
                    if (e > 0)
                    {
                        if (e < 3)
                            ShowMessage("+" + e);
                        else
                            ShowMessage("Yay! +" + e);
                    }
                    else
                        ShowMessage("Booom! -" + e);

                    Messages.AddScore(e);

                };

            var LevelCompleteCount = 0;

            Map.LevelComplete +=
                IsLocalPlayer =>
                {
                    if (IsLocalPlayer)
                    {
                        LevelCompleteCount++;

                        if (LevelCompleteCount == 3)
                            Messages.AwardCompletedThree();

                        if (LevelCompleteCount == 10)
                            Messages.AwardCompletedTen();


                        AddScore(Map.Level);
                    }
                    else
                        AddScore(2);
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
