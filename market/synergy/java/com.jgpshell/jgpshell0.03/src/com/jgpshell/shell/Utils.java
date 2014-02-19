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

package com.jgpshell.shell;

import java.io.FileInputStream;
import java.io.IOException;

/**
 * Many utils methods
 * 
 * @author Moez Ben MBarka
 *
 */
public class Utils {
	
	/**
	 * Delete quotes if the first character is quote
	 * @param txt 
	 * @return
	 */
	public static String delete_quotes(String txt){
		if (txt.charAt(0) == '"')
			return txt.substring(1, txt.length()-1);
		else
			return txt;
	}
	
	public static void display_apdu(byte[] apdu){
		
	}
	
	public static int close_file(Screen screen, FileInputStream file){
		try{
			file.close() ;
		}catch(IOException e){
			screen.error(Errors.IO_ERROR) ;
			return Errors.IO_ERROR ;
		}
		return Errors.SUCCESS ;
	}
}


