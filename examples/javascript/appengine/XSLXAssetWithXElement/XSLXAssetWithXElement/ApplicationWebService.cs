using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XSLXAssetWithXElement
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public void WebMethod2()
        {
            var x = new XSLXAssetWithXElement.Data.Book1.Sheet1();

            // shall the key route to gethashcode?

            // ex.Message = "SQL logic error or missing database\r\ntable Book1.Sheet1 has no column named Sheet2"

            var data = new Data.Book1Sheet1Row { Zoo = 77, Element = new XElement("foo"), Sheet2 = Data.Sheet2.EUR, Flag = true, Sheet14 = Data.Sheet2.ZEN };

            x.Insert(data);

            // Unable to cast object of type 'System.String' to type 'System.Xml.Linq.XElement
            var y = x.SelectAllAsEnumerable().ToArray();

            Console.WriteLine(new { y.Length });


        }

    }
}
