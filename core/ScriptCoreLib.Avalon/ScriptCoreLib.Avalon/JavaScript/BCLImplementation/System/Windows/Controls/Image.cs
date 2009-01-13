using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Image))]
	internal class __Image : __FrameworkElement
	{
		readonly IHTMLDiv InternalSprite = new IHTMLDiv();

		IHTMLImage InternalBitmap;

		public __Image()
		{
			InternalSprite.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			InternalSprite.style.left = "0px";
			InternalSprite.style.top = "0px";

			//InternalSprite.style.background = "red";

			InternalSprite.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
		}

		public override IHTMLElement InternalGetDisplayObject()
		{
			return InternalSprite;
		}

		public override IHTMLElement InternalGetOpacityTarget()
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

				Shared.EventHandler<IHTMLImage> Apply =
					img =>
					{
						InternalSprite.removeChildren();

						InternalBitmap = img;
						InternalBitmap.style.SetLocation(0, 0);

						InternalWidthValue = img.width;
						InternalHeightValue = img.height;

						InternalSetWidth(img.width);
						InternalSetHeight(img.height);

						InternalSprite.appendChild(InternalBitmap);
					};

				if (alias != null)
				{
					new IHTMLImage(alias).InvokeOnComplete(Apply);

				}
				else if (v.InternalBitmap != null)
				{
					v.InternalBitmap.Continue(i => i.InvokeOnComplete(Apply));

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

		public double InternalWidthValue = 200;
		public double InternalHeightValue = 200;

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
			InternalHeightValue = value;

			InternalUpdateStrech();

			this.InternalSprite.style.height = Convert.ToInt32(value) + "px";
		}

		public override void InternalSetWidth(double value)
		{
			InternalWidthValue = value;

			InternalUpdateStrech();

			this.InternalSprite.style.width = Convert.ToInt32(value) + "px";
		}

		private void InternalUpdateStrech()
		{
			if (this.InternalBitmap == null)
				return;

			this.InternalBitmap.width = Convert.ToInt32(InternalWidthValue);
			this.InternalBitmap.height = Convert.ToInt32(InternalHeightValue);
		}

	}
}
