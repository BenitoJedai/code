@echo off
set ConfigurationName=%2

pushd ..\bin\%ConfigurationName%\web\
echo + compile native [%1] %ConfigurationName%
setlocal

set _libname=%1

rem 2003 toolkit
rem set _toolkit=X:\util\vc2003.toolkit
rem set _init=%_toolkit%\vcvars32.bat

rem 2003
::set _toolkit=X:\util\dotnet2003\Vc7

::set _init=%_toolkit%\..\Common7\Tools\vsvars32.bat
set _toolkit=c:\Program Files\Microsoft Visual Studio 9.0\VC
set _init=%_toolkit%\vcvarsall.bat

rem 2005
rem set _toolkit=X:\util\vs2005re\VC
rem set _init=%_toolkit%\vcvarsall.bat

echo - setting vars
call "%_init%" >nul

set _targetpath=bin\Debug\web
set _sourcefiles=*.c




::set _java=x:\util\java5

::set _args=/I "%_java%\include"
::set _args=%_args% /I "%_java%\include\win32"
set _args=%_args% /TC /Zm200 
set _args=%_args% /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /Fe%_libname%




rem echo cl.exe  %_args%
set _command="%_toolkit%\bin\cl.exe" %_args%

rem cd
rem dir *.cpp

rem echo %_command%
call %_command%
rem > compile.log

endlocal
popd
