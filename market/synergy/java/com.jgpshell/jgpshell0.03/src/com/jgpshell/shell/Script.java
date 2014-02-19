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


import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;

/**
 * 
 * @author Moez Ben MBarka
 *
 */
public class Script {
	
	
	private InputStream script_code ;
	
	/* If true we are in a loop body*/
	private boolean is_loop = false ;
        
	/* To manage imbricated loops we should use an arry of Loop*/
	private Loop loop ;
	
	private Screen screen ;
	
	private Session session ;
	
	/* Last shell line parsed */
	private String last_line ;
	
	/**
	 * This variable is used to count the number of tmp variable created
	 */
	private int tmp_var_index =0 ;
	
	/**
	 * The script will be imported line by line to this array
	 */
	private ArrayList script_lines = new ArrayList() ;
	
	
	/**
	 * 
	 * @param screen
	 * @param session
	 */
	public Script(Screen screen, Session session){
		this.screen = screen ;		
		this.session = session;
	}
        
	/**
	 * init a loop object
	 * 
	 * @return
	 */
	public Loop init_loop(){
		is_loop=true ;
		loop = new Loop((Command)this, script_lines) ;
		return loop ;
	}
	
	
	/**
	 * Can be used in the future to manage Array of Loops(loop inside of another loop...)
	 * 
	 * @return
	 */
	public Loop get_loop(){
		return loop ;
	}
	
	/**
	 * Import the script line by line into the array script_lines
	 * 
	 * @param file
	 * @return
	 * @throws IOException
	 */
	public int import_script(String file) throws IOException{
		BufferedReader bfr = new BufferedReader(new FileReader(file));
		String line ;
		script_lines.clear() ;
		while((line=bfr.readLine()) != null){
			script_lines.add(line);
		}
		
		bfr.close() ;
		
		return Errors.SUCCESS ;
	}	
	
	/**
	 * Create new variable and put into it value.
	 * The name of the variable is : "tmp_[tmp_var_index]"
	 * 
	 * @param value
	 * @return The new variable name
	 */
	public String create_var(String value){
		String name = "tmp" + tmp_var_index;
		while(session.is_set(name)){
			tmp_var_index ++ ;
			name="tmp" + tmp_var_index;
		}
		session.set_variable(name, value) ;
		return name ;
	}
	
	/**
	 * Use the Array script_lines, loop_lines to set last_line to line index line
	 * 
	 * @param line
	 */
	public void set_last_line(int line){
		//System.out.println("line=" + line);
		if (is_loop){
			//We are parsing a loop body
			last_line=(String)(get_loop().get_loop_lines().
                                    get(line)) ;
		}
		else{
			last_line=(String)script_lines.get(line) ;
		}	
		//System.out.println("line " + line + "  :" + last_line) ;
	}
        
        /**
         * Sets the last command line.
         *
         * @param las_line The last line.
         */
        public void set_last_line(String line) {
            this.last_line = last_line;
        }
        
        /**
         * Returns the last command line.
         *
         * @return The last line.
         */
        public String get_last_line() {
            return last_line;
        }
        
        /**
         * Sets the script code input stream.
         *
         * @param script the script code input stream.
         */
        public void set_script_code(InputStream script) {
            this.script_code = script_code;
        }
}


