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

import java.sql.SQLException;

import com.linuxnet.jpcsc.Apdu;

/**
 * This class contains methods to get Data (userKeys, cardMangerAID,...) from the database
 *
 * @author Moez Ben MBarka.
 * @version $Revision$
 */
public class Init {
	
	/**
	 * 
	 */
	private Database con  ;
	
	/**
	 * 
	 */
	private String driver ="com.mysql.jdbc.Driver" ;
	
	private String server = "127.0.0.1" ;
	
	private String table = "grille" ;
	
	private String user = null ;
	
	private String pass= null ;
	
	private int defaultkeyIndex = 0;
	
	/**
	 * Try to use this key if can not access to the database
	 */
	private String defaultUserKey = "404142434445464748494a4b4c4d4e4f" ;
	
	/**
	 * @throws UserException 
	 * 
	 *
	 */
	public Init(){
		
	}
	
	public void connect() throws UserException{
		String url="jdbc:mysql://" + server + "/" + table ;
		con = new Database(driver, url);
		try {
			con.start();
		} catch (SQLException exception) {
			throw new UserException("Database error : "+ url);
		} catch (Exception exception) {
			throw new UserException("Database error : Check the driver " + driver +" was not found !");
		} 
	}
	
	/**
	 * 
	 * @param cardId
	 * @param keyIndexNumber
	 * @return
	 * @throws Exception
	 */
	public String getUserKeys(String cardId, int keyIndexNumber) throws Exception{
		return con.getField(cardId, "userKey") ;
	}

    /**
	 * 
	 * @param cardId
	 * @return
	 * @throws Exception
	 */
	public String getUserKeys(String cardId ) throws Exception{
		return con.getField(cardId, "userKey") ;
	}
	
	/**
	 * 
	 * @param cardId
	 * @return
	 * @throws Exception
	 */
	public byte[] getAIDmanager(String cardId) throws Exception{
		
		return Apdu.s2ba(con.getField(cardId, "cardManagerID")) ;
	}
	
	/**
	 * 
	 * @param cardId
	 * @return
	 * @throws Exception
	 */
	public byte[] getCardMaxSize(String cardId) throws Exception{
		
		return Apdu.s2ba(con.getField(cardId, "maxBlockSize")) ;
	}
	
	public void close() throws Exception{
		con.close();
	}
	
	/***** Accessors ******/
	/*********************/
	public String get_server(){
		return server ;
	}
	
	public String get_table(){
		return table;
	}
	
	public String get_user(){
		return user ;
	}
	
	public String get_pass(){
		return pass ;
	}
	
	public String getDefaultUserKeys(){
		return this.defaultUserKey ;
	}
	
	public int getDefaultKeyIndex(){
		return this.defaultkeyIndex ;
	}
	
	public void set_server(String server){
		this.server= server ;
	}
	
	public void set_table(String table){
		this.table= table ;
	}
	
	public void set_user(String user){
		this.user= user ;
	}
	
	public void set_pass(String pass){
		this.pass= pass ;
	}
}


