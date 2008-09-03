using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.system;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Imaging
{
	[Script(Implements = typeof(global::System.Windows.Media.Imaging.BitmapImage))]
	internal class __BitmapImage : __BitmapSource
	{
		static readonly LoaderContext Context = new LoaderContext(true);

		public __BitmapImage(Uri uri)
		{
			var e = new flash.display.Loader();

			var url = new ScriptCoreLib.ActionScript.flash.net.URLRequest(uri.OriginalString);


			
			e.contentLoaderInfo.complete +=
				ev =>
				{
					try
					{
						var v = (Bitmap)e.content;

	

						this.InternalBitmap.Value = v;

						if (this.DownloadCompleted != null)
							this.DownloadCompleted(null, null);
					}
					catch
					{
						if (this.DownloadFailed != null)
							this.DownloadFailed(null, null);
					}
				};

			e.contentLoaderInfo.ioError +=
				ev =>
				{
					if (this.DownloadFailed != null)
						this.DownloadFailed(null, null);
				};


			this.InternalBitmap = new Future<Bitmap>();

			e.load(url, Context);

	
		}

		public override event EventHandler DownloadCompleted;

		public override event EventHandler<ExceptionEventArgs> DownloadFailed;
	}
}
