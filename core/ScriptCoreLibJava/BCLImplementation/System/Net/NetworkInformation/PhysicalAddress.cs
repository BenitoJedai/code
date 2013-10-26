﻿using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    [Script(Implements = typeof(global::System.Net.NetworkInformation.PhysicalAddress))]
    internal class __PhysicalAddress
    {
        byte[] address;

        public __PhysicalAddress(byte[] address)
        {
            // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs
            // X:\jsc.svn\examples\java\JVMCLRNIC\JVMCLRNIC\Program.cs


            this.address = address;



        }

        public byte[] GetAddressBytes()
        {
            return this.address;
        }

        //public override string ToString()
        //{

        //}
    }
}
