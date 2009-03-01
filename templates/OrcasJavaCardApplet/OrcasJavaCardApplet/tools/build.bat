@echo off
set TargetFileName=%2
set ConfigurationName=%3

if %ConfigurationName%==Debug goto :eof

@call :jsc

@call compile.java
::@call create.jar

@call build.cap.bat

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

::call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js
call c:\util\jsc\bin\jsc.exe %TargetFileName%  -java


popd
goto :eof