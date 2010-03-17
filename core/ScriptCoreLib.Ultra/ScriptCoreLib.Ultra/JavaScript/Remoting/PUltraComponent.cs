using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;

namespace ScriptCoreLib.JavaScript.Remoting
{
	public abstract class PUltraComponent
	{
		public object Tag { get; set; }

		public virtual PHTMLDocument Document
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public virtual PHTMLElement Container
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void WhenReady(Action h)
		{

		}
	}
}
