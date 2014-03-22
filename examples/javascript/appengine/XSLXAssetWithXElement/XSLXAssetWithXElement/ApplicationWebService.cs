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


        //Implementation not found for type import :
        //type: System.Data.SQLite.SQLiteCommand
        //method: Void .ctor(System.String, System.Data.SQLite.SQLiteConnection)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        //assembly: W:\XSLXAssetWithXElement.ApplicationWebService.exe
        //type: XSLXAssetWithXElement.Data.Book1+Sheet1+Queries, XSLXAssetWithXElement.ApplicationWebService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        //offset: 0x0006
        // method:System.Threading.Tasks.Task Create(System.Data.SQLite.SQLiteConnection)

        void References()
        {
            // ex = {"Could not load file or assembly 'ScriptCoreLib.Ultra, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.":"ScriptCoreLib.Ultra, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null"}

            { var r = typeof(ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda); }
            { var r = typeof(System.Data.SQLite.SQLiteConnection); }
            { var r = typeof(ScriptCoreLib.Library.StringConversions); }
        }

        public void WebMethod2()
        {
            //  <h2> <i>Could not load file or assembly 'ScriptCoreLib.Extensions

            // Additional information: Invalid connection string: invalid URI

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
