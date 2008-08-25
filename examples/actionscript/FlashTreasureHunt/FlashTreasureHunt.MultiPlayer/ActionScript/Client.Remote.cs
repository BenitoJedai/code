using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTreasureHunt.Shared;
using FlashTreasureHunt.ActionScript.Properties;
using System.IO;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		readonly Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments> MyIdentity = new Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments>();



		public void InitializeEventsOnce()
		{
			Events.ServerPlayerHello +=
				e =>
				{
					new Handshake().VerifyArray(e.handshake);

					// Map.WriteLine("handshake ok");

					// at this point we know it is our server
					// at the other end
					// atleast it seems to knows our protocol


					Map.WriteLine("Howdy, " + e.name);




					//// now we know our player id.
					//this.MapSharedState.RemoteObjects[e.user] = this.MapSharedState.LocalObjects;


					// how long should we wait for the map?
					FirstMapLoader.Wait(3000);

					// this causes other events to be attached
					MyIdentity.Value = e;
				};

			MyIdentity.ValueChanged += InitializeOtherEvents;
		}

		public void InitializeOtherEvents()
		{
			Events.ServerPlayerJoined +=
				e =>
				{
					Map.WriteLine("joined: " + e.name);

					Messages.PlayerAdvertise(MyIdentity.Value.name);
				};

			Events.UserPlayerAdvertise +=
				e =>
				{
					Map.WriteLine("present: " + e.name);

				};

			Events.ServerPlayerLeft +=
				e =>
				{
					Map.WriteLine("left: " + e.name);

				};

			Events.ServerSendMap +=
				e =>
				{
					// we have been chosen to tell the new guy about current map

					Map.WriteLine("we sent out a map");

					var ms = new MemoryStream();
					var mw = new BinaryWriter(ms);

					var wm = Map.EgoView.Map.WallMap;

					mw.Write(wm.Values.Length);

					foreach (var v in wm.Values)
					{
						mw.Write(v);
					}

					var MemoryStream_Int32 = ms.ToArray().Select(i => (int)i).ToArray();

					Messages.SendMap(MemoryStream_Int32);
				};

			Events.UserSendMap +=
				e =>
				{
					FirstMapLoader.Signal(
						delegate
						{
							Map.WriteLine("syncing map");

							// we need to 
							Map.RemoveAllEntities();

							var wm = Map.EgoView.Map.WallMap;

							var MemoryStream_UInt8 = e.bytestream.Select(i => (byte)i).ToArray();
							var ms = new MemoryStream(MemoryStream_UInt8);
							var mr = new BinaryReader(ms);

							var Values = mr.ReadInt32();

							if (Values != wm.Values.Length)
							{
								Map.WriteLine("wrong length");
							}
							else
							{
								for (int i = 0; i < Values; i++)
								{
									wm[i] = mr.ReadUInt32();
								}

								Map.ResetEgoPosition();
							}
						}
					);
				};

		}

	}
}
