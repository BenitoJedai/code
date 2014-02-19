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

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.util.Date;
import java.sql.Time;



/**
 * Methods to generate log files
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class Log {
	/* Log file name */
	private String logFileName="GP_log.log" ;	
	
	/* It will be set to 1 if the log file is ready*/
	private int writerState = 0 ;
	
	private FileOutputStream fos ;
	
	private Date d ;
	
	private Time t;
	
	
	/**
	 * 
	 * @param state : 0-> cancel log, 1->output in the log file, 2->std output
	 */
	public Log(int state){
		writerState = state;
		try{
			fos= new FileOutputStream(logFileName,true);
		}catch(Exception e){
			System.out.println("Can not write to the log file :" + logFileName) ;
			System.out.println("Log disabled !") ;
			writerState = 0 ;
		}
		this.write("****** New log session ******");		
	}
	
	public void setLog(int logDest){
		this.writerState = logDest ;
	}
	
	public void write(String info){
		d= new Date() ;
		t = new Time(d.getTime()) ;
		String txt = t + "->" +info + "\n";
		
		if (writerState == 2){
			System.out.println(txt) ;	
		}
		else if(writerState ==1){
			try{
				fos.write(txt.getBytes()) ;
			}catch(IOException e){
				System.out.println("IO error !Can not write to the log file.");
			}
		}
	}
	
	public void close(){
		try{
			fos.close();
		}
		catch(IOException e){
			System.out.println("Can not close the log file :" + logFileName) ;
			System.out.println("Log disabled !") ;
			writerState = 0 ;
		}
	}
}

