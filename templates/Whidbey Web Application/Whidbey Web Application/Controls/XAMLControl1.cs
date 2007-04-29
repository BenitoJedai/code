using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Whidbey_Web_Application.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:XAMLControl1 runat=server></{0}:XAMLControl1>")]
    public class XAMLControl1 : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            jsc.server.WebTools.CompileAndRegisterClientScript(this.Page);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.DesignMode)
            {
                output.Write("[XAMLControl1]");
            }

            output.Write(Text);

        }
    }
}
