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


import java.io.ByteArrayInputStream;
import java.util.ArrayList;


/**
 * This class implements method to manage loop instruction in shell scripts
 * 
 * @author Moez Ben MBarak
 * @version $Revision$
 */
public class Loop {
	
	private Command script;
	
	private Session session;
	
	private Screen screen;
	
	private ArrayList script_lines = new ArrayList();
	
	/* Loop intervall */
	private String loop_min;
	private String loop_max;
	
	/* Name of the variable used for the loop*/
	private String loop_var;
	
	/* The code portion into the loop bloc */
	private int begin_line;
	private int begin_column;
	
	/* this index is used as a prefixe for the loop variable*/
	private int loop_index = 0;
	
	private ArrayList loop_lines = new ArrayList();
	
	/**
	 * 
	 * @param min
	 */
	public Loop(Command script , ArrayList script_lines){
		this.screen= script.get_screen();
		this.session = script.get_session();
		this.script_lines = script_lines;
		this.script=script;
	}
	
        /**
         * Returns the loop code.
         *
         * @return The loop code lines.
         */
        public ArrayList get_loop_lines() {
            return loop_lines;
        }
        
	/**
	 * This method is called at the end of a FOR bloc
	 * 
	 * @param el ending line of the FOR instruction bloc
	 * @param ec ending column of the FOR instruction bloc
	 */
	public int process(int el, int ec){
		
		String loop_code;
		
		while(session.get_int_variable(loop_var) <= session.get_int_variable(loop_max) ){			
			loop_code = get_loop_code(begin_line, begin_column+1, el, ec-1);
			if (loop_code.length() >0){
				JGPShell jgpshell = new JGPShell(script);
				//System.out.println("\nloop=" +loop_code);
				try{
					jgpshell.parse_program(new ByteArrayInputStream(loop_code.getBytes()));
				}catch(ParseException e){
					screen.error(Errors.SYNTAX_ERROR, "near " + jgpshell.get_last_cmd() + "(L=" + jgpshell.token.beginLine + " C=" + jgpshell.token.beginColumn);
					return Errors.SYNTAX_ERROR;
				}
			}
			/* Increment loop var */
			session.set_variable(loop_var, String.valueOf(Integer.parseInt(session.get_variable(loop_var)) + 1));
		}
		
		return Errors.SUCCESS;
	}
	
	/**
	 * 
	 * Use the array script_lines to retur the portion of the code 
	 * wicth contains the text between lines bl and el and between columns 
	 * bc and ec.
	 * At the same time, this method fill the array loop_lines.
	 * 
	 * @param bl begining line of the FOR instruction bloc
	 * @param bc begining column of the FOR instruction bloc
	 * @param el ending line of the FOR instruction bloc
	 * @param ec ending column of the FOR instruction bloc
	 * @return
	 */
	private String get_loop_code(int bl, int bc, int el, int ec){
		/* Dont forget that array are indexed from 0 ! */
		bl=bl-1;
		bc=bc-1;
		el=el-1;
		ec=ec-1;
		
		/* init */
		loop_lines.clear();
		String loop_code="";
		
		String first_line = (String)script_lines.get(bl);
		//System.out.println(first_line);
		/* If "}" is not at the end of the line  : add the rest of the line to loop_code*/
		if (bc<first_line.length()){
			loop_code=first_line.substring(bc, first_line.length());
			loop_lines.add(loop_code);
			loop_code=loop_code + "\n";
		}	
		
		/* Add the intermediate lines*/
		for(int i=bl+1; i<el; i++){
			loop_code=loop_code + (String)script_lines.get(i);
			loop_lines.add((String)script_lines.get(i));
			loop_code=loop_code + "\n";
		}
		
		String last_line=(String)script_lines.get(el);
		//System.out.println("ll=" +last_line.substring(0, ec));
		
		// add the last line if ec>0 
		if (ec>0){
			loop_code= loop_code + (last_line.substring(0, ec));
			loop_lines.add(last_line.substring(0, ec));
		}
		
		return loop_code;
	}
	
	/********* Accessors ***************/
	public void set_min(String min){
		loop_min=min;
		
		/* Init the loop variable with min value*/
		session.set_variable(loop_var, session.get_variable(min));
	}
	
	public void set_max(String max){
		loop_max = max;
	}
	
	public void set_loop_code_begin(int bl, int bc){
		//System.out.println("l=" + beginLine +"    c=" + beginColumn);
		begin_line=bl;
		begin_column=bc;
	}
	
	
	public void set_var(String var){
		loop_var=var;
	}
}


