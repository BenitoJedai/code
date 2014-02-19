set SRC_DIR=.\src
set BUILD_DIR=.\PWD\build\classes
set CLASSPATH=%CLASSPATH%;%SRC_DIR;%BUILD_DIR;%PWD\lib\swing-layout-1.0.jar

@echo off
IF ()==(%1) GOTO COMPILATION
IF %1==clean GOTO CLEAN
IF %1==jar GOTO jar
IF %1==doc GOTO DOC
IF %1==help GOTO USAGE
IF %1==? GOTO USAGE
GOTO USAGE

:COMPILATION
echo com.linuxnet.com
javac %SRC_DIR%\com\linuxnet\jpcsc\*.java -d %BUILD_DIR%

echo cardGridCom
javac  %SRC_DIR%\com\jgpshell\cardGridCom\*.java -d %BUILD_DIR%
	
	echo globalPlatform
	javac %SRC_DIR%\com\jgpshell\globalPlatform\*.java -d %BUILD_DIR%

	echo offCard
	javac %SRC_DIR%\com\jgpshell\offCard\*.java -d %BUILD_DIR%
	
	echo shell
	javac %SRC_DIR%\com\jgpshell\shell\*.java -d %BUILD_DIR%

	echo xshell
	javac %SRC_DIR%\com\jgpshell\xshell\*.java -d %BUILD_DIR%

GOTO END

:CLEAN
echo clean
del /Q /S %BUILD_DIR%\* .\build\jgpshell.jar .\docs\javadoc\*
GOTO END

:JAR
echo jar
jar cvf .\build\jgpshell.jar .\src\
GOTO END

:DOC
echo Génération de la doc :
javadoc -author -private -classpath ./src/  com.jgpshell.offCard com.jgpshell.globalPlatform com.jgpshell.cardGridCom com.linuxnet.jpcsc com.jgpshell.shell com.jgpshell.xshell -d ./docs/javadoc

GOTO END

:USAGE
 echo Usage : 
    echo compile : compile jgpshell classes.
    echo compile doc : creates the javadoc.
    echo compile jar : create jgpshell.jar
    echo compile clean : delete all built files.
    
    echo help/? : Displays this text.

:END
echo end
