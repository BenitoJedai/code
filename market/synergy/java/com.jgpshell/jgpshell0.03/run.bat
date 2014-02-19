@echo off
set BUILD_DIR=.\PWD\build\classes
set CLASSPATH=%CLASSPATH%;%BUILD_DIR%

IF ()==(%1) GOTO USAGE
IF %1==gui GOTO GUI
IF %1==? GOTO USAGE
GOTO NOGUI

:GUI
java -Djava.library.path=.\lib\ -classpath .\lib\swing-layout-1.0.jar:.\lib\mysql-connector-java-3.0.17-ga-bin.jar;%CLASSPATH%  com.jgpshell.shell.Shell %2 %3
GOTO END

:NOGUI
java -Djava.library.path=.\lib\ -classpath .\lib\mysql-connector-java-3.0.17-ga-bin.jar;%CLASSPATH%  com.jgpshell.xshell.XShell

:USAGE
echo run : run the shell on the console.
    echo run gui : run the X-shell.
    echo ? : Displays this help.

:END

