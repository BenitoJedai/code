using ScriptCoreLib;

using java.security;
using java.net;
using java.io;
using java.util;

namespace java.lang
{
    [Script(IsNative=true)]
    public class ClassLoader
    {
        #region methods
        /// <summary>
        /// Sets the default assertion status for this class loader to <tt>false</tt> and discards any package defaults or class assertion status settings associated with the class loader.
        /// </summary>
        public void clearAssertionStatus()
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>Replaced by <A HREF="../../java/lang/ClassLoader.html#defineClass(java.lang.String, byte[], int, int)"><CODE>defineClass(String, byte[], int, int)</CODE></A></I>
        /// </summary>
        protected Class defineClass(sbyte[] b, int off, int len)
        {
            return default(Class);
        }

        /// <summary>
        /// Converts an array of bytes into an instance of class <tt>Class</tt>.
        /// </summary>
        protected Class defineClass(string name, sbyte[] b, int off, int len)
        {
            return default(Class);
        }

        /// <summary>
        /// Converts an array of bytes into an instance of class <tt>Class</tt>, with an optional <tt>ProtectionDomain</tt>.
        /// </summary>
        protected Class defineClass(string name, sbyte[] b, int off, int len, ProtectionDomain protectionDomain)
        {
            return default(Class);
        }

        /// <summary>
        /// Defines a package by name in this <tt>ClassLoader</tt>.
        /// </summary>
        protected Package definePackage(string name, string specTitle, string specVersion, string specVendor, string implTitle, string implVersion, string implVendor, URL sealBase)
        {
            return default(Package);
        }

        /// <summary>
        /// Finds the specified class.
        /// </summary>
        protected Class findClass(string name)
        {
            return default(Class);
        }

        /// <summary>
        /// Returns the absolute path name of a native library.
        /// </summary>
        protected string findLibrary(string libname)
        {
            return default(string);
        }

        /// <summary>
        /// Returns the class with the given name if this loader has been recorded by the Java virtual machine as an initiating loader of a class with that name.
        /// </summary>
        protected Class findLoadedClass(string name)
        {
            return default(Class);
        }

        /// <summary>
        /// Finds the resource with the given name.
        /// </summary>
        protected URL findResource(string name)
        {
            return default(URL);
        }

        /// <summary>
        /// Returns an enumeration of <A HREF="../../java/net/URL.html" title="class in java.net"><CODE><tt>URL</tt></CODE></A> objects representing all the resources with the given name.
        /// </summary>
        protected Enumeration findResources(string name)
        {
            return default(Enumeration);
        }

        /// <summary>
        /// Finds a class with the specified name, loading it if necessary.
        /// </summary>
        protected Class findSystemClass(string name)
        {
            return default(Class);
        }

        /// <summary>
        /// Returns a <tt>Package</tt> that has been defined by this class loader or any of its ancestors.
        /// </summary>
        protected Package getPackage(string name)
        {
            return default(Package);
        }

        /// <summary>
        /// Returns all of the <tt>Packages</tt> defined by this class loader and its ancestors.
        /// </summary>
        protected Package[] getPackages()
        {
            return default(Package[]);
        }

        /// <summary>
        /// Returns the parent class loader for delegation.
        /// </summary>
        public ClassLoader getParent()
        {
            return default(ClassLoader);
        }

        /// <summary>
        /// Finds the resource with the given name.
        /// </summary>
        public URL getResource(string name)
        {
            return default(URL);
        }

        /// <summary>
        /// Returns an input stream for reading the specified resource.
        /// </summary>
        public InputStream getResourceAsStream(string name)
        {
            return default(InputStream);
        }

        /// <summary>
        /// Finds all the resources with the given name.
        /// </summary>
        public Enumeration getResources(string name)
        {
            return default(Enumeration);
        }

        /// <summary>
        /// Returns the system class loader for delegation.
        /// </summary>
        public static ClassLoader getSystemClassLoader()
        {
            return default(ClassLoader);
        }

        /// <summary>
        /// Find a resource of the specified name from the search path used to load classes.
        /// </summary>
        public static URL getSystemResource(string name)
        {
            return default(URL);
        }

        /// <summary>
        /// Open for reading, a resource of the specified name from the search path used to load classes.
        /// </summary>
        public static InputStream getSystemResourceAsStream(string name)
        {
            return default(InputStream);
        }

        /// <summary>
        /// Finds all resources of the specified name from the search path used to load classes.
        /// </summary>
        public static Enumeration getSystemResources(string name)
        {
            return default(Enumeration);
        }

        /// <summary>
        /// Loads the class with the specified name.
        /// </summary>
        public Class loadClass(string name)
        {
            return default(Class);
        }

        /// <summary>
        /// Loads the class with the specified name.
        /// </summary>
        protected Class loadClass(string name, bool resolve)
        {
            return default(Class);
        }

        /// <summary>
        /// Links the specified class.
        /// </summary>
        protected void resolveClass(Class c)
        {
        }

        /// <summary>
        /// Sets the desired assertion status for the named top-level class in this class loader and any nested classes contained therein.
        /// </summary>
        public void setClassAssertionStatus(string className, bool enabled)
        {
        }

        /// <summary>
        /// Sets the default assertion status for this class loader.
        /// </summary>
        public void setDefaultAssertionStatus(bool enabled)
        {
        }

        /// <summary>
        /// Sets the package default assertion status for the named package.
        /// </summary>
        public void setPackageAssertionStatus(string packageName, bool enabled)
        {
        }

        /// <summary>
        /// Sets the signers of a class.
        /// </summary>
        protected void setSigners(Class c, object[] signers)
        {
        }

        #endregion

    }
}
