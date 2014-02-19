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

package com.jgpshell.globalPlatform ;


/**
 * GP responses indicators. 
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class GP_responses{
	
	
	public final static int SUCCESS=0 ;
	
	public final static int MEMORY_FAILURE=2 ;
	
	public final static int FAILURE =3;
	
	public final static int REFERENCED_DATA_NOT_FOUND =4;
	
	public final static int APPLICATION_NOT_FOUND=5;
	
	public final static int INCORRECT_VALUES_IN_COMMAND_DATA=6;
	
	public final static int NOT_SPECIFIC_DIAGNOSIS=7;
	
	public final static int WRONG_LENGTH_LC=8;
	
	public final static int LOGICAL_CHANNEL_PROBLEM=9;
	
	public final static int SECURITY_STATUS_NOT_SATISFIED=10;
	
	public final static int CONDITIONS_USE_NOT_SATISFIED=11;
	
	public final static int INCORRECT_P1_P2=12;
	
	public final static int INVALID_INSTRUCTION=13;
	
	public final static int INVALID_CLASS=14;
	
	public final static int MORE_DATA_AVAILABLE=15;
	
	public final static int NOT_MEMORY_SPACE = 16;
	
	public final static int CARD_LIFE_CYCLE_IS_CARD_LOCKED=17 ;
	
	public final static int SECURE_MESSAGING_NOT_SUPPORTED=18 ;
	
	public final static int FUNCTION_NOT_SUPPORTED = 19 ;
	
	/**
	 * Return message error corresponding to the code err
	 *
	 * @param int errCode
	 */
	public static String getErrMsg(int err){
		switch(err){
		case MEMORY_FAILURE :
			return "Memory failure !" ;
			
		case REFERENCED_DATA_NOT_FOUND :
			return "R�ferenced data not found !" ;
			
		case APPLICATION_NOT_FOUND :
			return "Application not found !" ;
			
		case INCORRECT_VALUES_IN_COMMAND_DATA :
			return "Incorrect value in command data !" ;
			
		case NOT_SPECIFIC_DIAGNOSIS :
			return "Not specific diagnosis !" ;
			
		case WRONG_LENGTH_LC :
			return "Wrong length in Lc !" ;
			
		case LOGICAL_CHANNEL_PROBLEM :
			return "Logical channel not supported or not active !" ;
			
		case SECURITY_STATUS_NOT_SATISFIED :
			return "Security status not satisfied !" ;
			
		case CONDITIONS_USE_NOT_SATISFIED :
			return "Conditions of use not satisfied !" ;	
			
		case INCORRECT_P1_P2 :
			return "Incorrect P1 or P2!" ;
			
		case INVALID_INSTRUCTION :
			return "Invalid instruction !" ;
			
		case INVALID_CLASS :
			return "Invalid class !" ;
		
		case CARD_LIFE_CYCLE_IS_CARD_LOCKED :
			return "Card life cycle is 'card locked'." ;
		
		case NOT_MEMORY_SPACE :
			return "Not memory space !" ;
		
		case SECURE_MESSAGING_NOT_SUPPORTED :
			return "Secure messaging not supported." ;
		
		case FUNCTION_NOT_SUPPORTED :
			return "Function not supported, e.g:card life cycle state is locked." ;
			
		}
		
		return "Inknown GP error (" + err + ")!" ;
	}
	
	/*
	 * return an error code depending on the values of sw1 and sw2
	 */
	public static int sw_meaning(byte sw1, byte sw2){
		if (sw1 == (byte)0x62 && sw2 == (byte)0x83){
			return GP_responses.CARD_LIFE_CYCLE_IS_CARD_LOCKED ;
		}
		
		if (sw1 == (byte)0x63 && sw2 == (byte)0x10){
			return GP_responses.MORE_DATA_AVAILABLE ;
		}
		
		if (sw1 == (byte)0x64 && sw2 == (byte)0x00){
			return GP_responses.WRONG_LENGTH_LC ;
		}
		
		if (sw1 == (byte)0x65 && sw2 == (byte)0x81){
			return GP_responses.MEMORY_FAILURE ;
		}
		
		if (sw1 == (byte)0x68 && sw2 == (byte)0x82){
			return GP_responses.SECURE_MESSAGING_NOT_SUPPORTED ;
		}
		
		if (sw1 == (byte)0x68 && sw2 == (byte)0x81){
			return GP_responses.LOGICAL_CHANNEL_PROBLEM ;
		}
		
		if (sw1 == (byte)0x69 && sw2 == (byte)0x85){
			return GP_responses.CONDITIONS_USE_NOT_SATISFIED ;
		}
		
		if (sw1 == (byte)0x69 && sw2 == (byte)0x82){
			return GP_responses.SECURITY_STATUS_NOT_SATISFIED ;
		}
		
		if (sw1 == (byte)0x6A && sw2 == (byte)0x80){
			return GP_responses.INCORRECT_VALUES_IN_COMMAND_DATA ;
		}
		
		if (sw1 == (byte)0x6A && sw2 == (byte)0x81){
			return GP_responses.FUNCTION_NOT_SUPPORTED ;
		}
		
		if (sw1 == (byte)0x6A && sw2 == (byte)0x82){
			return GP_responses.APPLICATION_NOT_FOUND ;
		}	
		
		if (sw1 == (byte)0x6A && sw2 == (byte)0x84){
			return GP_responses.NOT_MEMORY_SPACE ;
		}
		
		if (sw1 == (byte)0x6A && sw2 == (byte)0x86){
			return GP_responses.INCORRECT_P1_P2 ;
		}
		
		if (sw1 == (byte)0x6A && sw2 == (byte)0x88){
			return GP_responses.REFERENCED_DATA_NOT_FOUND ;
		}
		
		if (sw1 == (byte)0x6D && sw2 == (byte)0x00){
			return GP_responses.INVALID_INSTRUCTION ;
		}
		
		if (sw1 == (byte)0x6E && sw2 == (byte)0x00){
			return GP_responses.INVALID_CLASS ;
		}
		
		if (sw1 == (byte)0x90 && sw2 == (byte)0x00){
			return GP_responses.SUCCESS ;
		}
		
		return GP_responses.FAILURE;
	}
	
	public static int get_GP_response(byte[] response){
		int i=response.length ;
		return sw_meaning(response[i-2], response[i-1]) ;
	}
}


