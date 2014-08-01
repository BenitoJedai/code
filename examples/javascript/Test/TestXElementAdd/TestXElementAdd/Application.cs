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
using TestXElementAdd;
using TestXElementAdd.Design;
using TestXElementAdd.HTML.Pages;

namespace TestXElementAdd
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
            var citiesData = new XElement("Cities");

            var cityH = new XElement("City");
            cityH.Add(new XElement("name", "Tallinn"));
            cityH.Add(new XElement("Longtitude", "24.7281"));
            cityH.Add(new XElement("Latitude", "59.4339"));
            citiesData.Add(cityH);

            var Cities = citiesData.Elements();


            foreach (var City in Cities)
            {
                new IHTMLPre { new { City } }.AttachToDocument();

                // {{ City = <City><name>Tallinn</name><Longtitude>24.7281</Longtitude><Latitude>59.4339</Latitude></City> }}
                var name = City.Element("name");

                new IHTMLPre { new { name } }.AttachToDocument();

            }
        }

    }
}
