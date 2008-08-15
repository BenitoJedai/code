using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Image))]
	internal class __Image : __FrameworkElement
	{
		readonly Sprite InternalSprite = new Sprite();

		Bitmap InternalBitmap;

		public override ScriptCoreLib.ActionScript.flash.display.InteractiveObject InternalGetDisplayObject()
		{
			return InternalSprite;
		}

		public override DisplayObject InternalGetOpacityTarget()
		{
			if (InternalBitmap != null)
				return InternalBitmap;

			return base.InternalGetOpacityTarget();
		}
		
		public ImageSource Source
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				
				__ImageSource v = value;

				var alias = v.InternalManifestResourceAlias;
				var c = KnownEmbeddedResources.Default[alias];

				if (c == null)
					throw new Exception("asset '" + alias + "' not found in KnownEmbeddedResources.Default.");

				InternalSprite.OrphanizeChildren();

				InternalBitmap = c.ToBitmapAsset();

				InternalSprite.addChild(InternalBitmap);
			}
		}
	}
}
