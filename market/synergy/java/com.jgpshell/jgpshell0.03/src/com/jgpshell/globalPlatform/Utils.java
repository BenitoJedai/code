//$Id$

/**
 * Author : Moez Ben MBarka Moez
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */



// $Id$

package com.jgpshell.globalPlatform;

import java.util.Random ;


/**
 * Some util methods. 
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class Utils {
	
    private static final char[] HEX_DIGITS =
    {
	'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'
    };
    private static int fromDigit(char ch) {
        if (ch >= '0' && ch <= '9')
            return ch - '0';
        if (ch >= 'A' && ch <= 'F')
            return ch - 'A' + 10;
        if (ch >= 'a' && ch <= 'f')
            return ch - 'a' + 10;
        throw new IllegalArgumentException("invalid hex digit '" + ch + "'");
    }
    
    /**
     * Returns a hex string representing the byte array.
     * @param ba The byte array to hexify.
     * @return The hex string.
     */
    public static String toHexString( byte[] ba ) {
        int length = ba.length;
        char[] buf = new char[length * 3];
        for (int i = 0, j = 0, k; i < length; ) {
            k = ba[i++];
            buf[j++] = HEX_DIGITS[(k >> 4) & 0x0F];
            buf[j++] = HEX_DIGITS[ k       & 0x0F];
            buf[j++] = ' ';
        }
        return new String(buf, 0, buf.length-1);
    }
    
    public static byte[] bytesFromHexString(String hex) throws NumberFormatException  {
        if (hex.length() == 0) return null;
        String myhex = hex + " ";
        int len = myhex.length();
        if ((len % 3) != 0) throw new NumberFormatException();
        byte[] buf = new byte[len / 3];
        int i = 0, j = 0;
        while (i < len) {
            try {
                buf[j++] = (byte) ((fromDigit(myhex.charAt(i++)) << 4) |
                        fromDigit(myhex.charAt(i++)));
            } catch (IllegalArgumentException e) {
                throw new NumberFormatException();
            }
            if (myhex.charAt(i++) != ' ') throw new NumberFormatException();
        }
        return buf;
    }
    
    public static byte[] buildHeader (byte cla,byte ins,byte p1,byte p2,byte lc){
        byte[] header={cla,ins,p1,p2,lc};
        return header;
    }

    public static byte[] rand_bytes(int size){
		Random rand =new Random() ;
		byte[] result  = new byte[size] ;
		rand.nextBytes(result) ;
		return result ;
	}
    
    public static byte[] clone_array(byte[] src){
    	byte[] dest = new byte[src.length] ;
    	System.arraycopy(src, 0, dest,0, src.length) ;
    	return dest;
    }
}


