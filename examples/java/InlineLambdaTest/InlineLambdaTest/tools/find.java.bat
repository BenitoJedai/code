@echo off

set TargetJava=%1

if '%TargetJava%' == '' (
    set TargetJava=javac.exe
)

if '%TargetJava%' == '' (
    echo no file specified
    goto :eof
)


:: get java from http://java.sun.com/javase/downloads/index_jdk5.jsp

set ReturnValue="C:\Program Files\Java\jdk1.6.0_14\bin\%TargetJava%"
if exist %ReturnValue% goto :exit


set ReturnValue=C:\j2sdk1.4.2_17\bin\%TargetJava%
if exist %ReturnValue% goto :eof


set ReturnValue=x:\util\java5\bin\%TargetJava%
if exist %ReturnValue% goto :exit

set ReturnValue=z:\util\java5\bin\%TargetJava%
if exist %ReturnValue% goto :exit

set ReturnValue="D:\Program Files\Java\jdk1.5.0_14\bin\%TargetJava%"
if exist %ReturnValue% goto :exit

set ReturnValue="C:\Program Files\Java\jdk1.6.0_06\bin\%TargetJava%"
if exist %ReturnValue% goto :exit


set ReturnValue=

:exit
echo found: %ReturnValue%
