using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Mahjong.Code;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Media;
using System.Windows;
using System.Collections;
using System.Windows.Shapes;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{

		public MahjongGameControl Map;
		public readonly Future<MahjongGameControl> MapInitialized = new Future<MahjongGameControl>();

		public readonly FutureAction<string> PlaySoundFuture = new FutureAction<string>();


		readonly Future InitializeMapDone = new Future();

		public void InitializeMap()
		{
			if (this.Map != null)
				throw new NotSupportedException();

			this.Map = new MahjongGameControlForNetwork();
			this.Map.AttachTo(Element);
			//this.Map.Navbar.Container.Visibility = Visibility.Hidden;
			this.MapInitialized.Value = this.Map;

			PlaySoundFuture.Continue(this.Map.PlaySoundFuture);


			#region MouseMove
			// we need to use a treshold and throttle too frequent updates
			var MouseMove = NumericOmitter.Of(
				(x, y) =>
				{
					this.Messages.MouseMove(x, y);

					//this.Map.DiagnosticsWriteLine("write: " + new { x, y }.ToString());
				}
			);

			this.Map.Sync_MouseMove += MouseMove;
			#endregion

			Action<string> DiagnosticsWriteLine = text => this.Map.DiagnosticsWriteLine(text);

			//#region DisplayLockLocal
			//var DisplayLockLocal = new TextBox
			//{
			//    Text = "local lock",
			//    Width = 200,
			//    Height = 20,
			//    Background = Brushes.Transparent,
			//    BorderThickness = new Thickness(0),
			//    Foreground = Brushes.Green
			//}.AttachTo(this.Map.DiagnosticsContainer).MoveTo(8, 64);

			//this.UserLock_ByLocal.Acquired +=
			//    delegate
			//    {
			//        //DiagnosticsWriteLine("UserLock_ByLocal.Acquired");
			//        DisplayLockLocal.Foreground = Brushes.Red;
			//    };

			//this.UserLock_ByLocal.Pending +=
			//    delegate
			//    {
			//        //DiagnosticsWriteLine("UserLock_ByLocal.Pending");
			//        DisplayLockLocal.Foreground = Brushes.Yellow;
			//    };

			//this.UserLock_ByLocal.Released +=
			//    delegate
			//    {
			//        //DiagnosticsWriteLine("UserLock_ByLocal.Released");
			//        DisplayLockLocal.Foreground = Brushes.Green;
			//    };
			//#endregion



			this.Map.Sync_RemovePair +=
				(a, b) =>
				{
					this.Messages.RemovePair(a, b);
				};

			this.Map.Sync_GoBack += this.Messages.GoBack;
			this.Map.Sync_GoForward += this.Messages.GoForward;


			//var SynchronizedCache = new Queue<Action<Action>>();



			this.Map.Sync_MapReloaded +=
				delegate
				{
					this.Messages.MapReload(SerializeMap());
				};


			this.Map.Sync_ScoreChangedBy += this.Messages.AddScore;
			this.Map.Sync_LocalPlayerCompletedLayout += this.Messages.AwardAchievementLayoutCompleted;

			InitializeMapDone.Signal();
		}
	}
}
