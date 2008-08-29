using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashPreloader.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	[Script]
	internal static class Extensions
	{
		public static Timer AtDelayDo(this int e, Action a)
		{
			var t = new Timer(e, 1);

			t.timer += delegate { a(); };

			t.start();

			return t;
		}

		public static Timer AtInterval(this int e, Action<Timer> a)
		{
			var t = new Timer(e);

			t.timer += delegate { a(t); };

			t.start();

			return t;
		}


		public static void InvokeWhenStageIsReady(this DisplayObject o, Action a)
		{
			if (o.stage == null)
				o.addedToStage +=
					delegate
					{
						a();
					};
			else
				a();
		}


		public static void FadeOut(this DisplayObject e)
		{
			FadeOut(e, 1000 / 15, 0.1, null
				);

		}

		public static void FadeOut(this DisplayObject e, Action done)
		{
			FadeOut(e, 1000 / 15, 0.1, done
				);

		}

		public static void FadeOut(this DisplayObject e, int timeout, double step, Action done)
		{
			timeout.AtInterval(
			   t =>
			   {
				   if (e.alpha < 0.1)
				   {
					   e.alpha = 0;


					   if (done != null)
						   done();

					   t.stop();
					   return;
				   }
				   e.alpha -= step;
			   }
		   );
		}

	}
}
