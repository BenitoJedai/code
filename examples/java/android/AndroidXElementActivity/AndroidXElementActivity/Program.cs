using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using AndroidXElementActivity.Activities;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidXElementActivity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            #region code I want to write for android, but cant yet

            // in the end we will get the version of clickonce

            var value = new WebClient().DownloadString(ApplicationActivity.Version);

            //+		xml.Elements().First().Name	{{urn:schemas-microsoft-com:asm.v1}assemblyIdentity}	System.Xml.Linq.XName

            XNamespace asmv1 = "urn:schemas-microsoft-com:asm.v1";

            var xml = XElement.Parse(value);
            var assemblyIdentity = xml.Element(asmv1 + "assemblyIdentity");
            var version = assemblyIdentity.Attribute("version");

            Console.WriteLine(version);

            #endregion


            global::jsc.AndroidLauncher.Launch(
                 typeof(AndroidXElementActivity.Activities.ApplicationActivity)
            );
        }
    }
}
