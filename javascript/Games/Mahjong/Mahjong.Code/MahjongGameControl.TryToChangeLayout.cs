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
		private void TryToChangeLayout(string NewLayoutComment)
		{
			this.Comment.IsReadOnly = true;
			this.CommentSuggestions.Enabled = false;

			Action Done =
				delegate
				{
					this.Comment.IsReadOnly = false;
					this.CommentSuggestions.Enabled = true;

				};

			// Player x would like to ....
			Vote("play `" + NewLayoutComment + "´",
				delegate
				{
					Done();

					SynchronizedChangeLayout(Layouts.ByComment[NewLayoutComment]);

				},
				Done
			);
		}

		public void SynchronizedChangeLayout(Layout value)
		{
			SynchronizedAsync(
					DoneLoadingNewMap =>
					{
						// new layout does indeed exist!
						MyLayout.Layout = value;

						// this is a long process actually

						MyLayout.LayoutProgress.Continue(
							delegate
							{
								if (Sync_MapReloaded != null)
									Sync_MapReloaded();

								DoneLoadingNewMap();
							}
						);
					}
				);
		}
		public Action<string, Action, Action> Sync_Vote;

		public void Vote(string Message, Action Continue, Action Abort)
		{
			if (Sync_Vote == null)
			{
				// in single player we do not need to vote
				Continue();

				return;
			}

			Sync_Vote(Message, Continue, Abort);
		}
	}
}
