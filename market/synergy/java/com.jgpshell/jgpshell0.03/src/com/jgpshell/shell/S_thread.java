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

import com.jgpshell.offCard.UserException;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.PipedInputStream;
import java.io.PipedOutputStream;
import java.util.ArrayList;
import java.util.Iterator;

/**
 * @author Moez
 *
 */
public class S_thread extends Thread{
	
	/**
	 * Table used to make correspondance between created thread and rdr.
	 * All R_thread in this table should have the status RUNNING.
	 */
	protected static Table r_threads = new Table();
	
	/**
	 * Contains the history of all R_threads created.
	 * All R_thread in this table should not have the status RUNNING.
	 * 
	 */
	protected static ArrayList r_threads_history = new ArrayList();
	
	/**
	 * Contains r_thread wicth have just finished. Once displayed to the user, 
	 * they have to be moved ot r_threads_history 
	 * 
	 */
	protected static ArrayList r_threads_pre_history = new ArrayList() ;
	
	/* The lastid assigned to a thread */
	private static int lastId ;
	
	/**
	 *  Contains the initial informations  about the r_thread.
	 */
	private R_thread current_r_thread ;
	
	
	/**
	 * 
	 */
	private Session current_session;
	
	
	/**
	 * 
	 * @param session The current session. It will be cloned before starting the new thread
	 * @param r_thread
	 * @param rdr_name
	 * @param tag 
	 */
	public S_thread(Session session,R_thread r_thread, String rdr_name, String tag){
		try{
			/**
			 * The session used by the new thread must not be a reference to the main session.
			 * So we have to create a clone !
			 */
			current_session = (Session)session.clone();			
		}catch(CloneNotSupportedException e){
			session.fatal(e) ;
		}
		
		/* By now, a thread can not create another one*/
		current_session.r_thread_selected = false;
		
		current_r_thread= r_thread;
		r_thread.set_tag(tag) ;
	}
	
	public S_thread(){
		lastId = 0 ;
	}
	
	
	/**
	 * Check if a thread is already created to rdr_name, in this case cmd is sent to it.
	 * If not a new thread is created
	 * 
	 * @param session
	 * @param cmd
	 * @param rdr_name
	 * @param tag
	 * @throws UserException
	 */
	public void assign_cmd(Session session, String cmd, String rdr_name, String tag) throws UserException{
		if (have_thread(rdr_name)){
			/* If a thread is already assigned to the rdr_name*/
			add_cmd(cmd, rdr_name) ;
		}
		else{
			/* Create a new thread to execute cmd into the rdr_name*/			
			create_thread(session, cmd, rdr_name, tag) ;
		}
	}
	
	/**
	 * Send cmd to the buffer used by the thread connected to rdr_name.
	 * 
	 * @param cmd
	 * @param rdr_name
	 */
	private void add_cmd(String cmd, String rdr_name){
		R_thread r_thread=(R_thread)r_threads.get(rdr_name) ;
		try {
			
			//System.out.println("*" + cmd + "*");
			cmd = cmd + "\n" ;
			r_thread.get_pipe_out().write(cmd.getBytes()) ;
			r_thread.get_pipe_out().flush() ;
		}
		catch(IOException e){
			//fatal rror : the thread will be stopped
			kill_r_thread(rdr_name) ;
		}
	}
	
	
	/**
	 * Create and start new thread to execute cmd into rdr_name
	 * 
	 * @param cmd
	 * @param rdr_name
	 * @throws UserException
	 */
	private void create_thread(Session session, String cmd, String rdr_name, String tag) throws UserException{
		PipedOutputStream pipe_out = new PipedOutputStream() ; 	
		PipedInputStream pipe_in ;
		try{
			/* This pipe_in will be used to send cmds to the thread */
			pipe_in= new PipedInputStream(pipe_out) ;
		}catch(IOException e){
			throw new UserException ("Can not create PipedReaeder for the thread", Errors.IO_ERROR);
		}
		
		/* Create the thread and add it to r_threads Table*/
		R_thread r_thread = new R_thread(get_nextId(), rdr_name, cmd, pipe_in, pipe_out) ;
		add_thread(rdr_name, r_thread) ;
		
		/* Launch the new thread*/
		S_thread n_thread = new S_thread(session, r_thread, rdr_name, tag) ;		
		n_thread.start()  ;
	}
	
	/**
	 * What each new thread has to do !
	 * 
	 */
	public void run(){
		PipedScreen screen = new PipedScreen(current_r_thread.get_pipe_in(), 
                        current_r_thread.get_pipe_out()) ;
		screen.disable_out() ;
		int ret=999 ; //return value
		int nb_cmd=0;
		
		/**/
		Command command = new Command(screen, current_session) ;
		
		/* The first thing to do is connecting to the rdr */
		int con=command.connect(current_r_thread.get_rdr_name()) ;
		if (con != Errors.SUCCESS){
			// If the connection failed, the thread must be killed
			//System.out.println(Errors.get_errorMsg(con)) ;
			current_r_thread.add_ret_value(con) ;
			current_r_thread.set_status(R_thread.STOPPED) ;
		}
		else{
			JGPShell jgpshell = new JGPShell(command) ;
			
			String cmd=current_r_thread.get_current_cmd();
			
			do{
				//System.out.println(cmd);
				if (nb_cmd > 0){
					//System.out.println("Reading...") ;
					cmd = screen.readLine() ;
					if (cmd.length() < 1){
						continue ;
					}
					/* add the cmd to cmd list */
					current_r_thread.add_cmd(cmd) ;
					//System.out.println("cmd=" + cmd + "*") ;
				}	
				try{	
					jgpshell.parse_command(new ByteArrayInputStream(cmd.getBytes())) ;
					ret=jgpshell.get_ret() ;
				}catch(TokenMgrError e){
					ret=Errors.SYNTAX_ERROR ;
				}catch(ParseException e){
					ret=Errors.SYNTAX_ERROR ;
				}
				current_r_thread.add_ret_value(ret) ;
				//System.out.println(cmd + "*" + ret) ;
				nb_cmd++;
			}while(current_r_thread.is_permanent() | screen.available());
		}
		/* move the R_thread to the history Collection */
		/* We set status to FINISHED only if the current ststus is RUNNING */
		if (current_r_thread.get_status() == R_thread_status.RUNNING){
			current_r_thread.set_status(R_thread_status.FINISHED) ;
		}	
		moveToPre_history(current_r_thread) ;		
	}
	
	/**
	 * Adds r_thread to the collection r_thread_history and deletes it from r_threads table. 
	 * 
	 * @param r_thread
	 */
	private void moveToPre_history(R_thread r_thread){
		r_threads.delete(r_thread.get_rdr_name()) ;
		r_threads_pre_history.add(r_thread) ;
	}
	
	/**
	 * Move All r_threads from r_threads_pre_history to r_threads_history.
	 *
	 */
	public void moveToHistory(){
		int l=r_threads_pre_history.size();
		for(int i=0; i<l; i++ ){
			r_threads_history.add(r_threads_pre_history.get(i)) ;
			r_threads_pre_history.remove(i) ;
		}
	}
	
	public boolean pre_history_empty(){
		return r_threads_pre_history.isEmpty() ;
	}
	
	/********* Accessors *********************************/
	private int get_nextId(){
		lastId++;
		return lastId ;
	}
	
	/********* Accessors to r_threads *******************/
	
	public void kill_r_thread(String rdr_name){		
		R_thread r_thread= get_r_thread(rdr_name) ;
		if (r_thread != null){
			get_r_thread(rdr_name).set_status(R_thread.STOPPED) ;
			get_r_thread(rdr_name).set_permanent(false) ;
			//send an empty cmd to force the r_thread to close
			add_cmd("\n", rdr_name) ;
		}			
	}
	
	/**
	 * Return true if a thread is already running operations in the reader rdr_name
	 * 
	 * @param rdr_name
	 * @return
	 */
	public boolean have_thread(String rdr_name){
		return  r_threads.is_set(rdr_name) ;
	}
	
	/**
	 * 
	 * @param name
	 * @param r_thread
	 */
	public void add_thread(String name, R_thread r_thread){
		r_threads.add(name, r_thread) ;
	}
	
	public R_thread get_r_thread(String name){
		if (r_threads.is_set(name)){
			return (R_thread)r_threads.get(name) ;
		}
		return null;
	}
	
	public static void display_r_threads(Table r_threads, Screen screen){
		//screen.writeln("Running:");
		Iterator it = r_threads.get_var_values().iterator() ;
		display_r_threads(it, screen) ;
	}
	
	public static void display_r_threads(ArrayList r_threads, Screen screen){
		//screen.writeln("History:");
		Iterator it=r_threads.iterator() ;
		display_r_threads(it, screen) ;
	}
	
	private static void display_r_threads(Iterator it, Screen screen){
		R_thread r_thread;
		Iterator cmd ;
		Iterator ret;
		Integer r;
		screen.writeln("") ;		
		while(it.hasNext()){
			r_thread = (R_thread)it.next();
			screen.writeln("[#" + r_thread.get_id() + " " + R_thread_status.get_statusMsg(r_thread.get_status()) + "  --- " + r_thread.get_rdr_name() + "] ") ;
			cmd=r_thread.get_cmds().iterator() ;
			ret=r_thread.get_ret_values().iterator() ;
			
			while(cmd.hasNext()&& ret.hasNext()){
				r=(Integer)ret.next();
				screen.writeln(cmd.next() + " :" + Errors.get_errorMsg(r.intValue()) + "(" + r.intValue() + ")") ;				
			}
			screen.writeln("") ;
		}
		screen.writeln("") ;
	}	
	
}


