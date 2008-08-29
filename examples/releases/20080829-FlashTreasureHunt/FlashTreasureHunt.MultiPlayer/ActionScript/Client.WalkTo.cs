using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlashTreasureHunt.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		public void InitializeWalkTo()
		{
			var LastPosition = new Point();

			var LastPositionReset = default(Timer);

			this.Map.EgoView.ViewPositionChanged +=
				delegate
				{
					// our ego is dead, we move as a ghost
					if (this.LocalCoPlayer.Guard == null)
						return;

					if (this.LocalCoPlayer.Guard.Health <= 0)
						return;

					if (Point.distance(LastPosition, this.Map.EgoView.ViewPosition) < FlashTreasureHunt.PlayerRadiusMargin / 2)
						return;

					LastPosition = this.Map.EgoView.ViewPosition;

					if (LastPositionReset != null)
						LastPositionReset.stop();

					LastPositionReset = 500.AtDelayDo(() => LastPosition = new Point());

					// based on time and distance block this call
					// from occurrung too often


					var ms = new MemoryStream();
					var mw = new BinaryWriter(ms);


					mw.Write(this.Map.EgoView.ViewPositionX);
					mw.Write(this.Map.EgoView.ViewPositionY);

					// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
					var MemoryStream_Int32 = ms.ToArray().Select(i => (int)i).ToArray();

					//Map.WriteLine("sent WalkTo " + MemoryStream_Int32.Length);

					this.Messages.WalkTo(MemoryStream_Int32);
				};

			var LastViewDirection = -1;

			this.Map.EgoView.ViewDirectionChanged +=
				delegate
				{
					// our ego is dead, we move as a ghost
					if (this.LocalCoPlayer.Guard == null)
						return;

					if (this.LocalCoPlayer.Guard.Health <= 0)
						return;

					var CurrentViewDirection = Convert.ToInt32(this.Map.EgoView.ViewDirection.RadiansToDegrees() / 10);

					if (CurrentViewDirection == LastViewDirection)
						return;

					LastViewDirection = CurrentViewDirection;

					//Map.WriteLine("sent LookAt ");

					this.Messages.LookAt(this.Map.EgoView.ViewDirection);
				};
		}

		void UserWalkTo(SharedClass1.RemoteEvents.UserWalkToArguments e)
		{
			var MemoryStream_UInt8 = e.bytestream.Select(i => (byte)i).ToArray();
			var ms = new MemoryStream(MemoryStream_UInt8);
			var mr = new BinaryReader(ms);

			var x = mr.ReadDouble();
			var y = mr.ReadDouble();

			this.CoPlayers.Where(k => k.Identity.user == e.user).ForEach(k => k.Guard.WalkTo(x, y));
		}

		void UserLookAt(SharedClass1.RemoteEvents.UserLookAtArguments e)
		{
			this.CoPlayers.Where(k => k.Identity.user == e.user).ForEach(k => k.Guard.Direction = e.arc);

		}

		void GuardWalkTo(int index, Point p)
		{
			var ms = new MemoryStream();
			var mw = new BinaryWriter(ms);

			mw.Write(index);
			mw.Write(p.x);
			mw.Write(p.y);

			// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
			var MemoryStream_Int32 = ms.ToArray().Select(i => (int)i).ToArray();

			//Map.WriteLine("sent GuardWalkTo " + MemoryStream_Int32.Length + " index " + index);

			this.Messages.GuardWalkTo(MemoryStream_Int32);
		}

		void UserGuardWalkTo(SharedClass1.RemoteEvents.UserGuardWalkToArguments e)
		{
			var MemoryStream_UInt8 = e.bytestream.Select(i => (byte)i).ToArray();
			var ms = new MemoryStream(MemoryStream_UInt8);
			var mr = new BinaryReader(ms);

			var index = mr.ReadInt32();
			var x = mr.ReadDouble();
			var y = mr.ReadDouble();


			var Targets = this.Map.GuardSprites.Where(k => k.ConstructorIndexForSync == index).ToArray();

			//Map.WriteLine("got UserGuardWalkTo " + MemoryStream_UInt8.Length + " " + new { index, Targets.Length });


			Targets.ForEach(k => k.WalkTo(x, y));

		}

		void UserGuardLookAt(SharedClass1.RemoteEvents.UserGuardLookAtArguments e)
		{
			this.Map.GuardSprites.Where(k => k.ConstructorIndexForSync == e.index).ForEach(k => k.Direction = e.arc);
			
		}

	}
}
