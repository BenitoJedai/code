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

package com.jgpshell.globalPlatform;

import com.jgpshell.offCard.UserException ;

import java.security.Key;
import javax.crypto.Cipher;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.DESedeKeySpec;
import javax.crypto.spec.IvParameterSpec;

import com.linuxnet.jpcsc.Apdu;

/**
 * Methods to manage the secure channel.
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class SecureChannel implements Cloneable{
	
	/**
	 *  Random array generated by the offcard entity
	 */
	private byte[] HostChallenge ;
	
	/**
	 *  Random array generated by the card 
	 */
	private byte[] CardChallenge =new byte[8] ; ;
	
	/**
	 * initializing  the ICV 
	 */
	private byte[] civ = {(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00,
			(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00} ;
	
	/*
	 * 
	 */
	private byte[] zero= {(byte) 0x00,(byte) 0x00,(byte) 0x00,(byte) 0x00,
			(byte) 0x00,(byte) 0x00,(byte) 0x00,(byte) 0x00};
	
	/**
	 * session keys 
	 */
	private byte[] S_ENC= new byte[16];
	private byte[] S_MAC= new byte[16];
	
	/**
	 * Triple DES Encryption Algo name 
	 */
	private final String ENCRYPTION_ALGO = "DESede" ;
	
	/* Static keys used to generate session keys */
	/* initial size must be 24 */
	private byte[] static_ENC = new byte[24] ;
	private byte[] static_MAC = new byte[24];
	
	private int keyIndex ;
	private int keyVersionNumber ;
	
	
	/**
	 * Indicates current securityLevel :
	 * 3 : C-Decryption and C-MAC
	 * 1 : C-MAC only
	 * 0 : NO secure messaging
	 */
	private int securityLevel ;
	
	
	/**
	 * 
	 */
	public Object clone() throws CloneNotSupportedException{
		SecureChannel n_sc= (SecureChannel)super.clone() ;
		n_sc.set_hostChallenge(Utils.clone_array(HostChallenge)) ;
		n_sc.set_cardChallenge(Utils.clone_array(CardChallenge)) ;
		n_sc.set_civ(Utils.clone_array(civ)) ;
		n_sc.set_S_ENC(Utils.clone_array(S_ENC)) ;
		n_sc.set_S_MAC(Utils.clone_array(S_MAC)) ;
		n_sc.set_static_ENC(Utils.clone_array(static_ENC)) ;
		n_sc.set_static_MAC(Utils.clone_array(static_MAC)) ;
		return n_sc;
	}
	
	/** 
	 * Creates a SecureChannel using the specified user keys. 
         *
	 * @param userKeys
	 */
	public SecureChannel(String userKeys){
		getInitValues(userKeys) ;
	}
	
	/**
	 * Creates a SecureChannel using the specified parameters. 
         *
	 * @param userKeys
	 * @param keyVersion
	 * @param keyIndexNumber
	 */
	public SecureChannel(String userKeys, int keyVersion, int keyIndexNumber){
		getInitValues(userKeys,keyIndexNumber ,keyVersion) ;
	}
	
	public SecureChannel(String userKeys, int keyIndex){
		getInitValues(userKeys, keyIndex);
	}
	
	
	/**
	 * 
	 *
	 */
	public SecureChannel(){
		byte[] static_ENC_={(byte)0X40, (byte)0X41,(byte)0X42,(byte)0X43,(byte)0X44,
				(byte)0X45,(byte)0X46,(byte)0X47,(byte)0X48,(byte)0X49,(byte)0X4a,
				(byte)0X4b,(byte)0X4c,(byte)0X4d,(byte)0X4e,(byte)0X4f, (byte)0X40, (byte)0X41,(byte)0X42,(byte)0X43,(byte)0X44,
				(byte)0X45,(byte)0X46,(byte)0X47} ;
		
		byte[] static_MAC_={(byte)0X40, (byte)0X41,(byte)0X42,(byte)0X43,(byte)0X44,
				(byte)0X45,(byte)0X46,(byte)0X47,(byte)0X48,(byte)0X49,(byte)0X4a,
				(byte)0X4b,(byte)0X4c,(byte)0X4d,(byte)0X4e,(byte)0X4f, (byte)0X40, (byte)0X41,(byte)0X42,(byte)0X43,(byte)0X44,
				(byte)0X45,(byte)0X46,(byte)0X47} ;
		System.arraycopy(static_MAC_, 0, static_MAC, 0, 24);
		System.arraycopy(static_ENC_, 0, static_ENC, 0, 24);
	}
	
	/** 
	 * Init static MAC and ENC keys from the string userKeys
         *
	 * @param String userKeys
	 */
	private void getInitValues(String userKeys){
		keyIndex =0 ;
		keyVersionNumber =0 ;
		makeStaticKeys(userKeys) ;
	}
	
	private void getInitValues(String userKeys, int keyIndex){
		this.keyIndex = keyIndex ;
		makeStaticKeys(userKeys) ;
	}
	
	
	/**
	 * Init static MAC and ENC keys from the database
	 * using card id and the key index
	 *
	 * @param String userKeys
	 * @param int keyIndex
	 * @param int keyVersionNumber
	 *
	 */	
	private void getInitValues(String userKeys, int keyIndex, int keyVersionNumber){
		this.keyIndex = keyIndex ;
		this.keyVersionNumber = keyVersionNumber ;
		makeStaticKeys(userKeys) ;
	}
	/** 
	 * 
	 *
	 * @param String userKeys
	 */
	private void makeStaticKeys(String userKeys){
		byte[] static_ENC_= Apdu.s2ba(userKeys) ;
		System.arraycopy(static_ENC_, 0, static_ENC, 0, 16) ;
		System.arraycopy(static_ENC_, 0, static_ENC, 16, 8) ;		
		System.arraycopy(static_ENC, 0, static_MAC, 0, 24) ;
	}
	
	/**
	 * 
	 * @return byte[]
	 */
	public byte[] initializeUpdate(){		
		byte[] header = Utils.buildHeader((byte)0x80 /*CLA*/, (byte)0x50/*INS*/, (byte)keyVersionNumber/*p1*/,(byte)keyIndex/*p2*/,(byte)0x08 /*LC*/);
		
		/* generate a random array of 8 bytes*/
		HostChallenge =  new byte[8] ;
		HostChallenge= Utils.rand_bytes(8);
		
		int len= header.length + HostChallenge.length + 1 /*Le*/;  
		byte[] cmd_buf = new byte[len];
		System.arraycopy(header, 0, cmd_buf, 0, header.length); /* Header */
		System.arraycopy(HostChallenge, 0, cmd_buf, header.length, HostChallenge.length); /* Header */
		
		cmd_buf[len - 1] = (byte)0x00 ;
		
		return cmd_buf ;
	}
	
	
	/**
	 * Added on 01/07/2006
	 * 
	 * @param cardCryptogram
	 * @return True if the cardCryptogram is OK
	 */
	private boolean check_cardCryptogram(byte[] cardCryptogram, byte[] S_ENC_) throws Exception{
		byte[] cardCrypto = new byte[24] ;  //before encryption
		byte[] cardCrypto_ = new byte[8] ;  //after encryption
		System.arraycopy(this.HostChallenge, 0, cardCrypto, 0, 8) ;
		System.arraycopy(this.CardChallenge, 0, cardCrypto, 8, 8) ;
		
		/* Applying DES Padding rule*/
		byte[] padding={(byte)0x80, (byte)0x00, (byte)0x00, (byte)0x00,
				(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00} ;
		System.arraycopy(padding, 0, cardCrypto, 16, 8) ;
		
		/*initializing  the ICV */
		byte[] iv = {(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00,
				(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00} ;
		IvParameterSpec IvSpec = new IvParameterSpec(iv) ;
		/* Applying the signature method */		
		System.arraycopy(des3_CBC_encryption(S_ENC_, cardCrypto, IvSpec), 16, cardCrypto_,0, 8) ;
		
		//Une fonction qui compare deux tablaux de bytes (A verifier si le cast en (int) marche) !
		for(int i=0; i<8; i++){
			if (cardCryptogram[i] != cardCrypto_[i]){
				return false ;
			}			
		}
		return true ;
	}
	
	/**   
	 * Makes the externalAuthenticate apdu using response1 
	 *
	 * @param response1 is the init_update response
	 * @param securityLevel
	 * @return
	 * @throws Exception
	 */
	public byte[] externalAuthenticate(byte[] response1, byte securityLevel) throws Exception{
		
		/* Calcule et v�rification du cardCryptogram*/
		byte[] cardCryptogram = new byte[8] ;
		System.arraycopy(response1, 20, cardCryptogram, 0, 8) ;
		
		
		
		this.securityLevel = securityLevel ;
		byte[] header = Utils.buildHeader((byte)0x80 /*CLA*/, (byte)0x82/*INS*/,
				securityLevel/*p1*/, (byte)0x00/*p2*/,(byte)0x08 /*LC	*/);
		
		int len= header.length + 8 /* 8 = HostCryptoGram.length */;  
		
		byte[] cmd_buf = new byte[len];
		System.arraycopy(header, 0, cmd_buf, 0, header.length); /* Header */
		
		/* Ces deux tableaux ne sont pas utilis�s apr�s*/
		byte[] keyDiversificationData = new byte[10] ;
		System.arraycopy(response1, 0, keyDiversificationData, 0, 10) ;		
		byte[] keyInformation = new byte[2] ;
		System.arraycopy(response1, 10, keyInformation, 0, 2) ;
		
		System.arraycopy(response1, 12, CardChallenge, 0, 8) ;
		
		
		
		byte[] derivationData = new byte[16] ;
		System.arraycopy(CardChallenge, 0, derivationData, 8, 4);
		System.arraycopy(CardChallenge, 4, derivationData, 0, 4);
		System.arraycopy(HostChallenge, 0, derivationData, 4, 4);
		System.arraycopy(HostChallenge, 4, derivationData, 12, 4);
		
		/* Building ENC and MAC session keys */
		S_ENC= des3_ECB_encryption(static_ENC, derivationData) ;
		S_MAC= des3_ECB_encryption(static_MAC, derivationData) ;
		byte[] S_ENC_ = new byte[24] ;
		byte[] S_MAC_ = new byte[24] ;
		
		System.arraycopy(S_ENC, 0, S_ENC_, 0, 16) ;
		System.arraycopy(S_ENC, 0, S_ENC_, 16, 8) ;
		
		System.arraycopy(S_MAC, 0, S_MAC_, 0, 16) ;
		System.arraycopy(S_MAC, 0, S_MAC_, 16, 8) ;
		
		
		//check the card cryptogram
		if (this.check_cardCryptogram(cardCryptogram, S_ENC_) == false){
			throw new UserException("Wrong card cryptogram !") ;
		}
		
		/* calculating hostCryptoGram */
		byte[] hostCryptogram = new byte[8] ;
		System.arraycopy(calculate_hostCryptogram(CardChallenge, S_ENC_), 16, hostCryptogram, 0, 8)  ;
		
		/* field Data: hostCryptogram  */ 
		System.arraycopy(hostCryptogram, 0, cmd_buf, header.length, hostCryptogram.length) ;
		
		return cmd_buf ;
		
	}
	
	
	
	/** Calculates and Adds C_MAC to the apdu
	 * 
	 * @param apdu
	 * @return byte[]
	 * @throws Exception
	 */
	public byte[] add_C_MAC(byte[] apdu) throws Exception{
		
		/* The S_MAC used to generate the key must have 24 bytes*/
		byte[] S_MAC_ = new byte[24] ;
		System.arraycopy(S_MAC, 0, S_MAC_, 0, 16) ;
		System.arraycopy(S_MAC, 0, S_MAC_, 16, 8) ;
		
		/* verifying if the Le exists */
		int len = apdu.length ;
		boolean withLe = false ;
		if (apdu.length == 5 + (int)apdu[4] +1){
			len = apdu.length -1 ;
			withLe = true ;
		}	
		
		/* Set Lc=Lc + C_MAC.length*/
		apdu[4] = (byte)(apdu[4] + (byte)0x08) ;
		
		/* Set bit 3 to indicate that the commande contanis a C-MAC*/
		apdu[0] =(byte)(apdu[0] + (byte)0x04) ;
		
		byte[] napdu = new byte[len] ;
		System.arraycopy(apdu, 0, napdu, 0, len) ;
		
		/* Calculating and appending DES padding*/
		byte[] padding = des_padding(napdu) ;
		byte[] apdu2 = new byte[napdu.length + padding.length] ;
		System.arraycopy(napdu, 0, apdu2, 0, napdu.length) ;
		System.arraycopy(padding, 0, apdu2, napdu.length , padding.length) ;
		
		IvParameterSpec IvSpec = new IvParameterSpec(civ) ;
		byte[] C_MAC_=des3_CBC_encryption(S_MAC_, apdu2, IvSpec);
		
		/* C_MAC is the last 8 bytes*/
		byte[] C_MAC = new byte[8] ;
		System.arraycopy(C_MAC_, C_MAC_.length - 8, C_MAC, 0, 8) ;
		
		/* Setting the new civ */
		System.arraycopy(C_MAC_, C_MAC_.length - 8, civ, 0, 8) ;
		
		/* APDU output: APDU input + C-MAC */
		byte[] napdu2 = new byte[apdu.length + C_MAC.length] ;
		
		if (withLe){
			len = apdu.length -1 ;
		}	
		System.arraycopy(apdu, 0, napdu2, 0, len) ;
		System.arraycopy(C_MAC, 0, napdu2, len, C_MAC.length) ;
		
		/* We add the Le (if it existed before)*/
		if (withLe){
			napdu2[napdu2.length - 1] =apdu[apdu.length - 1] ;
		}
		
		return napdu2;
	}
	
	
	
	
	
	/**
	 * Calculate necessary paddind prior to performing des operations
	 *
	 * @param apdu 
	 * @return The padding
	 *
	 */
	private byte[] des_padding(byte[] apdu){
		
		int len = apdu.length;
		int len_ =len;
		len = len + 1 ;
		while (len%8 != 0){
			len ++ ;
		}
		
		byte[] padding = new byte[len - len_ ];
		
		padding[0] = (byte) 0x80 ;
		
		for (int i=1; i<len - len_ ; i++){
			padding[i] = (byte)0x00 ;
		}
		return padding ;
	}
	
	
	/** Calculates the host Cryptogram 
	 * 
	 * @param cardChallenge
	 * @param S_ENC
	 * @return byte[]
	 * @throws Exception 
	 */
	private byte[] calculate_hostCryptogram(byte[] cardChallenge, byte[] S_ENC) throws Exception{
		
		byte[] hostCryptogram_ = new byte[24] ;
		
		/* Final hostCryptogram array*/
		byte[] hostCryptogram = new byte[8] ;
		
		System.arraycopy(cardChallenge, 0, hostCryptogram_, 0, cardChallenge.length) ;
		System.arraycopy(this.HostChallenge, 0, hostCryptogram_, 8, HostChallenge.length) ;
		
		/* Applying DES Padding rule*/
		byte[] padding={(byte)0x80, (byte)0x00, (byte)0x00, (byte)0x00,
				(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00} ;
		System.arraycopy(padding, 0, hostCryptogram_, 16, 8) ;
		/*initializing  the ICV */
		byte[] iv = {(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00,
				(byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00} ;
		IvParameterSpec IvSpec = new IvParameterSpec(iv) ;
		/* Applying the signature method */
		//System.out.println("**ici:" + Apdu.ba2s(S_ENC) + "**") ;
		hostCryptogram = des3_CBC_encryption(S_ENC, hostCryptogram_, IvSpec) ;
		return hostCryptogram ;
	}
	
	
	/** Encrypts the buffer with triple DES algorithm in mode ECB using the keyBuffer 
	 * 
	 * @param keyBuffer
	 * @param buffer
	 * @return
	 * @throws Exception
	 */
	private byte[] des3_ECB_encryption(byte[] keyBuffer, byte[] buffer) throws Exception{
		DESedeKeySpec key= new DESedeKeySpec(keyBuffer) ;
		SecretKeyFactory kf = SecretKeyFactory.getInstance(ENCRYPTION_ALGO) ;
		Key sk= kf.generateSecret(key) ;
		Cipher c=Cipher.getInstance(ENCRYPTION_ALGO + "/ECB/NoPadding") ;
		c.init(Cipher.ENCRYPT_MODE, sk) ;
		return c.doFinal(buffer) ;
	}
	
	/** Encrypts the buffer with triple DES algorithm in mode CBC using the keyBuffer and the vector iv
	 * 
	 * @param byte[] keyBuffer (its size must be 24 bytes)
	 * @param byte[] buffer
	 * @param IvParameterSpec iv
	 * @return byte[]
	 */
	private byte[] des3_CBC_encryption(byte[] keyBuffer, byte[] buffer, IvParameterSpec iv) throws Exception{
		DESedeKeySpec key= new DESedeKeySpec(keyBuffer) ;
		SecretKeyFactory kf = SecretKeyFactory.getInstance(ENCRYPTION_ALGO) ;
		Key sk= kf.generateSecret(key) ;
		
		Cipher c=Cipher.getInstance(ENCRYPTION_ALGO + "/CBC/NoPadding") ;
		c.init(Cipher.ENCRYPT_MODE, sk, iv) ;
		
		return c.doFinal(buffer) ;
	}
	
	private void set_hostChallenge(byte[] hc){
		HostChallenge = hc ;
	}
	
	private void set_cardChallenge(byte[] cc){
		CardChallenge = cc;
	}
	
	private void set_civ(byte[] iv){
		civ=iv;
	}
	
	private void set_S_ENC(byte[] enc){
		S_ENC = enc;
	}
	
	private void set_static_ENC(byte[] enc){
		static_ENC = enc;
	}
	
	private void set_S_MAC(byte[] mac){
		S_MAC = mac;
	}
	
	private void set_static_MAC(byte[] mac){
		static_ENC = mac;
	}
}


