using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Security;
using System.Reflection;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.Serialization;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.InteropServices;
using ScriptCoreLib.Shared.BCLImplementation.System.Security;
// is ScriptCoreLib built yet?
using ScriptCoreLib.Shared.BCLImplementation.System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/assembly.cs


    [Script(Implements = typeof(global::System.Reflection.Assembly))]
    public class __Assembly : ___Assembly, __IEvidenceFactory, __ICustomAttributeProvider, __ISerializable
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Security\IEvidenceFactory.cs

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
