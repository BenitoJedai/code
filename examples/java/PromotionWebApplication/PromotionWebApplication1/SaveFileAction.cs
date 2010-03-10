using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace PromotionWebApplication1
{
	public sealed class SaveFileAction : Sprite
	{
		public const int DefaultWidth = 64;
		public const int DefaultHeight = 32;

		public void WhenReady(Action e)
		{
			e();
		}

		public event Action AfterSave;

		ZIPFile zip = new ZIPFile();

		public void Add(string data, string name)
		{
			zip.Add(name, data);
		}

		public void SaveFile(string name)
		{
			var s = new Sprite();

			s.graphics.beginFill(0xff);
			s.graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);

			s.AttachTo(this);

			s.click +=
				delegate
				{
					var f = new global::ScriptCoreLib.CSharp.Avalon.Controls.FileDialog();



					// utf8?
					//var m = new MemoryStream(Encoding.ASCII.GetBytes(data));

					f.Save(zip, name.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".") + ".zip");

					s.Orphanize();

					if (AfterSave != null)
						AfterSave();

					zip = new ZIPFile();
				};
		}
	}
}
