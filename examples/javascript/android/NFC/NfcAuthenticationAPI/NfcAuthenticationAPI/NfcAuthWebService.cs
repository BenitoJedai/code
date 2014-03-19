using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace monese.experimental
{

    [Obsolete(@"
a generic experimental WebClient based service wrapper. 2013/01/09

2014-01-28 What if we want this component to be exposed via toolbox?

20140303 can we use it in testing and then for air-ios yet?
")]
    public class NfcAuthWebService :
        System.ComponentModel.Component,
        System.ComponentModel.IComponent
    {
        //        ---------------------------
        //Microsoft Visual Studio Express 2012 for Windows Desktop
        //---------------------------
        //Failed to create component 'MoneseWebServices'.  The error message follows:

        // 'Component of type MoneseWebServices could not be created.  
        //        Make sure the type implements IComponent and provides 
        //            an appropriate public constructor.  
        //                Appropriate constructors either take no parameters or 
        //                    take a single IContainer parameter.'
        //---------------------------
        //OK   Help   
        //---------------------------

        //---------------------------
        //Microsoft Visual Studio Express 2012 for Web
        //---------------------------
        //There are no components in 'C:\util\jsc\nuget\NfcAuthenticationAPI.1.0.0.0\lib\monese%20API.dll' that can be placed on the toolbox.
        //---------------------------
        //OK   
        //---------------------------

        public static string methodURL = "192.168.1.92:19221";

        public NfcAuthWebService()
            : this(null)
        {
            // X:\jsc.svn\examples\actionscript\Test\TestEmptyConstructor\TestEmptyConstructor\Class1.cs

        }

        public NfcAuthWebService(System.ComponentModel.IContainer c)
        {

        }

        static string UTF8ToBase64StringOrDefault(string e)
        {
            if (e == null)
                return null;

            var bytes = Encoding.UTF8.GetBytes(e);

            return Convert.ToBase64String(bytes);
        }

        static string UTF8FromBase64StringOrDefault(string e)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140101

            var o = default(string);
            // allow 0 char do be sent
            if (e != null)
            {
                //Console.WriteLine("before Convert.FromBase64String");
                var bytes = Convert.FromBase64String(e);

                //Console.WriteLine("before Encoding.UTF8.GetString");
                o = Encoding.UTF8.GetString(bytes);
            }

            //Console.WriteLine("exit UTF8FromBase64StringOrDefault");

            return o;
        }

        [Obsolete("do we have async/await available for AIR yet?")]
        public void InsertUserAuthAsync(string username, bool isCard, Action yield)
        {
            var _06000003_username = UTF8ToBase64StringOrDefault(username);
            var _06000003_isCard = isCard;

            var c = new WebClient();
            c.UploadValuesCompleted +=
                (sender, args) =>
                {
                    var resultBytes = args.Result;
                    var data = Encoding.UTF8.GetString(resultBytes);
                    yield();
                };

            c.UploadValuesAsync(
                // generated, whenn will the uri change and break this?
                address: new Uri("http://" + methodURL + "/xml/InsertUserAuth"),
                    data: new System.Collections.Specialized.NameValueCollection { 
                        {"WebMethodMetadataToken","06000003"},
                        {"WebMethodMetadataName","InsertUserAuth"},
                                { "_06000003_username", _06000003_username},
                                { "_06000003_isCard", _06000003_isCard.ToString()}
                            }
            );
        }
    }

}
