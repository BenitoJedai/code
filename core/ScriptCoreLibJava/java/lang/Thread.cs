using ScriptCoreLib;

namespace java.lang
{
    [Script(IsNative=true)]
    public class Thread : Runnable
    {
        public Thread(Runnable e)
        {
        }

        #region methods
        /// <summary>
        /// Returns the number of active threads in the current thread's thread group.
        /// </summary>
        public static int activeCount()
        {
            return default(int);
        }

        /// <summary>
        /// Determines if the currently running thread has permission to  modify this thread.
        /// </summary>
        public void checkAccess()
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>The definition of this call depends on <A HREF="../../java/lang/Thread.html#suspend()"><CODE>suspend()</CODE></A>,		   which is deprecated.  Further, the results of this call		   were never well-defined.</I>
        /// </summary>
        public int countStackFrames()
        {
            return default(int);
        }

        /// <summary>
        /// Returns a reference to the currently executing thread object.
        /// </summary>
        public static Thread currentThread()
        {
            return default(Thread);
        }

        /// <summary>
        /// Destroys this thread, without any cleanup.
        /// </summary>
        public void destroy()
        {
        }

        /// <summary>
        /// Prints a stack trace of the current thread.
        /// </summary>
        public static void dumpStack()
        {
        }

        /// <summary>
        /// Copies into the specified array every active thread in  the current thread's thread group and its subgroups.
        /// </summary>
        public static int enumerate(Thread[] tarray)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the context ClassLoader for this Thread.
        /// </summary>
        public ClassLoader getContextClassLoader()
        {
            return default(ClassLoader);
        }

        /// <summary>
        /// Returns this thread's name.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Returns this thread's priority.
        /// </summary>
        public int getPriority()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the thread group to which this thread belongs.
        /// </summary>
        public ThreadGroup getThreadGroup()
        {
            return default(ThreadGroup);
        }

        /// <summary>
        /// Returns <tt>true</tt> if and only if the current thread holds the monitor lock on the specified object.
        /// </summary>
        public static bool holdsLock(object obj)
        {
            return default(bool);
        }

        /// <summary>
        /// Interrupts this thread.
        /// </summary>
        public void interrupt()
        {
        }

        /// <summary>
        /// Tests whether the current thread has been interrupted.
        /// </summary>
        public static bool interrupted()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests if this thread is alive.
        /// </summary>
        public bool isAlive()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests if this thread is a daemon thread.
        /// </summary>
        public bool isDaemon()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests whether this thread has been interrupted.
        /// </summary>
        public bool isInterrupted()
        {
            return default(bool);
        }

        /// <summary>
        /// Waits for this thread to die.
        /// </summary>
        public void join()
        {
        }

        /// <summary>
        /// Waits at most <code>millis</code> milliseconds for this thread to  die.
        /// </summary>
        public void join(long millis)
        {
        }

        /// <summary>
        /// Waits at most <code>millis</code> milliseconds plus  <code>nanos</code> nanoseconds for this thread to die.
        /// </summary>
        public void join(long millis, int nanos)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method exists solely for use with <A HREF="../../java/lang/Thread.html#suspend()"><CODE>suspend()</CODE></A>,     which has been deprecated because it is deadlock-prone.     For more information, see      <a href="../../../guide/misc/threadPrimitiveDeprecation.html">Why      are Thread.stop, Thread.suspend and Thread.resume Deprecated?</a>.</I>
        /// </summary>
        public void resume()
        {
        }

        /// <summary>
        /// If this thread was constructed using a separate  <code>Runnable</code> run object, then that  <code>Runnable</code> object's <code>run</code> method is called;  otherwise, this method does nothing and returns.
        /// </summary>
        public void run()
        {
        }

        /// <summary>
        /// Sets the context ClassLoader for this Thread.
        /// </summary>
        public void setContextClassLoader(ClassLoader cl)
        {
        }

        /// <summary>
        /// Marks this thread as either a daemon thread or a user thread.
        /// </summary>
        public void setDaemon(bool on)
        {
        }

        /// <summary>
        /// Changes the name of this thread to be equal to the argument  <code>name</code>.
        /// </summary>
        public void setName(string name)
        {
        }

        /// <summary>
        /// Changes the priority of this thread.
        /// </summary>
        public void setPriority(int newPriority)
        {
        }

        /// <summary>
        /// Causes the currently executing thread to sleep (temporarily cease  execution) for the specified number of milliseconds.
        /// </summary>
        public static void sleep(long millis)
        {
        }

        /// <summary>
        /// Causes the currently executing thread to sleep (cease execution)  for the specified number of milliseconds plus the specified number  of nanoseconds.
        /// </summary>
        public static void sleep(long millis, int nanos)
        {
        }

        /// <summary>
        /// Causes this thread to begin execution; the Java Virtual Machine  calls the <code>run</code> method of this thread.
        /// </summary>
        public void start()
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method is inherently unsafe.  Stopping a thread with	     Thread.stop causes it to unlock all of the monitors that it	     has locked (as a natural consequence of the unchecked	     <code>ThreadDeath</code> exception propagating up the stack).  If       any of the objects previously protected by these monitors were in       an inconsistent state, the damaged objects become visible to       other threads, potentially resulting in arbitrary behavior.  Many       uses of <code>stop</code> should be replaced by code that simply       modifies some variable to indicate that the target thread should       stop running.  The target thread should check this variable         regularly, and return from its run method in an orderly fashion       if the variable indicates that it is to stop running.  If the       target thread waits for long periods (on a condition variable,       for example), the <code>interrupt</code> method should be used to       interrupt the wait.        For more information, see        <a href="../../../guide/misc/threadPrimitiveDeprecation.html">Why        are Thread.stop, Thread.suspend and Thread.resume Deprecated?</a>.</I>
        /// </summary>
        public void stop()
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method is inherently unsafe.  See <A HREF="../../java/lang/Thread.html#stop()"><CODE>stop()</CODE></A>        (with no arguments) for details.  An additional danger of this        method is that it may be used to generate exceptions that the        target thread is unprepared to handle (including checked        exceptions that the thread could not possibly throw, were it        not for this method).        For more information, see         <a href="../../../guide/misc/threadPrimitiveDeprecation.html">Why         are Thread.stop, Thread.suspend and Thread.resume Deprecated?</a>.</I>
        /// </summary>
        public void stop(Throwable obj)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method has been deprecated, as it is   inherently deadlock-prone.  If the target thread holds a lock on the   monitor protecting a critical system resource when it is suspended, no   thread can access this resource until the target thread is resumed. If   the thread that would resume the target thread attempts to lock this   monitor prior to calling <code>resume</code>, deadlock results.  Such   deadlocks typically manifest themselves as "frozen" processes.   For more information, see    <a href="../../../guide/misc/threadPrimitiveDeprecation.html">Why    are Thread.stop, Thread.suspend and Thread.resume Deprecated?</a>.</I>
        /// </summary>
        public void suspend()
        {
        }

        /// <summary>
        /// Causes the currently executing thread object to temporarily pause  and allow other threads to execute.
        /// </summary>
        public static void yield()
        {
        }

        #endregion

    }
}
