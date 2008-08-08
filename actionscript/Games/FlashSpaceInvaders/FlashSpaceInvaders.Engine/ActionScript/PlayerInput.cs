using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.Extensions;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class PlayerInput
	{
		public readonly KeyboardButtonGroup Cheats = "Name";
		public readonly KeyboardButtonGroup MovementWASD = "WASD";
		public readonly KeyboardButtonGroup MovementArrows = "Arrows";

		public Action StepLeft;
		public Action StepLeftEnd;
		public Action StepRight;
		public Action StepRightEnd;

		public Action FireBullet;

		public Action<double, double> SmartMoveTo;


		public readonly BooleanProperty Enabled = true;

		public PlayerInput(Stage stage, PlayerShip Ego, DisplayObject Owner)
		{



			#region Ego Movement


			// ego input
			stage.click +=
				e =>
				{
					if (!Enabled)
						return;

					var p = Owner.globalToLocal(e.ToStagePoint());

					if (SmartMoveTo != null)
						SmartMoveTo(p.x, p.y);
				};

			stage.mouseMove +=
				e =>
				{
					if (!Enabled)
						return;

					if (e.buttonDown)
					{
						var p = Owner.globalToLocal(e.ToStagePoint());

						if (SmartMoveTo != null)
							SmartMoveTo(p.x, p.y);
					}
				};


			var GoLeft = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.A],
                    MovementArrows[Keyboard.LEFT],
                },
				Filter = Enabled,
				Tick = () => this.StepLeft(),
				Up = () => this.StepLeftEnd()
			};

			var GoRight = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.D],
                    MovementArrows[Keyboard.RIGHT],
                },
				Filter = Enabled,
				Tick = () => this.StepRight(),
				Up = () => this.StepRightEnd()
			};
			#endregion


			#region EgoFire
			var DoFire = new KeyboardButton(stage, 400)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.CONTROL , KeyLocation.LEFT],
                    MovementArrows[Keyboard.RIGHT , KeyLocation.RIGHT],
                },
				Filter = Enabled,
				Tick = () => FireBullet()

			};

			stage.mouseDown +=
				delegate
				{
					if (!Enabled)
						return;

					DoFire.ForceKeyDown();
				};

			stage.mouseUp +=
				delegate
				{
					if (!Enabled)
						return;

					DoFire.ForceKeyUp();
				};
			#endregion
		}
	}
}
