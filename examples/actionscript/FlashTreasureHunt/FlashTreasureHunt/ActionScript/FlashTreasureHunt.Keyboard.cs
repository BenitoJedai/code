using ScriptCoreLib;
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

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	partial class FlashTreasureHunt
	{



		public void InitializeKeyboard()
		{
			var MovementArrows = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButtonGroup { Name = "Arrows" };

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







			var gunshot = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        MovementArrows[Keyboard.CONTROL]
			    },
				//Down = () => { GoBackwardSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				//Tick = () => { GoBackwardSmooth.move(new Point(DefaultControlWidth / 2, DefaultControlHeight)); },
				Up = () =>
				{

					FireWeapon();
			
				}
			};

			const int ClickRadius = 16;
			const int ClickTimeout = 400;


			stage.mouseDown +=
				e =>
				{
					// we should shoot with mouse only
					// if its a rapid click

					var clickpoint = e.ToStagePoint();

					var remove = default(Action);
						

					Action<MouseEvent> mouseUp =
						mouseUp_e =>
						{
							if (Point.distance(clickpoint, mouseUp_e.ToStagePoint()) < ClickRadius)
							{
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