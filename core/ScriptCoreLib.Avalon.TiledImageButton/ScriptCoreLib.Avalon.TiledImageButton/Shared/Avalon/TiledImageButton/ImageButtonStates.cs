using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{
	[Script]
	public class ImageButtonStates
	{
		bool InternalEnabled;
		public bool Enabled
		{
			get
			{
				return InternalEnabled;
			}
			set
			{
				InternalEnabled = value;

				InternalEnabledChanged();
			}
		}

		Action InternalEnabledChanged;

		public ImageButtonStates(UIElement Overlay, Action AsDisabled, Action AsEnabled, Action AsHot, Action AsPressed)
		{
			var IsHot = false;
			var IsDown = false;

			Overlay.MouseLeftButtonDown +=
				delegate
				{
					if (!Enabled)
						return;

					IsDown = true;
					AsPressed();
				};

			Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!Enabled)
						return;

					IsDown = false;

					if (IsHot)
						AsHot();
					else
						AsEnabled();
				};


			Overlay.MouseEnter +=
				delegate
				{
					if (!Enabled)
						return;

					IsHot = true;

					if (IsDown)
						AsPressed();
					else
						AsHot();
				};


			Overlay.MouseLeave +=
				delegate
				{
					if (!Enabled)
						return;

					IsHot = false;
					IsDown = false;

					AsEnabled();
				};

			InternalEnabledChanged = delegate
			{
				if (Enabled)
				{
					if (IsHot)
						AsHot();
					else
						AsEnabled();
				}
				else
					AsDisabled();
			};

			this.Enabled = true;

		}
	}


}
