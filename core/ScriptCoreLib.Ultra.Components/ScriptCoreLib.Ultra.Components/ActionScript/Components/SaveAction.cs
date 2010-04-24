using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace ScriptCoreLib.ActionScript.Components
{
	public abstract class SaveAction : Sprite, ScriptCoreLib.ActionScript.Components.ISaveAction
	{

		public SaveAction()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					var s = new Sprite();

					s.graphics.beginFill(0xff);
					s.graphics.drawRect(0, 0, 100, 100);

					s.AttachTo(this);

					s.click +=
						delegate
						{
							var f = new global::ScriptCoreLib.CSharp.Avalon.Controls.FileDialog();



							// utf8?
							//var m = new MemoryStream(Encoding.ASCII.GetBytes(data));

							f.Save(zip, (((ISaveAction)this).FileName).SkipUntilLastIfAny("/").TakeUntilLastIfAny(".") + ".zip");



							zip = new ZIPFile();
						};
				}
			);
		}



		ZIPFile zip = new ZIPFile();

		public void Add(string name, string data)
		{
			zip.Add(name, data);
		}

		public string FileName { get; set; }


	}

	internal sealed class SaveActionSprite : SaveAction
	{
		// this sprite is internal currently because non-internal ultra applications
		// cannot use it.
		// when simplifier is implemented this problem fades away.

		public const int DefaultWidth = 24;
		public const int DefaultHeight = 24;


		public void WhenReady(Action<ISaveAction> y)
		{
			y(this);
		}
	}
}
