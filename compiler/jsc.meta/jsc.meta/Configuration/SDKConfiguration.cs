﻿using System;
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

        public static DirectoryInfo JavaSDK_x86
        {
            get
            {
                if (Environment.Is64BitOperatingSystem)
                    return new DirectoryInfo(@"C:\Program Files (x86)\Java\jdk1.6.0_24");

                return new DirectoryInfo(@"C:\Program Files\Java\jdk1.6.0_24");
            }
        }

        public DirectoryInfo FlexSDK = new DirectoryInfo(@"C:\util\flex_sdk_4.1.0.16076");
        public DirectoryInfo JavaSDK = JavaSDK_x86;
        public DirectoryInfo GoogleAppEngineJavaSDK = new DirectoryInfo(@"C:\util\appengine-java-sdk-1.4.2");
        public DirectoryInfo ApacheAntSDK = new DirectoryInfo(@"C:\util\apache-ant-1.8.2");
        public DirectoryInfo XAMPLite = new DirectoryInfo(@"C:\util\xampplite-win32-1.7.3");

        public DirectoryInfo JavaSDK_bin
        {
            get
            {
                return new DirectoryInfo(Path.Combine(JavaSDK.FullName, @"bin"));
            }
        }

        public FileInfo JavaSDK_bin_jar
        {
            get
            {
                return new FileInfo(Path.Combine(JavaSDK.FullName, @"bin\jar.exe"));
            }
        }


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

        public FileInfo FlexSDK_compc
        {
            get
            {
                return
                    new FileInfo(Path.Combine(FlexSDK.FullName, @"bin\compc.exe"));
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
            Action<string, Action<DirectoryInfo>> With =
                (n, h) =>
                {
                    var k = e.Element(n);
                    if (k == null)
                        return;

                    h(new DirectoryInfo(k.Value));
                };



            var c = new SDKConfiguration();

            With("JavaSDK", value => c.JavaSDK = value);
            With("FlexSDK", value => c.FlexSDK = value);
            With("GoogleAppEngineJavaSDK", value => c.GoogleAppEngineJavaSDK = value);
            With("ApacheAntSDK", value => c.ApacheAntSDK = value);
            With("XAMPLite", value => c.XAMPLite = value);

            return c;
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
