using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;

namespace ScriptCoreLib.Shared.Avalon.TiledImageButton
{
	[Script]
	public class AeroNavigationBar : ISupportsContainer
	{
		public Canvas Container { get; set; }
		public readonly Image Background;
		public readonly TiledImageButtonControl ButtonGoBack;
		public readonly TiledImageButtonControl ButtonGoForward;

		public event Action GoBack;
		public event Action GoForward;

		public AeroNavigationBar()
		{
			Container = new Canvas { Width = 27 * 2 + 6, Height = 27 + 4 };

			Background = new Image
			{
				Source = "assets/ScriptCoreLib.Avalon.TiledImageButton/backMenuPic.png".ToSource(),
				Clip = new RectangleGeometry
				{
					Rect = new Rect { X = 0, Y = 0, Width = 27 * 2 + 6, Height = 27 + 4 }
				}
			}.MoveTo(-1, 0).AttachTo(Container);

			ButtonGoBack = new TiledImageButtonControl(
				"assets/ScriptCoreLib.Avalon.TiledImageButton/back-forward-large.png".ToSource(),
				 27, 27,
				 new TiledImageButtonControl.StateSelector
				 {
					 AsDisabled = s => s[0, 0],
					 AsEnabled = s => s[0, 1],
					 AsHot = s => s[0, 2],
					 AsPressed = s => s[0, 3]
				 }
			 );

			ButtonGoBack.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!this.ButtonGoBack.Enabled)
						return;

					if (this.GoBack != null)
						this.GoBack();
				};

			ButtonGoBack.Container.AttachTo(Container);

			ButtonGoForward = new TiledImageButtonControl(
				"assets/ScriptCoreLib.Avalon.TiledImageButton/back-forward-large.png".ToSource(),
				27, 27,
				 new TiledImageButtonControl.StateSelector
				 {
					 AsDisabled = s => s[1, 0],
					 AsEnabled = s => s[1, 1],
					 AsHot = s => s[1, 2],
					 AsPressed = s => s[1, 3]
				 }
			);

			ButtonGoForward.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (!this.ButtonGoForward.Enabled)
						return;

					if (this.GoForward != null)
						this.GoForward();
				};

			ButtonGoForward.Enabled = false;
			ButtonGoForward.Container.MoveTo(27, 0).AttachTo(Container);

			this.History = new HistoryInfo(this);
		}


		public readonly HistoryInfo History;

		[Script]
		public class HistoryInfo
		{
			public readonly AeroNavigationBar Owner;

			public HistoryInfo(AeroNavigationBar Owner)
			{
				this.Owner = Owner;

				this.Owner.GoBack +=
					delegate
					{
						GoBack.Pop()();
					};


				this.Owner.GoForward +=
					delegate
					{
						GoForward.Pop()();
					};

				this.Owner.ButtonGoForward.Enabled = false;
				this.Owner.ButtonGoBack.Enabled = false;
			}

			readonly Stack<Action> GoBack = new Stack<Action>();
			readonly Stack<Action> GoForward = new Stack<Action>();

			public void Add(Action BackHandler, Action ForwardHandler)
			{
				this.Owner.ButtonGoForward.Enabled = false;
				this.GoForward.Clear();

				

				var EnableGoBack = default(Action);

				EnableGoBack = 
					delegate
					{
						this.Owner.ButtonGoBack.Enabled = true;
						this.GoBack.Push(
							delegate
							{
								if (this.GoBack.Count == 0)
									this.Owner.ButtonGoBack.Enabled = false;

								BackHandler();

								this.Owner.ButtonGoForward.Enabled = true;
								GoForward.Push(
									delegate
									{
										if (this.GoBack.Count == 0)
											this.Owner.ButtonGoForward.Enabled = false;

										ForwardHandler();
										EnableGoBack();
									}
								);
							}
						);
					};

				ForwardHandler();
				EnableGoBack();
			}
		
		}

		
	}

}
