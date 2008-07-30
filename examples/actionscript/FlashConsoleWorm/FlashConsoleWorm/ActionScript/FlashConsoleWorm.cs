using System;
using System.Linq;
using System.Collections.Generic;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.events;

namespace FlashConsoleWorm.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = ColorBlack)]
	public class FlashConsoleWorm : Sprite
	{
		// based on javascript version

		// vNext should be semi 3D - http://www.freeworldgroup.com/games/3dworm/index.html


		public const uint ColorBlack = 0;
		public const uint ColorRed = 0xff0000;
		public const uint ColorGreen = 0x00ff00;

		public const int DefaultWidth = RoomWidth * DefaultZoom;
		public const int DefaultHeight = RoomHeight * DefaultZoom;

		public const int DefaultZoom = 5;

		public const int RoomWidth = 72;
		public const int RoomHeight = 48;

		public KeyboardButtonGroup MovementWASD;
		public KeyboardButtonGroup MovementArrows;
		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashConsoleWorm()
		{
			this.InvokeWhenStageIsReady(Initialize);

		}

		public Worm Ego;
		public List<Worm> Worms = new List<Worm>();
		public Sprite Canvas;

		public Point Wrapper(Point p)
		{
			return new Point
				{
					x = (p.x + RoomWidth) % RoomWidth,
					y = (p.y + RoomHeight) % RoomHeight
				};
		}

		private void Initialize()
		{
			Canvas = new Sprite();

			//s.bitmapData.setPixel(1, 1, ColorGreen);
			Canvas.scaleX = DefaultZoom;
			Canvas.scaleY = DefaultZoom;

			Canvas.AttachTo(this);

			// add scull ani here
			// add status text here


			Func<Point> GetRandomLocation =
				() => new Point
				{
					x = (RoomWidth - 1).Random(),
					y = (RoomHeight - 1).Random()
				};



			Func<Apple> CreateApple =
				   () => new Apple
				   {
					   GetRandomLocation = GetRandomLocation,
					   Wrapper = Wrapper
				   }.MoveToRandomLocation();

			var apples = new List<Apple>();

			15.Times(() =>
				CreateApple().AttachTo(Canvas).AddTo(apples)
			);

			Ego = new Worm
			{
				Wrapper = Wrapper,
				Location = GetRandomLocation(),
				Canvas = Canvas,
				Vector = Worm.VectorRight
				// Color = game_colors.worm.active
			}
			.AddTo(Worms)
			 .Grow()
			 .GrowToVector()
			 .GrowToVector();

			// var evilworm = new Worm
			// {
			//     Wrapper = Wrapper,
			//     Location = GetRandomLocation(),
			//     Canvas = canvas,
			//     Vector = new Point { x = 0, y = 1 },
			//     Color = 0x0000ff
			// }
			//.Grow()
			//.GrowToVector()
			//.GrowToVector();

			//var Worms = new List<Worm> { Ego, evilworm };

			#region keyboard

			MovementWASD = new KeyboardButtonGroup { Name = "WASD" };
			MovementArrows = new KeyboardButtonGroup { Name = "Arrows" };

			var GoLeft = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.A],
                    MovementArrows[Keyboard.LEFT],
                },
				Up = () => { Ego.Vector = Worm.VectorLeft; },
			}.Up;

			var GoRight = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.D],
                    MovementArrows[Keyboard.RIGHT],
                },
				Up = () => { Ego.Vector = Worm.VectorRight; },
			}.Up;

			var GoUp = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.W],
                    MovementArrows[Keyboard.UP],
                },
				Up = () => { Ego.Vector = Worm.VectorUp; },
			}.Up;

			var GoDown = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.S],
                    MovementArrows[Keyboard.DOWN],
                },
				Up = () => { Ego.Vector = Worm.VectorDown; },

			}.Up;

			#endregion

			Ego.VectorChanged +=
				delegate
				{
					Sounds.flag.ToSoundAsset().play();
				};
			//var info = new TextField
			//{
			//    textColor = 0xffffff,
			//    autoSize = TextFieldAutoSize.LEFT,
			//}.AttachTo(this);

			Action<MouseEvent> Go =

				e =>
				{
					#region Diaww
					var x = e.stageX;
					var y = e.stageY;


					var A = x > y;
					var B = x > (stage.height - y);

					var DiaClipLeft = !A && !B;
					var DiaClipRight = A && B;
					var DiaClipTop = A && !B;
					var DiaClipBottom = !A && B;
					#endregion

					//info.text = new { e.stageX, stage.width, e.stageY }.ToString();

					if (DiaClipLeft) GoLeft();
					else if (DiaClipRight) GoRight();
					else if (DiaClipTop) GoUp();
					else if (DiaClipBottom) GoDown();
				};

			stage.click += Go;
			stage.mouseMove +=
				e =>
				{
					if (e.buttonDown) Go(e);
				};


			Sounds.reveal.ToSoundAsset().play();

			100.AtInterval(
			   t =>
			   {
				   foreach (var p in Worms)
				   {
					   var worm = p;

					   if (worm.IsAlive)
					   {
						   if (worm.Parts.Count > 1)
							   if (Worms.Any(k => k.Parts.Any(i => i.Location.IsEqual(worm.NextLocation))))
							   {
								   Sounds.explosion.ToSoundAsset().play();




								   foreach (var v in worm.Parts)
								   {
									   v.Control.alpha = 0.5;
								   }



								   worm.IsAlive = false;

								   1000.AtDelayDo(
									   delegate
									   {
										   while (worm.Parts.Count > 1)
											   worm.Shrink();

										   Sounds.reveal.ToSoundAsset().play();

										   worm.IsAlive = true;
									   }
								   );

							   }

						   if (worm.IsAlive)
						   {
							   worm.GrowToVector();

							   // did we find an apple?
							   var a = apples.Where(i => i.Location.IsEqual(worm.Location)).ToArray();

							   if (a.Length > 0)
							   {
								   foreach (var v in a)
								   {
									   v.MoveToRandomLocation();
								   }

								   Sounds.sound20.ToSoundAsset().play();
								   // AddScore(1);
							   }
							   else
							   {
								   worm.Shrink();
							   }
						   }
					   }
				   }



			   }
			);
		}
	}
}