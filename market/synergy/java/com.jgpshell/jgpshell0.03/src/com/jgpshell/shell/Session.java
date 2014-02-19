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

import com.jgpshell.cardGridCom.CardGridUser;
import com.jgpshell.offCard.ApplicationOffCard;
import com.jgpshell.offCard.Card;
import com.jgpshell.offCard.Init;
import com.jgpshell.offCard.UserException;


import java.util.Iterator;
import java.util.Set;

/**
 * 
 * 
 * @author Moez
 *
 */
public class Session extends S_thread implements Cloneable{
	
	private Screen screen;
	
	private CardGridUser cgd ;
	
	
	/**
	 * the instance used to connect to the reader in the session
	 */
	private Card card  ;
	
	private ApplicationOffCard appOffCard ;
	
	
	/**
	 * This array will contains all session variables
	 */ 
	protected Table session_vars ;
	
	/**
	 *  This array will contains all private session variables
	 */
	//protected Table private_vars ;
	
	/**
	 * Must set to true if new reader is selected.
	 * Is set by many functions to re-init the session after selecting new rdr.
	 */
	public boolean new_rdr_selected = false ;
	
	/**
	 * If it is set to true, next cmds will be sent to the thread launched for 
	 * the reader r_thread_selected_rdr. If new thread is created, 
	 * it will be created with the tag r_thread_selected_tag
	 */
	public boolean r_thread_selected = false ;
	
	/**
	 * Has sense only if r_thread_selected=true
	 */
	public String  r_thread_selected_rdr;
	
	/**
	 * Has sense only if r_thread_selected=true
	 */
	public String r_thread_selected_tag ;
	
	
	private Init init = new Init() ;
	
	/**
	 * 
	 *
	 */
	public Session(){
		
		//set_session(0);
		/* intialize the first session*/
	}
	
	/**
	 * Re-init or create the session index
	 * 
	 * @param index index of the session to be initialized
	 * @throws UserException 
	 */
	public void init(){	
		
		screen.info("Initializing session ...");
		
		if (session_vars == null){
			session_vars = new Table() ;
			r_threads = new Table() ;
		}
		
		/* try the SQL connection */
		if (!is_set("Sql_server")){
			set_variable_shell("Sql_server", init.get_server());
		}	
		else
			init.set_server((String)session_vars.get("sql_server")) ;
		
		if (!is_set("Sql_table")){
			set_variable_shell("Sql_table", init.get_table());
		}	
		else
			init.set_table((String)session_vars.get("sql_table")) ;
		
		if (!is_set("Sql_user"))
			set_variable_shell("Sql_user", init.get_user());
		else
			init.set_user((String)session_vars.get("sql_user")) ;
		
		if (!is_set("Sql_pass"))
			set_variable_shell("Sql_pass", init.get_pass());
		else
			init.set_pass((String)session_vars.get("sql_pass")) ;
		
		try{
			init.connect() ;
			set_variable_private("SQL_CONNECTED", "1") ;
			init.close() ;
		}catch(Exception e){
			screen.error(Errors.UNABLE_CONNECT_SQL, "", e);
			set_variable_private("SQL_CONNECTED", "0") ;
		}
		
		cgd = new CardGridUser() ;
		update_readerList();
		
		appOffCard=new ApplicationOffCard(cgd) ;
		appOffCard.setInit(init) ;
		appOffCard.setLog(1);
		set_variable_shell("Log", "1") ;
		
		card= new Card(appOffCard);
		
		init_private_variables() ;
		
		screen.info("Session ready.");
	}
	
	/**
	 * Create an init private variables with default values
	 *
	 */
	public void init_private_variables(){
		/* No r_thread selected */
		set_variable_private("R_THREAD_SELECTED", "0") ;
	}
	
	
	/**
	 * This method is called if something happens although it was believed that must never happen ! 
	 * Makes every thing free and exit !
	 * 
	 * @param e
	 */
	public void fatal(Exception e){
		free();
		screen.error(Errors.FATAL_ERROR) ;
		e.printStackTrace() ;
		
		System.exit(1);
	}
	
	/**
	 * Create a clone of the currrent session.
	 * 
	 */
	public Object clone() throws CloneNotSupportedException{
		Session n_session=(Session) super.clone() ;
		
		n_session.cgd=new CardGridUser() ;
		n_session.appOffCard=new ApplicationOffCard(cgd) ;
		n_session.appOffCard.setInit(init) ;
		n_session.appOffCard.setLog(1);
		
		n_session.card= new Card(appOffCard); 
		
		
		//clone session vars...no need !!
		n_session.session_vars = session_vars.copy() ;
		
		return n_session;
	}
	
	/**
	 * Clone the session_vars Table. 
	 * It does not make reference copies but The variables are cloned.
	 * 
	 * @return
	 */
	public Table clone_session_vars(){
		Table n_table= new Table() ;
		Iterator it=session_vars.get_var_names().iterator() ;
		String name;
		while(it.hasNext()){
			name=(String)it.next() ;			
				try {
					n_table.add(name, ((Variable)session_vars.get(name)).clone());
				} catch (CloneNotSupportedException e) {
					fatal(e) ;
				}
		}
		
		return n_table;
	}
	
	/**
	 * releases the session index
	 * 
	 * @param index
	 */
	public void free(){
		if (is_equal("connected", "1")){
			try{
				appOffCard.close() ;
			}catch(UserException e){
				screen.error(Errors.FINALIZING_CONTEXT_FAILURE);
			}
		}	
		appOffCard.getLog().close() ;
	}
	
	/**
	 * frees the current session before reinitilising it
	 *
	 */
	public void reinit(){
		free() ;
		init() ;
	}
	
	/******* Updating the session **********/
	
	
	/**
	 * 
	 */
	public void update_readerList(){
		try{
			cgd.makeListReaders() ;
		}catch(UserException e){
			screen.error(Errors.ANY_RDR, "", e) ;
			//throw  new UserException(e.getMessage(), Errors.ANY_RDR);
		}
		set_variable_private("NB_READERS", String.valueOf(cgd.get_nbReader())) ;
	}
	
	
	/**
	 * Creates (if empty) the session entry 'keys' and 'keys_index' used to authentificate with the card
	 * 
	 * @param rdrName
	 */
	public void check_keys(String rdrName){
		if (!is_set("keys") | new_rdr_selected){			
			if (is_set("SQL_CONNECTED") && get_variable("SQL_CONNECTED").equals("1")){
				//if the SQL connection is ok, try to get the information from the table
				try{
					set_variable_shell("Keys", init.getUserKeys(rdrName));
				}catch(Exception e){
					screen.error(Errors.UNABLE_GET_KEYS, "rdrName", e) ;
					screen.info("Default values will be used !") ;
					set_variable_shell("Keys", init.getDefaultUserKeys()) ;
				}
			}
			else{
				screen.info("Default values will be used !") ;
				set_variable_shell("Keys", init.getDefaultUserKeys()) ;
			}
			
		}
		
		if (!is_set("Keys_index") | new_rdr_selected){
			set_variable_shell("Keys_index", "0") ;
		}
	}
	
	/******* Accessors ***********/
	
	public void set_screen(Screen screen){
		this.screen = screen;
	}
	
	public CardGridUser get_cgd(){
		return cgd;
	}
	
	public ApplicationOffCard get_appOffCard(){
		return appOffCard ;
	}
	
	public Card get_card(){
		card.set_appOffCard(get_appOffCard());
		return card ;
	}
	
	public String get_shell(){
		return "[#]" ;
	}
	
	/*****Function to facilitate the access to the session variables ******/
	
	/**
	 * 
	 */
	public String get_variable(String name){
		Object v = session_vars.get(name) ;
		if (v == null){
			return "undefined" ;
		}
		
		return ((Variable)v).value ;
	}
	
	
	public char[] get_variable_permissions(String name){
		Variable v = (Variable)session_vars.get(name) ;
		if (v == null){
			return null ;
		}
		return v.permissions ;
	}
	
	/**
	 * 
	 * @param name
	 * @return
	 */
	public int get_int_variable(String name){
		int res;
		
		try{
			res=Integer.parseInt(get_variable(name));
		}catch(NumberFormatException e){
			res=0 ;
		}
		
		return res;
	}
	
	
	/**
	 * Creates a new session variable with default flags and permissions(111) 
	 * 
	 * @param name
	 * @param value
	 * @return
	 */
	public int set_variable(String name, String value){
		return set_variable(name, value, "111") ;
	}
	
	
	/**
	 * Set a new variable with permissions=1 : read only
	 * 
	 * @param name
	 * @param value
	 * @return
	 */
	public int set_variable_private(String name, String value){
		return set_variable(name, value, "100") ;
		//private_vars.set(name, value);
	}
	
	
	/**
	 * Set a new variable with permissions=11 : can not delete
	 * 
	 * @param name
	 * @param value
	 * @return
	 */
	public int set_variable_shell(String name, String value){
		return set_variable(name, value, "110") ;
		//private_vars.set(name, value);
	}
	
	
	/**
	 * 
	 * @param name
	 * @param value
	 * @param permissions
	 * @return
	 */
	public int set_variable(String name, String value, String permissions){
		if (session_vars.is_set(name)){
			// if the variable already exists check permissions before modifying it
			Variable v= (Variable)session_vars.get(name) ;
			if (v.can_write()){
				v.value=value ;
			}
			else{
				return Errors.VARIABLE_ACCESS_DENIED ;
			}
		}
		else{
			session_vars.set(name, new Variable(name, value, permissions));
		}	
		
		return Errors.SUCCESS ;
	}
	
	public boolean is_set(String name){
		return session_vars.is_set(name) ;
	}
	
	public boolean is_equal(String name, String wanted){
		return (get_variable(name) == wanted) ;
	}
	
	public void unset(String name){
		session_vars.delete(name) ;
	}
	
	/**
	 * Display all session vars
	 *
	 */
	public void list_vars(){
		Set var_list ;
		var_list=session_vars.get_var_names() ;
		Iterator it=var_list.iterator() ;
		while(it.hasNext()){
			String name=(String)it.next() ;
			screen.writeln(name + "=" + get_variable(name) + " (" + String.valueOf(get_variable_permissions(name)) + ")") ;
		}	
	}
	
	/**
	 * 
	 * @return
	 */
	public int display_running_threads(){		
		S_thread.display_r_threads(r_threads, screen) ;
		return Errors.SUCCESS;
	}
	
	
	public int display_history_threads(){		
		S_thread.display_r_threads(r_threads_history, screen) ;		
		return Errors.SUCCESS;
	}
	
	public int display_pre_history_threads(){
		S_thread.display_r_threads(r_threads_pre_history, screen);
		return Errors.SUCCESS;
	}
}


