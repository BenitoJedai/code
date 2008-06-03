using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrcasWebApplication.Controls
{

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]
    public class WebCustomControl1 : WebControl
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

            this.Attributes["class"] = typeof(JavaScript.WebCustomControl1).Name;

            if (this.DesignMode)
                return;

            jsc.server.WebTools.CompileAndRegisterClientScript(this.Page);
        }


        protected override void RenderContents(HtmlTextWriter output)
        {

            if (this.DesignMode)
            {
                output.Write("[design]");
            }

            output.Write("default content prefix ::" + Text);

        }
    }
}
