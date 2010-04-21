using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using UltraRaycast6.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.display;
using UltraRaycast6.HTML.Images.FromAssets;

namespace UltraRaycast6
{
	public sealed class UltraRaycast6Sprite : Sprite
	{
		public const int DefaultWidth = global::RayCaster6.ActionScript.RayCaster6.DefaultControlWidth;
		public const int DefaultHeight = global::RayCaster6.ActionScript.RayCaster6.DefaultControlHeight;

		// this event is reachable from javascript!
		public event Action<string> AtGotItem;

		public UltraRaycast6Sprite()
		{
			var body = new global::RayCaster6.ActionScript.RayCaster6();

			body.InternalAtGotGold +=
				delegate
				{
					if (AtGotItem != null)
						AtGotItem("gold");
				};


			body.InternalAtGotAmmo +=
				delegate
				{
					if (AtGotItem != null)
						AtGotItem("ammo");
				};

			this.addChild(body);
		}
	}

	[Description("UltraRaycast6. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{


		public Application(IAbout a)
		{

			a.Ammo.Hide();
			a.Gold.Hide();
			a.Header.innerText = "Hi Chris!";


			var foo = new UltraRaycast6Sprite();



			foo.AttachSpriteTo(a.Foo);

			foo.AtGotItem +=
				e =>
				{
					if (e == "ammo")
					{
						new ammo().AttachTo(a.Bag);
					}
					else if (e == "gold")
					{
						new goldchest().AttachTo(a.Bag);
					}

					new UltraWebService().PlayerGotItem(e,
						y =>
						{
							// server anwsered :)
						}
					);
				};

		}

	}


}
