using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib;
using FlashConsoleWorm.Shared;
using FlashConsoleWorm.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashConsoleWorm.ActionScript.Nonoba
{
	partial class Client
	{
		readonly Dictionary<int, string> CoPlayerNames = new Dictionary<int, string>();
		private void InitializeEvents()
		{

			var MyIdentity = default(SharedClass1.RemoteEvents.ServerPlayerHelloArguments);

			Events.ServerPlayerHello +=
				e =>
				{
					MyIdentity = e;

					ShowMessage("Howdy, " + e.name);
				};

			var RemoteEgos = new Dictionary<int, Worm>();

			Action<int, string> CreateRemoteEgo =
				(user, name) =>
				{
					// could use event instead
					// dictonary.valueset +=
					CoPlayerNames[user] = name;

					// create remote ego
					RemoteEgos[user] = new Worm
					{
						Color = 0xff,
						Canvas = Map.Canvas,
						Wrapper = Map.Wrapper,
						Location = new Point()
					}.AddTo(Map.Worms).Grow();

					ShowMessage("remote worm created - " + Map.Worms.Count);
				};

			Events.ServerPlayerJoined +=
			  e =>
			  {
				  CreateRemoteEgo(e.user, e.name);


				  ShowMessage("Player joined - " + e.name);


				  Messages.PlayerAdvertise(MyIdentity.name);
			  };

			var Cursors = new Dictionary<int, ShapeWithMovement>();


			#region ServerPlayerLeft
			Events.ServerPlayerLeft +=
				  e =>
				  {
					  if (CoPlayerNames.ContainsKey(e.user))
					  {
						  CoPlayerNames.Remove(e.user);
					  }

					  if (Cursors.ContainsKey(e.user))
					  {
						  Cursors[e.user].Orphanize();
						  Cursors.Remove(e.user);
					  }

					  ShowMessage("Player left - " + e.name);
				  }; 
			#endregion

			Events.UserPlayerAdvertise +=
				e =>
				{
					if (CoPlayerNames.ContainsKey(e.user))
						return;

					CreateRemoteEgo(e.user, e.name);

					// ShowMessage("Player already here - " + e.name);
				};


			#region MouseMove
			Events.UserMouseMove +=
					e =>
					{
						var s = default(ShapeWithMovement);

						if (Cursors.ContainsKey(e.user))
							s = Cursors[e.user];
						else
						{
							s = new ShapeWithMovement
							{
								filters = new[] { new DropShadowFilter() },
								alpha = 0.5
							};


							var g = s.graphics;

							g.beginFill((uint)e.color);
							g.moveTo(0, 0);
							g.lineTo(14, 14);
							g.lineTo(0, 20);
							g.lineTo(0, 0);
							g.endFill();

							Cursors[e.user] = s;
						};

						s.AttachTo(this).MoveTo(e.x, e.y);
					}; 
			#endregion

			Events.UserMouseOut +=
			   e =>
			   {
				   if (Cursors.ContainsKey(e.color))
				   {
					   Cursors[e.color].Orphanize();
				   }
			   };

			Events.UserVectorChanged +=
				e =>
				{
					if (RemoteEgos.ContainsKey(e.user))
					{
						RemoteEgos[e.user].Vector = new Point(e.x, e.y);
					}
				};
		}
	}
}
