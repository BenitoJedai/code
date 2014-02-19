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


package com.jgpshell.shell;

import java.io.PipedInputStream;
import java.io.PipedOutputStream;
import java.util.ArrayList;

/**
 * This class define useful methods to manage thread working for one reader 
 * 
 * @author Moez
 *
 */
public class R_thread extends R_thread_status{
	
	/**
	 * 
	 */
	private int id;
	
	/**
	 * 
	 */
	private int status ;
	
	/**
	 * if permanent is set true, the thread will not be closed 
	 * if no command in the queue.
	 * 
	 */
	private boolean permanent = false;
	
	/**
	 * The reader name associated to the thread
	 */
	private String rdr_name ;
	
	/**
	 * Collection of the return values of the xecuted commands
	 */
	private ArrayList ret_values =new ArrayList();
	
	/**
	 * Collection of the executed commands
	 */
	private ArrayList cmds=new ArrayList() ;
	
	/**
	 * Last cmd added
	 */
	private String current_cmd;
	
	private PipedInputStream pipe_in ;
	
	private PipedOutputStream pipe_out ;
	
	private char[] tag;
	
	/**
	 * 
	 * @param rdr_name
	 * @param rdr_number
	 * @param cmd
	 * @param pipe_in
	 * @param pipe_out
	 */
	public R_thread(int id, String rdr_name, String cmd, PipedInputStream pipe_in, PipedOutputStream pipe_out){
		this.id=id;
		this.rdr_name = rdr_name;
		add_cmd(cmd) ;
		this.pipe_in=pipe_in;
		this.pipe_out  =pipe_out;
		set_status(RUNNING) ;
	}
	
	/**
	 * Configure the r_thread according to the tag :
	 * tag[0]='1' : permanent=true
	 * 
	 * @param tag
	 */
	public void set_tag(String t){
		//System.out.println("tag=" +t) ;
		if (t != null){
			this.tag = t.toCharArray();
			if (tag[0]== '1'){
				set_permanent(true) ;
			}
		}	
	}
	
	/********** accessors ***********/
	
	/**
	 * 
	 */
	public String get_rdr_name(){
		return rdr_name ;
	}
	
	/**
	 * 
	 * @return
	 */
	public PipedInputStream get_pipe_in(){
		return pipe_in ;
	}
	
	/**
	 * 
	 * @return
	 */
	public PipedOutputStream get_pipe_out(){
		return pipe_out ;
	}
	
	/**
	 * 
	 * @param ret_value
	 */
	public void add_ret_value(int ret_value){
		ret_values.add(new Integer(ret_value)) ;
	}
	
	/**
	 * 
	 * @param cmd
	 */
	public void add_cmd(String cmd){
		cmds.add(cmd) ;
		current_cmd=cmd;
	}
	
	/**
	 * 
	 * @return
	 */
	public String get_current_cmd(){
		return current_cmd ;
	}
	
	/**
	 * 
	 * @return
	 */
	public int get_status(){
		return status ;
	}
	
	public void set_status(int status){
		this.status=status;
	}
	
	public int get_id(){
		return id;
	}
	
	public void set_permanent(boolean per){
		permanent=per ;
	}
	
	public boolean is_permanent(){
		return permanent;
	}
	
	/**
	 * 
	 */
	public ArrayList get_ret_values(){
		return ret_values;
	}
	
	public ArrayList get_cmds(){
		return cmds ;
	}
}


