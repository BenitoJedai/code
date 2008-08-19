using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.ui;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	partial class FlashTreasureHunt
	{

		KeyboardButton fKeyTurnLeft = new uint[] { Keyboard.LEFT, 'j', 'J', };
		KeyboardButton fKeyTurnRight = new uint[] { Keyboard.RIGHT, 'l', 'L', };

		KeyboardButton fKeyStrafeLeft = new uint[] { 'a', 'A' };
		KeyboardButton fKeyStrafeRight = new uint[] { 'd', 'D' };

		KeyboardButton fKeyUp = new uint[] { Keyboard.UP, 'i', 'I', 'w', 'W' };
		KeyboardButton fKeyDown = new uint[] { Keyboard.DOWN, 'k', 'K', 's', 'S' };

		public void InitializeKeyboard()
		{
			stage.keyDown +=
				e =>
				{
					var key = e.keyCode;

					fKeyStrafeLeft.ProcessKeyDown(key);
					fKeyStrafeRight.ProcessKeyDown(key);
					fKeyTurnLeft.ProcessKeyDown(key);
					fKeyTurnRight.ProcessKeyDown(key);

					fKeyUp.ProcessKeyDown(key);
					fKeyDown.ProcessKeyDown(key);


				};

			stage.keyUp +=
				e =>
				{
					var key = e.keyCode;


					fKeyStrafeLeft.ProcessKeyUp(key);
					fKeyStrafeRight.ProcessKeyUp(key);

					fKeyTurnLeft.ProcessKeyUp(key);
					fKeyTurnRight.ProcessKeyUp(key);

					fKeyUp.ProcessKeyUp(key);
					fKeyDown.ProcessKeyUp(key);

				};

			(1000 / 30).AtInterval(
					delegate
					{
						if (fKeyTurnRight.IsPressed)
							EgoView.ViewDirection += 10.DegreesToRadians();
						else if (fKeyTurnLeft.IsPressed)
							EgoView.ViewDirection -= 10.DegreesToRadians();

						if (fKeyUp.IsPressed || fKeyStrafeLeft.IsPressed || fKeyStrafeRight.IsPressed)
						{
							var d = EgoView.ViewDirection;



							if (fKeyStrafeLeft.IsPressed)
								d -= 90.DegreesToRadians();
							else if (fKeyStrafeRight.IsPressed)
								d += 90.DegreesToRadians();


							EgoView.MoveTo(
									EgoView.ViewPositionX + Math.Cos(d) * 0.2,
									EgoView.ViewPositionY + Math.Sin(d) * 0.2
							);
						}
						else if (fKeyDown.IsPressed)
							EgoView.MoveTo(
								 EgoView.ViewPositionX + Math.Cos(EgoView.ViewDirection) * -0.2,
								 EgoView.ViewPositionY + Math.Sin(EgoView.ViewDirection) * -0.2
						 );


					}
			);
		}
	}
}