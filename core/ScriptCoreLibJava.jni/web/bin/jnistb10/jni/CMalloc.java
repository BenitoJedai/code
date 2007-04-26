package jni;
/*
 * @(#)CMalloc.java	1.6 98/03/22
 *
 * Copyright (c) 1998 Sun Microsystems, Inc. All Rights Reserved.
 *
 * See also the LICENSE file in this distribution.
 */

/**
 * A <code>CPtr</code> to memory obtained from the C heap via a call to
 * <code>malloc</code>.
 * <p>
 * In some cases it might be necessary to use memory obtained from
 * <code>malloc</code>.  For example, <code>CMalloc</code> helps accomplish
 * the following idiom:
 * <pre>
 * 		void *buf = malloc(BUF_LEN * sizeof(char));
 *		call_some_function(buf);
 *		free(buf);
 * </pre>
 * <p>
 * <b>Remember to <code>free</code> any <code>malloc</code> space
 * explicitly</b>.  This class could perhaps contain a <code>finalize</code>
 * method that does the <code>free</code>, but note that in Java you should
 * not use finalizers to free resources.
 *
 * @author Sheng Liang
 * @see CPtr
 */
public class CMalloc extends CPtr {

    /**
     * Allocate space in the C heap via a call to C's <code>malloc</code>.
     *
     * @param size number of <em>bytes</em> of space to allocate
     */
    public CMalloc(int size) {
        this.size = size;
        peer = malloc(size);
	if (peer == 0) {
	    throw new OutOfMemoryError();
	}
    }

    /**
     * De-allocate space obtained via an earlier call to <code>malloc</code>.
     */
    public void free() {
        free(peer);
	peer = 0;
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyIn</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyIn(int,byte[],int,int) 
     */
    public void copyIn(int bOff, byte[] buf, int index, int length) {
        boundsCheck(bOff, length * 1);
	super.copyIn(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyIn</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyIn(int,short[],int,int)
     */
    public void copyIn(int bOff, short[] buf, int index, int length) {
        boundsCheck(bOff, length * 2);
	super.copyIn(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyIn</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyIn(int,char[],int,int)
     */
    public void copyIn(int bOff, char[] buf, int index, int length) {
        boundsCheck(bOff, length * 2);
	super.copyIn(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyIn</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyIn(int,int[],int,int) 
     */
    public void copyIn(int bOff, int[] buf, int index, int length) {
        boundsCheck(bOff, length * 4);
	super.copyIn(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyIn</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyIn(int,long[],int,int) 
     */
    public void copyIn(int bOff, long[] buf, int index, int length) {
        boundsCheck(bOff, length * 8);
	super.copyIn(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyIn</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyIn(int,float[],int,int)
     */
    public void copyIn(int bOff, float[] buf, int index, int length) {
        boundsCheck(bOff, length * 4);
	super.copyIn(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyIn</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyIn(int,double[],int,int) 
     */
    public void copyIn(int bOff, double[] buf, int index, int length) {
        boundsCheck(bOff, length * 8);
	super.copyIn(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyOut</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyOut(int,byte[],int,int) 
     */
    public void copyOut(int bOff, byte[] buf, int index, int length) {
        boundsCheck(bOff, length * 1);
	super.copyOut(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyOut</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyOut(int,short[],int,int)
     */
    public void copyOut(int bOff, short[] buf, int index, int length) {
        boundsCheck(bOff, length * 2);
	super.copyOut(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyOut</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyOut(int,char[],int,int) 
     */
    public void copyOut(int bOff, char[] buf, int index, int length) {
        boundsCheck(bOff, length * 2);
	super.copyOut(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyOut</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyOut(int,int[],int,int)
     */
    public void copyOut(int bOff, int[] buf, int index, int length) {
        boundsCheck(bOff, length * 4);
	super.copyOut(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyOut</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyOut(int,long[],int,int) 
     */
    public void copyOut(int bOff, long[] buf, int index, int length) {
        boundsCheck(bOff, length * 8);
	super.copyOut(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyOut</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyOut(int,float[],int,int) 
     */
    public void copyOut(int bOff, float[] buf, int index, int length) {
        boundsCheck(bOff, length * 4);
	super.copyOut(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.copyOut</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#copyOut(int,double[],int,int) 
     */
    public void copyOut(int bOff, double[] buf, int index, int length) {
        boundsCheck(bOff, length * 8);
	super.copyOut(bOff, buf, index, length);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getByte</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getByte(int)
     */
    public byte getByte(int offset) {
        boundsCheck(offset, 1);
	return super.getByte(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getShort</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getShort(int)
     */
    public short getShort(int offset) {
        boundsCheck(offset, 2);
	return super.getShort(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getInt</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getInt(int)
     */
    public int getInt(int offset) {
        boundsCheck(offset, 4);
	return super.getInt(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getLong</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getLong(int)
     */
    public long getLong(int offset) {
        boundsCheck(offset, 8);
	return super.getLong(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getFloat</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getFloat(int)
     */
    public float getFloat(int offset) {
        boundsCheck(offset, 4);
	return super.getFloat(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getDouble</code>.  But this method performs a bounds checks
     * to ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getDouble(int)
     */
    public double getDouble(int offset) {
        boundsCheck(offset, 8);
	return super.getDouble(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getCPtr</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getCPtr(int)
     */
    public CPtr getCPtr(int offset) {
        boundsCheck(offset, SIZE);
	return super.getCPtr(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.getString</code>.  But this method performs a bounds checks
     * to ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#getString(int)
     */
    public String getString(int offset) {
        boundsCheck(offset, 0);
	return super.getString(offset);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setByte</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setByte(int)
     */
    public void setByte(int offset, byte value) {
        boundsCheck(offset, 1);
	super.setByte(offset, value);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setShort</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setShort(int)
     */
    public void setShort(int offset, short value) {
        boundsCheck(offset, 2);
	super.setShort(offset, value);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setInt</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setInt(int)
     */
    public void setInt(int offset, int value) {
        boundsCheck(offset, 4);
	super.setInt(offset, value);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setLong</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setLong(int)
     */
    public void setLong(int offset, long value) {
        boundsCheck(offset, 8);
	super.setLong(offset, value);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setFloat</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setFloat(int)
     */
    public void setFloat(int offset, float value) {
        boundsCheck(offset, 4);
	super.setFloat(offset, value);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setDouble</code>.  But this method performs a bounds checks
     * to ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setDouble(int)
     */
    public void setDouble(int offset, double value) {
        boundsCheck(offset, 8);
	super.setDouble(offset, value);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setCPtr</code>.  But this method performs a bounds checks to
     * ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setCPtr(int)
     */
    public void setCPtr(int offset, CPtr value) {
        boundsCheck(offset, SIZE);
	super.setCPtr(offset, value);
    }

    /**
     * Indirect the C pointer to <code>malloc</code> space, a la
     * <code>CPtr.setString</code>.  But this method performs a bounds checks
     * to ensure that the indirection does not cause memory outside the
     * <code>malloc</code>ed space to be accessed.
     *
     * @see CPtr#setString(int)
     */
    public void setString(int offset, String value) {
        byte[] bytes = value.getBytes();
	int length = bytes.length;
        boundsCheck(offset, length + 1);
	super.copyIn(offset, bytes, 0, length);
	super.setByte(offset + length, (byte)0);
    }

    /* Size of the malloc'ed space. */
    private int size;

    /* Call the real C malloc. */
    private static native long malloc(int size);

    /* Call the real C free. */
    private static native void free(long ptr);

    /* Private to prevent creation of uninitialized malloc space. */
    private CMalloc() {}
    
    /* Check that indirection won't cause us to write outside the malloc'ed
       space. */
    private void boundsCheck(int off, int sz) {
        if (off < 0 || off + sz > size) {
	    throw new IndexOutOfBoundsException();
	}
    }
}



