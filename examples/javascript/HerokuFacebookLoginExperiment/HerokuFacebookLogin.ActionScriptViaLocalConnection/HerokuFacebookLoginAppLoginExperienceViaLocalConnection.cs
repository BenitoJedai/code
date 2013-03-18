using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HerokuFacebookLogin.ActionScriptViaLocalConnection
{
    public delegate void HerokuFacebookLoginAppLoginExperienceAction(
        string id,
        string name,
        string third_party_id,
        string accessToken
    );

    // the most important class
    public static class HerokuFacebookLoginAppLoginExperienceViaLocalConnection
    {
        public static Action<string> Diagnostics = delegate { };

        static LocalConnection tx = new LocalConnection();
        static LocalConnection rx;
        static int ii;

        public static void Invoke(HerokuFacebookLoginAppLoginExperienceAction yield)
        {
            InternalInvoke(
                 data =>
                 {
                     if (yield == null)
                         return;


                     //content.t.Text = data;

                     //content.r.Fill = Brushes.Green;




                     try
                     {
                         var xml = XElement.Parse(data);

                         if (xml.Name.LocalName == "response")
                         {
                             Diagnostics("Invoke ack for " + new { data });


                             #region tx
                             tx.status +=
                                 delegate
                                 {

                                 };
                             tx.allowDomain("*");
                             tx.allowInsecureDomain("*");
                             tx.send("_Invoke" + ii + "_ack", "Invoke", "ack!" + ii);
                             #endregion

                             var name = xml.Attribute("name").Value;
                             var id = xml.Attribute("id").Value;
                             var third_party_id = xml.Attribute("third_party_id").Value;
                             var accessToken = xml.Attribute("accessToken").Value;

                             Diagnostics("Invoke yield");

                             yield(id, name, third_party_id, accessToken);
                             yield = null;
                         }
                     }
                     catch
                     {

                     }

                 }
            );
        }

        public static void InternalInvoke(Action<string> yield)
        {
            #region rx
            if (rx == null)
            {
                rx = new LocalConnection();
                var r = new Random();

                ii = r.Next();


                var client = new DynamicContainer { Subject = new object() };

                Action<string> Invoke = data =>
                {
                    //if (yield == null)
                    //    return;

                    // http://livedocs.adobe.com/flex/3/html/help.html?content=17_Networking_and_communications_4.html
                    //Diagnostics("InternalInvoke yield");

                    yield(data);
                    //yield = null;

                    //content.r.Fill = Brushes.Green;
                };

                // http://stackoverflow.com/questions/6834455/as3-localconnection-errors
                client["Invoke"] = Invoke.ToFunction();
                rx.allowDomain("*");
                rx.allowInsecureDomain("*");
                rx.status += delegate
                { };

                rx.client = client.Subject;

                // ArgumentError: Error #2082: Connect failed because the object is already connected.
                // Error #2044: Unhandled StatusEvent:. level=error, code=

                // If the string for connectionName begins with an underscore (for example, _connectionName), Flash Player 
                // does not add a prefix to the string. This means the receiving and sending LocalConnection objects will 
                // use identical strings for connectionName. If the receiving object uses LocalConnection.allowDomain() to specify that connections from any domain will be accepted, you can move the SWF file with the receiving LocalConnection object to another domain without altering any sending LocalConnection objects.
                rx.connect("_Invoke" + ii);

                //content.MouseLeftButtonUp +=
                //    delegate
                //    {
            }
            #endregion

            Diagnostics("NavigateTo");
            new Uri("http://young-beach-4377.herokuapp.com/#c" + ii).NavigateTo();
            //};
        }
    }


}
