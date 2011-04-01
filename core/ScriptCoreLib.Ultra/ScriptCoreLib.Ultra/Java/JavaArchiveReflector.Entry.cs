using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
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
                        return null;

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
