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

/**
 * This class define session variable : name, value, access
 * 
 * @author Moez
 *
 */
public class Variable  implements Cloneable{
	
	public String name ;
	
	public String value ;
	
	/**
	 * "rwd" (r, w, d = 0/1)
	 * 
	 */
	public char[] permissions = new char[3] ;
	
	/**
	 * 
	 * @param name
	 * @param value
	 * @param perm
	 */
	public Variable(String name, String value, String perm){
		this.name=name ;
		this.value=value ;
		this.permissions=perm.toCharArray() ;

	}

	/**
	 * 
	 */
	public Object clone() throws CloneNotSupportedException{
		Variable n_var = (Variable)super.clone() ; 
		
		return n_var;
	}
	
	/**
	 * 
	 * @return
	 */
	public boolean can_write(){
		//String p=String.valueOf(permissions) ;
		if (permissions[1] == '1'){
			return true ;
		}
		return false ;
	}
}


