using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.MochiLibrary
{
	[Script]
	public abstract class MochiAdPreloaderBase : PreloaderSprite
	{

		#region mochiad internals

		// are those fields still needed?

		public bool _mochiad_loaded;
		public object _mochiad;
		public object clip;
		public double origFrameRate;

		#endregion

		public string _mochiads_game_id = "";

		public MochiAdPreloaderBase()
		{
			// stage.align = StageAlign.TOP_LEFT;

			if (loaderInfo != null)
				loaderInfo.ioError +=
					 delegate
					 {
						 // Ignore event to prevent unhandled error exception
					 };

		}

		public void showPreGameAd(Action a)
		{
			showPreGameAd(a, Convert.ToInt32(width), Convert.ToInt32(height));
		}

		public virtual bool IsBackgroundVisible()
		{
			return true;
		}

		public void showPreGameAd(Action a, int width, int height)
		{
			var Options = new MochiAdOptions();

			Options.clip = this;
			Options.id = _mochiads_game_id;
			Options.res = width + "x" + height;
			Options.ad_finished = a;
			Options.no_bg = !IsBackgroundVisible();
			Options.showPreGameAd();
		}

	}
}
