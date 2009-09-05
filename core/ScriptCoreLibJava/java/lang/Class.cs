// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;
using java.lang.reflect;
using java.net;
using java.security;

namespace java.lang
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/lang/Class.html
	[Script(IsNative = true)]
	public class Class
	{
		/// <summary>
		/// Returns the assertion status that would be assigned to this
		/// class if it were to be initialized at the time this method is invoked.
		/// </summary>
		public bool desiredAssertionStatus()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the <code>Class</code> object associated with the class or
		/// interface with the given string name.
		/// </summary>
		public static Class forName(string @className)
		{
			return default(Class);
		}

		/// <summary>
		/// Returns the <code>Class</code> object associated with the class or
		/// interface with the given string name, using the given class loader.
		/// </summary>
		public Class forName(string @name, bool @initialize, ClassLoader @loader)
		{
			return default(Class);
		}

		/// <summary>
		/// Returns an array containing <code>Class</code> objects representing all
		/// the public classes and interfaces that are members of the class
		/// represented by this <code>Class</code> object.
		/// </summary>
		public Class[] getClasses()
		{
			return default(Class[]);
		}

		/// <summary>
		/// Returns the class loader for the class.
		/// </summary>
		public ClassLoader getClassLoader()
		{
			return default(ClassLoader);
		}

		/// <summary>
		/// Returns the <code>Class</code> representing the component type of an
		/// array.
		/// </summary>
		public Class getComponentType()
		{
			return default(Class);
		}

		/// <summary>
		/// Returns a <code>Constructor</code> object that reflects the specified
		/// public constructor of the class represented by this <code>Class</code>
		/// object.
		/// </summary>
		public Constructor getConstructor(Class[] @parameterTypes)
		{
			return default(Constructor);
		}

		/// <summary>
		/// Returns an array containing <code>Constructor</code> objects reflecting
		/// all the public constructors of the class represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Constructor[] getConstructors()
		{
			return default(Constructor[]);
		}

		/// <summary>
		/// Returns an array of <code>Class</code> objects reflecting all the
		/// classes and interfaces declared as members of the class represented by
		/// this <code>Class</code> object.
		/// </summary>
		public Class[] getDeclaredClasses()
		{
			return default(Class[]);
		}

		/// <summary>
		/// Returns a <code>Constructor</code> object that reflects the specified
		/// constructor of the class or interface represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Constructor getDeclaredConstructor(Class[] @parameterTypes)
		{
			return default(Constructor);
		}

		/// <summary>
		/// Returns an array of <code>Constructor</code> objects reflecting all the
		/// constructors declared by the class represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Constructor[] getDeclaredConstructors()
		{
			return default(Constructor[]);
		}

		/// <summary>
		/// Returns a <code>Field</code> object that reflects the specified declared
		/// field of the class or interface represented by this <code>Class</code>
		/// object.
		/// </summary>
		public Field getDeclaredField(string @name)
		{
			return default(Field);
		}

		/// <summary>
		/// Returns an array of <code>Field</code> objects reflecting all the fields
		/// declared by the class or interface represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Field[] getDeclaredFields()
		{
			return default(Field[]);
		}

		/// <summary>
		/// Returns a <code>Method</code> object that reflects the specified
		/// declared method of the class or interface represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Method getDeclaredMethod(string @name, Class[] @parameterTypes)
		{
			return default(Method);
		}

		/// <summary>
		/// Returns an array of <code>Method</code> objects reflecting all the
		/// methods declared by the class or interface represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Method[] getDeclaredMethods()
		{
			return default(Method[]);
		}

		/// <summary>
		/// If the class or interface represented by this <code>Class</code> object
		/// is a member of another class, returns the <code>Class</code> object
		/// representing the class in which it was declared.
		/// </summary>
		public Class getDeclaringClass()
		{
			return default(Class);
		}

		/// <summary>
		/// Returns a <code>Field</code> object that reflects the specified public
		/// member field of the class or interface represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Field getField(string @name)
		{
			return default(Field);
		}

		/// <summary>
		/// Returns an array containing <code>Field</code> objects reflecting all
		/// the accessible public fields of the class or interface represented by
		/// this <code>Class</code> object.
		/// </summary>
		public Field[] getFields()
		{
			return default(Field[]);
		}

		/// <summary>
		/// Determines the interfaces implemented by the class or interface
		/// represented by this object.
		/// </summary>
		public Class[] getInterfaces()
		{
			return default(Class[]);
		}

		/// <summary>
		/// Returns a <code>Method</code> object that reflects the specified public
		/// member method of the class or interface represented by this
		/// <code>Class</code> object.
		/// </summary>
		public Method getMethod(string @name, Class[] @parameterTypes)
		{
			return default(Method);
		}

		/// <summary>
		/// Returns an array containing <code>Method</code> objects reflecting all
		/// the public <em>member</em> methods of the class or interface represented
		/// by this <code>Class</code> object, including those declared by the class
		/// or interface and and those inherited from superclasses and
		/// superinterfaces.
		/// </summary>
		public Method[] getMethods()
		{
			return default(Method[]);
		}

		/// <summary>
		/// Returns the Java language modifiers for this class or interface, encoded
		/// in an integer.
		/// </summary>
		public int getModifiers()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the  name of the entity (class, interface, array class,
		/// primitive type, or void) represented by this <tt>Class</tt> object,
		/// as a <tt>String</tt>.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the package for this class.
		/// </summary>
		public Package getPackage()
		{
			return default(Package);
		}

		/// <summary>
		/// Returns the <code>ProtectionDomain</code> of this class.
		/// </summary>
		public ProtectionDomain getProtectionDomain()
		{
			return default(ProtectionDomain);
		}

		/// <summary>
		/// Finds a resource with a given name.
		/// </summary>
		public URL getResource(string @name)
		{
			return default(URL);
		}

		/// <summary>
		/// Finds a resource with a given name.
		/// </summary>
		public InputStream getResourceAsStream(string @name)
		{
			return default(InputStream);
		}

		/// <summary>
		/// Gets the signers of this class.
		/// </summary>
		public Object[] getSigners()
		{
			return default(Object[]);
		}

		/// <summary>
		/// Returns the <code>Class</code> representing the superclass of the entity
		/// (class, interface, primitive type or void) represented by this
		/// <code>Class</code>.
		/// </summary>
		public Class getSuperclass()
		{
			return default(Class);
		}

		/// <summary>
		/// Determines if this <code>Class</code> object represents an array class.
		/// </summary>
		public bool isArray()
		{
			return default(bool);
		}

		/// <summary>
		/// Determines if the class or interface represented by this
		/// <code>Class</code> object is either the same as, or is a superclass or
		/// superinterface of, the class or interface represented by the specified
		/// <code>Class</code> parameter.
		/// </summary>
		public bool isAssignableFrom(Class @cls)
		{
			return default(bool);
		}

		/// <summary>
		/// Determines if the specified <code>Object</code> is assignment-compatible
		/// with the object represented by this <code>Class</code>.
		/// </summary>
		public bool isInstance(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Determines if the specified <code>Class</code> object represents an
		/// interface type.
		/// </summary>
		public bool isInterface()
		{
			return default(bool);
		}

		/// <summary>
		/// Determines if the specified <code>Class</code> object represents a
		/// primitive type.
		/// </summary>
		public bool isPrimitive()
		{
			return default(bool);
		}

		/// <summary>
		/// Creates a new instance of the class represented by this <tt>Class</tt>
		/// object.
		/// </summary>
		public object newInstance()
		{
			return default(object);
		}

		/// <summary>
		/// Converts the object to a string.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
