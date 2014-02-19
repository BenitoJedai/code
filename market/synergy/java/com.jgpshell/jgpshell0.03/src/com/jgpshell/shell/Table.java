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

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Set;

/**
 * Re-Implements HashMap.
 * 
 * @author Moez Ben MBarka
 *
 */
public class Table {
	

	private HashMap table ;
	
	private int size;
	
	
	public Table(int size){
		this.size = size;
		init() ;
	}
	
	public Table(){
		//default size
		this(10) ;
	}
	
	private void init(){
		table = new HashMap(this.size) ;
	}
	
	/**
	 * Add new object
	 */
	public void add(Object name, Object value){
		table.put(name, value);
	}
	
	/**
	 * get the variable name
	 * 
	 * @param name
	 * @return Returns null if no object name was added
	 */
	public Object get(Object name){
		if (!is_set(name)){
			return null ;
		}
		return table.get(name);
	}
	
	/**
	 * updates the value of the object name or add it if it does not exist
	 * 
	 * @param name
	 * @param value
	 */
	public void set(Object name, Object value){
		if (is_set(name)){
			delete(name);			
		}
		add(name, value) ;
		//screen.command("SET \"" + name + "\"  \"" + value + "\"");
	}
	
	/**
	 * Delete the object name
	 * 
	 * @param name
	 */
	public void delete(Object name){
		table.remove(name);
	}
	
	/**
	 * 
	 * @param name
	 * @return true is the object name is into the table
	 */
	public boolean is_set(Object name){
		return table.containsKey(name);
	}
	
	/**
	 * return true if table["name"] = "wanted"
	 * 
	 * @param name
	 * @param wanted
	 * @return
	 */
	public boolean is_equal(Object name, Object wanted){
		if (!is_set(name)){
			return false ;
		}
		return get(name).equals(wanted) ;
	}
	
	/**
	 * 
	 * @return
	 */
	public Set get_var_names(){
		return table.keySet();
	}
	
	/**
	 * 
	 * @return
	 */
	public Collection get_var_values(){
		return table.values();
	}
	
	/**
	 * Copies the content of Table to a new one and returns it
	 * The content is not cloned but just a reference is copied
	 * 
	 * @return
	 */
	public Table copy(){
		Table n_table= new Table() ;
		Iterator it=get_var_names().iterator() ;
		String name;
		while(it.hasNext()){
			name=(String)it.next() ;
			n_table.add(name, get(name));
		}
		
		return n_table;
		
	}
}


