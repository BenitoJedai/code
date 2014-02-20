@echo off
set TargetFileName=%2
set ConfigurationName=%3

if %ConfigurationName%==Debug goto :eof

rem set JAVA_HOME=C:\util\j2sdk1.4.2_19
set JAVA_HOME=C:\Progra~2\Java\jdk1.6.0_32


rem set JC_HOME=C:\util\java_card_kit-2_2_1
set JC_HOME=C:\util\java_card_kit-2_2_2-windows\java_card_kit-2_2_2\java_card_kit-2_2_2-rr-bin-windows-do


@call :jsc

@call compile.java
::@call create.jar

@call build.cap.bat

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

::call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js
call c:\util\jsc\bin\jsc.meta.exe %TargetFileName%  -java -InternalInvokeEntryPoints


popd
goto :eof