### Simple script to compile jgpshell ###
### Author : Moez Ben MBarka ###

#!/bin/bash
SRC_DIR=$PWD/src
BUILD_DIR=$PWD/build
CLASS_DIR=$BUILD_DIR/classes
CLASSPATH=$CLASSPATH:$SRC_DIR:$CLASS_DIR:$PWD/lib/swing-layout-1.0.jar
JAVADOC_DIR=$PWD/docs/javadoc
JAR_DIR=$BUILD_DIR

usage(){
    echo Usage : 
    echo compile : compile jgpshell classes.
    echo compile doc : creates the javadoc.
    echo compile jar : create jgpshell.jar
    echo compile clean : delete all built files.
    
    echo help/? : Displays this text.
}

case $1 in
    clean)
	echo rm -rf $BUILD_DIR dist $JAVADOC_DIR *~
	rm -rf $BUILD_DIR dist $JAVADOC_DIR *~
	;;
    jar)
	#Crétaion du .jar
	jar cvf $JAR_DIR/jgpshell.jar $SRC_DIR
	;;
    doc)
	#Documenation javadoc
	javadoc -author -private -classpath ./src/  com.jgpshell.offCard com.jgpshell.globalPlatform com.jgpshell.cardGridCom com.linuxnet.jpcsc com.jgpshell.shell com.jgpshell.xshell -d $JAVADOC_DIR
	;;
    ?)
	usage
	;;
    help)
	usage
	;;
    *)
	#compilation
	if [ ! -d $BUILD_DIR ]; then
	    mkdir $BUILD_DIR
	    mkdir $CLASS_DIR

	fi

	echo javac $SRC_DIR/com/linuxnet/jpcsc/*.java -d $CLASS_DIR
	javac $SRC_DIR/com/linuxnet/jpcsc/*.java -d $CLASS_DIR
	
	echo javac  $SRC_DIR/com/jgpshell/cardGridCom/*.java -d $CLASS_DIR
	javac  $SRC_DIR/com/jgpshell/cardGridCom/*.java -d $CLASS_DIR
	
	echo javac $SRC_DIR/com/jgpshell/globalPlatform/*.java -d $CLASS_DIR
	javac $SRC_DIR/com/jgpshell/globalPlatform/*.java -d $CLASS_DIR

	echo javac $SRC_DIR/com/jgpshell/offCard/*.java -d $CLASS_DIR
	javac $SRC_DIR/com/jgpshell/offCard/*.java -d $CLASS_DIR
	
	echo javac $SRC_DIR/com/jgpshell/shell/*.java -d $CLASS_DIR
	javac $SRC_DIR/com/jgpshell/shell/*.java -d $CLASS_DIR

	echo javac $SRC_DIR/com/jgpshell/xshell/*.java -d $CLASS_DIR
	javac $SRC_DIR/com/jgpshell/xshell/*.java -d $CLASS_DIR
	;;
esac	



