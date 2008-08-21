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

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	partial class FlashTreasureHunt
	{


		KeyboardButton fKeyStrafeLeft = new uint[] { 'a', 'A' };
		KeyboardButton fKeyStrafeRight = new uint[] { 'd', 'D' };



		public void InitializeKeyboard()
		{
			var MovementArrows = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButtonGroup { Name = "Arrows" };

			Action<ManualControl> Setup =
				n =>
				{
					n.delta_acc_min = 0.03;
					n.delta_acc = n.delta_acc_min;
					n.delta_acc_acc = n.delta_acc_min * 0.01;


					n.delta_deacc_min = 0.1;
					n.delta_deacc = n.delta_deacc_min;
				};

			var StrafeLeftSmooth = AttachMovementInput(EgoView, false, false);
			Setup(StrafeLeftSmooth);
			var StrafeLeft = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementArrows[Keyboard.A],
                },
				Down = () => { StrafeLeftSmooth.down(new Point(DefaultControlWidth / 2, 0)); },
				Tick = () => { StrafeLeftSmooth.move(new Point(0, DefaultControlHeight / 2)); },
				Up = () => { StrafeLeftSmooth.up(); }
			};

			var StrafeRightSmooth = AttachMovementInput(EgoView, false, false);
			Setup(StrafeRightSmooth);
			var StrafeRight = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementArrows[Keyboard.D],
                },
				Down = () => { StrafeRightSmooth.down(new Point(DefaultControlWidth / 2, 0)); },
				Tick = () => { StrafeRightSmooth.move(new Point(DefaultControlWidth, DefaultControlHeight / 2)); },
				Up = () => { StrafeRightSmooth.up(); }
			};

			var GoLeftSmooth = AttachMovementInput(EgoView, false, false);
			Setup(GoLeftSmooth);
			var GoLeft = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementArrows[Keyboard.LEFT],
                },
				Down = () => { GoLeftSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				Tick = () => { GoLeftSmooth.move(new Point(0, DefaultControlHeight / 2)); },
				Up = () => { GoLeftSmooth.up(); }
			};

			var GoRightSmooth = AttachMovementInput(EgoView, false, false);
			Setup(GoRightSmooth);
			var GoRight = new global::FlashTreasureHunt.ActionScript.UI.KeyboardButton(stage)
			{
				Groups = new[]
			    {
			        MovementArrows[Keyboard.RIGHT],
			    },
				Down = () => { GoRightSmooth.down(new Point(DefaultControlWidth / 2, DefaultControlHeight / 2)); },
				Tick = () => { GoRightSmooth.move(new Point(DefaultControlWidth, DefaultControlHeight / 2)); },
				Up = () => { GoRightSmooth.up(); }
			};

			var GoForwardSmooth = AttachMovementInput(EgoView, false, false);
			Setup(GoForwardSmooth);
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
			Setup(GoBackwardSmooth);
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


		}
	}
}