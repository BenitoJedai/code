using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test453ForEachStringArray
{
    public class Class1
    {

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/test453foreachstringarray-1


        //public static string ToFormDataString(NameValueCollection data)
        public static string ToFormDataString(string[] data_AllKeys)
        {
            var xx = "";

            foreach (var item in data_AllKeys)
            {
                //Console.WriteLine("WebClient.ToFormDataString " + new { item });


                //if (xx != "")
                {
                    // script: error JSC1000: No implementation found for this native method, please implement [static System.String.Concat(System.String, System.String)]

                    //xx += "&";
                    xx = "&";
                }
            }

            return xx;
        }

    }
}
