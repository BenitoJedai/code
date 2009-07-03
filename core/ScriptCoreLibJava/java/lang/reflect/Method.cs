using ScriptCoreLib;

namespace java.lang.reflect
{
    [Script(IsNative=true)]
    public class Method : AccessibleObject
    {
        #region methods
        /// <summary>
        /// Returns the <code>Class</code> object representing the class or interface that declares the method represented by this <code>Method</code> object.
        /// </summary>
        public Class getDeclaringClass()
        {
            return default(Class);
        }

        /// <summary>
        /// Returns an array of <code>Class</code> objects that represent  the types of the exceptions declared to be thrown by the underlying method represented by this <code>Method</code> object.
        /// </summary>
        public Class[] getExceptionTypes()
        {
            return default(Class[]);
        }

        /// <summary>
        /// Returns the Java language modifiers for the method represented by this <code>Method</code> object, as an integer.
        /// </summary>
        public int getModifiers()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the name of the method represented by this <code>Method</code>  object, as a <code>String</code>.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Returns an array of <code>Class</code> objects that represent the formal parameter types, in declaration order, of the method represented by this <code>Method</code> object.
        /// </summary>
        public Class[] getParameterTypes()
        {
            return default(Class[]);
        }

        /// <summary>
        /// Returns a <code>Class</code> object that represents the formal return type of the method represented by this <code>Method</code> object.
        /// </summary>
        public Class getReturnType()
        {
            return default(Class);
        }

        /// <summary>
        /// Invokes the underlying method represented by this <code>Method</code>  object, on the specified object with the specified parameters.
        /// </summary>
        public object invoke(object obj, object[] args)
        {
            return default(object);
        }

        #endregion

    }
}
