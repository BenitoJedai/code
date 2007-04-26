@echo off
setlocal

rem entrypoint must be a public class which has a public static main() declared

set __java_entry=bin\java.exe

if "%JAVA_HOME%"=="" goto nojava
set __java=%JAVA_HOME%
goto default
:nojava
set __java=x:\util\java5
:default
rem case1
if exist "%__java%\%__java_entry%" goto filefound
set __java=C:\Program Files\Java\jre1.5.0_06
rem case2
if exist "%__java%\%__java_entry%" goto filefound
echo *** java runtime cannot be found at %__java%
goto :eof
:filefound
"%__java%\%__java_entry%" %*