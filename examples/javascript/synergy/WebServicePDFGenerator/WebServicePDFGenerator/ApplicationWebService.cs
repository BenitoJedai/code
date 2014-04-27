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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WebServicePDFGenerator
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        [Obsolete(" is MemoryStream supported?")]
        //public async Task<MemoryStream> Invoke()
        public async Task<string> Invoke()
        {
            // http://www.mikesdotnetting.com/Article/80/Create-PDFs-in-ASP.NET-getting-started-with-iTextSharp
            var m = new MemoryStream();

            // step 1
            Document document = new Document();
            // step 2

            PdfWriter.GetInstance(document, m);
            // step 3
            document.Open();
            // step 4
            document.Add(new Paragraph("Hello World!"));
            // step 5
            document.Close();


            return Convert.ToBase64String(m.ToArray());
        }

    }
}
