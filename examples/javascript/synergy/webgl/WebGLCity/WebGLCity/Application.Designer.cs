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

        }
    }
}
