using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using FlashStratusDemo.Shared;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;

namespace FlashStratusDemo.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class FlashStratusDemo : Sprite
	{

		// http://www.adobe.com/devnet/flashplayer/articles/rtmfp_stratus_app_03.html

		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashStratusDemo()
		{

			var t = new TextField
			{
				multiline = true,
				text = "powered by jsc",
				background = true,
				x = 0,
				y = 0,
				width = 400,
				alwaysShowSelection = true,
			}.AttachTo(this);

			var c = new NetConnection();



			Func<NetStatusEvent, string> get_Code =
				e =>
				{
					var info = new DynamicContainer { Subject = e.info };
					var code = (string)info["code"];

					return code;
				};


			c.netStatus +=
				status =>
				{
					// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/NetStatusEvent.html#info


					t.appendText("\nc.netStatus: " + get_Code(status));

					if (get_Code(status) == "NetConnection.Connect.Success")
					{
						t.appendText("\n" + c.nearID);

						#region we could be a client

						var q = new TextField
						{
							background = true,
							x = 0,
							y = 200,
							width = 400,
							alwaysShowSelection = true,
							text = "enter id here",
							type = TextFieldType.INPUT
						}.AttachTo(this);


						q.change +=
							delegate
							{
								if (q.text.Length != c.nearID.Length)
									return;

								if (q.text == c.nearID)
									return;

								t.appendText("\ntarget set");
								q.Orphanize();

								var r = new NetStream(c, q.text);

								r.netStatus +=
									r_status =>
									{

										t.appendText("\nr.netStatus: " + get_Code(r_status));
									};

								r.client = new DynamicDelegatesContainer
									{
										{"handler1", 
											(string x) =>
											{
												t.appendText("\nhandler1: " + x);
												t.setSelection( t.length, t.length);
											}
										}
									}.Subject;

								r.play("stream1");
							};
						#endregion

						// yay! we are online
						var s = new NetStream(c, NetStream.DIRECT_CONNECTIONS);

						s.client = new DynamicDelegatesContainer
						{
							{"onPeerConnect", 
								(NetStream x) =>
								{
									
									t.appendText("\nonPeerConnect: " + x.farID);
									t.setSelection( t.length, t.length);

									q.Orphanize();

									return true;
								}
							}
						}.Subject;

						s.netStatus +=
							s_status =>
							{

								t.appendText("\ns.netStatus: " + get_Code(s_status));
							};

						s.publish("stream1");

						#region broadcast the data
						var counter = 0;

						5000.AtInterval(
							delegate
							{
								counter++;

								s.send("handler1", "counter = " + counter);
							}
						);
						#endregion

					}
				};




			// "Developer Key" means any license key, activation code, or similar 
			// installation, access or usage control codes, including serial numbers 
			// and electronic certificates digitally signed by Adobe, designed to 
			// uniquely identify your Developer Program and link it to you 
			// the Developer.

			// Attention: You cannot use this key in your applications.
			//c.connect("rtmfp://stratus.adobe.com/3f37a156abb67621000856d1-08d2970f1b43/");
			c.connect("rtmfp://stratus.adobe.com/3f37a156abb67621000856d1-08d2970f1b43");

		}


	}



	

}