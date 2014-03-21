using monese.experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using System.Diagnostics;


namespace TestXMoneseAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201402/20140205
            // X:\jsc.smokescreen.svn\market\appengine\xmoneseservicesweb\xmoneseAPI\MoneseWebServices.cs

            new MoneseWebServices().With(
                async x =>
                {

                    //404
                    //{ RegisterUserShort_value = 404 }
                    //404
                    //{ GetUserIDAsync_value = 404 }


                    x.methodURL = "my.monese.com";

                    //var RegisterUserShort_value = await x.RegisterUserShort("a@", "1234");

                    //xx = 398
                    //Console.WriteLine(new { RegisterUserShort_value });
                    //Debugger.Break();
                    Console.WriteLine("WTF");

                    //// feels like the chrome. api dev, where
                    // jsc also adds await extensions?

                    try
                    {
                        var GetUserIDAsync_value = await x.GetUserID("fff", "");
                        Console.WriteLine(new { GetUserIDAsync_value });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }



                    //x.ChangeUserMobileDataAsync(
                    //    GetUserIDAsync_value,
                    //    "5550000",
                    //    delegate
                    //    {
                    //        Console.WriteLine("ChangeUserMobileDataAsync done");

                    //        //407
                    //        //{ RegisterUserShort_value = 407 }
                    //        //407
                    //        //{ GetUserIDAsync_value = 407 }
                    //        //ChangeUserMobileDataAsync done
                    //        //MC44NA==
                    //        //0.84
                    //        //{ GetCurrencyRateBasedOnStringAsync_value = 0.84 }




                    //        x.GetCurrencyRateBasedOnStringAsync("GBP",
                    //            GetCurrencyRateBasedOnStringAsync_value =>
                    //            {
                    //                Console.WriteLine(new { GetCurrencyRateBasedOnStringAsync_value });
                    //            }
                    //        );
                    //    }
                    //);
                }
            );


            Console.ReadKey();

        }
    }
}
