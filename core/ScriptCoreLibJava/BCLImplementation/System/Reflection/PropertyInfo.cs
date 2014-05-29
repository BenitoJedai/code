﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;
using java.lang;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(PropertyInfo))]
    internal class __PropertyInfo : __MethodBase
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140514
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs


        //Implementation not found for type import :
        //type: System.Reflection.PropertyInfo
        //method: System.Type get_PropertyType()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        public global::System.Type PropertyType
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public static bool operator !=(__PropertyInfo left, __PropertyInfo right)
        {
            return (object)left != (object)right;
        }

        public static bool operator ==(__PropertyInfo left, __PropertyInfo right)
        {
            return (object)left == (object)right;

        }



        public override ParameterInfo[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override global::System.Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public virtual object GetValue(object obj, object[] index)
        {
            return null;
        }
    }
}
