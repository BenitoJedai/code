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

import com.jgpshell.offCard.UserException;

import java.io.ByteArrayInputStream;
import java.awt.*;

/**
 * This is the main class. It inits the shell and wait for user command.
 *
 * @author Moez
 *
 */
public class Shell {
    
    /* This array will contains all global variables*/
    public static Table global = new Table() ;
    
    public static JGPShell jgpshell = null;
    private static Screen screen = null;
    private static Command command = null;
    private static Session session = null;
    
    public  Shell(){
        
    }
    
    public static Command getCommand()
    {
        return command;
    }
    
    public static JGPShell getJGPShell()
    {
        return jgpshell;
    }
    
    /**
     * Only to say Welcome !
     *
     * @param screen
     */
    private static void welcome(){
        screen.writeln("Welcome !") ;
        screen.writeln("JGPShell v 0.1 !");
        screen.writeln("") ;
        screen.writeln("") ;
    }
    
    public static void init_shell(Screen screen) {
        session = new Session() ;
        command = new Command(screen, session) ;
        jgpshell = new JGPShell(command) ;
        session.set_screen(screen) ;
        session.init() ;
    }
    
    /**
     * Init the shell: Session, Screen and Command
     *
     */
    public static void run_shell() {
        
        welcome() ;
        
        String cmd ;
        
        while(true){
            cmd=screen.ready() ;
            command.execute_cmd(jgpshell, cmd);
        }
        
    }
    
    
    /**
     * @param args
     */
    public static void main(String[] args) throws ParseException{
        screen = new Screen();
        init_shell(screen) ;
        run_shell();
    }
    
}




