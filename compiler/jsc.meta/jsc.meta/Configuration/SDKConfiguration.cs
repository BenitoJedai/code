using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace jsc.meta.Configuration
{
    public class SDKConfiguration
    {
        const string DefaultFile = @"c:\util\jsc\bin\jsc.SDKConfiguration.xml";

        public DirectoryInfo JavaSDK = new DirectoryInfo(@"C:\Program Files\Java\jdk1.6.0_21");
        public DirectoryInfo FlexSDK = new DirectoryInfo(@"C:\util\flex_sdk_4.1.0.16076");
        public DirectoryInfo GoogleAppEngineJavaSDK = new DirectoryInfo(@"C:\util\appengine-java-sdk-1.3.7");
        public DirectoryInfo ApacheAntSDK = new DirectoryInfo(@"C:\util\apache-ant-1.8.1");
        public DirectoryInfo XAMPLite = new DirectoryInfo(@"C:\util\xampplite-win32-1.7.3");

        public FileInfo ApacheAntSDK_ant
        {
            get
            {
                return new FileInfo(Path.Combine(ApacheAntSDK.FullName, @"bin\ant.bat"));
            }

        }
        public FileInfo FlexSDK_mxmlc
        {
            get
            {
                return
                    new FileInfo(Path.Combine(FlexSDK.FullName, @"bin\mxmlc.exe"));
            }
        }

        public FileInfo FlexSDK_flashplayer
        {
            get
            {
                return
                    new FileInfo(Path.Combine(FlexSDK.FullName, @"runtimes\player\10.1\win\FlashPlayerDebugger.exe"));
            }
        }



        public static implicit operator XElement(SDKConfiguration e)
        {
            return new XElement("Configuration",
                new XElement("JavaSDK", e.JavaSDK.FullName),
                new XElement("FlexSDK", e.FlexSDK.FullName),
                new XElement("GoogleAppEngineJavaSDK", e.GoogleAppEngineJavaSDK.FullName),
                new XElement("ApacheAntSDK", e.ApacheAntSDK.FullName),
                new XElement("XAMPLite", e.XAMPLite.FullName)
            );
        }

        public static implicit operator SDKConfiguration(XElement e)
        {
            return new SDKConfiguration
            {
                JavaSDK = new DirectoryInfo(e.Element("JavaSDK").Value),
                FlexSDK = new DirectoryInfo(e.Element("FlexSDK").Value),
                GoogleAppEngineJavaSDK = new DirectoryInfo(e.Element("GoogleAppEngineJavaSDK").Value),
                ApacheAntSDK = new DirectoryInfo(e.Element("ApacheAntSDK").Value),
                XAMPLite = new DirectoryInfo(e.Element("XAMPLite").Value)
            };
        }


        public static SDKConfiguration Default
        {
            get
            {
                var f = new FileInfo(DefaultFile);

                if (f.Exists)
                    return XElement.Load(f.FullName);

                return new SDKConfiguration();
            }
            set
            {
                ((XElement)value).Save(DefaultFile);
            }
        }

    }
}
