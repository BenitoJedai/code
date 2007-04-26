using ScriptCoreLib;

using java.net;

namespace java.lang
{
    [Script(IsNative=true)]
    public class Package
    {
        #region methods
        /// <summary>
        /// Return the title of this package.
        /// </summary>
        public string getImplementationTitle()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the name of the organization, vendor or company that provided this implementation.
        /// </summary>
        public string getImplementationVendor()
        {
            return default(string);
        }

        /// <summary>
        /// Return the version of this implementation.
        /// </summary>
        public string getImplementationVersion()
        {
            return default(string);
        }

        /// <summary>
        /// Return the name of this package.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Find a package by name in the callers <code>ClassLoader</code> instance.
        /// </summary>
        public static Package getPackage(string name)
        {
            return default(Package);
        }

        /// <summary>
        /// Get all the packages currently known for the caller's <code>ClassLoader</code> instance.
        /// </summary>
        public static Package[] getPackages()
        {
            return default(Package[]);
        }

        /// <summary>
        /// Return the title of the specification that this package implements.
        /// </summary>
        public string getSpecificationTitle()
        {
            return default(string);
        }

        /// <summary>
        /// Return the name of the organization, vendor, or company that owns and maintains the specification of the classes that implement this package.
        /// </summary>
        public string getSpecificationVendor()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the version number of the specification that this package implements.
        /// </summary>
        public string getSpecificationVersion()
        {
            return default(string);
        }

        /// <summary>
        /// Compare this package's specification version with a desired version.
        /// </summary>
        public bool isCompatibleWith(string desired)
        {
            return default(bool);
        }

        /// <summary>
        /// Returns true if this package is sealed.
        /// </summary>
        public bool isSealed()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns true if this package is sealed with respect to the specified code source url.
        /// </summary>
        public bool isSealed(URL url)
        {
            return default(bool);
        }

        #endregion

    }
}
