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

/**
 * GlobalPlatform constants.
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class GP_constants {
	/* Macros used for tht Application Privileges */
	private static final byte SECURITY_DOMAIN           = (byte) 0x80;
	private static final byte DAP_VERIFICATION          = (byte) 0xC0;
	private static final byte DELEGATED_MANAGEMENT      = (byte) 0xA0;
	private static final byte CARD_LOCK                 = (byte) 0x10;
	private static final byte CARD_TERMINATE            = (byte) 0x08;
	private static final byte DEFAULT_SELECTED          = (byte) 0x04;
	private static final byte CVM_MANAGEMENT            = (byte) 0x02;
	private static final byte MANDATED_DAP_VERIFICATION = (byte) 0xC1;
	
	/* Life cycle states*/
	private static final byte INSTALLED = (byte)0x03;
	private static final byte SELECTABLE = (byte)0x07;
	private static final byte LOCKED = (byte)0x83;
	private static final byte LOADED = (byte)0x01;
	
	public static String get_text_privilege(byte privil){
		String privilege ;
		switch(privil) {
		case SECURITY_DOMAIN :
			privilege = "Security Domain";
			break;
			
		case DAP_VERIFICATION :
			privilege = "DAP Verification";
			break;
			
		case DELEGATED_MANAGEMENT :
			privilege = "Delegated Management";
			break;
			
		case CARD_LOCK :
			privilege = "Card Lock";
			break;
			
		case CARD_TERMINATE :
			privilege = "Card terminate";
			break;
			
		case DEFAULT_SELECTED :
			privilege = "Default Selected";
			break;
			
		case CVM_MANAGEMENT :
			privilege = "CVM management";
			break;     
			
		case MANDATED_DAP_VERIFICATION :
			privilege = "Mandated DAP Verification";
			break;
			
		default :
			privilege = "Unknown";
		}		
		return privilege ;
	}
	
	public static String get_text_life_cycle(byte life_cycle){
		String lifeCycleState ;
		switch(life_cycle) {
		case INSTALLED:
			lifeCycleState = "INSTALLED";
			break;
		case SELECTABLE:
			lifeCycleState = "SELECTABLE";
			break;
		case LOCKED:
			lifeCycleState = "LOCKED";
			break;
		case LOADED:
			lifeCycleState = "LOADED";
			break;	
			
		default :
			lifeCycleState = "Unknown";
		}
		
		return lifeCycleState ;
	}
	
}


