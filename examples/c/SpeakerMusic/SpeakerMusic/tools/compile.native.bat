@echo off
set ConfigurationName=%2

pushd ..\bin\%ConfigurationName%\web\
echo + compile native [%1] %ConfigurationName%
setlocal

set _libname=%1

echo - setting vars 
echo call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd"
call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd"

set _targetpath=bin\Debug\web
set _sourcefiles=*.c


::set _args=/I "%_java%\include"
::set _args=%_args% /I "%_java%\include\win32"
set _args=%_args% /TC /Zm200 
::set _args=%_args% /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /EHsc  %_sourcefiles% 
set _args=%_args% /Fe%_libname%




rem echo cl.exe  %_args%
set _command="cl.exe" %_args%

rem cd
rem dir *.cpp

echo %cd%
echo call %_command%
call %_command%
echo done
rem > compile.log

endlocal
popd
