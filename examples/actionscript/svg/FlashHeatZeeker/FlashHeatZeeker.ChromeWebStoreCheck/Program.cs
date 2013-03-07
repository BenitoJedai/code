using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlashHeatZeeker.ChromeWebStoreCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Beep();
            var ra = new Random();

            while (true)
            {

                Console.WriteLine(DateTime.Now);

                SystemSounds.Beep.Play();

                // pbaaphpbkehboammnlcihpemkkimdfgo
                // http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Dpbaaphpbkehboammnlcihpemkkimdfgo%26uc%26lang%3Den-US&prod=chrome

                var uri = "http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Dpbaaphpbkehboammnlcihpemkkimdfgo%26uc%26lang%3Den-US&prod=chrome";

                System.Net.WebRequest request = System.Net.WebRequest.Create(uri);
                request.Method = "GET";

                // The remote server returned an error: (404) Not Found.

                try
                {
                    var r = request.GetResponse();
                    Console.WriteLine(new { r.ContentLength });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("404"))
                    {
                        Thread.Sleep(1000 + ra.Next(15000));

                        continue;
                    }

                    Console.WriteLine(ex);

                }

                Console.WriteLine("ready!");
                Console.Title = "ready!";

                Console.ReadKey();
            }

            // The remote server returned an error: (413) Request Entity Too Large.

        }
    }
}
