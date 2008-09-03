using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Imaging
{
	[Script(Implements = typeof(global::System.Windows.Media.Imaging.BitmapSource))]
	internal class __BitmapSource : __ImageSource
	{
		public static implicit operator __BitmapSource(BitmapSource e)
		{
			return (__BitmapSource)(object)e;
		}

		public static implicit operator BitmapSource(__BitmapSource e)
		{
			return (BitmapSource)(object)e;
		}

		//
		// Summary:
		//     Occurs when the bitmap content has been completely downloaded.
		public virtual event EventHandler DownloadCompleted
		{
			add
			{
			}
			remove
			{
				throw new NotImplementedException();
			}
		}
		//
		// Summary:
		//     Occurs when the bitmap content failed to download.
		public virtual event EventHandler<ExceptionEventArgs> DownloadFailed
		{
			add
			{
			}
			remove
			{
				throw new NotImplementedException();
			}
		}
	}

}
