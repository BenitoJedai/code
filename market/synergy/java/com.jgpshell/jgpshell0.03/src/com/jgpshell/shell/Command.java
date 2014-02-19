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

import com.jgpshell.offCard.CardApplication;
import com.jgpshell.offCard.UserException;
import com.jgpshell.globalPlatform.GP_constants;
import com.jgpshell.globalPlatform.SecureChannel;

import com.linuxnet.jpcsc.Apdu;

import java.io.ByteArrayInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;

/**
 * This class implements all the shell commands.
 *
 * @author Moez Ben MBarka
 * @version $Revision$
 */
public class Command extends Script
{
    
    private Screen screen ;
    
    private Session session ;
    
    /**
     * Initializes the object with specified parameters.
     *
     * @param screen Will be used for input/output
     * @param session Will be used for the shell session.
     */
    public Command(Screen screen, Session session){
        super(screen, session) ;
        this.screen = screen ;
        this.session = session;
    }
    
    /**
     * Returns the session associated to this Command.
     *
     * @return A Session object.
     */
    public Session get_session() {
        return session;
    }
    
    /**
     * Returns the screen associated to this Command.
     *
     * @return A Session object.
     */
    public Screen get_screen() {
        return screen;
    }
    
    /**
     * Executes the command using the specified <code>jgpshell<code> as a parser.
     *
     * @param jgpshell The parser to use.
     * @param cmd The command to execute.
     */
    public void execute_cmd(JGPShell jgpshell, String cmd) {
        int start=0;
        String tag="0";
        if (!session.pre_history_empty()){
            session.display_pre_history_threads() ;
            session.moveToHistory() ;
        }
        
        set_last_line(cmd);
        if (cmd.length() < 1){
            return ;
        }
        if (cmd.charAt(0) == '#' && cmd.charAt(1) != '/'){
            //Can not create new thread for a global command
            start= 1 ;
            if(cmd.charAt(1) == '#'){
                if (cmd.charAt(2) != '/'){
                    start=2 ;
                    tag="1" ;
                } else{
                    start =0;
                }
            }
        }
        if (start != 0 && session.is_equal("RDR_SELECTED", "1")){
            /* A new thread can be created only if a reader is already selected*/
            try{
                session.assign_cmd(session, cmd.substring(start), session.get_variable("RDR_NAME"), tag) ;
                return ;
            } catch(UserException e){
                screen.error(e.getCode(), "",e) ;
            }
        }
        if (start != 0){
            cmd=cmd.substring(start);
        }
        try{
            jgpshell.parse_command(new ByteArrayInputStream(cmd.getBytes()));
        }catch(TokenMgrError e){
            screen.writeln("Unknown comand ! ") ;
            screen.writeln(e.getMessage()) ;
        }catch(ParseException e){
            screen.writeln("Check the usage of the command " + jgpshell.get_last_cmd() + "! ") ;
            screen.writeln(e.getMessage()) ;
        }
        
    }
    
    /**
     * Check if <code>r_thread_selected</code> is set to true.
     * In this case, we try to send last_line to a new/existing thread connected
     * to the reader r_thread_selected_rdr
     *
     * @return FAILURE(or other failure code) if r_thread_selected=false, or an error occured
     * when trying to create/contact the r_thread, returns SUCCESS if r_thread_selected is true
     * and the cmd was succefully sent to the r_thread.
     * FAILURE is also returned if the first character of last_line is '/'
     * (Shell command can not be sent with r_thread mode)
     *
     */
    public int check_r_thread(){
        if (session.r_thread_selected && get_last_line().charAt(0)!= '/'){
            try{
                session.assign_cmd(session, get_last_line(), session.r_thread_selected_rdr,
                        session.r_thread_selected_tag) ;
                System.out.println("last_line=" + get_last_line()) ;
                return Errors.SUCCESS ;
            }catch(UserException e){
                screen.error(e.getCode(),"",e) ;
                return e.getCode() ;
            }
        }
        return Errors.FAILURE ;
    }
    
    /********** Shell commands *******************/
    /*********************************************/
    
    /******** Global commands (with '/') ********/
    
    /**
     * Connects to the reader with specified index.
     *
     * @param The reader to connect to.
     *
     * @return The connection result.
     */
    public int connect(int index){
        session.update_readerList() ;
        String rdr_name = session.get_cgd().get_reader(index);
        if (rdr_name == null){
            screen.error(Errors.WRONG_RDR_NB, Integer.toString(index)) ;
            return Errors.WRONG_RDR_NB;
        }
        
        return connect(rdr_name) ;
    }
    
    /**
     * Connects to the reader with the specified name.
     *
     * @param rdrName The name of the reader.
     *
     * @return The connection result.
     */
    public int connect(String rdrName){
        session.new_rdr_selected = true ;
        try{
            session.get_appOffCard().connect_card(rdrName) ;
        }catch(UserException e){
            screen.error(Errors.UNABLE_CONNECT_CARD, rdrName, e) ;
            return Errors.UNABLE_CONNECT_CARD ;
        }
        
        session.set_variable_private("RDR_SELECTED", "1") ;
        session.set_variable_private("RDR_NAME", rdrName);
        //session.set_variable_private("RDR_NUMBER", rdrName);
        screen.info("Card into "+ rdrName + " ready !" );
        screen.set_shell(rdrName) ;
        session.new_rdr_selected = false ;
        return Errors.SUCCESS;
    }
    
    /**
     * Connect to the card into the reader number index and do the authetification
     *
     * @param The index of the reader.
     *
     * @return The connection result.
     */
    public int select(int index){
        session.update_readerList() ;
        
        String rdr_name = session.get_cgd().get_reader(index);
        if (rdr_name == null){
            screen.error(Errors.WRONG_RDR_NB, Integer.toString(index)) ;
            return Errors.WRONG_RDR_NB;
        }
        
        return select(rdr_name) ;
    }
    
    /**
     * Connect to the card into the reader rdrName and do the authetification
     *
     * @param rdrName The name of the reader
     *
     * @return The connection result.
     */
    public int select(String rdrName){
        session.new_rdr_selected = true ;
        //Looking for the keys
        session.check_keys(rdrName) ;
        
        try{
            session.get_appOffCard().connect(rdrName, session.get_variable("Keys"),
                    Integer.parseInt(session.get_variable("Keys_index"))) ;
        }catch(UserException e){
            screen.error(Errors.UNABLE_CONNECT_CARD, rdrName, e) ;
            return Errors.UNABLE_CONNECT_CARD;
        }
        
        session.set_variable_private("RDR_SELECTED", "1") ;
        session.set_variable_private("RDR_NAME", rdrName);
        //session.set_variable_private("RDR_NUMBER", rdrName);
        screen.info("Card into "+ rdrName + " ready !" );
        screen.set_shell(session.get_shell() + rdrName) ;
        session.new_rdr_selected = false ;
        return Errors.SUCCESS;
    }
    
    /**
     * Lists the connected readers.
     *
     * @return The number of connected readers.
     */
    public int ls(){
        session.update_readerList() ;
        
        int nbReaders=session.get_cgd().get_nbReader() ;
        
        for(int i=0; i<nbReaders; i++){
            screen.write(i+1 +": " + session.get_cgd().get_reader(i+1) + "  ") ;
            
            if (session.get_cgd().contains_card(i)){
                screen.write("Card inserted  ");
                screen.writeln("ATR=" + Apdu.ba2s(session.get_cgd().getATR(i))) ;
            } else{
                screen.writeln("No card inserted !") ;
            }
            
        }
        screen.writeln("---- " + nbReaders +" reader(s) found !");
        return Errors.SUCCESS;
    }
    
    
    /**
     * The echo command.
     *
     * @param text The param of the echo command.
     *
     * @int The command result status.
     **/
    public int echo(String text){
        screen.writeln(text) ;
        return Errors.SUCCESS;
    }
    
    /**
     * Displays the value of variable name
     *
     * @param name The variable name.
     *
     * @return The command result status.
     */
    public int echo_var(String name){
        screen.writeln(session.get_variable(name)) ;
        return Errors.SUCCESS ;
    }
    
    /**
     * Reinitiate the current session
     *
     * @return The command result status.
     */
    public int reinit(){
        session.reinit() ;
        return Errors.SUCCESS;
    }
    
    
    /**
     * Lists the current session variable list.
     *
     * @return The command result status.
     */
    public int list_vars(){
        session.list_vars() ;
        return Errors.SUCCESS ;
    }
    
    /**
     * Displays the list of the running threads.
     *
     * @return The command result status.
     */
    public int display_running_threads(){
        session.display_running_threads() ;
        return Errors.SUCCESS;
    }
    
    /**
     * Display the history of the running threads.
     *
     * @return The command result status.
     */
    public int display_history_threads(){
        session.display_history_threads() ;
        return Errors.SUCCESS;
    }
    
    /**
     * Kill the thread associated to the reader with the index <code>index</code>.
     *
     * @param index The index of the reader.
     *
     * @return The command result status.
     */
    public int kill_r_thread(int index){
        session.update_readerList() ;
        String rdr_name = session.get_cgd().get_reader(index);
        if (rdr_name == null){
            screen.error(Errors.WRONG_RDR_NB, Integer.toString(index)) ;
            return Errors.WRONG_RDR_NB;
        }
        
        session.kill_r_thread(rdr_name) ;
        return Errors.SUCCESS;
    }
    
    /**
     * Loads and runs a script file.
     *
     * @param file The bname of the script file.
     *
     * @return The command result status.
     */
    public int run(String file){
        JGPShell jgpshell = new JGPShell(this) ;
        FileInputStream script=null ;
        try{
            import_script(file) ;
            script= new FileInputStream(file) ;
        }catch(FileNotFoundException e){
            screen.error(Errors.FILE_NOT_FOUND, file) ;
            //Utils.close_file(screen, script) ;
            return Errors.FILE_NOT_FOUND ;
        }catch(IOException e){
            screen.error(Errors.IO_ERROR, file) ;
            //Utils.close_file(screen, script) ;
            return Errors.FILE_NOT_FOUND ;
        }
        
        set_script_code(script);
        
        try{
            jgpshell.parse_program(script) ;
        }catch(ParseException e){
            screen.error(Errors.SYNTAX_ERROR, "near " + jgpshell.get_last_cmd() +
                    "  (L=" + jgpshell.get_parser().getToken(0).beginLine + 
                    " C=" + jgpshell.get_parser().getToken(0).beginColumn) ;
            Utils.close_file(screen, script) ;
            return Errors.SYNTAX_ERROR ;
        }
        
        return Utils.close_file(screen, script) ;
    }
    
    
    /**
     * call begin_r_thread(index)  with default flag
     *
     * @param index
     * @return
     */
    public int start_r_thread(int index){
        return start_r_thread(index, null) ;
    }
    
    /**
     * call begin_r_thread(rdr_name)  with default flag
     *
     * @param index
     * @return
     */
    public int start_r_thread(String rdr_name){
        return start_r_thread(rdr_name, null) ;
    }
    
    /**
     * Launchs new thread to send cmds to the reader index
     *
     * @param index
     * @return
     */
    public int start_r_thread(int index, ArrayList params){
        session.update_readerList() ;
        String rdr_name = session.get_cgd().get_reader(index);
        if (rdr_name == null){
            screen.error(Errors.WRONG_RDR_NB, Integer.toString(index)) ;
            return Errors.WRONG_RDR_NB;
        }
        
        return start_r_thread(rdr_name, params) ;
    }
    
    /**
     * Launchs new thread to send cmds to the reader rdr_name
     *
     * @param rdr_name
     * @return
     */
    public int start_r_thread(String rdr_name, ArrayList params){
        //Make a tag String using the parameters array
        String tag=r_thread_params_to_tag(params) ;
        screen.writeln(tag) ;
        session.r_thread_selected = true ;
        session.r_thread_selected_rdr = rdr_name ;
        session.r_thread_selected_tag = tag ;
        session.set_variable_private("R_THREAD_SELECTED", "1") ;
        session.set_variable_private("R_THREAD_SELECTED_RDR", rdr_name);
        session.set_variable_private("R_THREAD_SELECTED_TAG", tag) ;
        
        screen.set_shell("[Mode r_thread enabled][" + rdr_name + "]");
        
        return Errors.SUCCESS ;
    }
    
    /**
     * return tag String equivalent to the parameters into the ArrayList params
     *
     * @param params Array of parameters
     * @return
     */
    public String r_thread_params_to_tag(ArrayList params){
        char[] tag ={'0','0','0'} ; // Maximum 3 parameters(but can be easily be increased)
        //System.out.println("ps=" + params.size()) ;
        if (params !=null && params.size() > 0){
            Iterator it= params.iterator() ;
                        /* For each valid parameter found , we set tag[x]='1' where x is
                         * the position into the tag used for this parameter*/
            while(it.hasNext()){
                String p = (String)it.next() ;
                //System.out.println("p=" + p) ;
                /* The first element of the array may be the command itself:/start, we ignore it*/
                if (p.equals("/start")){
                    continue;
                }
                if (p.equals("-p")){
                    /* permanent thread */
                    tag[0] = '1' ;
                    continue ;
                }
                screen.error(Errors.INVALID_PARAMETER, p) ;
            }
        }
        return String.valueOf(tag);
    }
    
    /**
     * Just set r_thread_selected to false, so next cmds will be executed by
     * the shell and not be sent to a thread.
     *
     * @return
     */
    public int end_r_thread(){
        session.r_thread_selected = false ;
        session.set_variable_private("R_THREAD_SELECTED", "0") ;
        
        screen.init_shell() ;
        
        return Errors.SUCCESS ;
    }
    
    
    /**
     * Close all the opened sessions and exit
     *
     * @return
     */
    public int exit(){
        session.free() ;
        System.exit(0) ;
        return Errors.SUCCESS;
    }
    
    /******** Card commands ***************/
    /*************************************/
    
    public int close(){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (!session.get_appOffCard().is_connected()){
            screen.error(Errors.NOT_CONNECTED_ANY_CARD);
            return Errors.NOT_CONNECTED_ANY_CARD;
        }
        try{
            session.get_appOffCard().close() ;
            session.set_variable_private("rdr_selected", "0") ;
        }catch(Exception e){
            screen.error(Errors.FINALIZING_CONTEXT_FAILURE);
            return Errors.FINALIZING_CONTEXT_FAILURE;
        }
        session.unset("RDR_SELECTED") ;
        screen.set_shell(session.get_shell());
        return Errors.SUCCESS;
    }
    
    /**
     *
     * @return
     */
    public int init_update(){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            session.check_keys(session.get_variable("RDR_NAME")) ;
            try{
                SecureChannel sc=new SecureChannel(session.get_variable("Keys"), 
                        Integer.parseInt(session.get_variable("Keys_index"))) ;
                session.get_appOffCard().initialize_update(sc) ;
            }catch(UserException e){
                screen.error(Errors.INIT_UPDATE_FAILURE, session.get_variable("RDR_NAME"), e) ;
                return Errors.INIT_UPDATE_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS ;
    }
    
    /**
     *
     * @return
     */
    public int ext_auth(){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            session.check_keys(session.get_variable("RDR_NAME")) ;
            try{
                session.get_appOffCard().complete_authentication() ;
            }catch(UserException e){
                screen.error(Errors.EXT_AUTH_FAILURE, session.get_variable("RDR_NAME"), e) ;
                return Errors.EXT_AUTH_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS ;
    }
    
    /**
     * execute init-update and ext-auth
     *
     * @return
     */
    public int authenticate(){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        int ret1=init_update() ;
        if (ret1  == Errors.SUCCESS){
            return ext_auth() ;
        }
        return ret1 ;
    }
    
    /**
     *
     * @param apdu
     * @return
     */
    public int send(String apdu){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().trySend(Apdu.s2ba(apdu));
            }catch(UserException e){
                screen.error(Errors.SEND_APDU_FAILURE, session.get_variable("RDR_NAME"), e) ;
                return Errors.SEND_APDU_FAILURE;
            }catch(Exception e){
                screen.error(Errors.BAD_APDU, apdu) ;
                return Errors.BAD_APDU;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
    }
    
    /**
     * Upload the package into the seleted card
     *
     * @param packagePath
     * @return
     */
    public int upload(String packagePath){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().upload(packagePath) ;
            }catch(UserException e){
                screen.error(Errors.UPLOAD_FAILURE, packagePath, e) ;
                return Errors.UPLOAD_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
        
    }
    
    /**
     * Upload the package into the seleted card
     *
     * @param packagePath
     * @return
     */
    public int install(String packagePath){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().install_only(packagePath) ;
            }catch(UserException e){
                screen.error(Errors.INSTALL_FAILURE, packagePath, e) ;
                return Errors.INSTALL_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
        
    }
    
    /**
     * Install the appllet with the AID appletAID contained into the package packageAID
     *
     * @param packageAID
     * @param appletAID
     * @return
     */
    public int install(String packageAID, String appletAID){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().install_applet(Apdu.s2ba(packageAID), Apdu.s2ba(appletAID)) ;
            }catch(UserException e){
                screen.error(Errors.INSTALL_APPLET_FAILURE, "",e) ;
                return Errors.INSTALL_APPLET_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS ;
    }
    
    /**
     * Delete the package into the seleted card
     *
     * @param packagePath
     * @return
     */
    public int delete(String packagePath){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().delete(packagePath) ;
            }catch(UserException e){
                screen.error(Errors.DELETE_FAILURE, packagePath, e) ;
                return Errors.DELETE_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
        
    }
    
    /**
     * Remove the package from the seleted card
     *
     * @param packagePath
     * @return
     */
    public int delete_aid(String aid){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().delete_aid(Apdu.s2ba(aid)) ;
            }catch(UserException e){
                screen.error(Errors.DELETE_FAILURE, "", e) ;
                return Errors.DELETE_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
        
    }
    
    /**
     * Remove the package from the seleted card
     *
     * @param packagePath
     * @return
     */
    public int select_aid(String aid){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().select_aid(Apdu.s2ba(aid)) ;
            }catch(UserException e){
                screen.error(Errors.SELECT_FAILURE, "", e) ;
                return Errors.SELECT_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
        
    }
    
    /**
     * Format the seleted card
     *
     * @param packagePath
     * @return
     */
    public int format(){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_card().format();
            }catch(UserException e){
                screen.error(Errors.FORMAT_FAILURE, "", e) ;
                return Errors.FORMAT_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
        
    }
    
    /**
     * Display the content of selected card
     *
     * @param packagePath
     * @return
     */
    public int ls_card(){
        if (check_r_thread() == Errors.SUCCESS){
            return Errors.SUCCESS ;
        }
        
        Collection list= null;
        CardApplication ca ;
        if (session.is_set("RDR_SELECTED") && session.is_equal("RDR_SELECTED","1")){
            try{
                session.get_appOffCard().setLog(0) ;
                
                list = session.get_card().list_Applications();
                Iterator it = list.iterator();
                while (it.hasNext()){
                    ca = (CardApplication)it.next();
                    screen.writeln(Apdu.ba2s(ca.get_aid())+ "..............." + 
                            GP_constants.get_text_life_cycle(ca.get_life_cycle()));
                }
            }catch(UserException e){
                screen.error(Errors.LS_FAILURE, "", e) ;
                return Errors.LS_FAILURE;
            }
        } else{
            screen.error(Errors.CONNECT_BEFORE) ;
            return Errors.CONNECT_BEFORE;
        }
        return Errors.SUCCESS;
        
    }
    
    
    
    /******** Set commands ***************/
    /*************************************/
    
    /**
     * Sets the session variable with the specified name to the specified value.
     *
     * @param name The name of the variable.
     * @param value The value of the variable.
     *
     * @return The command result status. 
     */
    public int set(String name, String value){
        int res=session.set_variable(name, value) ;
        if (res!= Errors.SUCCESS){
            screen.error(res) ;
            return res ;
        }
        return Errors.SUCCESS;
    }
    
    /**
     * Sets the log variable level.
     *
     * @param level The log level.
     *
     * @return The command result status. 
     */
    public int set_log(int level){
        session.get_appOffCard().setLog(level) ;
        session.set_variable("log", String.valueOf(level)) ;
        return Errors.SUCCESS;
    }
    
}


