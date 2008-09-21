using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Mahjong.Shared;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;
using ScriptCoreLib.Shared.Avalon.Cursors;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.TextSuggestions;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.Code
{
	partial class MahjongGameControl
	{

		#region Score
		public TextBox ScoreBox;

		public Action ScoreBoxGradientStop;

		int _Score;
		public int Score
		{
			get { return _Score; }
			set
			{
				if (ScoreBox == null)
				{
					this.ScoreBox = new TextBox
					{
						TextAlignment = TextAlignment.Right,
						Background = Brushes.Transparent,
						Foreground = Brushes.White,
						BorderThickness = new Thickness(0),
						Width = DefaultScaledWidth - CommentMargin * 2,
						Height = 18,
						IsReadOnly = true
					}.MoveTo(CommentMargin, DefaultScaledHeight - 18 - CommentMargin).AttachTo(CoPlayerMouseContainer);
				}

				var s = value - _Score;
				_Score = value;
				this.ScoreBox.Text = value + " points";

				if (Sync_ScoreChangedBy != null)
					Sync_ScoreChangedBy(s);

				if (ScoreBoxGradientStop != null)
					ScoreBoxGradientStop();

				if (s < 0)
					ScoreBox.Foreground = Brushes.Red;
				else
					ScoreBox.Foreground = Brushes.Yellow;


				var w = ScoreBox.Foreground.ToGradient(Brushes.White, 52).GetEnumerator();

				ScoreBox.Foreground = ScoreBox.Foreground;
				ScoreBoxGradientStop =
					1000.AtDelay(
						delegate
						{
							ScoreBoxGradientStop = (1000 / 30).AtIntervalWithTimer(
								t =>
								{
									if (w.MoveNext())
									{
										ScoreBox.Foreground = w.Current;
										return;
									}

									t.Stop();
									ScoreBoxGradientStop = null;
								}
							).Stop;
						}
					).Stop;


				#region show change
				if (s != 0)
				{
					var cy = DefaultScaledHeight - 18 - CommentMargin;
					var cx = CommentMargin;
					var c = new TextBox
					{
						TextAlignment = TextAlignment.Right,
						Background = Brushes.Transparent,
						Foreground = Brushes.Yellow,
						BorderThickness = new Thickness(0),
						Width = DefaultScaledWidth - CommentMargin * 2,
						Height = 18,
						IsReadOnly = true,
						Text = s + " points"
					}.MoveTo(cx, cy).AttachTo(CoPlayerMouseContainer);

					if (s < 0)
						c.Foreground = Brushes.Red;
					else
						c.Text = "+" + c.Text;

					var g = c.Foreground.ToGradient(Brushes.Black, 32).GetEnumerator();

					(1000 / 30).AtIntervalWithTimer(
						t =>
						{
							if (!g.MoveNext())
							{
								c.Orphanize();
								t.Stop();
								return;
							}

							c.Foreground = g.Current;
							cy--;

							c.MoveTo(cx, cy);
						}
					);

				}
				#endregion


				DiagnosticsWriteLine(this.ScoreBox.Text);
			}
		}

		public event Action<int> Sync_ScoreChangedBy;
		#endregion



	}
}
