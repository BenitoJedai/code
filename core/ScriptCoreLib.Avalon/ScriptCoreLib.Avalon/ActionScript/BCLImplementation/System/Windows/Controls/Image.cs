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
				var stream = v.InternalStreamAlias;

				if (alias != null)
				{
					var c = KnownEmbeddedResources.Default[alias];

					if (c == null)
						throw new Exception("asset '" + alias + "' not found in KnownEmbeddedResources.Default.");


					InternalBitmap = c.ToBitmapAsset();

					InternalSprite.OrphanizeChildren();
					InternalSprite.addChild(InternalBitmap);
				}
				else if (stream != null)
				{
					// this is a lazy load, yet it might load later than c# wpf counterpart
					stream.ToByteArray().LoadBytes<Bitmap>(
						e =>
						{
							InternalBitmap = e;

							InternalSprite.OrphanizeChildren();
							InternalSprite.addChild(InternalBitmap);
						}
					);
				}
				

			}
		}

		Stretch InternalStretch;

		public Stretch Stretch
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				// emulate wpf stretch

				this.InternalStretch = value;
			}
		}

		public double InternalWidth;
		public double InternalHeight;

		public override void InternalSetHeight(double value)
		{
			InternalHeight = value;

			InternalUpdateStrech();
		}

		public override void InternalSetWidth(double value)
		{
			InternalWidth = value;

			InternalUpdateStrech();
		}

		private void InternalUpdateStrech()
		{
			if (InternalStretch == Stretch.None)
			{
				return;
			}

			if (InternalStretch == Stretch.Fill)
			{
				this.InternalBitmap.width = InternalWidth;
				this.InternalBitmap.height = InternalHeight;

				return;
			}
		}
	}
}
