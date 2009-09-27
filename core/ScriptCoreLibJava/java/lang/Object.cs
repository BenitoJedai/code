// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.lang.Object

using ScriptCoreLib;

namespace java.lang
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/lang/Object.html
	[Script(IsNative = true)]
	public class Object
	{
		/// <summary>
		/// 
		/// </summary>
		public Object()
		{
		}

		/// <summary>
		/// Creates and returns a copy of this object.
		/// </summary>
		protected object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Indicates whether some other object is "equal to" this one.
		/// </summary>
		public override bool Equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Called by the garbage collector on an object when garbage collection
		/// determines that there are no more references to the object.
		/// </summary>
		protected void finalize()
		{
		}

		/// <summary>
		/// Returns the runtime class of an object.
		/// </summary>
		public Class getClass()
		{
			return default(Class);
		}

		/// <summary>
		/// Returns a hash code value for the object.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Wakes up a single thread that is waiting on this object's
		/// monitor.
		/// </summary>
		public void notify()
		{
		}

		/// <summary>
		/// Wakes up all threads that are waiting on this object's monitor.
		/// </summary>
		public void notifyAll()
		{
		}

		/// <summary>
		/// Returns a string representation of the object.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Causes current thread to wait until another thread invokes the
		/// <A HREF="../../java/lang/Object.html#notify()"><CODE>notify()</CODE></A> method or the
		/// <A HREF="../../java/lang/Object.html#notifyAll()"><CODE>notifyAll()</CODE></A> method for this object.
		/// </summary>
		public void wait()
		{
		}

		/// <summary>
		/// Causes current thread to wait until either another thread invokes the
		/// <A HREF="../../java/lang/Object.html#notify()"><CODE>notify()</CODE></A> method or the
		/// <A HREF="../../java/lang/Object.html#notifyAll()"><CODE>notifyAll()</CODE></A> method for this object, or a
		/// specified amount of time has elapsed.
		/// </summary>
		public void wait(long @timeout)
		{
		}

		/// <summary>
		/// Causes current thread to wait until another thread invokes the
		/// <A HREF="../../java/lang/Object.html#notify()"><CODE>notify()</CODE></A> method or the
		/// <A HREF="../../java/lang/Object.html#notifyAll()"><CODE>notifyAll()</CODE></A> method for this object, or
		/// some other thread interrupts the current thread, or a certain
		/// amount of real time has elapsed.
		/// </summary>
		public void wait(long @timeout, int @nanos)
		{
		}


        [Script(ExternalTarget = "getClass", DefineAsInstance=true)]
        public static Class getClass(object a)
        {
            return default(Class);
        }

    }
}
