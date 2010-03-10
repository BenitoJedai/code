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

		internal Bitmap InternalBitmap;

		public __Image()
		{

		}

		internal bool InternalSmoothing;

		public void InternalSetSmoothing(bool value)
		{
			// http://livedocs.adobe.com/flex/3/langref/flash/display/Bitmap.html#smoothing

			InternalSmoothing = value;
			if (InternalBitmap != null)
				InternalBitmap.smoothing = value;
		}

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

					InternalUpdateStrech();
					InternalSetSmoothing(InternalSmoothing);
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

							InternalUpdateStrech();
							InternalSetSmoothing(InternalSmoothing);
						}
					);
				}
				else if (v.InternalBitmap != null)
				{
					v.InternalBitmap.Continue(
						e =>
						{
							InternalBitmap = e;

							InternalSprite.OrphanizeChildren();
							InternalSprite.addChild(InternalBitmap);

							InternalUpdateStrech();
							InternalSetSmoothing(InternalSmoothing);
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

				InternalUpdateStrech();
			}
		}

		public bool InternalWidthAssigned;
		public bool InternalHeightAssigned;
		public double InternalWidthValue = 0;
		public double InternalHeightValue = 0;

		public override double InternalGetHeight()
		{
			return InternalHeightValue;
		}

		public override double InternalGetWidth()
		{
			return InternalWidthValue;
		}


		public override void InternalSetHeight(double value)
		{
			InternalHeightAssigned = true;
			InternalHeightValue = value;

			InternalUpdateStrech();
		}

		public override void InternalSetWidth(double value)
		{
			InternalWidthAssigned = true;
			InternalWidthValue = value;

			InternalUpdateStrech();
		}

		private void InternalUpdateStrech()
		{
			if (this.InternalBitmap == null)
				return;


			if (InternalWidthAssigned)
				this.InternalBitmap.width = InternalWidthValue;

			if (InternalHeightAssigned)
				this.InternalBitmap.height = InternalHeightValue;


		}
	}
}
