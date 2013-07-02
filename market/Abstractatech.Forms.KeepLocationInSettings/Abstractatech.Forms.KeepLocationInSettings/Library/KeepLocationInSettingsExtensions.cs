using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;

//namespace Abstractatech.Forms.KeepLocationInSettings.Library
namespace System.Windows.Forms
{
    public static class KeepLocationInSettingsExtensions
    {
        public static T KeepLocationInSettings<T>(this T f, SettingsBase s, string key = "KeepLocationInSettings")
            where T : Form
        {
            f.Load +=
                delegate
                {
                    var sizeString = (string)s[key];
                    if (!string.IsNullOrEmpty(sizeString))
                    {
                        // Load { sizeString = NaN }
                        // Load { sizeString = <size Width="null" Height="null"/> }

                        //Console.WriteLine("KeepLocationInSettings " + new { sizeString });
                        var size = XElement.Parse(sizeString);

                        f.Top = int.Parse(size.Attribute("Top").Value);
                        f.Left = int.Parse(size.Attribute("Left").Value);
                        f.Width = int.Parse(size.Attribute("Width").Value);
                        f.Height = int.Parse(size.Attribute("Height").Value);

                    }
                };

            f.FormClosing +=
                delegate
                {
                    // A first chance exception of type 'System.Configuration.SettingsPropertyNotFoundException' occurred in System.dll

                    s[key] = new XElement(
                        "size",
                        new XAttribute("Top", "" + f.Top),
                        new XAttribute("Left", "" + f.Left),
                        new XAttribute("Width", "" + f.Width),
                        new XAttribute("Height", "" + f.Height)
                    ).ToString();
                };


            return f;

        }
    }
}
