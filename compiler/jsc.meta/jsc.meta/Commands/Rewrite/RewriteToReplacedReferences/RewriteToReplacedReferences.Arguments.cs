﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc.meta.Commands.Rewrite.RewriteToReplacedReferences
{
    partial class RewriteToReplacedReferences
    {
        public FileInfo Assembly;

        public FileInfo ilasm = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ilasm.exe");

        public FileInfo ildasm = new FileInfo(@"C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\ildasm.exe");
        

        /*
         
         
         * Google suggests:
         * http://www.nokola.com/trycsharp/HowToBuild.aspx
         * http://forums.silverlight.net/forums/p/102774/234470.aspx
         * http://stackoverflow.com/questions/1159800/silverlight-support-for-dynamic-code
         * http://www.codeproject.com/KB/silverlight/SLAssemblies.aspx
         * http://biofractal.blogspot.com/2009/03/convert-dotnet-assemblies-to.html
         * http://www.netfxharmonics.com/2008/12/Reusing-NET-Assemblies-in-Silverlight
         * http://forums-silverlight-dit.neudesic.com/forums/p/4495/171796.aspx
         * http://timross.wordpress.com/category/silverlight/
         * http://forums.silverlight.net/forums/t/23489.aspx
         * http://neilmosafi.blogspot.com/2008/03/silverlight-projects-vs-normal-projects.html
         */
    }
}
