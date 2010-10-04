using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Windows.Forms
{
	public static class WindowsFormsExtensions
	{
		// extension methods should not implicitly refer to new assemblies
		// otherwise the compiler will require to add them.

		/// <summary>
		/// This method adds a Windows Forms user user control to HTML.
		/// Supports Collection Initializer pattern.
		/// </summary>
		/// <param name="that"></param>
		/// <param name="child"></param>
		public static void Add(this INode that, Control child)
		{
			child.GetHTMLTarget().AttachTo(that);
		}

		/// <summary>
		/// This method adds a Windows Forms user user control to HTML.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static T AttachControlTo<T>(this T that, INode parent) where T : Control
		{
			parent.Add(that);

			return that;
		}

        public static T AutoSizeControlTo<T>(this T e, IHTMLElement shadow) where T : Control
        {
            Action Update =
                delegate
                {
                    var w = shadow.scrollWidth;
                    var h = shadow.scrollHeight;

                    e.Width = w;
                    e.Height = h;
                };


            Native.Window.onresize +=
                delegate
                {
                    Update();
                };

            Update();

            return e;
        }
	}
}
