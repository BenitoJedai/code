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

package com.jgpshell.offCard ;

/**
 * Class used for all user exceptions
 *
 * @author Moez Ben M Barka.
 * @version $Revision$
 */
public class UserException extends Exception{
	private int code = 0 ;
	
	public UserException(){
		super() ;
	}
	
	public UserException(String msg){
		super(msg) ;
	}
	
	public UserException(String msg, int code){
		super(msg);
		this.code = code ;
	}

	
	public int getCode(){
		return code;
	}
	
	public String getMessage(){
		return super.getMessage() ;
	}
}


