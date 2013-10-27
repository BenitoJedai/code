﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using System.Net.NetworkInformation;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    [Script(Implements = typeof(global::System.Net.NetworkInformation.NetworkInterface))]
    public class __NetworkInterface
    {
        public java.net.NetworkInterface InternalValue;

        //Implementation not found for type import :
        //type: System.Net.NetworkInformation.NetworkInterface
        //method: System.Net.NetworkInformation.IPInterfaceProperties GetIPProperties()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!



        public virtual IPInterfaceProperties GetIPProperties()
        {
            return new __IPInterfaceProperties { InternalValue = this };

        }


        public virtual PhysicalAddress GetPhysicalAddress()
        {
            var value = default(sbyte[]);

            try
            {
                value = this.InternalValue.getHardwareAddress();
            }
            catch
            {
                throw;
            }

            return new __PhysicalAddress(
                (byte[])(object)value
            );
        }

        public virtual bool SupportsMulticast
        {
            get
            {
                var value = false;

                try
                {
                    value = this.InternalValue.supportsMulticast();
                }
                catch
                {
                    throw;
                }

                return value;
            }
        }
        public OperationalStatus OperationalStatus
        {
            get
            {
                var isUp = false;

                try
                {
                    isUp = this.InternalValue.isUp();
                }
                catch
                {
                    throw;
                }

                if (isUp)
                    return global::System.Net.NetworkInformation.OperationalStatus.Up;


                return global::System.Net.NetworkInformation.OperationalStatus.Down;
            }
        }

        public string Description
        {
            get
            {
                return InternalValue.getDisplayName();
            }
        }

        public string Name
        {
            get
            {
                return InternalValue.getName();
            }
        }

        public static global::System.Net.NetworkInformation.NetworkInterface[] GetAllNetworkInterfaces()
        {
            // X:\jsc.svn\examples\java\JVMCLRNIC\JVMCLRNIC\Program.cs

            var i = default(java.util.Enumeration);

            try
            {
                i = java.net.NetworkInterface.getNetworkInterfaces();
            }
            catch
            {
                throw;
            }

            var a = new List<global::System.Net.NetworkInformation.NetworkInterface>();

            while (i.hasMoreElements())
            {
                var current = (java.net.NetworkInterface)i.nextElement();


                a.Add(
                    new __NetworkInterface
                    {
                        InternalValue = current
                    }
                );

            }

            return a.ToArray();

        }

        public static implicit operator global::System.Net.NetworkInformation.NetworkInterface(__NetworkInterface i)
        {
            return (global::System.Net.NetworkInformation.NetworkInterface)(object)i;
        }

        public static implicit operator __NetworkInterface(global::System.Net.NetworkInformation.NetworkInterface i)
        {
            return (__NetworkInterface)(object)i;
        }

        public static implicit operator java.net.NetworkInterface(__NetworkInterface i)
        {
            return i.InternalValue;
        }
    }
}
