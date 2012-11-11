using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestAssetAsComponent.Design
{
    public class ImageLinkComponent : 
        //Control
        Component
    {
        public string href { get; set; }

        public IHTMLImage img;


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // ImageLinkComponent
            // 
            //this.BackColor = System.Drawing.Color.Blue;

        }

        public ImageLinkComponent()
        {
            this.InitializeComponent();
        }
    }
}
