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

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import javax.swing.JTextArea;


/**
 * @author Moez Ben MBaraka
 *
 */
public class Screen {
    
    private BufferedWriter out ;
    
    private BufferedReader input ;
    
    private String shell = "" ;
    
    /* Must be set to true if the X-client is used. */
    private boolean MODE_X = false;
    
    private JTextArea shellArea = null;
    
    /* No output if false. */
    private boolean out_status = true ;
    
    /**
     * Creates a Screen in an X-Mode.
     */
    public Screen(JTextArea shellArea) {
        MODE_X = true;
        this.shellArea = shellArea;
    }
    
    public Screen() {
        this(System.in, System.out);
    }
    
    
    /**
     *
     * @param in
     * @param out
     */
    public Screen(InputStream in, OutputStream out){
        this.out = new BufferedWriter(new OutputStreamWriter(out));
        input = new BufferedReader(new InputStreamReader(in)) ;
    }
    
    public String ready(){
        return this.ready(this.shell + ">") ;
    }
    
    /**
     *
     * @param txt
     */
    public String ready(String msg){
        this.write(msg) ;
        return this.readLine();
    }
    
    
    
    /**
     *
     * @return
     */
    public String readLine(){
        try{
            return input.readLine();
        } catch(IOException e){
            e.printStackTrace() ;
            return null;
        }
    }
    
    public void write(String msg){
        if (out_status){
            try{
                if (MODE_X) {
                    System.out.println("MODEX ON");
                    shellArea.append(msg);
                } else {
                    out.write(msg);
                    out.flush() ;
                }
            }catch(IOException e){
                e.printStackTrace() ;
            }
        }
    }
    
    public void writeln(String msg){
        if (out_status){
            //System.out.println(msg) ;
            this.write(msg + "\n");
            //out.flush();
        }
        
    }
    
    public void disable_out(){
        out_status= false;
    }
    
    
    /**** function to be used to displays errors or msg *****/
    /**
     * Display an error
     * @param msg the error message
     */
    public void error(String msg){
        writeln(msg) ;
    }
    
    /**
     * Get the error msg fromm Errors class and displays it with the previous method
     *
     * @param errCode
     * @param opt
     */
    public void error(int errCode, String opt){
        error(Errors.get_errorMsg(errCode, opt)) ;
    }
    
    /**
     *
     * @param errCode
     * @param opt
     * @param e
     */
    public void error(int errCode, String opt,Exception e){
        error(e.getMessage()) ;
        error("--> " + Errors.get_errorMsg(errCode, opt)) ;
    }
    
    public void error(int errCode){
        error("--> " + Errors.get_errorMsg(errCode)) ;
    }
    
    
    public void info(String msg){
        writeln("---" + msg + "---");
    }
    
    public void command(String msg){
        writeln(shell + ">" + msg) ;
    }
    
    /********* Accessors **************/
    public void set_shell(String shell){
        this.shell= shell;
    }
    
    public void init_shell(){
        set_shell("") ;
    }
}


