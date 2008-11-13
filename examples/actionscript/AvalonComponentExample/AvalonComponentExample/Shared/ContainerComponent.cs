using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace AvalonComponentExample.Shared
{
	public partial class ContainerComponent : Component, ISupportsContainer, INotifyPropertyChanged
	{
		public Canvas Container { get; private set; }
		public Canvas BackgroundContainer { get; private set; }
		public Canvas ContentContainer { get; private set; }

		public ContainerComponent()
			: this(null)
		{
		}

		public ContainerComponent(IContainer container)
		{
			if (container != null)
				container.Add(this);

			InitializeComponent();

			this.Container = new Canvas();
			this.BackgroundContainer = new Canvas().AttachTo(this.Container);
			this.ContentContainer = new Canvas().AttachTo(this.Container);

			this.Width = 200;
			this.Height = 200;
			this.BackgroundGradientStart = Colors.White;

			var a = new List<Rectangle>();

			this.UpdateBackground =
				delegate
				{
					this.BackgroundContainer.Width = this.Width;
					this.BackgroundContainer.Height = this.Height;

					this.BackgroundGradientStart.ToGradient(this.BackgroundGradientStop, this.Height / 2).ForEach(
						(c, i) =>
						{
							a.Add(
								new Rectangle
								{
									Width = this.Width,
									Height = 2,
									Fill = new SolidColorBrush(c)
								}.AttachTo(this.BackgroundContainer).MoveTo(0, i * 2)
							);
						}
					);

					this.ContentContainer.Width = this.Width;
					this.ContentContainer.Height = this.Height;
					this.ContentContainer.ClipToBounds = true;
				};

			this.BackgroundGradientStop = Colors.Blue;

		}

		Action UpdateBackground;

		public int Width
		{
			get { return Convert.ToInt32( this.Container.Width); }
			set
			{
				this.Container.Width = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Width"));

				if (UpdateBackground != null)
					UpdateBackground();
			}
		}

		public int Height
		{
			get { return Convert.ToInt32(this.Container.Height); }
			set
			{
				this.Container.Height = value;

				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Height"));

				if (UpdateBackground != null)
					UpdateBackground();
			}
		}

		System.Windows.Media.Color _BackgroundGradientStart;
		public System.Windows.Media.Color BackgroundGradientStart
		{
			get
			{
				return _BackgroundGradientStart;
			}
			set
			{
				_BackgroundGradientStart = value;

				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BackgroundGradientStart"));

				if (UpdateBackground != null)
					UpdateBackground();

			}
		}

		System.Windows.Media.Color _BackgroundGradientStop;
		public System.Windows.Media.Color BackgroundGradientStop
		{
			get
			{
				return _BackgroundGradientStop;
			}
			set
			{
				_BackgroundGradientStop = value;

				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BackgroundGradientStop"));

				if (UpdateBackground != null)
					UpdateBackground();
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
