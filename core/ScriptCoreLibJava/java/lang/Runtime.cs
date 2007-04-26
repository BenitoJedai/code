using ScriptCoreLib;

using java.io;
using java.util;

namespace java.lang
{
    /// <summary>
    /// Every Java application has a single instance of class Runtime that allows the application to interface with the environment in which the application is running. The current runtime can be obtained from the getRuntime method. 
    /// </summary>
    [Script(IsNative = true)]
    public class Runtime
    {
        // Method Summary
        /// <summary>
        /// Registers a new virtual-machine shutdown hook.
        /// </summary>
        public void addShutdownHook(Thread hook)
        {
            return;
        }

        /// <summary>
        /// Returns the number of processors available to the Java virtual machine.
        /// </summary>
        public int availableProcessors()
        {
            return default(int);
        }

        /// <summary>
        /// Executes the specified string command in a separate process.
        /// </summary>
        public Process exec(string command)
        {
            return default(Process);
        }

        /// <summary>
        /// Executes the specified command and arguments in a separate process.
        /// </summary>
        public Process exec(string[] cmdarray)
        {
            return default(Process);
        }

        /// <summary>
        /// Executes the specified command and arguments in a separate process with the specified environment.
        /// </summary>
        public Process exec(string[] cmdarray, string[] envp)
        {
            return default(Process);
        }

        /// <summary>
        /// Executes the specified command and arguments in a separate process with the specified environment and working directory.
        /// </summary>
        public Process exec(string[] cmdarray, string[] envp, File dir)
        {
            return default(Process);
        }

        /// <summary>
        /// Executes the specified string command in a separate process with the specified environment.
        /// </summary>
        public Process exec(string cmd, string[] envp)
        {
            return default(Process);
        }

        /// <summary>
        /// Executes the specified string command in a separate process with the specified environment and working directory.
        /// </summary>
        public Process exec(string command, string[] envp, File dir)
        {
            return default(Process);
        }

        /// <summary>
        /// Terminates the currently running Java virtual machine by initiating its shutdown sequence.
        /// </summary>
        public void exit(int status)
        {
            return;
        }

        /// <summary>
        /// Returns the amount of free memory in the Java Virtual Machine.
        /// </summary>
        public long freeMemory()
        {
            return default(long);
        }

        /// <summary>
        /// Runs the garbage collector.
        /// </summary>
        public void gc()
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public InputStream getLocalizedInputStream(InputStream @in)
        {
            return default(InputStream);
        }

        /// <summary>
        /// 
        /// </summary>
        public OutputStream getLocalizedOutputStream(OutputStream @out)
        {
            return default(OutputStream);
        }

        /// <summary>
        /// Returns the runtime object associated with the current Java application.
        /// </summary>
        public static Runtime getRuntime()
        {
            return default(Runtime);
        }

        /// <summary>
        /// Forcibly terminates the currently running Java virtual machine.
        /// </summary>
        public void halt(int status)
        {
            return;
        }

        /// <summary>
        /// Loads the specified filename as a dynamic library.
        /// </summary>
        public void load(string filename)
        {
            return;
        }

        /// <summary>
        /// Loads the dynamic library with the specified library name.
        /// </summary>
        public void loadLibrary(string libname)
        {
            return;
        }

        /// <summary>
        /// Returns the maximum amount of memory that the Java virtual machine will attempt to use.
        /// </summary>
        public long maxMemory()
        {
            return default(long);
        }

        /// <summary>
        /// De-registers a previously-registered virtual-machine shutdown hook.
        /// </summary>
        public bool removeShutdownHook(Thread hook)
        {
            return default(bool);
        }

        /// <summary>
        /// Runs the finalization methods of any objects pending finalization.
        /// </summary>
        public void runFinalization()
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void runFinalizersOnExit(bool value)
        {
            return;
        }

        /// <summary>
        /// Returns the total amount of memory in the Java virtual machine.
        /// </summary>
        public long totalMemory()
        {
            return default(long);
        }

        /// <summary>
        /// Enables/Disables tracing of instructions.
        /// </summary>
        public void traceInstructions(bool on)
        {
            return;
        }

        /// <summary>
        /// Enables/Disables tracing of method calls.
        /// </summary>
        public void traceMethodCalls(bool on)
        {
            return;
        }


    }
}

