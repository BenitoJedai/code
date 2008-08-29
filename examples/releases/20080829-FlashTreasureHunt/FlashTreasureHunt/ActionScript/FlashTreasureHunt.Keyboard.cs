﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.events;
using FlashTreasureHunt.ActionScript.UI;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	partial class FlashTreasureHunt
	{

		KeyboardButtonGroup MovementArrows = new KeyboardButtonGroup { Name = "Arrows" };
		KeyboardButtonGroup Keyboard_Settings = new KeyboardButtonGroup { Name = "Settings" };
		KeyboardButtonGroup Keyboard_Cheats = new KeyboardButtonGroup { Name = "Cheats" };

		Action Smooth_DisableJoin;

		public void InitializeKeyboard()
		{
			

			Action<double, ManualControl> Setup =
				(m, n) =>
				{
					n.delta_acc_min = 0.03 * m;
					n.delta_acc = n.delta_acc_min;
					n.delta_acc_acc = n.delta_acc_min * 0.01;


					n.delta_deacc_min = 0.4 * m;
					n.delta_deacc = n.delta_deacc_min;
				};

			#region strafe
			var StrafeLeftSmooth = AttachMovementInput(EgoView, false, false);
			Setup(1, StrafeLeftSmooth);
			var StrafeLeft = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementArrows[Keyboard.A],
                },
				Down = () => { StrafeLeftSmooth.down(new Point(DefaultControlWidth / 2, 0)); },
				Tick = () => { StrafeLeftSmooth.move(new Point(0, 0)); },
				Up = () => { StrafeLeftSmooth.up(); }
			};
			Smooth_DisableJoin += () => StrafeLeftSmooth.disable_join = true; 

			var StrafeRightSmooth = AttachMovementInput(EgoView, false, false);
			Setup(1, StrafeRightSmooth);
			var StrafeRight = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementArrows[Keyboard.D],
                },
				Down = () => { StrafeRightSmooth.down(new Point(DefaultControlWidth / 2, 0)); },
				Tick = () => { StrafeRightSmooth.move(new Point(DefaultControlWidth, 0)); },
				Up = () => { StrafeRightSmooth.up(); }
			};
			Smooth_DisableJoin += () => StrafeRightSmooth.disable_join = true;
			#endregion

			#region turn
			var GoLeftSmooth = AttachMovementInput(EgoView, false, false);
			Setup(8, GoLeftSmooth);
			var GoLeft = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementArrows[Keyboard.LEFT],
                },
				Down = () => { GoLeftSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				Tick = () => { GoLeftSmooth.move(new Point(DefaultControlWidth / 4, DefaultControlHeight / 2)); },
				Up = () => { GoLeftSmooth.up(); }
			};

			var GoRightSmooth = AttachMovementInput(EgoView, false, false);
			Setup(8, GoRightSmooth);
			var GoRight = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        MovementArrows[Keyboard.RIGHT],
			    },
				Down = () => { GoRightSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				Tick = () => { GoRightSmooth.move(new Point(DefaultControlWidth * 3 / 4, DefaultControlHeight / 2)); },
				Up = () => { GoRightSmooth.up(); }
			};
			#endregion


			var GoForwardSmooth = AttachMovementInput(EgoView, false, false);
			Setup(1, GoForwardSmooth);
			var GoForward = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        MovementArrows[Keyboard.UP],
			        MovementArrows[Keyboard.W],
			    },
				Down = () => { GoForwardSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				Tick = () => { GoForwardSmooth.move(new Point(DefaultControlWidth / 2, 0)); },
				Up = () => { GoForwardSmooth.up(); }
			};
			Smooth_DisableJoin += () => GoForwardSmooth.disable_join = true; 

			var GoBackwardSmooth = AttachMovementInput(EgoView, false, false);
			Setup(1, GoBackwardSmooth);
			var GoBackward = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        MovementArrows[Keyboard.DOWN],
			        MovementArrows[Keyboard.S],
			    },
				Down = () => { GoBackwardSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				Tick = () => { GoBackwardSmooth.move(new Point(DefaultControlWidth / 2, DefaultControlHeight)); },
				Up = () => { GoBackwardSmooth.up(); }
			};
			Smooth_DisableJoin += () => GoBackwardSmooth.disable_join = true; 







			var gunshot = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        MovementArrows[Keyboard.CONTROL],
			        MovementArrows[Keyboard.SPACE]
			    },
				//Down = () => { GoBackwardSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				Tick = () => 
				{ 
					FireWeapon();
				},
				Up = () =>
				{

					FireWeapon();

				}
			};

			var suicide = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        MovementArrows[Keyboard.K],
			    },
				Up = () =>
				{

					if (MovementEnabled)
						if (Sync_Suicide != null)
							Sync_Suicide();


				}
			};

			var endlevel = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        Keyboard_Cheats[Keyboard.U],
			    },
				Up = () =>
				{

					if (MovementEnabled)
					{
						this.WriteLine("Cheat -> EnterEndLevelMode");

						EnterEndLevelMode();
					}
				}
			};

			const int ClickRadius = 16;
			const int ClickTimeout = 400;


			stage.mouseDown +=
				e =>
				{
					if (!MovementEnabled)
						return;

					// we should shoot with mouse only
					// if its a rapid click

					var clickpoint = e.ToStagePoint();

					var remove = default(Action);


					Action<MouseEvent> mouseUp =
						mouseUp_e =>
						{
							if (Point.distance(clickpoint, mouseUp_e.ToStagePoint()) < ClickRadius)
							{
								if (MovementEnabled)
									FireWeapon();
							}

							remove();
						};

					remove =
						delegate
						{
							if (mouseUp == null)
								return;

							stage.mouseUp -= mouseUp;
							mouseUp = null;
						};

					stage.mouseUp += mouseUp;

					ClickTimeout.AtDelayDo(remove);

				};
		}
	}
}