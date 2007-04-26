using ScriptCoreLib;

namespace java.lang
{
    [Script(IsNative=true)]
    public class ThreadGroup
    {
        #region methods
        /// <summary>
        /// Returns an estimate of the number of active threads in this thread group.
        /// </summary>
        public int activeCount()
        {
            return default(int);
        }

        /// <summary>
        /// Returns an estimate of the number of active groups in this thread group.
        /// </summary>
        public int activeGroupCount()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>The definition of this call depends on <A HREF="../../java/lang/ThreadGroup.html#suspend()"><CODE>suspend()</CODE></A>,		   which is deprecated.  Further, the behavior of this call		   was never specified.</I>
        /// </summary>
        public bool allowThreadSuspension(bool b)
        {
            return default(bool);
        }

        /// <summary>
        /// Determines if the currently running thread has permission to  modify this thread group.
        /// </summary>
        public void checkAccess()
        {
        }

        /// <summary>
        /// Destroys this thread group and all of its subgroups.
        /// </summary>
        public void destroy()
        {
        }

        /// <summary>
        /// Copies into the specified array every active thread in this  thread group and its subgroups.
        /// </summary>
        public int enumerate(Thread[] list)
        {
            return default(int);
        }

        /// <summary>
        /// Copies into the specified array every active thread in this  thread group.
        /// </summary>
        public int enumerate(Thread[] list, bool recurse)
        {
            return default(int);
        }

        /// <summary>
        /// Copies into the specified array references to every active  subgroup in this thread group.
        /// </summary>
        public int enumerate(ThreadGroup[] list)
        {
            return default(int);
        }

        /// <summary>
        /// Copies into the specified array references to every active  subgroup in this thread group.
        /// </summary>
        public int enumerate(ThreadGroup[] list, bool recurse)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the maximum priority of this thread group.
        /// </summary>
        public int getMaxPriority()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the name of this thread group.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the parent of this thread group.
        /// </summary>
        public ThreadGroup getParent()
        {
            return default(ThreadGroup);
        }

        /// <summary>
        /// Interrupts all threads in this thread group.
        /// </summary>
        public void interrupt()
        {
        }

        /// <summary>
        /// Tests if this thread group is a daemon thread group.
        /// </summary>
        public bool isDaemon()
        {
            return default(bool);
        }

        /// <summary>
        /// Tests if this thread group has been destroyed.
        /// </summary>
        public bool isDestroyed()
        {
            return default(bool);
        }

        /// <summary>
        /// Prints information about this thread group to the standard  output.
        /// </summary>
        public void list()
        {
        }

        /// <summary>
        /// Tests if this thread group is either the thread group  argument or one of its ancestor thread groups.
        /// </summary>
        public bool parentOf(ThreadGroup g)
        {
            return default(bool);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method is used solely in conjunction with      <tt>Thread.suspend</tt> and <tt>ThreadGroup.suspend</tt>,       both of which have been deprecated, as they are inherently       deadlock-prone.  See <A HREF="../../java/lang/Thread.html#suspend()"><CODE>Thread.suspend()</CODE></A> for details.</I>
        /// </summary>
        public void resume()
        {
        }

        /// <summary>
        /// Changes the daemon status of this thread group.
        /// </summary>
        public void setDaemon(bool daemon)
        {
        }

        /// <summary>
        /// Sets the maximum priority of the group.
        /// </summary>
        public void setMaxPriority(int pri)
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method is inherently unsafe.  See     <A HREF="../../java/lang/Thread.html#stop()"><CODE>Thread.stop()</CODE></A> for details.</I>
        /// </summary>
        public void stop()
        {
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method is inherently deadlock-prone.  See     <A HREF="../../java/lang/Thread.html#suspend()"><CODE>Thread.suspend()</CODE></A> for details.</I>
        /// </summary>
        public void suspend()
        {
        }

        /// <summary>
        /// Called by the Java Virtual Machine when a thread in this  thread group stops because of an uncaught exception.
        /// </summary>
        public void uncaughtException(Thread t, Throwable e)
        {
        }

        #endregion

    }
}
