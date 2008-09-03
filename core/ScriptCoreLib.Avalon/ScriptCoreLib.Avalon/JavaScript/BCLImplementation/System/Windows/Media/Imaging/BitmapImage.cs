using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media.Imaging
{
	[Script(Implements = typeof(global::System.Windows.Media.Imaging.BitmapImage))]
	internal class __BitmapImage : __BitmapSource
	{

		public __BitmapImage(Uri uri)
		{
			var e = new IHTMLImage();

			//var url = new ScriptCoreLib.ActionScript.flash.net.URLRequest(uri.OriginalString);


			e.InvokeOnComplete(
				img =>
				{
					this.InternalBitmap.Value = e;
				}
			);

			e.onerror +=
				ev =>
				{
					if (this.DownloadFailed != null)
						this.DownloadFailed(null, null);
				};


			this.InternalBitmap = new Future<IHTMLImage>();



			e.src = uri.OriginalString;
		}

		public override event EventHandler DownloadCompleted;

		public override event EventHandler<ExceptionEventArgs> DownloadFailed;
	}
}
