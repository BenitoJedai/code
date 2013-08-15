using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Components;
using System;
using System.Linq;
using System.Xml.Linq;

namespace WebGLCity
{
    // Designer
    public sealed partial class Application : ApplicationComponent
    {
        // The type Application is made of several partial classes in the same file. 


        private ApplicationWebService service;


        private void InitializeComponent()
        {
            this.service = new WebGLCity.ApplicationWebService();
            this.renderer = new WebGLCity.Components.XTHREEWebGLRenderer();
            // 
            // renderer
            // 
            this.renderer.ClearColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));

        }

        private Components.XTHREEWebGLRenderer renderer;
    }
}
