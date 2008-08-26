using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTreasureHunt.Shared;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using System.IO;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		private void ReadSync(int[] bytestream)
		{
			Map.WriteLine("syncing, got " + bytestream.Length);

			// we need to 
			Map.RemoveAllEntities();

			var wm = new Texture32();

			var MemoryStream_UInt8 = bytestream.Select(i => (byte)i).ToArray();
			var ms = new MemoryStream(MemoryStream_UInt8);
			var mr = new BinaryReader(ms);

			#region read map
			var Values = mr.ReadInt32();

			if (Values != wm.Values.Length)
			{
				Map.WriteLine("wrong length");
				return;
			}
			uint xor = 0;

			for (int i = 0; i < Values; i++)
			{
				var v = mr.ReadUInt32();

				xor ^= v;

				wm[i] = v;
			}

			var xor_Expected = mr.ReadUInt32();

			if (xor != xor_Expected)
			{
				Map.WriteLine("xor failed " + new { xor, xor_Expected });
				return;
			}

			//Map.WriteLine("xor ok " + new { xor, xor_Expected });
			#endregion

			this.Map.GoldTotal = mr.ReadInt32();
			this.Map.AmmoTotal = mr.ReadInt32();
			this.Map.NonblockingTotal = mr.ReadInt32();


			#region write goal pos

			this.Map.TheGoldStack.Position.x = mr.ReadDouble();
			this.Map.TheGoldStack.Position.y = mr.ReadDouble();

			// you can pick up the end goal, once it ise revealed
			this.Map.TheGoldStack.AddTo(this.Map.GoldSprites);
			#endregion


			#region read gold

			var GoldSprites_Count = mr.ReadInt32();

			//Map.WriteLine("gold: " + GoldSprites_Count);

			for (int i = 0; i < GoldSprites_Count; i++)
			{
				var ConstructorIndexForSync = mr.ReadInt32();

				var GoldSprite_x = mr.ReadDouble();
				var GoldSprite_y = mr.ReadDouble();

				Map.InsertGoldSprite(ConstructorIndexForSync, GoldSprite_x, GoldSprite_y);
			}

			#endregion


			#region read ammo

			var AmmoSprites_Count = mr.ReadInt32();

			//Map.WriteLine("ammo: " + AmmoSprites_Count);

			for (int i = 0; i < AmmoSprites_Count; i++)
			{
				var ConstructorIndexForSync = mr.ReadInt32();

				var AmmoSprite_x = mr.ReadDouble();
				var AmmoSprite_y = mr.ReadDouble();

				Map.InsertAmmoSprite(ConstructorIndexForSync, AmmoSprite_x, AmmoSprite_y);
			}

			#endregion


			#region read ammo

			var NonblockSprites_Count = mr.ReadInt32();

			//Map.WriteLine("nonblock: " + NonblockSprites_Count);

			for (int i = 0; i < NonblockSprites_Count; i++)
			{
				var ConstructorIndexForSync = mr.ReadInt32();

				var NonblockSprite_x = mr.ReadDouble();
				var NonblockSprite_y = mr.ReadDouble();

				Map.InsertNonblockSprite(ConstructorIndexForSync, NonblockSprite_x, NonblockSprite_y);
			}

			#endregion


			#region end of stream
			var Found_SyncEndOfStream = mr.ReadUInt32();
			var Expected_SyncEndOfStream = SyncEndOfStream;

			// TODO: fix compiler bug while comparing (var uint) vs (literal uint)

			if (Found_SyncEndOfStream != Expected_SyncEndOfStream)
			{
				Map.WriteLine("invalid SyncEndOfStream: " + new { Found_SyncEndOfStream, Expected_SyncEndOfStream });

				return;
			}
			#endregion


			Map.EgoView.Map.WorldMap = wm;

			Map.ResetEgoPosition();

			#region restore coplayers

			this.CoPlayers.ForEach(k => k.Guard.AddTo(this.Map.EgoView.Sprites).AddTo(this.Map.EgoView.BlockingSprites));
			this.LocalCoPlayer.Guard.RemoveFrom(this.Map.EgoView.Sprites);
			
			#endregion

			PlayerAdvertise();
		}

	
		public const uint SyncEndOfStream = 0xF0F0F0F0;

		private void WriteSync()
		{
			

			var ms = new MemoryStream();
			var mw = new BinaryWriter(ms);

			#region write map
			var wm = Map.EgoView.Map.WorldMap;

			mw.Write(wm.Values.Length);

			uint xor = 0;

			foreach (var v in wm.Values)
			{
				xor ^= v;

				mw.Write(v);
			}

			mw.Write(xor);
			//Map.WriteLine("sent xor  " + new { xor });
			#endregion

			mw.Write(this.Map.GoldTotal);
			mw.Write(this.Map.AmmoTotal);
			mw.Write(this.Map.NonblockingTotal);

			#region write goal pos

			mw.Write(this.Map.TheGoldStack.Position.x);
			mw.Write(this.Map.TheGoldStack.Position.y);

			#endregion

			#region write gold
			// not all gold sprites should be synced,
			// for example personal pickups, and game goal
			var GoldSprites = Map.GoldSprites.Where(k => k.ConstructorIndexForSync >= 0).ToArray();

			mw.Write(GoldSprites.Length);

			foreach (var v in GoldSprites)
			{
				mw.Write(v.ConstructorIndexForSync);
				mw.Write(v.Position.x);
				mw.Write(v.Position.y);
			}
			#endregion

			#region write ammo
			var AmmoSprites = Map.AmmoSprites.Where(k => k.ConstructorIndexForSync >= 0).ToArray();

			mw.Write(AmmoSprites.Length);

			foreach (var v in AmmoSprites)
			{
				mw.Write(v.ConstructorIndexForSync);
				mw.Write(v.Position.x);
				mw.Write(v.Position.y);
			}
			#endregion

			#region write nonblock
			var NonblockSprites = Map.NonblockSprites.Where(k => k.ConstructorIndexForSync >= 0).ToArray();

			mw.Write(NonblockSprites.Length);

			foreach (var v in NonblockSprites)
			{
				mw.Write(v.ConstructorIndexForSync);
				mw.Write(v.Position.x);
				mw.Write(v.Position.y);
			}
			#endregion


			mw.Write(SyncEndOfStream);

			// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
			var MemoryStream_Int32 = ms.ToArray().Select(i => (int)i).ToArray();

			Map.WriteLine("sent " + MemoryStream_Int32.Length);

			Messages.SendMap(MemoryStream_Int32);
		}

	}
}
