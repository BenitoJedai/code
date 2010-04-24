using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.Components.Avalon.Images;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ScriptCoreLib.ActionScript.Components
{
	public abstract class SaveAction : Sprite, ScriptCoreLib.ActionScript.Components.ISaveAction
	{

		public SaveAction()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{

					var c = new Canvas
					{
						Width = RTA_save.ImageDefaultWidth,
						Height = RTA_save.ImageDefaultHeight,

					};

					new Rectangle
					{
						Width = 24,
						Height = 24,
						Fill = Brushes.White,
						Opacity = 0
					}.AttachTo(c);

					new RTA_save().AttachTo(c).MoveTo(
						(24 - RTA_save.ImageDefaultWidth ) / 2,	
						(24 - RTA_save.ImageDefaultHeight ) / 2
					);

					c.AttachToContainer(this);

					c.MouseLeftButtonUp +=
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
