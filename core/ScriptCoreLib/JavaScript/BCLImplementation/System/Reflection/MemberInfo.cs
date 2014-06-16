﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MemberInfo))]
    public abstract class __MemberInfo
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\MemberInfo.cs

        public abstract Type DeclaringType { get; }


        public abstract string Name
        {
            get;
        }

        public virtual int MetadataToken
        {
            get
            {
                // see also>
                // X:\jsc.svn\examples\javascript\WebMethodXElementTransferExperiment\WebMethodXElementTransferExperiment\ApplicationWebService.cs

                // do we know our tokens?
                return 0;
            }
        }

        //script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.MethodInfo.op_Equality(System.Reflection.MethodInfo, System.Reflection.MethodInfo)]

        public abstract object[] GetCustomAttributes(Type x, bool inherit);
        public abstract object[] GetCustomAttributes(bool inherit);

        // script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.MemberInfo.op_Equality(System.Reflection.MemberInfo, System.Reflection.MemberInfo)]
        // X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\Application.cs

        public static bool operator !=(__MemberInfo left, __MemberInfo right)
        {
            //return left.Name != right.Name;
            return (object)left != (object)right;
        }

        public static bool operator ==(__MemberInfo left, __MemberInfo right)
        {
            //return left.Name == right.Name;
            return (object)left == (object)right;
        }
    }
}
