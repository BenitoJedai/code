using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/ConstructorInfo.cs

    [Script(Implements = typeof(ConstructorInfo))]
    internal class __ConstructorInfo : __MethodBase
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\ConstructorInfo.cs

        public global::java.lang.reflect.Constructor InternalConstructor;

        public object Invoke(object[] parameters)
        {
            // require by
            // X:\jsc.svn\examples\javascript\WebCamAvatarsExperiment\WebCamAvatarsExperiment\ApplicationWebService.cs

            throw new NotImplementedException();
        }

        public static bool operator !=(__ConstructorInfo left, __ConstructorInfo right)
        {
            return (object)left != (object)right;
        }

        public static bool operator ==(__ConstructorInfo left, __ConstructorInfo right)
        {
            return (object)left == (object)right;

        }


        public override string Name
        {
            get { return ".ctor"; }
        }

        public override global::System.Type DeclaringType
        {
            get
            {
                if (InternalConstructor == null)
                    return null;

                return (__Type)InternalConstructor.getDeclaringClass();
            }
        }

        public override ParameterInfo[] GetParameters()
        {
            if (this.InternalConstructor == null)
                return new ParameterInfo[0];

            var a = this.InternalConstructor.getParameterTypes();
            var n = new ParameterInfo[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                n[i] = new __ParameterInfo
                {
                    ParameterType = (__Type)a[i],
                    Position = i
                };
            }

            return n;
        }



        public static implicit operator ConstructorInfo(__ConstructorInfo m)
        {
            return (ConstructorInfo)(object)m;
        }

        public static implicit operator __ConstructorInfo(Constructor InternalConstructor)
        {
            return new __ConstructorInfo { InternalConstructor = InternalConstructor };
        }


        public override bool InternalIsStatic()
        {
            return Modifier.isStatic(InternalConstructor.getModifiers());
        }

        public override bool InternalIsPublic()
        {
            return Modifier.isPublic(InternalConstructor.getModifiers());
        }

    }
}
