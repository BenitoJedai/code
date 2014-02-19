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

import com.jgpshell.cardGridCom.CardGridUser; 
import com.jgpshell.globalPlatform.* ;

import java.util.Vector;

import com.linuxnet.jpcsc.Apdu;
import com.linuxnet.jpcsc.PCSCException;


/**
 * This class imlements hight level methods to access most of global platform functionalities 
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class ApplicationOffCard implements Cloneable{
	
	/**
	 * This instance is used to send commands to the card
	 */
	private CardGridUser card ;
	
	
	/**
	 *max size to send in one time to the card
	 */
	private int max_size = 64 ;
	
	/**
	 * Card manager AID
	 */
	private byte[] cardManagerAID ;
	
	/**	
	 * User's Keys
	 */
	private String userKeys = "";
	
	/**
	 * Card Name
	 */
	private String cardName ="" ;
	
	/**
	 * Used to access to the requested informations from the database
	 */
	private Init init = new Init() ; ;
	
	/**
	 * 
	 */
	private SecureChannel sc ;
	
	private byte[] init_update_resp ; 
	
	/**
	 * Must be set to True after the connection with the card
	 */
	private boolean connected =false ;
	
	/**
	 * Must be set to True after the mutual authentication with the card
	 */
	private boolean authenticated = false ;
	
	/**
	 * Must be set to true after the selection of the card manager
	 */
	private boolean openSelected = false ;
	
	/**
	 * Must be set to true if the CardGriduser contxt has been inialized
	 */
	private boolean cgd_initiated = false ;
	
	/**
	 * Must be set to true after init-update
	 */
	private boolean init_update_done = false ;
	
	/**
	 * 
	 */
	private Log log = new Log(1) ;
	
	/**
	 * Indicates current securityLevel :
	 * 3 : C-Decryption and C-MAC
	 * 1 : C-MAC only
	 * 0 : NO secure messaging (default)
	 */
	private int securityLevel = 0 ;
	
	
	
	/**
	 * 
	 *
	 */
	public ApplicationOffCard( ){
		log.write("New session...") ;
		init_sql() ;
	}
	
	/**
	 * 
	 * @param cgd
	 */
	public ApplicationOffCard(CardGridUser cgd){
		this.cgd_initiated = true;
		this.card = cgd;
		log.write("New session...") ;
		//init_sql() ;
	}
	
	/**
	 * 
	 */
	public Object clone() throws CloneNotSupportedException{
		ApplicationOffCard n_offCard = (ApplicationOffCard)super.clone() ;
		n_offCard.set_sc((SecureChannel)sc.clone());
		n_offCard.set_cardManagerAID(cardManagerAID) ;
		n_offCard.set_userKeys(userKeys) ;
		
		return n_offCard;
	}
		
	
	/**
	 * Init the connection to the MySQL server
	 *
	 */
	private void init_sql(){
		try{
			init.connect() ;
		}catch(UserException e){
			log.write(e.getMessage());
		}
	}
	
	/**
	 * update the instance init(can be used to modify the connection parameters) 
	 * and try to connect to the server
	 * 
	 * @param init
	 */
	public void init_sql(Init init){
		setInit(init);
		init_sql() ;
	}
	
	/**
	 * 
	 * @param cardName
	 */
	public void connect_card(String cardName) throws UserException{
		this.cardName=cardName;
		if (this.connected == false){
			log.write("Connection to the card : " + cardName +"....") ;
			if (this.cgd_initiated == false){
				/* If the CardGridUser context is not iniated yet */
				try{
					card = new CardGridUser(cardName) ;
				}catch(PCSCException e){
					throw new UserException("CardGridUser : Connection Failure ! " + e.getMessage());
				}	
			}	
			else{
				try{
					card.connect(cardName);
				}catch(PCSCException e){
					throw new UserException("CardGridUser : Connection Failure ! " + e.getMessage());
				}						
			}
			connected = true ;
			log.write("Connection success !") ;
		}
		else{
			log.write("I think I'm already connected to "+ cardName + " !");
		}
	}
	
	
	/**
	 * Connection to the card, selection of the security domain and authentification
	 * 
	 * @param cardName
	 */
	public void connect (String cardName) throws UserException{
		/* Connection to the card */
		this.cardName = cardName ;
		init_cardManagement(cardName) ;	
		/*try{
		 userKeys= init.getUserKeys(cardName) ;
		 }catch(Exception e){
		 throw new UserException("Cannt get userKeys from the database.") ;
		 }*/
		mutual_authentication(userKeys) ;
	}
	
	/**
	 * Connection to the card, selection of the security domain and authentification
	 * 
	 * @param cardName
	 * @param userkeys
	 * @throws UserException
	 */
	public void connect(String cardName, String userkeys) throws UserException{
		this.cardName = cardName ;
		this.userKeys = userKeys ;
		/* Connection to the card */
		init_cardManagement(cardName) ;	
		mutual_authentication(userKeys) ;
	}
	
	/**
	 * Connection to the card, selection of the security domain and authentification
	 * 
	 * @param cardName
	 * @param userkeys
	 * @param keyIndex
	 * @throws UserException
	 */
	public void connect(String cardName, String userKeys, int keyIndex) throws UserException{
		this.cardName = cardName ;
		this.userKeys = userKeys ;
		/* Connection to the card */
		init_cardManagement(cardName) ;	
		mutual_authentication(userKeys, keyIndex) ;
	}
	
	/**
	 * Connection to the card, selection of the security domain and authentification
	 * 
	 * @param cardName
	 * @param keyVersion
	 * @param keyIndexNumber
	 * @throws UserException
	 */
	public void connect(String cardName, int keyVersion, int keyIndexNumber) throws UserException{
		this.cardName = cardName ;
		/* Connection to the card */
		
		init_cardManagement(cardName) ;	
		
		/*try{
		 userKeys= init.getUserKeys(cardName) ;
		 }catch(Exception e){
		 throw new UserException("Cannt get userKeys from the database.") ;
		 }*/
		log.write("Mutual authentication....") ;
		mutual_authentication(userKeys, keyVersion, keyIndexNumber) ;
		
	}
	
	
	/**
	 * Cleanup operation. Destroy connection context .
	 *
	 * @throws UserException
	 */
	public void close() throws UserException{
		try{
			card.close() ;
		}catch(PCSCException e){
			throw new UserException("CardGridUser : finalizing contex failure! " + e.getMessage()) ;
		}
		log.write("Connection to " + cardName + " closed !");
		connected = false ;
		authenticated = false ;
		openSelected = false ;
	
		cgd_initiated = false ;
		init_update_done = false ;
	}
	
	/**
	 * Inits the connection to the card and select the security issuer domain
	 *
	 * @param String cardName the name of the card
	 * @throws UserException
	 *
	 */
	protected void init_cardManagement(String cardName) throws UserException{
		if (this.connected == false){
			connect_card(cardName);
		}
		/* Selection of the card management */
		
		selectCardManagement(cardName) ;  
		
		if (this.userKeys.equals("")){
			try{
				userKeys= init.getUserKeys(cardName) ;
			}catch(Exception e){
				//throw new UserException("Cannt get userKeys from the database.") ;
				log.write("Cannt get userKeys from the database.") ;
				log.write("i will try to use the default userkey !") ;
				userKeys= init.getDefaultUserKeys() ;
				
			}
		}
		
	}
	
	/** Establishes mutual authentification of the user
	 * 
	 * @param userKeys
	 * @param keyVersion
	 * @param keyIndexNumber
	 * @throws UserException
	 */
	protected void mutual_authentication(String userKeys, int keyVersion, int keyIndexNumber) throws UserException{
		sc = new SecureChannel(userKeys, keyVersion, keyIndexNumber) ;
		initialize_update() ;
	}
	
	/**
	 * Etablish the mutual authentication
	 *
	 * @param String userKey
	 *
	 * @throws UserException
	 *
	 */
	public void mutual_authentication(String userKeys) throws UserException{
		sc = new SecureChannel(userKeys) ;
		initialize_update() ;
		complete_authentication() ;
	}
	
	/** 
	 * Etablish the mutual authentication
	 * 
	 * @param userKeys
	 * @param keyIndex
	 * @throws UserException
	 */
	public void mutual_authentication(String userKeys, int keyIndex) throws UserException{
		sc = new SecureChannel(userKeys, keyIndex) ;
		initialize_update() ;
		complete_authentication() ;	
	}
	
	public void initialize_update(SecureChannel sc) throws UserException{
		this.sc=sc ;
		initialize_update() ;
	}
	
	/**
	 * Begins the authentifications with the card
	 *
	 * @throws UserException
	 */
	private void initialize_update() throws UserException{
		if (openSelected == false){
			this.init_cardManagement(cardName) ;
		}
		log.write("Initialize update....") ;
		int cardResp ;
		byte[] apdu1 = sc.initializeUpdate() ;
		int sc0 = securityLevel ;
		securityLevel =0 ;
		init_update_resp = trySend(apdu1) ;
		securityLevel = sc0 ;
		cardResp = GP_responses.get_GP_response(init_update_resp) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("init_update:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
		this.init_update_done = true ;
		log.write("Initialize-update success !") ;
	}
	
	
	/** Completes the authentification
	 * 
	 * @param initResp
	 * @throws UserException
	 */
	public void complete_authentication() throws UserException{
		if (this.init_update_done == false){
			// someone wants to do ext-auth before init-update !
			throw new UserException("Can not send external-authenticate command before init-update !") ;
		}
		int cardResp ;
		byte apdu2[] ;
		log.write("External authenticate....") ;
		try{
			apdu2 = sc.externalAuthenticate(init_update_resp, (byte)securityLevel) ;
		}
		catch(Exception e){
			throw new UserException("ExternalAuthenticate:::" + e.getMessage()) ;
		}
		
		byte[] apduResp2 ;
		apduResp2 = trySendAuthentication(apdu2) ;
		cardResp = GP_responses.get_GP_response(apduResp2) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("externalAuthenticate:::"  + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
		authenticated = true ;	
		log.write("Authentication completed !") ;
	}
	
	/** Selects a card 
	 * 
	 * @param cardName
	 * @throws UserException
	 */
	private void selectCardManagement(String cardName) throws UserException{
		log.write("Selecting the card manager....") ;
		try{
			cardManagerAID = init.getAIDmanager(cardName) ;
		}catch(Exception e){
			//throw new UserException("Cannt get cardManagerAID from database !") ;
			log.write("Cannt get cardManagerAID from database !") ;
			log.write("I try to ask the card...") ;
			
			byte[] apResp = trySend(ApplicationOnCard.selectSecurityDomain());
			int resp=GP_responses.get_GP_response(apResp) ;
			if (resp != GP_responses.SUCCESS){
				throw new UserException("Can not get the card manager AID !:::"  + GP_responses.getErrMsg(resp), resp) ;
			}
			cardManagerAID=getSelectedApplicationAID(apResp) ;
		}
		
		byte[] apdu = ApplicationOnCard.selectApplication(cardManagerAID, (byte)0) ;
		
		/* Security level is not applicable to the SELECT command 
		 * so it must be, temporarily, set to 0 */
		int s0= securityLevel; /* We save current securityLevel */
		securityLevel=0; 
		byte[] apduResp = trySend(apdu) ;
		securityLevel =s0 ; /*Last Security level*/
		
		int cardResp = GP_responses.get_GP_response(apduResp) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("Select card manager:::"  + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
		log.write("Card manager selected !") ;
		openSelected= true ;
	}
	
	public void upload(String packagePath) throws UserException {
		
		if (!connected  | !authenticated){
			throw new UserException("The authentication was not initiated !") ;
		}
		
		if (openSelected == false){
			selectCardManagement(cardName) ;
		}
		
		int cardResp; 
		
		/* Install For Load command */
		CapFiles file = null;
		try{
			file = new CapFiles(packagePath, max_size) ;
		}catch(Exception e){
			throw new UserException(e.getMessage()) ;
		}
		log.write("File " + packagePath + " read.");
		
		byte[] loadFileAID = file.getAIDHeader() ;
		log.write("PackageAID:" + Apdu.ba2s(loadFileAID));
		byte[] securityDomainAID = cardManagerAID ;
		
		byte[] apdu2 = ApplicationOnCard.installForLoad(loadFileAID, securityDomainAID,
				null, null, null) ;		
		byte[] apduResp2 = trySend(apdu2) ;		
		cardResp = GP_responses.get_GP_response(apduResp2) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("installForLoad:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
		log.write("InstallForLoad sent:(" + Apdu.ba2s(apdu2)+ ")");
		
		
		
		/************** Load applets ************************/
		log.write("Applets loading...") ;

		Vector data = new Vector() ;
		data = file.get() ;
		/*Inializing the number of blocks sent */
		int sentBlocks =0 ;
		/* Number of blocks to be send */
		int lastBlock = data.size() - 1;
		log.write("Block max_size :" + max_size) ;
		log.write("Number of blocks :" + data.size()) ;
		/* Firs block */
		byte[] block1 =(byte[])data.elementAt(0) ;
		byte[] apdu1 = new byte[block1.length + 4] ;
		
		apdu1[0] = (byte)0xC4 ;/* DAP block Flag*/
		apdu1[1] = (byte)0x82 ; /* Security domain AID Flag*/
		apdu1[2] = (byte)0x01 ;
		apdu1[3] = (byte)0x2C ;
		int offset= 4;/*2 ;*/
		//System.arraycopy(securityDomainAID, 0, apdu1, offset, securityDomainAID.length) ;
		//offset += securityDomainAID.length ;
		//apdu1[offset++]=(byte)0xC3 ;/* Load File DATA block signature Flag*/
		System.arraycopy(block1, 0, apdu1, offset, block1.length) ;
		byte[] apdu1_ ;
		
		/* APDU construction */
		if (sentBlocks == lastBlock){
			/* The last block to be sent*/			
			apdu1_ = ApplicationOnCard.load((byte)0x80, (byte)0x00, apdu1.length, apdu1) ;
		}else{
			/* More blocks */
			apdu1_ = ApplicationOnCard.load((byte)0x00, (byte)0x00, apdu1.length, apdu1) ;
		}
		/* APDU sending */
		byte[] respAPDU = trySend(apdu1_) ;
		log.write("Loading...Block 0 sent...") ;
		cardResp =GP_responses.get_GP_response(respAPDU) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("Load:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
		int i ;	
		for (i=1; i<lastBlock; i++){			
			byte[] block_i=(byte[])data.elementAt(i) ;
			/* APDU construction */
			byte[] apdu_i = ApplicationOnCard.load((byte)0x00, (byte)i, block_i.length, block_i) ;
			/* APDU sending */
			respAPDU = trySend(apdu_i) ;
			log.write("Loading...Block " + i + " sent...") ;
			cardResp =GP_responses.get_GP_response(respAPDU) ;
			if (cardResp != GP_responses.SUCCESS){
				throw new UserException("Load:::"  + GP_responses.getErrMsg(cardResp), cardResp) ;
			}
		}
		
		/* Sending the lasBlock */
		if (lastBlock != 0){
			byte[] last = (byte[])data.elementAt(lastBlock) ;
			byte[] lastAPDU = ApplicationOnCard.load((byte)0x80, (byte)lastBlock, last.length, last) ;
			/* APDU sending */
			respAPDU = trySend(lastAPDU) ;
			log.write("loading...Block " + i +" sent...") ;
			cardResp =GP_responses.get_GP_response(respAPDU) ;
			if (cardResp != GP_responses.SUCCESS){
				throw new UserException("Load:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
			}
		}
	}
	
	/** Upload and Installs a package
	 * 
	 * @param packagePath
	 * @throws UserException
	 */
	public void install(String packagePath) throws UserException {
		 
		if (!connected  | !authenticated){
			throw new UserException("The connection was initialized !") ;
		}
		
		if (openSelected == false){
			selectCardManagement(cardName) ;
		}
		log.write("Installing " + packagePath + "... ");
		
		/**************** Upload the package ********************/
		upload(packagePath);
		
		install_only(packagePath) ;
	}
	
	/**
	 * Install the packae packagePath 
	 * The package must be uploaded with upload before
	 * 
	 * @param packagePath
	 * @throws UserException
	 */
	public void install_only(String packagePath) throws UserException{
		
		CapFiles file = null;
		try{
			file = new CapFiles(packagePath, max_size) ;
		}catch(Exception e){
			throw new UserException(e.getMessage()) ;
		}
		log.write("File " + packagePath + " read.");
		
		/**************** Install For Install command ********************/
		/* APDU construction */
		
		int nbApplets = file.getNumberOfApplets() ;
		log.write("Number of applets :" + nbApplets) ;
		
		for(int i=0; i<nbApplets; i++){
			install_applet(file.getAIDHeader(), (byte[])file.getAIDApplet().elementAt(i)) ;			
		}
		log.write("Installing " + packagePath + " completed.");
	}
	
	/**
	 * 
	 * @param packageAID
	 * @param appletAID
	 */
	public void install_applet(byte[] packageAID, byte[] appletAID) throws UserException{
		byte[] apduResp3 ;
		byte[] apdu3;
		int cardResp;
		byte[] ip = {(byte)0xC9, (byte)0x00};
		try{
			apdu3 =  ApplicationOnCard.installForInstall(packageAID, appletAID,
					appletAID,
					(byte) 0x04 /* Application privilege */,
					ip /* InstallParameters*/,
					null /* Instal token*/) ;
		}catch(Exception e){
			throw new UserException("Check the package and applet AID format !") ;
		}
		/* APDU sending */
		apduResp3= trySend(apdu3) ;
		log.write("InstallForInstall sent(Package: " + Apdu.ba2s(packageAID) + " , Applet: " + Apdu.ba2s(appletAID) + ")") ;
		cardResp = GP_responses.get_GP_response(apduResp3) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("installForInstall:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
	}
	
	/**
	 * Gets the privileges of an application/Package using its AID.
	 * This method is not working well. It always uses p1=0x20(Executable load file only).
	 * The privilege returned may be wrong !
	 * 
	 * @param byte[] AID
	 * 
	 * @return String
	 *
	 */
	public String getPrivileges(byte[] AID) throws  UserException{
		/*
		 String privileges;
		 int i = 0;
		 byte [] lc;
		 Search criteria for lc '4F' 
		 lc[i] = (byte)0x4F;
		 System.arraycopy(AID, 0, lc, i++, AID.length) ;
		 byte [] aidResp = ApplicationOnCard.getStatus( (byte)0x10, (byte)0x00, lc);
		 */
		
		if (!connected  | !authenticated){
			throw new UserException("The connection was initialized !") ;
		}
		
		if (openSelected == false){
			this.selectCardManagement(cardName) ;
			this.mutual_authentication(userKeys) ;
		}
		
		int i = 0;
		byte[] data = new byte[1 + AID.length];
		/* Search criteria for lc '4F' */
		data[i] = (byte)0x4F;
		System.arraycopy(AID, 0, data, i++, AID.length) ;
		byte [] apdu = ApplicationOnCard.getStatus( (byte)0x20 , (byte)0x00, data);
		log.write("GETSTATUS sent...") ;
		byte[] aidResp = trySend(apdu) ;
		int cardResp = GP_responses.get_GP_response(aidResp) ;
		log.write("AidResp=" + Apdu.ba2s(aidResp)) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("GetStatus:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
		
		return GP_constants.get_text_privilege(aidResp[ 2 + (int)aidResp[0] ]);		
	}
	
	
	
	/** Installs in the card cardName the package in packagePath
	 *
	 * @param String cardName
	 * @param String userKey
	 * @param String packagePath
	 * 
	 * @throws UserException
	 */
	public  void install(String cardName, String userKey, String packagePath) throws UserException{
		log.write(userKey) ;	
		init_cardManagement(cardName) ;
		mutual_authentication(userKey) ;
		this.install(packagePath) ;
	}
	
	/**
	 * gets the selected application  AID
	 *
	 * @param byte[] APDU response to the selection
	 *
	 * @return byte[]  
	 */
	private byte[] getSelectedApplicationAID(byte[] apdu){
		int i=0, j=0 ;
		/* Application/File AID tag is 0x84 */
		while(apdu[i] != (byte)0x84){
			i++ ;
		}
		
		j = i+1 ;
		/* proprietary data tag is 0xA5*/
		while(apdu[j] != (byte)0xA5){
			j++ ;
		}
		/*AID data is betweent apdu[i] and apdu[j] -> AID.length = j-i-1 */
		int len = j-i-2 ;
		byte[] aid = new byte[len] ;
		System.arraycopy(apdu, i+2, aid, 0, len) ;
		return aid ;
	}
	
	
	/** Deletes a package 
	 * 
	 * @param packagePath
	 * @throws UserException
	 */
	public void delete(String packagePath) throws UserException{
		if (!connected  | !authenticated){
			throw new UserException("Coonection is not initialized !") ;
		}
		
		if (openSelected == false){
			selectCardManagement(cardName) ;
		}
		CapFiles file = null;
		try{
			file = new CapFiles(packagePath, max_size) ;
		}catch(Exception e){
			throw new UserException(e.getMessage()) ;
		}
		/********** Delete applets *******************/
		log.write("Applets deleting...") ;
		int i;
		int nbApplets= file.getNumberOfApplets();
		byte[] loadAID;
		for(i=0; i<nbApplets; i++){
			loadAID = (byte[])file.getAIDApplet().elementAt(i) ;		
			log.write("Deleting applet:"+ Apdu.ba2s(loadAID)) ;
			delete_aid(loadAID);
		}	
		/********** Delete The package *******************/
		byte[] loadAIDP = (byte[])file.getAIDHeader() ;		
		log.write("Package deleting :" + Apdu.ba2s(loadAIDP) + "...");
		delete_aid(loadAIDP);
		log.write("Package " + packagePath + " deleted.") ;
		
	}
	
	/**
	 * Send the GP command Delete with the AID aid
	 * 
	 * @param aid
	 * @throws UserException
	 */
	public void delete_aid(byte[] aid) throws UserException{
		int cardResp ;
		byte[] apdu1P = ApplicationOnCard.deleteApplication(aid,(byte)00);
		byte[] apduRespP ;
		apduRespP = trySend(apdu1P);
		
		cardResp = GP_responses.get_GP_response(apduRespP) ;
		
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("Delete:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
	}
	
	/** Deletes a package from a giving card 
	 * 
	 * @param String card 
	 * @param String userKeys
	 * @param String packagePath
	 *
	 * @throws UserException
	 */
	public void delete(String card, String userKey, String packagePath) throws UserException{
		init_cardManagement(card);    	
		mutual_authentication(userKeys) ;
		delete(packagePath) ;
	}
	
	
	
	/**
	 * Obtains the state of an application Life Cycle
	 *
	 * @param byte[] AID
	 *
	 * @return String
	 *
	 * @throws UserException
	 */
	public String getLifeCycle(byte[] AID) throws UserException{
		if (!connected  | !authenticated){
			throw new UserException("Connection is not initialized!") ;
		}		
		if (openSelected == false){
			this.selectCardManagement(cardName) ;
			this.mutual_authentication(userKeys) ;
		}

		int i = 0;
		byte[] data = new byte[1 + AID.length];
		/* Search criteria for lc '4F' */
		data[i++] = (byte)0x4F;
		System.arraycopy(AID, 0, data, i, AID.length) ;
		System.out.println("begin");
		byte [] apdu = ApplicationOnCard.getStatus((byte)0x20 , (byte)0x00, data);
		System.out.println("end");
		byte[] aidResp = trySend(apdu) ;
		System.out.println("AID=" + Apdu.ba2s(AID)) ;
		System.out.println("apdu=" + Apdu.ba2s(apdu)) ;
		System.out.println("resp=" + Apdu.ba2s(aidResp)) ;
		int cardResp = GP_responses.get_GP_response(aidResp) ;
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("GetStatus:"+ Apdu.ba2s(aidResp)+"::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}

		return GP_constants.get_text_life_cycle(aidResp[ 1 + (int)aidResp[0] ]) ;
	}	
	
	
	/** Selects a package 
	 * 
	 * @param packagePath
	 * @throws UserException
	 */
	public void select(String packagePath) throws UserException{
		if (!connected ){
			throw new UserException("The Connection is not initialized !") ;
		}
		
		if (openSelected == false){
			selectCardManagement(cardName) ;
		}
		CapFiles file = null;
		try{
			file = new CapFiles(packagePath, max_size) ;
		}catch(Exception e){
			throw new UserException(e.getMessage()) ;
		}
		byte[] loadAID = file.getAIDHeader() ;
		
		select_aid(loadAID);
		
		openSelected = false ;
		authenticated = false ;
	}
	
	/** Selects a package in a giving card 
	 * 
	 * @param card
	 * @param userKey
	 * @param packagePath
	 * @throws UserException
	 */
	public void select(String card, String userKey, String packagePath ) throws UserException{  			
		init_cardManagement(card);  
		mutual_authentication(userKeys) ;
		select(packagePath) ;
	}
	
	/** 
	 * 
	 * @param card
	 * @param userKey
	 * @param packagePath
	 * @throws UserException
	 */
	public void selectNext(String card, String userKey, String packagePath ) throws UserException{  	
		
		init_cardManagement(card);    	
		
		/* Mutual authentication */
		mutual_authentication(userKey);
		
		CapFiles file = null;
		try{
			file = new CapFiles(packagePath, max_size) ;
		}catch(Exception e){
			throw new UserException(e.getMessage()) ;
		}
		byte[] loadAID = file.getAIDHeader() ;
		byte[] apdu1 = ApplicationOnCard.selectApplication(loadAID, (byte) 2) ;
		byte[] apduResp ;
		apduResp = trySend(apdu1);
		
		int cardResp = GP_responses.get_GP_response(apduResp) ;
		
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("Select:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
		
	}
	
	/**
	 * Selects the applet with the given AID on the card. 
	 * 
	 * @param aid
	 */
	public void select_aid(byte[] aid) throws UserException{
		byte[] apdu1 = ApplicationOnCard.selectApplication(aid, (byte) 0) ;
		byte[] apduResp ;
		apduResp = trySend(apdu1);
		log.write("Application selected :" + Apdu.ba2s(apdu1)) ;	
		int cardResp = GP_responses.get_GP_response(apduResp) ;
		
		if (cardResp != GP_responses.SUCCESS){
			throw new UserException("Select:::" + GP_responses.getErrMsg(cardResp), cardResp) ;
		}
	}
	
	/** Try to send the apdu to the card
	 * 
	 * @param apdu
	 * @return
	 * @throws UserException
	 */
	public byte[] trySend(byte[] apdu) throws UserException{
		if (securityLevel == 1 ){
			try{
				apdu=sc.add_C_MAC(apdu) ;
			}catch(Exception e){
				e.printStackTrace();
				throw new UserException("add_C_MAC" + e.getMessage()) ;
			}
		}
		byte[] apduResp ;
		try{
			apduResp = card.sendAPDU(apdu) ;
			log.write("Apdu sent:" + Apdu.ba2s(apdu)) ;
			log.write("Apdu received:" + Apdu.ba2s(apduResp)) ;
			
		}
		catch(Exception e){
			throw new UserException(" : sendAPDU:"+ Apdu.ba2s(apdu)+ ":" + e.getMessage() ) ;
		}
		return apduResp ;
	}
	
	/** Try to send external-authenticate command
	 * 
	 * @param apdu
	 * @return
	 * @throws UserException
	 */
	private byte[] trySendAuthentication(byte[] apdu) throws UserException{
		int s0= securityLevel ;
		securityLevel = 1 ;
		byte [] resp = trySend(apdu) ;
		securityLevel = s0 ;
		return resp ;
	}
	
	/****** Accessors ***********/
	
	public boolean is_connected(){
		return this.connected;
	}
	
	/**
	 * Set/Change current securityLevel
	 *
	 *
	 * @param level
	 */
	public void set_securityLevel(int level) throws UserException{
		log.write("SecurityLevel set to " + level + ".") ;
		this.securityLevel = level ;
		if (authenticated){
			mutual_authentication(userKeys) ;
		}
	}
	
	/**
	 * Cancels or changes log destination
	 *
	 * @param 0-> cancel log, 1->output in the log file, 2->std output
	 *
	 */
	public void setLog(int logDest){
		log.setLog(logDest) ;
	}
	
	public Log getLog(){
		return log ;
	}
	
	public void setInit(Init init){
		this.init = init;
	}
	
	public Init getInit(){
		return this.init ;
	}
	
	private void set_sc(SecureChannel sc){
		this.sc = sc;
	}
	
	/**
	 * 
	 * @return
	 */
	public byte[] get_cardManagerAID(){
		return cardManagerAID ;
	}
	
	/**
	 * Copy cAID into cardManagerAID. Is not a reference.
	 * 
	 * @param cardManagerAID
	 */
	private void set_cardManagerAID(byte[] cAID){
		cardManagerAID = new byte[cAID.length] ;
		System.arraycopy(cAID, 0, cardManagerAID, 0, cAID.length);
	}
	
	/**
	 * 
	 * @param uk
	 */
	private void set_userKeys(String uk){
		this.userKeys = new String(uk) ;
	}
}


