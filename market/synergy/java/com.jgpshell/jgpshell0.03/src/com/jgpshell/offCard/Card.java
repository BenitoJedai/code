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

import com.jgpshell.globalPlatform.* ;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;

import com.linuxnet.jpcsc.Apdu;


/**
 * This class implements methods to connect and interact with cards.
 *
 * @author Moez Ben MBarka
 * @version $revision$
 */
public class Card {
	
	
    private String cardName;
    private String key;
	  
    private ApplicationOffCard appOffCard ;
	
    private Log log ;
	
    
    public Card(){
    	appOffCard = new ApplicationOffCard();
        log = appOffCard.getLog() ;
    }
	
    /**
     * The most recommanded constructor.<br>
     * Try to connect to the reader with ApplicationOffCard and after gives the instance to Card.
     * It is better than connecting to the card using Card.connect() methods, 
     * however such connection should also work
     * 
     * @param appOffCard
     */
	public Card(ApplicationOffCard appOffCard){
		this.appOffCard = appOffCard ;
		log = appOffCard.getLog() ;
	}
    
    
    /**
     * 
     * @param cardName
     * @param key
     * @throws UserException
     */
    public Card(String cardName, String key) throws UserException {
    	appOffCard = new ApplicationOffCard();
        log = appOffCard.getLog() ;
        this.connect(cardName, key) ;
    }
	
    /**
     * 
     * @param appOffCard
     */
    public void set_appOffCard(ApplicationOffCard appOffCard){
    	this.appOffCard=appOffCard;
    }
    
    /** Connects to the card 
     * 
     * @param cardName
     * @throws UserException
     */
    public void connect(String cardName) throws UserException{
        this.cardName = cardName;
        if (!appOffCard.is_connected()){
            appOffCard.connect(cardName);
        }
    }
    
    /**
     * 
     * @param cardName
     * @param key
     * @throws UserException
     */
    public void connect(String cardName, String key) throws UserException{
        this.cardName = cardName;
        this.key = key;
        if (!appOffCard.is_connected()){
            appOffCard.connect(cardName, key);
        }
    }

    /**
     * 
     * @param cardName
     * @param keyVersion
     * @param keyIndexNumber
     * @throws UserException
     */
    public void connect(String cardName,int keyVersion, int keyIndexNumber) throws UserException{
        this.cardName = cardName;
       
            
        try{
            this.key= appOffCard.getInit().getUserKeys(cardName) ;
        }catch(Exception e){
            throw new UserException("Cannot get userKeys from the database.") ;
        }
        if (!appOffCard.is_connected()){
            appOffCard.connect(cardName, key);
        }
    }
    

    /**
     * Accessor return the private instance of ApplicationOffCard
     *
     */
    public ApplicationOffCard getApplicationOffCard(){
        return appOffCard;
    }



	
    /** Gives a list of applications in the card 
     * 
     * @param card
     * @param key
     * @return
     * @throws UserException
     */
    public Collection list_Applications(String card, String key)throws UserException{
        connect(cardName, key) ;
        return list_Applications() ;
    }
	
    /**
     * Lists applications in card 
     *
     *
     * @return Collection
     * @throws UserException
     * 
     */
    public Collection list_Applications() throws UserException{
        Collection listApplications = new HashSet();
	
        if (!appOffCard.is_connected())
            appOffCard.connect(cardName, key) ;
		
        /********** Getting Load files only *************/
        log.write("List LOad file only...") ;
        list_applications_command(listApplications, (byte)0x20) ;
		
        /******** Getting Applications and security domains Only (P1 = '0x40') **************/
        log.write("List Applications only...") ;
        list_applications_command(listApplications, (byte)0x40) ;
		
		
        return listApplications ;
    }
	
    /**
     * 
     * @param listApplications
     * @param p1
     * @throws UserException
     */
    private void list_applications_command(Collection listApplications, byte p1) throws UserException{

        byte[] data ={0x4F, 0x00};
		
        byte[] list = ApplicationOnCard.getStatus(p1/*P1*/ ,(byte)0x00/*P2*/ ,data);
        byte[] response = appOffCard.trySend(list);
        int res=GP_responses.get_GP_response(response) ;
        if (res != GP_responses.SUCCESS && res!= GP_responses.MORE_DATA_AVAILABLE && res!= GP_responses.REFERENCED_DATA_NOT_FOUND){
            throw new UserException("Getstatus:::" + GP_responses.getErrMsg(res), res) ;
        }
        parses_response(listApplications, response) ;
		
        while (res == GP_responses.MORE_DATA_AVAILABLE){
            /* Getting next occurences*/
            list = ApplicationOnCard.getStatus((byte)0x40/*P1*/ ,(byte)0x01/*P2*/ ,data);
            response = appOffCard.trySend(list);
            res=GP_responses.get_GP_response(response) ;
            if (res != GP_responses.SUCCESS && res!= GP_responses.MORE_DATA_AVAILABLE && res!= GP_responses.REFERENCED_DATA_NOT_FOUND){
		 throw new UserException("Getstatus:::" + GP_responses.getErrMsg(res), res) ;
            }
            parses_response(listApplications, response) ;
        }
    }
	
    /**
     * parse the response to get Applications/Load Files AID
     * response is the reponse of the card to the commande GET STATUS
     * witch the bit b2 of P2 is set to 1 (table 9-22 of Global Platform specifications)
     *
     * @param listApplications
     * @param response
     * @throws UserException
     */
    private void parses_response(Collection listApplications, byte[] response) throws UserException{
        int t, i =0;
        byte[] application ; 
        
        while(i < response.length -2){
            t= (int)response[i] ;
            application = new byte[t];
            CardApplication ca = new CardApplication() ;
            System.arraycopy(response, i+1, application, 0, t);
            ca.set_aid(application) ;
            ca.set_life_cycle(response[i+1+t]) ;
            listApplications.add(ca);
            i = i + t +3;
        }
    }
	
	
	
    /** Formats a card 
     * 
     * @param cardName
     * @param key
     * @throws UserException
     */
    public void format(String cardName, String key) throws UserException{
        connect(cardName, key) ;
        format() ;
    }
	
    /**
     * 
     * @throws UserException
     */

    public void format()throws UserException{
        Collection listApplications = this.list_Applications();
        Iterator it = listApplications.iterator();
        int i=0 ;
        byte[] cmd;
        byte[] reponse ;
        CardApplication ca;
        byte[] aid;
        int res ;
        while (it.hasNext()&& i<1){
            ca = (CardApplication)it.next() ;
            aid=ca.get_aid();
            log.write("Deleting " + Apdu.ba2s(aid) + " ...");
            cmd=ApplicationOnCard.deleteApplication(aid,(byte)0x00);
			
            reponse = appOffCard.trySend(cmd);
            res=GP_responses.get_GP_response(reponse) ;
            //	System.out.println("RespApdu=" + Apdu.ba2s(reponse)) ;	
            if (res != GP_responses.SUCCESS){
                log.write("Deleting " + Apdu.ba2s(aid) + " failed !") ;
            }
            else{
                log.write(Apdu.ba2s(aid) + " deleted.") ;
            }	
        }
		
    }	
}


