using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM.HTML;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript.FancyTextControl
{
	interface AssemblyReferenceToken :
		ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
	{

	}

	[Script]
	public class FancyTextControl
	{
		public FancyTextControl(IHTMLElement v)
		{
			// first test
			v.style.color = Color.Red;

			var t = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text);

			v.onclick +=
				delegate
				{
					if (v.parentNode == null)
						return;

					t.value = v.innerText;

					v.parentNode.insertBefore(t, v);
					v.parentNode.removeChild(v);

					t.focus();
				};

			t.onblur +=
				delegate
				{
					if (t.parentNode == null)
						return;

					v.innerText = t.value;

					t.parentNode.insertBefore(v, t);
					t.parentNode.removeChild(t);
				};

		}

		static FancyTextControl()
        {
			Native.Window.onload +=
				delegate
				{
					foreach (var v in Native.Document.getElementsByClassName("FancyTextControl").ToArray())
					{
						new FancyTextControl(v);	
					}
				};
		}
	}
}

public partial class Controls_FancyTextControl : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}
}
