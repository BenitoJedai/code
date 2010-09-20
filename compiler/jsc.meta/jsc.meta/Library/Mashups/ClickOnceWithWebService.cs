using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc.meta.Library.Mashups
{
    static class ClickOnceWithWebService
    {
        class SpecialDesktopApplication
        {
            public static void Main(string[] args)
            {
                // 
                Console.WriteLine("JSC software as service http://www.jsc-solutions.net/SpecialDesktopApplication");

                var UltraApplicationInputZIP = new MemoryStream(File.ReadAllBytes(args[0]));

                new WebService().SoftwareAsService(
                    UltraApplicationInputZIP,
                    output =>
                    {

                    }
                );


            }
        }

        class WebService
        {
            public void WebMethod1(string FromClient)
            {

            }
            
            public void SoftwareAsService(
                MemoryStream UltraApplicationInputZIP,
                Action<MemoryStream> UltraApplicationOutputZIP)
            {

            }
        }

    }
}
