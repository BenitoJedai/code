@echo off

if '%1' == '' (
    echo no file specified
    goto :eof
)

:: get java from http://java.sun.com/javase/downloads/index_jdk5.jsp

set ReturnValue=x:\util\java5\bin\%1
if exist %ReturnValue% goto :eof

set ReturnValue=z:\util\java5\bin\%1
if exist %ReturnValue% goto :eof

set ReturnValue=


