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

package com.jgpshell.cardGridCom;

import com.jgpshell.offCard.UserException;

import com.linuxnet.jpcsc.*;


/**
 * This class is intended to be a wrapper between the JPCSC class ang JCGShell class.
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class CardGridUser implements Cloneable{
	
	
	private Context ctx =new Context();;
	
	
	private Card card =null;
	
	
	private String[] sa = null;
	
	
	private State[] states;
	
	private int timeout_min=1;
	
	/**
	 * Must be set to true after etablishing context
	 */
	private boolean context_initiated = false ;
	
	/**
	 * 
	 */
	public Object clone() throws CloneNotSupportedException{
		CardGridUser n_cgd = (CardGridUser)super.clone() ;
		
		//n_cgd.card=(Card)card.clone() ;
		//n_cgd.ctx=(Context)ctx.clone() ;		
		
		return n_cgd;
	}
	
	public CardGridUser() {
		
		initContext() ;
		
	}	
	
	public CardGridUser(String cardName){
		initContext() ;
		card = this.connect(cardName) ;
	}
	
	/**
	 * 
	 *
	 */
	private void initContext(){
		ctx.EstablishContext(PCSC.SCOPE_SYSTEM, null, null);
		context_initiated= true ;		
	}
	
	
	/** Connects the card 
	 *
	 * @param String cardName
	 */		
	public Card connect(String cardName){
		//System.out.println("Connecting...");
		card=ctx.Connect(cardName, PCSC.SHARE_SHARED, PCSC.PROTOCOL_T0|PCSC.PROTOCOL_T1);
		return card;
	}
	
	
	/**
	 * 
	 */
	public void close(){
		card.Disconnect();
		ctx.ReleaseContext() ;
		context_initiated=false ;		
	}
	
	
	public void release(){
		ctx.ReleaseContext();
	}
	
	
	/**
	 * 
	 * @param ba
	 * @return
	 * @throws Exception
	 */
	public byte[] sendAPDU(byte[] ba) throws Exception { 
		byte[] rpsAPDU = card.Transmit(ba, 0, ba.length);	    
		return rpsAPDU;	    	    	    
	}
	
	/**
	 * 
	 * @return
	 * @throws UserException
	 */
	public String[] makeListReaders() throws UserException {
		if (!context_initiated){
			initContext() ;
		}
		try{
			sa = ctx.ListReaders();
		}catch(Exception e){
			sa = null;
			throw new UserException(e.getMessage());
		} 
		this.init_states();
		return sa;		
	}
	
	/**
	 * Returns the reader name number <b>index-1</b>
	 * @param index
	 * @return null if no reader has the index:index-1 (index=0 or index>nbReader)
	 */
	public String get_reader(int index){
		index=index-1 ;
		if (index>=0 && index<get_nbReader()){
			return this.sa[index];
		}
		return null ;
	}
	
	
	public int get_nbReader(){
		if (sa != null){
			return sa.length;
		}
		else{
			return 0;
		}
	}
	
	
	/**
	 * init PCSC state with the card cardName
	 * 
	 * @param cardName
	 */
	public void init_states(){
		if (sa != null){
			states = new State[sa.length];
			for (int i=0; i<sa.length; i++){
				states[i] = new State(sa[i]);
			}	
		}	
	}
	
	public String cardStateToString(int index){
		return states[index].toString();
	}
	
	public void contextGetStatusChange(){
		ctx.GetStatusChange(timeout_min, states);
	}
	
	private State checkForCard(int index){
		states[index]= ctx.WaitForCard(sa[index], timeout_min);
		return states[index] ;
	}
	
	/**
	 * Check if there is an inserted card in the reader index
	 * 
	 * @param index
	 * @return
	 */
	public boolean contains_card(int index){
		if (checkForCard(index) != null){
			return true;
		}
		return false ;
	}
	
	public byte[] getATR(int index){
		return states[index].rgbAtr ;
	}
	
	
}


