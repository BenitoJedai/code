using ScriptCoreLib;

 
namespace java.io
{
    /// <summary>
    /// Convenience class for writing character files. The constructors of this class assume that the default character encoding and the default byte-buffer size are acceptable. To specify these values yourself, construct an OutputStreamWriter on a FileOutputStream. 
    /// </summary>
    [Script(IsNative = true)]
    public class FileWriter : OutputStreamWriter
    {
        // Field Summary
        // Fields inherited from class java.io.Writer

        // Constructor Summary
        /// <summary>
        /// Constructs a FileWriter object given a File object.
        /// </summary>
        public FileWriter(File file)
        {
        }

        /// <summary>
        /// Constructs a FileWriter object given a File object.
        /// </summary>
        public FileWriter(File file, bool append)
        {
        }

        /// <summary>
        /// Constructs a FileWriter object associated with a file descriptor.
        /// </summary>
        public FileWriter(FileDescriptor fd)
        {
        }

        /// <summary>
        /// Constructs a FileWriter object given a file name.
        /// </summary>
        public FileWriter(string fileName)
        {
        }

        /// <summary>
        /// Constructs a FileWriter object given a file name with a boolean indicating whether or not to append the data written.
        /// </summary>
        public FileWriter(string fileName, bool append)
        {
        }

        // Methods inherited from class java.io.OutputStreamWriter
        // Methods inherited from class java.io.Writer
        // Methods inherited from class java.lang.Object

    }
}

