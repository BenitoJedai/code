using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;
using java.lang;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(MethodBase))]
	internal abstract class __MethodBase : __MemberInfo
	{
        public virtual bool InternalIsAbstract()
        {
            throw new NotImplementedException();
        }


        public virtual bool InternalIsStatic()
        {
            throw new NotImplementedException();
        }

        public virtual bool InternalIsPublic()
        {
            throw new NotImplementedException();
        }

        public virtual bool InternalIsFamily()
        {
            throw new NotImplementedException();
        }

        public virtual object InternalInvoke(object obj, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public bool IsAbstract { get { return InternalIsAbstract(); } }

        public bool IsStatic { get { return InternalIsStatic(); } }

        public bool IsPublic { get { return InternalIsPublic(); } }

        public bool IsFamily { get { return InternalIsFamily(); } }

		public object Invoke(object obj, object[] parameters)
		{
            return InternalInvoke(obj, parameters);
		}



		public abstract ParameterInfo[] GetParameters();
		
	}
}
