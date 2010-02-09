using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.applet;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.ActionScript.Extensions;
using java.awt;
using ScriptCoreLib.JavaScript.DOM;
using System.ComponentModel;
using java.awt.@event;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.ActionScript.flash.external;
using Ultra1.Common;

namespace Ultra4
{
#if level2
	//[Description("OrcasClientScriptApplication. Write javascript, flash and java applets within a C# project.")]

	partial class UltraApplication
	{

		partial class UltraApplet : IUltraPolyglot
		{
			partial void SetStatus(string v)
			{
				this.Status = v;
			}

			 partial void RaiseClicked()
			{
				if (Clicked != null)
					Clicked();
			}

			public string Status { get; set; }
			public event Action Clicked;

			public event Action MyLoaded
			{
				add
				{
					value();
				}
				remove
				{

				}
			}
		}



		partial class UltraSprite : IUltraPolyglot
		{
			partial void SetStatus(string v)
			{
				this.Status = v;
			}

			public string Status { get; set; }
			public event Action Clicked;

		 partial void RaiseClicked()
			{
				if (Clicked != null)
					Clicked();
			}

			public event Action MyLoaded
			{
				add
				{
					value();
				}
				remove
				{

				}
			}

			public void Special1(IUltraData1 a, IUltraData1 b)
			{

			}
		}

	
	}
#endif
}
