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


/**
 * This class defines methods to build APDU commands for standad
 * operation on the cards (connection, installation...).
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class ApplicationOnCard {
    /**
     * Builds APDU to select the security domain
     * Data and Lc field omitted
     *
     * @return byte[]
     */
    public static byte[] selectSecurityDomain() {
        byte[] cmd_buf = Utils.buildHeader((byte)0x00 /*CLA*/, (byte)0xA4/*INS*/,
                (byte)0x04/*p1*/,(byte)0x00/*p2*/,(byte)0x00/*Le*/);
        return cmd_buf ;
    }
    
    /**
     * builds the APDU of the SELECT command
     *
     * @param byte[] aid
     * @param byte p2
     * @return byte[]
     */
    public static byte[] selectApplication(byte[] aid, byte p2){
        byte[] header = Utils.buildHeader((byte)0x00 /*CLA*/, (byte)0xA4/*INS*/,
                (byte)0x04/*p1*/,p2/*p2*/,(byte)aid.length /*LC*/);
        int len= header.length + aid.length + 1;
        byte[] cmd_buf = new byte[len];
        System.arraycopy(header, 0, cmd_buf, 0, header.length); /* Header */
        System.arraycopy(aid, 0, cmd_buf, header.length, aid.length); /* Data */
        cmd_buf[len-1]=(byte)0x00; /*Le*/
        return cmd_buf;
    }
    
    
    
    /**
     * builds the APDU of the DELETE command
     *
     * @param byte[] aid
     * @param byte p2
     * @return byte[]
     */
    public static byte[] deleteApplication(byte[] aid, byte p2){
        int i=0;
        byte[]header = Utils.buildHeader((byte)0x80/* CLA */,(byte)0xE4 /* INS */,
                (byte)0x00 /* P1 */, p2/* P2 */,
                (byte) (aid.length+2)/*lc: Data length= length(4F+ AID.length + AID)*/);
        /*Begin Data*/
        int len = aid.length + header.length + 3;
        byte[] cmd_buf = new byte[len];
        System.arraycopy(header, 0, cmd_buf, 0, header.length) ; /* Header */
        i=header.length;
        cmd_buf[i++]= (byte)0x4F ; /*Tag for an AID*/
        cmd_buf[i++]= (byte)aid.length ;
        System.arraycopy(aid, 0, cmd_buf, i, aid.length) ; /* Data */
        i=i + aid.length ;
        cmd_buf[i]= 0x00; /* Le */
        return cmd_buf ;
    }
    
    
    /**
     * builds the APDU of the GETSTATUS command
     *
     * @param byte p1
     * @param byte p2
     * @param byte data
     * @return byte[]
     */
    public static byte[] getStatus(byte p1, byte p2, byte[] data) {
        byte[] cmd_buf = new byte[6+ data.length];
        byte[] p1p2= new byte[2] ;
        p1p2[0]=p1 ;
        p1p2[1]= p2;
        //System.out.println("data=" + Apdu.ba2s(data)) ;
        byte[]header = Utils.buildHeader((byte)0x80/* CLA */,(byte)0xF2 /* INS */,p1 /* P1 */, p2/* P2 */,
                (byte) (data.length)/*lc: Data length*/);
        
        System.arraycopy(header, 0, cmd_buf, 0, header.length);
        System.arraycopy(data, 0, cmd_buf, header.length, data.length); /* Lc */
        cmd_buf[header.length + data.length ] = (byte)0x00 /* Le */;
        return cmd_buf;
}


/**
 * builds the APDU of the installForLoad command
 *
 * @param byte[] loadFileAID
 * @param byte[] securityDomainAID
 * @param byte[] loadFileDatablockHash
 * @param byte[] loadParametersField
 * @param byte[] loadToken
 * @return byte[]
 */
public static byte[] installForLoad(byte[] loadFileAID,byte[] securityDomainAID,
        byte[] loadFileDatablockHash,byte[] loadParametersField,byte[] loadToken){
    int i=0; int lc;
    lc = 5 + loadFileAID.length+ securityDomainAID.length ;
    
    if (loadFileDatablockHash != null){
        lc += 	loadFileDatablockHash.length ;
    }
    
    if (loadToken != null){
        lc += 	loadToken.length ;
    }
    
    if (loadParametersField != null){
        lc += 	loadParametersField.length ;
    }
    
    byte[] header = Utils.buildHeader((byte)0x80 /*CLA*/, (byte)0xE6/*INS*/, 
            (byte)0x02/*p1*/,(byte)0x00/*p2*/,(byte) lc /*LC*/);
    byte[] cmd_buf = new byte[lc + header.length + 1];
    
    
    /* Begin Data */
    
    System.arraycopy(header, 0, cmd_buf, 0, header.length) ; /* Header */
    i=header.length;
    
    
    cmd_buf[i++]= (byte)loadFileAID.length;
    System.arraycopy(loadFileAID, 0, cmd_buf, i,loadFileAID.length);
    i=i + loadFileAID.length;
    
    cmd_buf[i++]= (byte)securityDomainAID.length ;
    System.arraycopy(securityDomainAID, 0, cmd_buf, i,securityDomainAID.length);
    i= i + securityDomainAID.length ;
    
    if (loadFileDatablockHash != null){
        cmd_buf[i++]= (byte)loadFileDatablockHash.length ;
        System.arraycopy(loadFileDatablockHash, 0, cmd_buf, i, loadFileDatablockHash.length);
        i=i +loadFileDatablockHash.length ;
    }else
        cmd_buf[i++]= (byte)0 ;
    
    if (loadParametersField != null){
        cmd_buf[i++]= (byte)loadParametersField.length ;
        System.arraycopy(loadParametersField, 0, cmd_buf, i, loadParametersField.length);
        i=i+loadParametersField.length;
    } else
        cmd_buf[i++]= (byte)0 ;
    
    if (loadToken != null){
        cmd_buf[i++]= (byte)loadToken.length ;
        System.arraycopy(loadToken, 0, cmd_buf, i, loadToken.length);
        i=i + loadToken.length ;} else
            cmd_buf[i++]= (byte)0 ;
    
    cmd_buf[i++]= (byte)0 ;
    return cmd_buf ;
}

/**
 * builds the APDU of the installForInstall command
 *
 * @param byte[] executableLoadFileAID
 * @param byte[] executableModuleAID
 * @param byte[] applicationAID
 * @param byte applicationPrivileges
 * @param byte[] installParameters
 * @param byte[] installToken
 * @return byte[]
 */
public static byte[] installForInstall(byte[] executableLoadFileAID,
        byte[] executableModuleAID,
        byte[] applicationAID,
        byte applicationPrivileges,
        byte[] installParameters,
        byte[] installToken){
    int i=0; int lc;
    lc = 7 + executableLoadFileAID.length + executableModuleAID.length + applicationAID.length + 
            installParameters.length ;
    if (installToken != null){
        lc+= installToken.length;
    }
    
    byte[] header = Utils.buildHeader((byte)0x80 /*CLA*/, (byte)0xE6/*INS*/, (byte)0x0C/*p1*/,
            (byte)0x00/*p2*/,(byte) lc /*LC*/);
    byte[] cmd_buf = new byte [lc + header.length +1];
    
    System.arraycopy(header, 0, cmd_buf, 0, header.length) ; /* Header */
    i=header.length;
    /* Begin Data */
    cmd_buf[i++]= (byte)executableLoadFileAID.length ;
    System.arraycopy(executableLoadFileAID, 0, cmd_buf, i, executableLoadFileAID.length);
    i=i + executableLoadFileAID.length ;
    
    cmd_buf[i++]= (byte)executableModuleAID.length ;
    System.arraycopy(executableModuleAID, 0, cmd_buf, i, executableModuleAID.length);
    i=i + executableModuleAID.length ;
    
    cmd_buf[i++]= (byte)applicationAID.length ;
    System.arraycopy(applicationAID, 0, cmd_buf, i, applicationAID.length);
    i=i + applicationAID.length ;
    
    cmd_buf[i++]= (byte) 1;
    cmd_buf[i++]= applicationPrivileges;
    
    cmd_buf[i++]= (byte) installParameters.length ;
    System.arraycopy(installParameters, 0, cmd_buf, i, installParameters.length);
    i=i+installParameters.length;
    
    if (installToken != null){
        cmd_buf[i++]= (byte)installToken.length ;
        System.arraycopy(installToken, 0, cmd_buf, i, installToken.length);
        i=i+installToken.length;
    } else
        cmd_buf[i++]= (byte)0 ;
    
    cmd_buf[i++]= (byte)0 ;
    return cmd_buf ;
}

/**
 * Builds the APDU of the installForInstall command without the conditional field installToken
 *
 * @param byte[] executableLoadFileAID
 * @param byte[] executableModuleAID
 * @param byte[] applicationAID
 * @param byte applicationPrivileges
 * @param byte[] installParameters
 * @return byte[]
 */
public static byte[] installForInstall(byte[] executableLoadFileAID,
        byte[] executableModuleAID,
        byte[] applicationAID,
        byte applicationPrivileges,
        byte[] installParameters){
    return installForInstall(executableLoadFileAID,
            executableModuleAID,
            applicationAID,
            applicationPrivileges,
            installParameters,
            null) ;
}
/**
 * Builds Load APDU
 *
 * @param byte P1
 * @param byte P2
 * @param int Lc
 * @param byte[] loadData
 * @return byte[]
 */
public static byte[] load(byte P1, byte P2, int Lc, byte[]loadData){
    byte[] cmd_buf = new byte[5 + (int)Lc] ;
    byte[] header = Utils.buildHeader((byte)0x80 /*CLA*/, (byte)0xE8/*INS*/, P1/*p1*/,P2/*p2*/,(byte)Lc /*LC*/);
    
    System.arraycopy(header, 0, cmd_buf, 0, header.length);
    System.arraycopy(loadData, 0, cmd_buf, header.length, loadData.length);
    
    return cmd_buf ;
}



}


