@echo off

if '%1' == '' (
    echo no file specified
    goto :eof
)

:: get java from http://java.sun.com/javase/downloads/index_jdk5.jsp

set ReturnValue=C:\j2sdk1.4.2_17\bin\%1
if exist %ReturnValue% goto :eof



set ReturnValue=x:\util\java5\bin\%1
if exist %ReturnValue% goto :eof

set ReturnValue=z:\util\java5\bin\%1
if exist %ReturnValue% goto :eof

set ReturnValue="D:\Program Files\Java\jdk1.5.0_14\bin\%1"
if exist %ReturnValue% goto :eof

set ReturnValue="C:\Program Files\Java\jdk1.6.0_06\bin\%1"
if exist %ReturnValue% goto :eof


set ReturnValue=


