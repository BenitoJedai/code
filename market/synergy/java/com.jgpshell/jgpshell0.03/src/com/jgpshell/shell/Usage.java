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
 * @author Moez Ben MBarka
 *
 */
public class Usage {
	
	/**
	 * displays a general help
	 */
	public static void usage(Screen screen){
		screen.writeln("JGPShell v 0.1");
		screen.writeln("commands list :") ;
	}
	
	/**
	 * Display help corresponding to the command cmd and returns true if the command is found
	 * returns false if not.
	 * 
	 * @param screen
	 * @param cmd
	 * @return
	 */
	public static boolean usage(Screen screen, String cmd){
		if (cmd.equals("/ls")){
			screen.writeln("/ls : Displays the list of connected readers") ;
			return true ;
		}
		
		
		return false;
	}
	
}


