using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using Mahjong.ActionScript;
using Mahjong.Code;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using Mahjong.PromotionalAssets;

namespace Mahjong.Specialize.ActionScript
{

	[Script]
	public static class Specialize
	{

		/// <summary>
		/// This will enable sounds 
		/// </summary>
		/// <param name="e"></param>
		public static void BindToPlaySound(this FutureAction<string> e)
		{
			e.Value = global::Mahjong.ActionScript.__Assets.Default.PlaySound;
		}

		public static void AddSocialBookmarks(this MahjongGameControl u)
		{
			var x = (MahjongGameControl.DefaultScaledWidth - 62 - 16 - MahjongGameControl.CommentMargin) / 2;
			AddLink(u, "assets/Mahjong.Assets/plus_google.png", 
				x, 
				 MahjongGameControl.DefaultScaledHeight - 19 - MahjongGameControl.CommentMargin, 
				 62, 17, 
				 MahjongInfo.GoogleGadgetAddLink
			);

			AddLink(u, "assets/Mahjong.Assets/su.png",
				x + MahjongGameControl.CommentMargin + 62,
				 MahjongGameControl.DefaultScaledHeight - 19 - MahjongGameControl.CommentMargin,
				 16, 16,
				 "http://www.stumbleupon.com/submit?url=" + MahjongInfo.URL
			);
		}

		private static void AddLink(MahjongGameControl u, string src, int x, int y, int w, int h, string href)
		{
			var s = new Sprite
			{
				alpha = 0.5
			}.AttachTo(u.ToSprite());

			var img = __Assets.Default[src].ToBitmapAsset().AttachTo(s);
			s.useHandCursor = true;

			var Overlay = new Sprite
			{
				alpha = 0
			};

			Overlay.AttachTo(s);

			Overlay.graphics.beginFill(0);
			Overlay.graphics.drawRect(0, 0, w, h);
			Overlay.buttonMode = true;
			Overlay.useHandCursor = true;
			Overlay.click +=
				delegate
				{
					new URLRequest(href).NavigateTo("_blank");
					s.alpha = 0.5;
				};

			s.mouseOver +=
				delegate
				{
					s.alpha = 1;
				};

			s.mouseOut +=
				delegate
				{
					s.alpha = 0.5;
				};

			s.MoveTo(
				x,
				y
			);
		}

		public static void BindToFullScreenExclusively(this MahjongGameControl u)
		{
			Action<Sprite> Enter =
				s =>
				{
					// hide chat while fullscreen zoom
					s.GetStageChild().Siblings().ForEach(k => k.visible = false);

					var h = default(Action<FullScreenEvent>);

					h =
						e =>
						{
							if (e.fullScreen)
								return;

							s.GetStageChild().Siblings().ForEach(k => k.visible = true);

							s.stage.fullScreen -= h;
						};

					s.stage.fullScreen += h;


				};

			BindToFullScreen(u, Enter, null);
		}

		public static void BindToFullScreen(this MahjongGameControl u)
		{
			BindToFullScreen(u, null, null);
		}

		public static void BindToFullScreen(this MahjongGameControl u, Action<Sprite> Enter, Action<Sprite> Exit)
		{
			var s = u.ToSprite();

			s.InvokeWhenStageIsReady(
				delegate
				{
					//s.stage.scaleMode = StageScaleMode.NO_SCALE;

					var IsFullscreen = false;

					Action ToggleUnsafe =
						delegate
						{
							if (IsFullscreen)
							{
								s.stage.SetFullscreen(false);

								if (Exit != null)
									Exit(s);

								return;
							}

							var p = s.localToGlobal(new Point());

							s.stage.fullScreenSourceRect = new Rectangle
							{
								left = p.x,
								top = p.y,
								width = MahjongGameControl.DefaultScaledWidth,
								height = MahjongGameControl.DefaultScaledHeight
							};

							s.stage.SetFullscreen(true);

							if (Enter != null)
								Enter(s);
						};

					Action Toggle =
						delegate
						{
							try
							{
								ToggleUnsafe();
							}
							catch
							{
								u.FullscreenButton.ButtonGoFullscreen.Enabled = false;
							}
						};

					s.stage.fullScreen +=
						e =>
						{
							IsFullscreen = e.fullScreen;
						};

					u.FullscreenButton.ButtonGoFullscreen.Enabled = true;
					u.FullscreenButton.GoFullscreen += Toggle;

					s.contextMenu = new ContextMenuEx
							{
								{ "Fullscreen",  Toggle }
							};

					s.contextMenu.hideBuiltInItems();


				}
			);
		}

		public static void AddKnownEmbeddedResources()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.AddRange(__Assets.ReferencedKnownEmbeddedResources());

		}
	}
}
