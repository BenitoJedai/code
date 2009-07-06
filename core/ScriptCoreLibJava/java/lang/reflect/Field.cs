using ScriptCoreLib;

namespace java.lang.reflect
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/lang/reflect/Field.html
    [Script(IsNative=true)]
    public class Field
    {
        #region methods
        /// <summary>
        /// Returns the value of the field represented by this <code>Field</code>, on the specified object.
        /// </summary>
        public object get(object obj)
        {
            return default(object);
        }

        /// <summary>
        /// Gets the value of a static or instance <code>boolean</code> field.
        /// </summary>
        public bool getBoolean(object obj)
        {
            return default(bool);
        }

        /// <summary>
        /// Gets the value of a static or instance <code>byte</code> field.
        /// </summary>
        public sbyte getByte(object obj)
        {
            return default(sbyte);
        }

        /// <summary>
        /// Gets the value of a static or instance field of type <code>char</code> or of another primitive type convertible to type <code>char</code> via a widening conversion.
        /// </summary>
        public char getChar(object obj)
        {
            return default(char);
        }

        /// <summary>
        /// Returns the <code>Class</code> object representing the class or interface that declares the field represented by this <code>Field</code> object.
        /// </summary>
        public Class getDeclaringClass()
        {
            return default(Class);
        }

        /// <summary>
        /// Gets the value of a static or instance field of type <code>double</code> or of another primitive type convertible to type <code>double</code> via a widening conversion.
        /// </summary>
        public double getDouble(object obj)
        {
            return default(double);
        }

        /// <summary>
        /// Gets the value of a static or instance field of type <code>float</code> or of another primitive type convertible to type <code>float</code> via a widening conversion.
        /// </summary>
        public float getFloat(object obj)
        {
            return default(float);
        }

        /// <summary>
        /// Gets the value of a static or instance field of type <code>int</code> or of another primitive type convertible to type <code>int</code> via a widening conversion.
        /// </summary>
        public int getInt(object obj)
        {
            return default(int);
        }

        /// <summary>
        /// Gets the value of a static or instance field of type <code>long</code> or of another primitive type convertible to type <code>long</code> via a widening conversion.
        /// </summary>
        public long getLong(object obj)
        {
            return default(long);
        }

        /// <summary>
        /// Returns the Java language modifiers for the field represented by this <code>Field</code> object, as an integer.
        /// </summary>
        public int getModifiers()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the name of the field represented by this <code>Field</code> object.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Gets the value of a static or instance field of type <code>short</code> or of another primitive type convertible to type <code>short</code> via a widening conversion.
        /// </summary>
        public short getShort(object obj)
        {
            return default(short);
        }

        /// <summary>
        /// Returns a <code>Class</code> object that identifies the declared type for the field represented by this <code>Field</code> object.
        /// </summary>
        public Class getType()
        {
            return default(Class);
        }

        /// <summary>
        /// Sets the field represented by this <code>Field</code> object on the specified object argument to the specified new value.
        /// </summary>
        public void set(object obj, object value)
        {
        }

        /// <summary>
        /// Sets the value of a field as a <code>boolean</code> on the specified object.
        /// </summary>
        public void setBoolean(object obj, bool z)
        {
        }

        /// <summary>
        /// Sets the value of a field as a <code>byte</code> on the specified object.
        /// </summary>
        public void setByte(object obj, sbyte b)
        {
        }

        /// <summary>
        /// Sets the value of a field as a <code>char</code> on the specified object.
        /// </summary>
        public void setChar(object obj, char c)
        {
        }

        /// <summary>
        /// Sets the value of a field as a <code>double</code> on the specified object.
        /// </summary>
        public void setDouble(object obj, double d)
        {
        }

        /// <summary>
        /// Sets the value of a field as a <code>float</code> on the specified object.
        /// </summary>
        public void setFloat(object obj, float f)
        {
        }

        /// <summary>
        /// Sets the value of a field as an <code>int</code> on the specified object.
        /// </summary>
        public void setInt(object obj, int i)
        {
        }

        /// <summary>
        /// Sets the value of a field as a <code>long</code> on the specified object.
        /// </summary>
        public void setLong(object obj, long l)
        {
        }

        /// <summary>
        /// Sets the value of a field as a <code>short</code> on the specified object.
        /// </summary>
        public void setShort(object obj, short s)
        {
        }

        #endregion

    }
}
