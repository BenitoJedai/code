using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTreasureHunt.Shared;
using FlashTreasureHunt.ActionScript.Properties;
using System.IO;
using ScriptCoreLib.ActionScript.RayCaster;

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
					FirstMapLoader.Wait(5000);

					if (e.user_with_map == -1)
						FirstMapLoader.Signal(
							delegate
							{
								Map.WriteLine("using generated map");
							}
						);

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

					var wm = Map.EgoView.Map.WorldMap;

					mw.Write(wm.Values.Length);

					uint xor = 0;

					foreach (var v in wm.Values)
					{
						xor ^= v;

						mw.Write(v);
					}

					mw.Write(xor);

					Map.WriteLine("sent xor  " + new { xor });

					// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
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

							var wm = new Texture32();
							
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
								uint xor = 0;

								for (int i = 0; i < Values; i++)
								{
									var v = mr.ReadUInt32();

									xor ^= v;

									wm[i] = v;
								}

								var xor_Expected = mr.ReadUInt32();

								if (xor == xor_Expected)
								{
									Map.WriteLine("xor ok " + new { xor, xor_Expected });

									Map.EgoView.Map.WorldMap = wm;

									Map.ResetEgoPosition();
								}
								else
								{
									Map.WriteLine("xor failed " + new { xor, xor_Expected });
								}
							}

						}
					);
				};

		}

	}
}
