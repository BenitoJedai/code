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
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.Statement;
import java.sql.ResultSet;
import java.sql.DriverManager;

/**
 * The Database class allows to send queries to
 * the MySQL Database.
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
class Database {
	private String driver;
	private String url;
	private String user = null ;
	private String pass = null ;
	private Connection connection = null;
	//private DatabaseMetaData metaData = null;
	private Statement statement = null;
	private ResultSet resultSet = null;
	
	/**
	 * Class constructor.
	 *
	 * @param driver  the driver to be connected to the database 
	 *                using java and JDBC.
	 * @param url     the url location of the database.
	 */
	Database (String driver, String url, String user, String pass) {
		this.driver = driver;
		this.url = url;
		this.user=user;
		this.pass=pass;
	}
	
	Database (String driver, String url) {
		this.driver = driver;
		this.url = url;
	}
	
	/** 
	 * Starts connection to the database. Queries can then be executed.
	 * @exception SQLException if there has been an error in the MySQL querie.
	 * @exception Exception    if the connection to the database can't be established.
	 *
	 */
	void start() throws SQLException, Exception {
		if (connection != null) {
			stop();
		}
		Class.forName(driver).newInstance();
		if (user!=null){
			connection = DriverManager.getConnection(url, user, pass);
		}
		else{
			connection = DriverManager.getConnection(url) ;
		}
		
		DatabaseMetaData dbmt = connection.getMetaData();
		System.out.println("Connection to the database succeded "+ dbmt.getDatabaseProductName()+ " \n");
		
		statement = connection.createStatement();
		
	}
	
	/** 
	 * Closes the connection to the database.
	 * @exception SQLException if there's an error trying to close the connection
	 *                         to the database.
	 */
	void close() throws SQLException {
		if (resultSet != null) {
			resultSet.close();
			resultSet = null;
		}
	}
	
	/**
	 * Executes a query on the database. Database must have been started.
	 * Closing the returned ResultSet is optional.
	 * @param query the MySQL query that will be executed on the database.
	 * @return the result of the MySQL query.
	 * @exception SQLException if there's been an executing the query on the database.
	 */
	ResultSet query(String query) throws SQLException {
		close();
		resultSet = statement.executeQuery(query);
		return resultSet;
	}
	
	/** 
	 * Stops connection to <code>Database</code>. 
	 * Queries cannot be sent any more unless <code>Database</code>
	 * is started again.
	 * @exception SQLException if there's been an error closing connections.
	 */
	void stop() throws SQLException {
		if (connection != null) {
			close();
			
			if (statement != null) {
				statement.close();
			}
			
			if (connection != null) {
				connection.close();
			}
			connection = null;
		}
	}
	
	public String getField(String cardID, String field) throws UserException {
		//field est "userKeyVersion", "userKeyID" ou "cardManagerID"
		String mysqlQuery = "select * from Cards where Name = '" + cardID  +"';" ;
		String message="" ;
		try{
			ResultSet resultSet = query(mysqlQuery);
			resultSet.next();
			message=resultSet.getString(field);
		}catch(Exception e){
			throw new UserException("Can not access to the database.") ;
                }
		return message ;
	}
}



