using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Ultra.Components.Avalon.Images;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.ActionScript.Components
{
	public class SaveActionImplementation : ISaveAction
	{
		public ZIPFile zip = new ZIPFile();

		public SaveActionImplementation()
		{
			this.FileName = "project1.zip";
		}

		public void Clear()
		{
			this.zip = new ZIPFile();
		}

		public void Add(string name, XElement data)
		{
			zip.Add(name, data.ToString());
		}

		public void Add(string name, string data)
		{
			zip.Add(name, data);
		}

		public string FileName { get; set; }	
	}

	public abstract class SaveAction : Sprite
	{
		public SaveActionImplementation Implementation = new SaveActionImplementation();

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
						(22 - RTA_save.ImageDefaultWidth) / 2,
						(22 - RTA_save.ImageDefaultHeight) / 2
					);

					c.AttachToContainer(this);

					c.MouseLeftButtonUp +=
						delegate
						{
							var f = new global::ScriptCoreLib.CSharp.Avalon.Controls.FileDialog();



							// utf8?
							//var m = new MemoryStream(Encoding.ASCII.GetBytes(data));

							f.Save(
								Implementation.zip, 
								(Implementation.FileName).SkipUntilLastIfAny("/").TakeUntilLastIfAny(".") + ".zip"
							);



							//zip = new ZIPFile();
						};
				}
			);
		}



	


	}


	//internal sealed class InternalSaveActionSprite : SaveAction, ISaveActionWhenReady
	//{
	//    // this sprite is internal currently because non-internal ultra applications
	//    // cannot use it.
	//    // when simplifier is implemented this problem fades away.

	//    public const int DefaultWidth = 24 - 2;
	//    public const int DefaultHeight = 24 - 2;


	//    public void WhenReady(Action<ISaveAction> y)
	//    {
	//        y(this);
	//    }
	//}
}
