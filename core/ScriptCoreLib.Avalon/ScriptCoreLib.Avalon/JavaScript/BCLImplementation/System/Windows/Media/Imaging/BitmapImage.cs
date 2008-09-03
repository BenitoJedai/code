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
		public Uri UriSource
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				var e = new IHTMLImage();

				//var url = new ScriptCoreLib.ActionScript.flash.net.URLRequest(uri.OriginalString);


				e.InvokeOnComplete(
					img =>
					{
						this.InternalBitmap.Value = e;

						if (this.DownloadCompleted != null)
							this.DownloadCompleted(null, null);
					}
				);

				e.onerror +=
					ev =>
					{
						if (this.DownloadFailed != null)
							this.DownloadFailed(null, null);
					};





				e.src = value.OriginalString;
			}
		}

		public __BitmapImage()
			: this(null)
		{

		}
		public __BitmapImage(Uri uri)
		{
			this.InternalBitmap = new Future<IHTMLImage>();
			if (uri != null)
				this.UriSource = uri;
		}

		public override event EventHandler DownloadCompleted;

		public override event EventHandler<ExceptionEventArgs> DownloadFailed;

		public void BeginInit()
		{
		}

		public void EndInit()
		{
		}
	}
}
