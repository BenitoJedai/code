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

import java.io.BufferedWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PipedInputStream;
import java.io.PipedOutputStream;
import java.io.Writer;

/**
 * @author Moez
 *
 */
public class PipedScreen extends Screen{
	
	private PipedInputStream pipe_in ;
	
	private PipedOutputStream pipe_out ;
	
	private Writer out ;
	
	public PipedScreen(PipedInputStream pipe_in, PipedOutputStream pipe_out){
		super(pipe_in, System.out) ;
		this.pipe_in = pipe_in ;
		this.pipe_out = pipe_out ;
		
		//out= new BufferedWriter(new OutputStreamWriter(System.out));
	}
	
	/**
	 * 
	 * @return true if cmd queu is not empty 
	 * @throws IOException
	 */
	public boolean available() {
		try{
			return (pipe_in.available() >0) ;
		}catch(IOException e){
			return false ;
		}
	}

	
}


