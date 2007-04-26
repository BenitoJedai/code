package jni;

/*
 * @(#)CFunc.java	1.6 98/03/22
 *
 * Copyright (c) 1998 Sun Microsystems, Inc. All Rights Reserved.
 *
 * See also the LICENSE file in this distribution.
 */

/**
 * An abstraction for a C function pointer.  An instance of <code>CFunc</code>
 * repesents a pointer to some C function.  <code>callXXX</code> methods
 * provide means to call the function; select a <code>XXX</code> variant based
 * on the return type of the C function.
 *<p>
 * Beware that the <code>copyIn</code>, <code>copyOut</code>,
 * <code>setXXX</code>, and <code>getXXX</code> methods inherited from the
 * parent will indirect machine code.
 *
 * @author Sheng Liang
 * @see CPtr
 */
public class CFunc extends CPtr {

    /* calling convention, not set now because we only support the "C"
     * convention.
     */
    private int callingConvention;

    /* Find names function in the named dll. */
    private native long find(String lib, String fname);

    /**
     * Create a new <code>CFunc</code> that is linked with a C function that
     * follows a given calling convention.
     * <p>
     * The allocated instance represents a pointer to the named C function
     * from the named library, called with the named calling convention.
     * <p>
     * @param lib   library in which to find the C function
     * @param fname name of the C function to be linked with
     * @param conv  calling convention used by the C function
     */
    public CFunc(String lib, String fname, String conv) {
        if (!conv.equals("C")) {
	    throw new IllegalArgumentException
	        ("unrecognized calling convention: " + conv);
	}
        peer = find(lib, fname);
    }

    /**
     * Create a new <code>CFunc</code> that is linked with a C function that
     * follows the standard "C" calling convention.
     * <p>
     * The allocated instance represents a pointer to the named C function
     * from the named library, called with the standard "C" calling
     * convention.
     * <p>
     * @param lib   library in which to find the C function
     * @param fname name of the C function to be linked with
     */
    public CFunc(String lib, String fname) {
        peer = find(lib, fname);
    }
    
    /**
     * Call the C function being represented by this object.
     *
     * @param  args arguments to pass to the C function
     * @return      <code>int</code> value returned by the underlying
     *		    C function
     */
    public native int callInt(Object[] args);
    
    /**
     * Call the C function being represented by this object.
     *
     * @param  args arguments to pass to the C function
     */
    public native void callVoid(Object[] args);

    /**
     * Call the C function being represented by this object.
     *
     * @param  args arguments to pass to the C function
     * @return      <code>float</code> value returned by the underlying
     *		    C function
     */
    public native float callFloat(Object[] args);

    /**
     * Call the C function being represented by this object.
     *
     * @param  args arguments to pass to the C function
     * @return      <code>double</code> value returned by the underlying
     *		    C function
     */
    public native double callDouble(Object[] args);

    /**
     * Call the C function being represented by this object.
     *
     * @param  args arguments to pass to the C function
     * @return      C pointer returned by the underlying C function
     */
    public native CPtr callCPtr(Object[] args);

    /* Don't allow creation of unitializaed CFunc objects. */
    private CFunc() {}

}
