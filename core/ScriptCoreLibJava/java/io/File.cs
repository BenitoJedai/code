using ScriptCoreLib;

using java.net;

namespace java.io
{
    /// <summary>
    /// http://java.sun.com/j2se/1.4.2/docs/api/java/io/File.html
    /// </summary>
    [Script(IsNative = true)]
    public class File
    {
        public static string separator;

        public File(string filename)
        {
        }

        #region methods
        /// <summary>
        /// Tests whether the application can read the file denoted by this abstract pathname.
        /// </summary>
        public bool canRead()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests whether the application can modify to the file denoted by this abstract pathname.
        /// </summary>
        public bool canWrite()
        {
            return default(bool);
        }

        /// <summary>
        /// Compares two abstract pathnames lexicographically.
        /// </summary>
        public int compareTo(File pathname)
        {
            return default(int);
        }

        /// <summary>
        /// Compares this abstract pathname to another object.
        /// </summary>
        public int compareTo(object o)
        {
            return default(int);
        }

        /// <summary>
        /// Atomically creates a new, empty file named by this abstract pathname if and only if a file with this name does not yet exist.
        /// </summary>
        public bool createNewFile()
        {
            return default(bool);
        }

        /// <summary>
        /// Creates an empty file in the default temporary-file directory, using the given prefix and suffix to generate its name.
        /// </summary>
        public static File createTempFile(string prefix, string suffix)
        {
            return default(File);
        }

        /// <summary>
        /// Creates a new empty file in the specified directory, using the given prefix and suffix strings to generate its name.
        /// </summary>
        public static File createTempFile(string prefix, string suffix, File directory)
        {
            return default(File);
        }

        /// <summary>
        /// Deletes the file or directory denoted by this abstract pathname.
        /// </summary>
        public bool delete()
        {
            return default(bool);
        }

        /// <summary>
        /// Requests that the file or directory denoted by this abstract  pathname be deleted when the virtual machine terminates.
        /// </summary>
        public void deleteOnExit()
        {
        }

        /// <summary>
        /// Tests whether the file or directory denoted by this abstract pathname exists.
        /// </summary>
        public bool exists()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the absolute form of this abstract pathname.
        /// </summary>
        public File getAbsoluteFile()
        {
            return default(File);
        }

        /// <summary>
        /// Returns the absolute pathname string of this abstract pathname.
        /// </summary>
        public string getAbsolutePath()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the canonical form of this abstract pathname.
        /// </summary>
        public File getCanonicalFile()
        {
            return default(File);
        }

        /// <summary>
        /// Returns the canonical pathname string of this abstract pathname.
        /// </summary>
        public string getCanonicalPath()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the name of the file or directory denoted by this abstract pathname.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the pathname string of this abstract pathname's parent, or <code>null</code> if this pathname does not name a parent directory.
        /// </summary>
        public string getParent()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the abstract pathname of this abstract pathname's parent, or <code>null</code> if this pathname does not name a parent directory.
        /// </summary>
        public File getParentFile()
        {
            return default(File);
        }

        /// <summary>
        /// Converts this abstract pathname into a pathname string.
        /// </summary>
        public string getPath()
        {
            return default(string);
        }

        /// <summary>
        /// Tests whether this abstract pathname is absolute.
        /// </summary>
        public bool isAbsolute()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests whether the file denoted by this abstract pathname is a directory.
        /// </summary>
        public bool isDirectory()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests whether the file denoted by this abstract pathname is a normal file.
        /// </summary>
        public bool isFile()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests whether the file named by this abstract pathname is a hidden file.
        /// </summary>
        public bool isHidden()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the time that the file denoted by this abstract pathname was last modified.
        /// </summary>
        public long lastModified()
        {
            return default(long);
        }

        /// <summary>
        /// Returns the length of the file denoted by this abstract pathname.
        /// </summary>
        public long length()
        {
            return default(long);
        }

        /// <summary>
        /// Returns an array of strings naming the files and directories in the directory denoted by this abstract pathname.
        /// </summary>
        public string[] list()
        {
            return default(string[]);
        }

        /// <summary>
        /// Returns an array of strings naming the files and directories in the directory denoted by this abstract pathname that satisfy the specified filter.
        /// </summary>
        public string[] list(FilenameFilter filter)
        {
            return default(string[]);
        }

        /// <summary>
        /// Returns an array of abstract pathnames denoting the files in the directory denoted by this abstract pathname.
        /// </summary>
        public File[] listFiles()
        {
            return default(File[]);
        }

        /// <summary>
        /// Returns an array of abstract pathnames denoting the files and directories in the directory denoted by this abstract pathname that satisfy the specified filter.
        /// </summary>
        public File[] listFiles(FileFilter filter)
        {
            return default(File[]);
        }

        /// <summary>
        /// Returns an array of abstract pathnames denoting the files and directories in the directory denoted by this abstract pathname that satisfy the specified filter.
        /// </summary>
        public File[] listFiles(FilenameFilter filter)
        {
            return default(File[]);
        }

        /// <summary>
        /// List the available filesystem roots.
        /// </summary>
        public static File[] listRoots()
        {
            return default(File[]);
        }

        /// <summary>
        /// Creates the directory named by this abstract pathname.
        /// </summary>
        public bool mkdir()
        {
            return default(bool);
        }

        /// <summary>
        /// Creates the directory named by this abstract pathname, including any necessary but nonexistent parent directories.
        /// </summary>
        public bool mkdirs()
        {
            return default(bool);
        }

        /// <summary>
        /// Renames the file denoted by this abstract pathname.
        /// </summary>
        public bool renameTo(File dest)
        {
            return default(bool);
        }

        /// <summary>
        /// Sets the last-modified time of the file or directory named by this abstract pathname.
        /// </summary>
        public bool setLastModified(long time)
        {
            return default(bool);
        }

        /// <summary>
        /// Marks the file or directory named by this abstract pathname so that only read operations are allowed.
        /// </summary>
        public bool setReadOnly()
        {
            return default(bool);
        }

        /// <summary>
        /// Constructs a <tt>file:</tt> URI that represents this abstract pathname.
        /// </summary>
        public URI toURI()
        {
            return default(URI);
        }

        /// <summary>
        /// Converts this abstract pathname into a <code>file:</code> URL.
        /// </summary>
        public URL toURL()
        {
            return default(URL);
        }

        #endregion

    }
}
