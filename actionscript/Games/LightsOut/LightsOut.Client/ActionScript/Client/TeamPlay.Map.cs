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

            Map.NetworkClick +=
                delegate
                {
                    DecreaseClickCountdown(true);
                };



            Map.GameResetByLocalPlayer +=
                delegate
                {
                    ResetClickCountdown(Map.Level);

                    SendMap();
                };




            var LevelCompleteCount = 0;

            Map.LevelComplete +=
                IsLocalPlayer =>
                {
                    if (IsLocalPlayer)
                    {
                        LevelCompleteCount++;

                        if (LevelCompleteCount < 3)
                            ShowMessage((3 - LevelCompleteCount) + " more games to the next award");
                        else if (LevelCompleteCount == 3)
                        {
                            ShowMessage("Congrats! You have completed three games!");
                            Messages.AwardCompletedThree();
                        }
                        else if (LevelCompleteCount < 10)
                            ShowMessage((10 - LevelCompleteCount) + " more games to the next award");
                        else if (LevelCompleteCount == 10)
                        {
                            ShowMessage("Congrats! You have completed ten games!");
                            Messages.AwardCompletedTen();
                        }


                        AddScore(Map.Level * LevelCompleteCount);
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

            ResetClickCountdown(Map.Level);
        }

        public void AddScore(int e)
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

        }
    }
}
