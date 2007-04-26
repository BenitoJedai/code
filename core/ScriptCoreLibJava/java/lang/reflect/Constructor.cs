using ScriptCoreLib;

namespace java.lang.reflect
{
    [Script(IsNative=true)]
    public class Constructor
    {
        #region methods
        /// <summary>
        /// Returns the <code>Class</code> object representing the class that declares the constructor represented by this <code>Constructor</code> object.
        /// </summary>
        public Class getDeclaringClass()
        {
            return default(Class);
        }

        /// <summary>
        /// Returns an array of <code>Class</code> objects that represent the types of of exceptions declared to be thrown by the underlying constructor represented by this <code>Constructor</code> object.
        /// </summary>
        public Class[] getExceptionTypes()
        {
            return default(Class[]);
        }

        /// <summary>
        /// Returns the Java language modifiers for the constructor represented by this <code>Constructor</code> object, as an integer.
        /// </summary>
        public int getModifiers()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the name of this constructor, as a string.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Returns an array of <code>Class</code> objects that represent the formal parameter types, in declaration order, of the constructor represented by this <code>Constructor</code> object.
        /// </summary>
        public Class[] getParameterTypes()
        {
            return default(Class[]);
        }

        /// <summary>
        /// Uses the constructor represented by this <code>Constructor</code> object to create and initialize a new instance of the constructor's declaring class, with the specified initialization parameters.
        /// </summary>
        public object newInstance(object[] initargs)
        {
            return default(object);
        }

        #endregion

    }
}
