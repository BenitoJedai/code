package jni;

/*
 * @(#)Main.java	1.5 98/03/22
 *
 * Copyright (c) 1998 Sun Microsystems, Inc. All Rights Reserved.
 *
 * See also the LICENSE file in this distribution.
 */

/**
 * Provides a <code>main()</code> method for running the shared stubs example.
 * Shared stubs allow you to call native code (such as the standard C
 * library's <code>printf</code> function) from Java code, with very little
 * work.
 * <p>
 * This class uses our prototype implementation of the shared stubs
 * distributed with this example.
 *
 * @author Sheng Liang
 * @see    CFunc
 * @see	   CPtr
 * @see    CMalloc
 */
public class Main {

    /**
     * Demonstrates calling <code>printf</code>, <code>scanf</code>, etc from
     * the C library using shared stubs.
     * <p>
     * Note that this is an example demonstrating the use of shared stubs, and
     * must not be construed as us encouraging the use of <code>printf</code>
     * from Java code!  Infact, we strongly discourage you from doing so ---
     * the Java platform APIs provide powerful, type-safe and portable
     * alternatives for these C functions, and the Java versions will be a
     * performance win.  We hope that shared stubs will be useful to you if
     * you absolutely must write native methods.
     *<p>
     * Output from C's printf is enclosed in <>, to distinguish it from things
     * we print with System.out.println.
     *
     * @param main_args Arguments passed from the command line. Currently
     * 			unused.
     */
    public static void main(String[] main_args) {

	/* Which OS are we running on? */
	String osName = System.getProperty("os.name");
	String libc, libm;
	/* JDK1.1 returns "Solaris", 1.2 returns "SunOS". */
	if (osName.equals("SunOS") || osName.equals("Solaris")) {
	    libc = "libc.so";
	    libm = "libm.so";
	} else {
	    libc = libm = "msvcrt.dll";		  // Win32
	}
	
	/* Printing a message with printf(). Note that we first create an
	   instance of CFunc that wraps around C's printf(). Then we do the
	   actual call, dispatching to one of the "callXXX" methods on this
	   instance, based on the return type; in this case it happens to be
	   callInt.  Arguments to the C function are passed as an array of
	   Objects.  Notice we use the "anonymous array creation" syntax for
	   creating the array of arguments. */
	CFunc printf = new CFunc(libc, "printf");
	int ires = printf.callInt(new Object[]
		  {"\n<output from printf(): Running %s, eh?>\n", osName});
	System.out.println("printf() returned " + ires);

    
	int i = 4;
          
        if (i == 3)
        {
          
	/* Call time() with a NULL pointer. */
	CFunc time = new CFunc(libc, "time");
	ires = time.callInt(new Object[]{ CPtr.NULL });
	System.out.println("\ntime() reports seconds since 1/1/70 as " + ires);


	/* Little more complicated.  Firstly, ctime() takes a "int *" which
           points to an int containing the elapsed seconds since the epoch.
           So we will malloc() a 4 byte chunk with C's malloc, and initialize
           it the value we just got from time().  Secondly, ctime() returns a
           string, so be aware of that. */
	CMalloc timePtr = new CMalloc(4);
	timePtr.setInt(0, ires);
	CFunc ctime = new CFunc(libc, "ctime");
	CPtr returnedString = ctime.callCPtr(new Object[]{ timePtr });
	System.out.print("\nctime() reports " + returnedString.getString(0));
	/* We malloc()ed something from C heap, so we have to free() it. */
	timePtr.free();


	/* Read first word from stdin with scanf(). */
	System.out.println("\nPlease type something and then hit <return>");
	CFunc scanf = new CFunc(libc, "scanf");
	CMalloc cbuf = new CMalloc(128);
	ires = scanf.callInt(new Object[]{ "%s", cbuf });
	System.out.println("scanf() says first word you typed is \"" +
			   cbuf.getString(0) + "\"");
	/* malloc()ed memory must be freed! */
	cbuf.free();


	/* Caculate C's sin(2.0) with Math.sin(2.0). */
	CFunc sin = new CFunc(libm, "sin");
	double dres = sin.callDouble(new Object[]{new Double(2.0) });
	System.out.println("\nC's  sin(2.0) = " + dres);
	System.out.println("Math.sin(2.0) = " + Math.sin(2.0));
	

	/* clock().  Takes no arguments. */
	CFunc clock = new CFunc(libc, "clock");
	System.out.println("\nclock() returned " + 
			   clock.callInt(new Object[0]));
        }
        
    }
}
