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
 * This class defines all shell error codes.
 * 
 * @author Moez Ben MBarka
 * @version $revision
 */
public class Errors {
	
	/******** Error constants ***************/
	/***************************************/
	
	public static final int SUCCESS =0 ;
	public static final int FAILURE =1;

	/* Errors before the init of the session */
	public static final int UNABLE_GET_LIST_RDRS =10 ;
	
	public static final int UNABLE_INIT_SESSION = 20 ;
	
	public static final int ANY_RDR = 50 ;
	
	
	/* errors after the init of the session */
	public static final int WRONG_RDR_NB=100 ;
	
	public static final int UNABLE_CONNECT_CARD =110;	
	
	public static final int FINALIZING_CONTEXT_FAILURE = 150;
	
	public static final int INIT_UPDATE_FAILURE = 115 ;
	
	public static final int EXT_AUTH_FAILURE = 117;
	
	public static final int AUTHENTICATION_FAILURE =120;
	
	public static final int CONNECT_BEFORE = 130;
	
	
	/* Warnings */
	public static final int NOT_CONNECTED_ANY_CARD = 200;
	
	
	/* SQL errors/warnings */	
	public static final int UNABLE_CONNECT_SQL =300 ; 
	public static final int UNABLE_GET_KEYS = 310;
	
	
	/* Errors when communicate with the card after authentification */
	public static final int SEND_APDU_FAILURE = 400 ;
	public static final int UPLOAD_FAILURE = 410;
	public static final int INSTALL_FAILURE = 420;
	public static final int INSTALL_APPLET_FAILURE = 430;
	public static final int DELETE_FAILURE = 440;
	public static final int FORMAT_FAILURE = 450;
	public static final int LS_FAILURE=460;
	public static final int SELECT_FAILURE=470;
	
	
	/* General errors */
	public static final int BAD_APDU = 500 ;
	
	/* Shell Errors */
	public static final int IO_ERROR = 600 ;
	public static final int SYNTAX_ERROR = 610;
	public static final int VARIABLE_ACCESS_DENIED=620 ;
	public static final int INVALID_PARAMETER=630 ;
	public static final int FATAL_ERROR=640 ;
	
	/* script errors */
	public static final int FILE_NOT_FOUND = 700 ;
	
	
	
	
	/**
	 * 
	 * @param errCode
	 * @param opt : additional info about the error
	 * @return
	 */
	public static String get_errorMsg(int errCode, String opt){
		
		switch(errCode){
		case SUCCESS :
			return "Success." ;
			
		case FAILURE :
			return "Failure." ;
			
		case UNABLE_GET_LIST_RDRS :
			return "Unable to gt the list of readers" ;
		
		case UNABLE_INIT_SESSION :
			return "Unable to init the session : " + opt + ". Try /reinit ." ;
			
		case ANY_RDR :
			return "No reader detected !" ;
			
		case WRONG_RDR_NB :
			return "Wrong reader number :" + opt ;
			
		case UNABLE_CONNECT_CARD : 
			return "Can not connect to the card into the reader : " + opt ;
	
		case FINALIZING_CONTEXT_FAILURE :
			return "CardGridUser : finalizing contex failure!" ;
		
		case NOT_CONNECTED_ANY_CARD :
			return "Exit from what ? You are not connected to any card !";
		
		case UNABLE_CONNECT_SQL :
			return "MySQL connection failure." ;
		
		case UNABLE_GET_KEYS :
			return "MySQL problem : Can not get keys of the reader " + opt + " from the database." ;
		
		case AUTHENTICATION_FAILURE :
			return "Authentification failure to the card into the reader " + opt + "!" ;
		
		case CONNECT_BEFORE :
			return "Connect before to one reader with /connect rdr_number ." ;
			
		case INIT_UPDATE_FAILURE :
			return "The command init-update failed !" ;
			
		case EXT_AUTH_FAILURE :
			return "The command ext-auth failed. Try to do init-update again." ;
		
		case SEND_APDU_FAILURE :
			return "Unable to send the apdu to to the card into the reader " + opt + "." ; 
		
		case BAD_APDU :
			return "Invalid apdu format : " + opt + " !" ;
			
		case UPLOAD_FAILURE :
			return "The upload of " + opt + " failed." ;
			
		case INSTALL_FAILURE :
			return "The installation of " + opt + " failed" ;
		
		case INSTALL_APPLET_FAILURE :
			return "The installation of the applet failed." ;
		
		case DELETE_FAILURE :
			return "The delete commmand failed." ;
			
		case FORMAT_FAILURE :
			return "The format command failed" ;
		
		case LS_FAILURE :
			return "The ls command failed." ;
		
		case SELECT_FAILURE :
			return "The command select failed." ;
		
		case IO_ERROR :
			return "General IO error !" ;
		
		case SYNTAX_ERROR :
			return "Syntax error (near '" + opt +"') !" ;
			
		case FILE_NOT_FOUND :
			return opt + " :File not found !" ;	
			
		case VARIABLE_ACCESS_DENIED :
			return "Access denied !" ;
		
		case INVALID_PARAMETER :
			return "Invalid parameter: " + opt ;
			
		case FATAL_ERROR :
			return "jgpshell fatal error !" ;
			
		default :
			return "Unknown error !" ;	
		}
	}
	
	/**
	 * 
	 * @param errCode
	 * @return
	 */
	public static String get_errorMsg(int errCode){
		return get_errorMsg(errCode, "") ;
	}
	
}


