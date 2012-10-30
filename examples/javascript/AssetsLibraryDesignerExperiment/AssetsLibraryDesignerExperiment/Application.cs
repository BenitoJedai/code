using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AssetsLibraryDesignerExperiment.Design;
using AssetsLibraryDesignerExperiment.HTML.Pages;
using System.ComponentModel;

namespace AssetsLibraryDesignerExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : Component
    {
        // inspired by http://codepen.io/FWeinb/pen/BeJLo

        public readonly ApplicationWebService service = new ApplicationWebService();


        private Components.Class1 class11;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.class11 = new AssetsLibraryDesignerExperiment.Components.Class1();
            this.class21 = new AssetsLibraryDesignerExperiment.Components.Class2();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            // 
            // class11
            // 
            // emmiting failed : System.Exception: BCL implementation does not implement a field: Empty at System.Drawing.Color
            this.class11.BackColor = System.Drawing.Color.Empty;
            this.class11.Foo = null;
            this.class11.ForeColor = System.Drawing.Color.Empty;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

        }

        private Components.Class2 class21;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApplication page = null)
        {
            InitializeComponent();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        private System.Windows.Forms.Timer timer1;
        private IContainer components;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("hey");
        }



    }
}
