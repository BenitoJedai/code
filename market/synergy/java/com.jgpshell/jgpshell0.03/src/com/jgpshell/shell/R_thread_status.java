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
 * Defines possible status of a R_thread
 * 
 * @author Moez
 *
 */
public class R_thread_status {
	
	/* The thread is running */
	public static final int RUNNING = 10 ;
	
	public static final int FINISHED = 20 ;
	
	public static final int STOPPED =30;
	
	public static String get_statusMsg(int status){
		
		switch(status){
		case RUNNING :
			return "Running" ;
		case FINISHED :
			return "Finished" ;
		case STOPPED :
			return "Stopped" ;
		default :
			return "I dont know !" ;
		}
	}
}


