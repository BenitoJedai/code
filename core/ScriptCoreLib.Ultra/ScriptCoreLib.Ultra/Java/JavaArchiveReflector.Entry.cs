extern alias jvm;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using jvm::java.io;
using jvm::java.net;
using jvm::java.util.zip;
using jvm::ScriptCoreLibJava.Extensions;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System.Reflection;

namespace ScriptCoreLib.Java
{
    public partial class JavaArchiveReflector
    {

        public class Entry
        {
            public string Name;

            public string TypeFullName;

            public GetType InternalGetType;

            Type InternalType;

            public Type Type
            {
                get
                {
                    if (InternalGetType == null)
                    {
                        System.Console.WriteLine("JavaArchiveReflector.Entry.get_Type, yet InternalGetType is null. why?");

                        return null;
                    }

                    InternalType = InternalGetType();

                    return InternalType;
                }
            }

            MethodInfo[] InternalMethods;

            public MethodInfo[] Methods
            {
                get
                {
                    if (InternalMethods == null)
                        InternalMethods = this.Type.GetMethods();

                    return InternalMethods;
                }
            }


        }

    }

}
