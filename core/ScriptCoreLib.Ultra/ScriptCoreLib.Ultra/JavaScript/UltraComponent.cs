using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript
{
	public abstract class UltraComponent 
	{
		// Should we inherit ComponentModel.Component?

		// http://www.google.com/search?q=UltraComponent



		public object Tag { get; set; }

		public virtual IHTMLElement Container
		{
			get
			{
				throw new NotImplementedException();
			}
		}


		public virtual IHTMLImage[] Images
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public virtual IHTMLAnchor[] Anchors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

	}


}
