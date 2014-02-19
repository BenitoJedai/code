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

package com.jgpshell.offCard;

/**
 * This class implements methods to manage application/applets installed on a Card.
 * 
 * @author Ben M Barka Moez.
 * @version $revision$
 */
public class CardApplication {
		
	private byte[] aid ;
	
	private byte privilege ;
	
	private byte life_cycle ;
	
	public CardApplication(){
		
	}
	
	/************ Accessors ***************/
	
	public void set_aid(byte[] aid){
		this.aid=aid ;
	}
	
	public void set_privilege(byte privilege){
		this.privilege=privilege ;
	}
	
	public void set_life_cycle(byte life_cycle){
		this.life_cycle=life_cycle ;
	}
	
	
	public byte[] get_aid(){
		return aid ;
	}
	
	public byte get_privilege(){
		return privilege ;
	}
	
	public byte get_life_cycle(){
		return life_cycle ;
	}
	
	
}


