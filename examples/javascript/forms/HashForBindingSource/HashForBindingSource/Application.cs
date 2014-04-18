using HashForBindingSource;
using HashForBindingSource.Design;
using HashForBindingSource.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HashForBindingSource
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        ApplicationControl content;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.content = new HashForBindingSource.ApplicationControl();
            // 
            // content
            // 
            this.content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.content.Location = new System.Drawing.Point(0, 0);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(652, 474);
            this.content.TabIndex = 0;
            this.content.zeBindingSourceCurrentChanged += new System.Action(this.content_zeBindingSourceCurrentChanged);

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            InitializeComponent();



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140418

            content.AttachControlToDocument();

            Console.WriteLine(
                new { Native.document.location.hash }
                );


            // notice chrome will not show hash or url in dev version anymore!
            // 4:71ms { hash =  }
            //content.ParentForm.Text =
            //    Native.document.location.hash;

            //Native.window.onhashchange +=
            //    e =>
            //    {
            //        content.ParentForm.Text =
            //            Native.document.location.hash;

            //    };



            // history api?
            Native.window.onhashchange +=
                delegate
                {
                    content.FindHash(Native.document.location.hash);
                };

            // allow refresh?
            content.FindHash(Native.document.location.hash);

        }

        private void content_zeBindingSourceCurrentChanged()
        {
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.get_Current()]

            var hash = (
                     (HashForBindingSource.DataSourcez.Dataz.ZeDocumentTextzNavigateRow)

                     // this faults!
                //(item as System.Data.DataRowView).Row

                         (
                             (System.Data.DataRowView)(this.content.zeBindingSource.Current)
                         ).Row
                     ).hash;

            Native.document.title = hash;


            Native.document.location.hash = hash;

        }

    }
}
