using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using System.Collections;

namespace ScriptCoreLib.JavaScript.Remoting
{
	public abstract class PUltraComponent
	{
		public object Tag { get; set; }


		internal PHTMLElement InternalElement;
		internal PHTMLDocument InternalDocument;


		public PHTMLElement Container
		{
			get
			{
				return this.InternalElement;
			}
		}

		public PHTMLDocument Document
		{
			get
			{
				return this.InternalDocument;
			}
		}

		internal void InternalMarkReady()
		{
			var a = InternalWhenReady;

			InternalWhenReady = null;

			foreach (Action item in a.ToArray())
			{
				item();
			}
		}

		internal ArrayList InternalWhenReady = new ArrayList();

		public void WhenReady(Action h)
		{
			if (InternalWhenReady == null)
			{
				h();
				return;
			}

			InternalWhenReady.Add(h);

		}
	}
}
