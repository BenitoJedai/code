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

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		public void WalkTo()
		{

			this.Map.EgoView.ViewPositionChanged +=
				delegate
				{
					// based on time and distance block this call
					// from occurrung too often


					var ms = new MemoryStream();
					var mw = new BinaryWriter(ms);


					mw.Write(this.Map.EgoView.ViewPositionX);
					mw.Write(this.Map.EgoView.ViewPositionY);

					// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
					var MemoryStream_Int32 = ms.ToArray().Select(i => (int)i).ToArray();

					Map.WriteLine("sent WalkTo " + MemoryStream_Int32.Length);

					this.Messages.WalkTo(MemoryStream_Int32);
				};
		}

		void WalkTo(SharedClass1.RemoteEvents.UserWalkToArguments e)
		{
			var MemoryStream_UInt8 = e.bytestream.Select(i => (byte)i).ToArray();
			var ms = new MemoryStream(MemoryStream_UInt8);
			var mr = new BinaryReader(ms);

			var x = mr.ReadDouble();
			var y = mr.ReadDouble();

			// this is teleporting, we need to smooth that
			this.CoPlayers.Where(k => k.Identity.user == e.user).ForEach(k => k.Guard.Position.To(x, y));
		}
	}
}
