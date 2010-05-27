using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls;

namespace ScriptCoreLib.JavaScript.Extensions
{
	[Script]
	public static class AvalonExtensions
	{
        public static IHTMLElement ToHTMLElement(this global::System.Windows.Controls.Panel e)
        {
            __Panel p = e;

            return p.InternalSprite;
        }

 

		public static T AttachToContainer<T>(this T e, IHTMLElement c) where T : global::System.Windows.Controls.Panel
		{
			c.appendChild(e.ToHTMLElement());

			return e;
		}
	}
}
