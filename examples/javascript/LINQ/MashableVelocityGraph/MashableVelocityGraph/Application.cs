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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MashableVelocityGraph;
using MashableVelocityGraph.Design;
using MashableVelocityGraph.HTML.Pages;
using MashableVelocityGraph.Data;
using System.Windows.Forms;

namespace MashableVelocityGraph
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //var beCOMPLETE = new MashableVelocityGraph.Data.VisualizationzDateToCountRow();

            //Unhandled Exception: System.NotSupportedException: Type 'MashableVelocityGraph.Data.VisualizationzDateToCountRow' was not completed.
            //   at System.Reflection.Emit.ModuleBuilder.PreSave(String fileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
            //   at System.Reflection.Emit.AssemblyBuilder.SaveNoLock(String assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
            //   at System.Reflection.Emit.AssemblyBuilder.Save(String assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvoke()

            //var v = new Visualizationz { Dock = DockStyle.Fill };
            new UserControl1 { }.With(
                async v =>
                {

                    v.AttachControlToDocument();

                    var x = await this.WebMethod2();


                    v.visualizationzDateToCountBindingSourceBindingSource.DataSource = x.AsDataTable();
                    //v.visualizationzDateToCountBindingSourceBindingSource.DataSource = x;


                    v.chart1.DataBind();
                }
            );

            //var f = new Form { };

            //f.Controls.Add(v);
            //v.htmml   
        }

    }
}
