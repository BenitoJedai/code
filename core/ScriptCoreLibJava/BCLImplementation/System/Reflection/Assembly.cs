using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.InteropServices;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Security;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.Serialization;
using System.Reflection;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.Assembly))]
    internal class __Assembly : ___Assembly, __IEvidenceFactory, __ICustomAttributeProvider, __ISerializable
    {
        public __Type InternalReference;

        FileInfo InternalDeclaringFileCached;
        public FileInfo InternalDeclaringFile
        {
            get
            {
                if (InternalDeclaringFileCached == null)
                {
                    InternalDeclaringFileCached = this.InternalReference.InternalTypeDescription.GetDeclaringFile();
                }

                return InternalDeclaringFileCached;

            }
        }

        public string FullName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.Location);
            }
        }

        public virtual string Location
        {
            get
            {
                return InternalDeclaringFile.FullName;
            }
        }

        public static implicit operator Assembly(__Assembly e)
        {
            return (Assembly)(object)e;
        }

    }
}
