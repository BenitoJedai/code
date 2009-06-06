@echo off
set TargetFileName=%2
set ConfigurationName=%3

if %ConfigurationName%==Debug goto :eof

@call :jsc

::@call compile.java
::@call create.jar

pushd ..\bin\%ConfigurationName%\web
set JAVA_HOME=C:\Program Files\Java\jdk1.6.0_14
call C:\util\apache-ant-1.7.1\bin\ant -f build.xml
popd

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

::call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js
call c:\util\jsc\bin\jsc.exe %TargetFileName%  -java


popd
goto :eof