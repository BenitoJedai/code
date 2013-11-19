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
using MultiAppDatabaseExperiment;
using MultiAppDatabaseExperiment.Design;
using MultiAppDatabaseExperiment.HTML.Pages;
using MultiAppDatabase.Schema.Clients;

namespace MultiAppDatabaseExperiment
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
            var content = new UserControl1
            {
                _ClientsTable_Insert = base._ClientsTable_Insert 
            };

            content.AttachControlToDocument();

           //page.Login.onclick += delegate
           // {
           //     base._ClientsTable_Insert(
           //                 new ClientsTable.Insert
           //                 {
           //                     //Don't trust text
           //                     Username = page.username.value,
           //                     Password = page.password.value,
           //                     ScreenWidth = Native.screen.width,
           //                     ScreenHeight = Native.screen.height
           //                 }
           //         );
           // };
        }

    }
}
