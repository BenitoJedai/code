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

				//InternalSprite.OrphanizeChildren();
				InternalSprite.removeChildren();

				InternalBitmap = new IHTMLImage(alias);

				InternalSprite.appendChild(InternalBitmap);
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
				this.InternalBitmap.width = Convert.ToInt32(InternalWidth);
				this.InternalBitmap.height = Convert.ToInt32(InternalHeight);

				return;
			}
		}
		
	}
}
