### Simple script to run jgpshell ###
### Author : Moez Ben MBarka ###

#!/bin/bash
BUILD_DIR=$PWD/build/classes
CLASSPATH=$CLASSPATH:$BUILD_DIR

usage(){
    echo run : run the shell on the console.
    echo run gui : run the X-shell.
    echo ? : Displays this help.
}


case $1 in
    gui)
	java -Djava.library.path=./lib/ -classpath $CLASSPATH:./lib/mysql-connector-java-3.0.17-ga-bin.jar:./lib/swing-layout-1.0.jar  com.jgpshell.xshell.XShell
	;;	
    ?)
	usage
	;;
    *)
    java -Djava.library.path=./lib/ -classpath $CLASSPATH:./lib/mysql-connector-java-3.0.17-ga-bin.jar: com.jgpshell.shell.Shell
	
	;;
esac
