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
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using System.Windows.Controls;

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
					var IsFullscreen = false;

					Action Toggle =
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
