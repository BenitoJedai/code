@echo off
set TargetFileName=%2
set ConfigurationName=%3

if %ConfigurationName%==Debug goto :eof

@call :jsc

@call compile.java
@call create.jar

:: deploy
pushd ..\bin\%ConfigurationName%\web
call setup.settings.cmd
pushd bin
dir
copy %PackageName% C:\util\robocode\robots\
popd
popd

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

::call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js
call c:\util\jsc\bin\jsc.exe %TargetFileName%  -java



popd
goto :eof